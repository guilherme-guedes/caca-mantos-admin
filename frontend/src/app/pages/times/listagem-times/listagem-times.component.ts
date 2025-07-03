import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { finalize, Subject, takeUntil } from 'rxjs';
import { IColunaTabela, TabelaDinamicaComponent } from '../../../shared/components/tabela-dinamica/tabela-dinamica.component';
import { Time } from '../../../models/time';
import { Router } from '@angular/router';
import { TimeTableService } from '../../../services/time-table.service';
import { TimeService } from '../../../services/time.service';
import { TimeMapperService } from '../../../services/time-mapper.service';
import { PaginaMapperService } from '../../../services/pagina-mapper.service';
import { FiltroTime } from '../../../models/dto/filtro-time';

@Component({
  selector: 'app-listagem-times',
  imports: [ReactiveFormsModule, TabelaDinamicaComponent],
  templateUrl: './listagem-times.component.html',
  styleUrl: './listagem-times.component.css'
})
export class ListagemTimesComponent implements OnInit, OnDestroy{
  private destroy$ = new Subject<void>();
  private TAMANHO_PAGINA = 10;
  carregando: boolean;
  form: FormGroup;
  camposTimes: IColunaTabela[];
  times : Time[];     

  constructor(private fb: FormBuilder,
              private readonly router: Router,
              private readonly tableService: TimeTableService,
              private readonly timeService: TimeService,
              private readonly mapperTime: TimeMapperService,
              private readonly mapperPagina: PaginaMapperService) {
      this.carregando = true;
      this.camposTimes = [];
      this.times = [];
      this.form = this.criarFormVazio();
  }    

  ngOnInit(): void {    
    this.camposTimes = this.tableService.criarColunasTabelaCompleta();  
    this.carregarTimes(1);    
  }

  private criarFormVazio() {
    return this.fb.group({
      trecho: [''],
      destaque: [false],
      principal: [false],
      ativo: [true]
    });
  }  
  
  private criarFiltro(){
      let filtro : FiltroTime = {} as FiltroTime;
      Object.assign(filtro, this.form);
      return filtro;
  }
  
  private carregarTimes(pagina: number) {
      var filtro = this.criarFiltro();

      this.timeService.consultar(pagina, this.TAMANHO_PAGINA, filtro.trecho, filtro.ativo, filtro.destaque, filtro.principal)
        .pipe(takeUntil(this.destroy$), finalize(() => this.carregando = false))
        .subscribe({
          next: (dadosPaginaLojas) => {
            this.times = this.mapperPagina.paraModelos(dadosPaginaLojas, this.mapperTime.paraModelos);
          },
          error: (error) => {
            console.log("Erro: " + error);
          }
        });
  }
  
  getValue(objeto: any, path: string): any {
    return path.split('.').reduce((current, prop) => current?.[prop], objeto);
  }

  // Eventos
  onSubmit(item: any) {
    if (this.form.valid) {
      const filtro = Object.assign({}, this.form.value);      
      // consultar lojas por filtro
    }
  } 
  
  adicionarTime() {
    this.router.navigate(['/times/novo-time']);
  }
  
  editarTime(time: Time) {
    this.router.navigate([`/times/${time.id}` ]);
  }
  
  removerTime(time: Time) {
    if (confirm(`Deseja realmente excluir o time ${time.nome}?`)) {
      this.times = this.times.filter(t => t.id !== time.id);
      console.log('Atualizar lista e remover no back');
    }
  }
      
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}

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
import { PaginacaoComponent } from "../../../shared/components/paginacao/paginacao.component";
import { Pagina } from '../../../models/dto/pagina';
import { SwitchThreeStatesComponent } from '../../../shared/components/switch-three-states/switch-three-states.component';
import { NullableBool } from '../../../shared/components/switch-three-states/switch-three-states.types';

@Component({
  selector: 'app-listagem-times',
  imports: [ReactiveFormsModule, TabelaDinamicaComponent, PaginacaoComponent, SwitchThreeStatesComponent],
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
  paginacao: Pagina<Time>;

  constructor(private formBuilder: FormBuilder,
              private readonly router: Router,
              private readonly tableService: TimeTableService,
              private readonly timeService: TimeService,
              private readonly mapperTime: TimeMapperService,
              private readonly mapperPagina: PaginaMapperService) {
      this.carregando = true;
      this.camposTimes = [];
      this.times = [];
      this.paginacao = new Pagina<Time>();
      this.form = this.criarFormVazio();
  }    

  ngOnInit(): void {    
    this.camposTimes = this.tableService.criarColunasTabelaCompleta();  
    this.consultarTimes(1);    
  }

  private criarFormVazio() {
    return this.formBuilder.group({
      trecho: [''],
      destaque: null as NullableBool,
      principal: null as NullableBool,
      ativo: null as NullableBool
    });
  }  
  
  private carregarFiltros(){
      let filtro : FiltroTime = {} as FiltroTime;
      Object.assign(filtro, this.form.value);
      return filtro;
  }
  
  private consultarTimes(pagina: number) {
      var filtro = this.carregarFiltros();

      this.timeService.consultar(pagina, this.TAMANHO_PAGINA, filtro.trecho, filtro.destaque,  filtro.ativo, filtro.principal)
        .pipe(takeUntil(this.destroy$), finalize(() => this.carregando = false))
        .subscribe({
          next: (dadosPaginaLojas) => {
            this.paginacao =  this.mapperPagina.paraModelosPaginados(dadosPaginaLojas, this.mapperTime.paraModelos);
            this.times = this.paginacao.itens;
          },
          error: (error) => {
            console.log("Erro: " + error);
          }
        });
  }

  // Eventos
  onSubmit(item: any) {
    this.consultarTimes(1);
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

  onPageChanged(pagina: number){
    this.consultarTimes(pagina);
  }
      
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}

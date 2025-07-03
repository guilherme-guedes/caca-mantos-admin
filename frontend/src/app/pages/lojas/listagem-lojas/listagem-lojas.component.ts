import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { IColunaTabela, TabelaDinamicaComponent } from '../../../shared/components/tabela-dinamica/tabela-dinamica.component';
import { LojaTableService } from '../../../services/loja-table.service';
import { Loja } from '../../../models/loja';
import { finalize, Subject, takeUntil } from 'rxjs';
import { LojaService } from '../../../services/loja.service';
import { LojaMapperService } from '../../../services/loja-mapper.service';
import { PaginaMapperService } from '../../../services/pagina-mapper.service';
import { FiltroLoja } from '../../../models/dto/filtro-loja';
import { PaginacaoComponent } from "../../../shared/components/paginacao/paginacao.component";
import { Pagina } from '../../../models/dto/pagina';

@Component({
  selector: 'app-listagem-lojas',
  imports: [ReactiveFormsModule, TabelaDinamicaComponent, PaginacaoComponent],  
  templateUrl: './listagem-lojas.component.html',
  styleUrl: './listagem-lojas.component.css'
})
export class ListagemLojasComponent implements OnInit, OnDestroy{
  private destroy$ = new Subject<void>();
  private TAMANHO_PAGINA = 10;
  carregando: boolean;
  formGroup: FormGroup;
  camposLojas: IColunaTabela[];
  lojas : Loja[];
  paginacao: Pagina<Loja>;

  constructor(private formBuilder: FormBuilder,
              private readonly router: Router,
              private readonly tableService: LojaTableService,
              private readonly lojaService: LojaService,
              private readonly mapperLoja: LojaMapperService,
              private readonly mapperPagina: PaginaMapperService) {
      this.carregando = true;
      this.camposLojas = [];
      this.lojas = [];
      this.formGroup = this.criarFormVazio();
      this.paginacao = new Pagina<Loja>();
  }    

  ngOnInit(): void {    
    this.camposLojas = this.tableService.criarColunasTabelaCompleta();  
    this.consultarLojas(1);
  }

  private criarFormVazio() {
    return this.formBuilder.group({
      trecho: [''],
      parceira: [],
      ativa: []
    });
  }  

  private carregarFiltro(){
    let filtro : FiltroLoja = {} as FiltroLoja;
    Object.assign(filtro, this.formGroup);
    return filtro;
  }
  
  private consultarLojas(pagina: number) {
      var filtro = this.carregarFiltro()   

      this.lojaService.consultar(pagina, this.TAMANHO_PAGINA, filtro.trecho, filtro.ativa, filtro.parceira)
                    .pipe(takeUntil(this.destroy$), finalize(() => this.carregando = false))
                    .subscribe({
                      next: (dadosPaginaLojas) => {
                        this.paginacao =  this.mapperPagina.paraModelosPaginados(dadosPaginaLojas, this.mapperLoja.paraModelos);
                        this.lojas = this.paginacao.itens;
                      },
                      error: (error) => {
                        console.log("Erro: " + error);
                      }
                    });
  }
  
  obterValorColuna(objeto: any, campo: string): any {
    return campo.split('.').reduce((objetoCorrente, propriedade) => objetoCorrente?.[propriedade], objeto);
  }
  
  colunaLogica(campo: string): boolean{
    return campo === 'ativa' || campo === 'parceira';
  }

  // Eventos
  onSubmit(item: any) {
    this.consultarLojas(1);
  } 

  adicionarLoja() {
    this.router.navigate(['/lojas/nova-loja']);
  }
  
  editarLoja(loja: Loja) {
    this.router.navigate([`/lojas/${loja.id}` ]);
  }
  
  removerLoja(loja: Loja) {
    if (confirm(`Deseja realmente excluir a loja ${loja.nome}?`)) {
      this.lojas = this.lojas.filter(t => t.id !== loja.id);
      console.log('Atualizar lista e remover no back');
    }
  }

  onPageChanged(pagina: number){
    this.consultarLojas(pagina);
  }
      
      
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}

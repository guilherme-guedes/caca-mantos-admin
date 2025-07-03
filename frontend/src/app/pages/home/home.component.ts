import { Component, OnDestroy, OnInit } from '@angular/core';
import { TabelaDinamicaComponent } from "../../shared/components/tabela-dinamica/tabela-dinamica.component";
import { IColunaTabela } from "../../shared/components/tabela-dinamica/tabela-dinamica.component";
import { Time } from '../../models/time';
import { Loja } from '../../models/loja';
import { Router } from '@angular/router';
import { CounterComponent } from '../../shared/components/dashboard/counter/counter.component';
import { LojaTableService } from '../../services/loja-table.service';
import { TimeService } from '../../services/time.service';
import { finalize, Subject, takeUntil } from 'rxjs';
import { TimeMapperService } from '../../services/time-mapper.service';
import { PaginaMapperService } from '../../services/pagina-mapper.service';
import { TimeTableService } from '../../services/time-table.service';
import { LojaService } from '../../services/loja.service';
import { LojaMapperService } from '../../services/loja-mapper.service';

@Component({
  selector: 'home-page',
  imports: [TabelaDinamicaComponent, CounterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  camposLojas: IColunaTabela[] = [];
  camposTimes: IColunaTabela[] = [];
  carregando = false;
  times : Time[] = []; 
  lojas : Loja[] = []; 
  
  constructor(private readonly router: Router,
              private readonly lojaTableService: LojaTableService,
              private readonly timeTableService: TimeTableService,
              private readonly timeService: TimeService,
              private readonly lojaService: LojaService,
              private readonly mapperTime: TimeMapperService,
              private readonly mapperLoja: LojaMapperService,
              private readonly mapperPagina: PaginaMapperService){
  }
  
  ngOnInit(): void {
    this.carregando = true;
    this.carregarTimes();
    this.carregarLojas();
  }

  private carregarTimes() {
    this.camposTimes = this.timeTableService.criarColunasTabelaResumida();
    this.timeService.consultar(1, 5)
      .pipe(takeUntil(this.destroy$), finalize(() => this.carregando = false))
      .subscribe({
        next: (dadosPaginaTimes) => {
          this.times = this.mapperPagina.paraModelos(dadosPaginaTimes, this.mapperTime.paraModelos);
        },
        error: (error) => {
          console.log("Erro: " + error);
        }
      });
  }

  private carregarLojas() {
    this.camposLojas = this.lojaTableService.criarColunasTabelaResumida();
    this.lojaService.consultar(1, 5)
      .pipe(takeUntil(this.destroy$), finalize(() => this.carregando = false))
      .subscribe({
        next: (dadosPaginaLojas) => {
          this.lojas = this.mapperPagina.paraModelos(dadosPaginaLojas, this.mapperLoja.paraModelos);
        },
        error: (error) => {
          console.log("Erro: " + error);
        }
      });
  }

  // Eventos  
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
    
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}

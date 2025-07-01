import { Component, OnDestroy, OnInit } from '@angular/core';
import { ListaResumidaComponent } from "../../shared/components/lista-resumida/lista-resumida.component";
import { IColunaTabela } from "../../shared/components/lista-resumida/lista-resumida.component";
import { Time } from '../../models/time';
import { Loja } from '../../models/loja';
import { Router } from '@angular/router';
import { CounterComponent } from '../../shared/components/dashboard/counter/counter.component';
import { LojaTableService } from '../../services/loja-table.service';
import { TimeService } from '../../services/time.service';
import { finalize, Subject, takeUntil } from 'rxjs';
import { TimeMapperService } from '../../services/time-mapper.service';
import { PaginaMapperService } from '../../services/pagina-mapper.service';

@Component({
  selector: 'home-page',
  imports: [ListaResumidaComponent, CounterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  camposLojas: IColunaTabela[] = [];
  carregando = false;
  times : Time[] = []; 
  
  camposTimes: IColunaTabela[] = [
    { key: 'identificador', label: 'Identificador' },
    { key: 'nome', label: 'Nome' },
  ];
  
  constructor(private readonly router: Router,
              private readonly lojaTableService: LojaTableService,
              private readonly timeService: TimeService,
              private readonly mapperTime: TimeMapperService,
              private readonly mapperPagina: PaginaMapperService<Time>){
  }
  
  ngOnInit(): void {
    this.carregando = true;
    this.carregarTimes();
  }
  
  lojas = [
    { id: '1', nome: 'Memórias do Esporte', site: 'memoriasdoesporte.com.br' },
    { id: '2', nome: 'BK Camisetas', site: 'bkcamisetas.com.br' },
    { id: '3', nome: 'Manto Sagrado', site: 'mantosagrado.com.br' },
    { id: '4', nome: 'Brechó do Futebol', site: 'brechodofut.com.br' },
    { id: '5', nome: 'Chanti Sports', site: 'chantisports.com.br' },
  ];

  private carregarTimes() {
    this.camposLojas = this.lojaTableService.criarColunasTabelaLojaResumida();
    this.timeService.consultarTimes(1, 5)
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

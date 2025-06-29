import { Component } from '@angular/core';
import { ListaResumidaComponent } from "../../shared/components/lista-resumida/lista-resumida.component";
import { IColunaTabela } from "../../shared/components/lista-resumida/lista-resumida.component";
import { Time } from '../../models/time';
import { Loja } from '../../models/loja';
import { Router } from '@angular/router';
import { CounterComponent } from '../../shared/components/dashboard/counter/counter.component';
import { LojaTableService } from '../../services/loja-table.service';

@Component({
  selector: 'home-page',
  imports: [ListaResumidaComponent, CounterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  camposLojas: IColunaTabela[] = [];

  constructor(private readonly router: Router,
              private readonly lojaTableService: LojaTableService){
    this.camposLojas = this.lojaTableService.criarColunasTabelaLojaResumida();
  }
  
  times = [
    { id: '1', nome: 'Fluminense', identificador: 'fluminense-rj' },
    { id: '2', nome: 'Botafogo', identificador: 'botafogo-rj' },
    { id: '3', nome: 'Flamengo', identificador: 'flamengo-rj' },
    { id: '4', nome: 'Vasco', identificador: 'vasco-rj' },
    { id: '5', nome: 'Palmeiras', identificador: 'palmeiras-sp' }
  ];
  
  camposTimes: IColunaTabela[] = [
    { key: 'id', label: 'Id' },
    { key: 'nome', label: 'Nome' },
    { key: 'identificador', label: 'Identificador' }
  ];
  
  lojas = [
    { id: '1', nome: 'Memórias do Esporte', site: 'memoriasdoesporte.com.br' },
    { id: '2', nome: 'BK Camisetas', site: 'bkcamisetas.com.br' },
    { id: '3', nome: 'Manto Sagrado', site: 'mantosagrado.com.br' },
    { id: '4', nome: 'Brechó do Futebol', site: 'brechodofut.com.br' },
    { id: '5', nome: 'Chanti Sports', site: 'chantisports.com.br' },
  ];

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
}

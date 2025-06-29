import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FiltroLoja } from '../../../models/filtros/filtro-loja';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { IColunaTabela } from '../../../shared/components/lista-resumida/lista-resumida.component';
import { LojaTableService } from '../../../services/loja-table.service';
import { Loja } from '../../../models/loja';

@Component({
  selector: 'app-listagem-lojas',
  imports: [CommonModule, ReactiveFormsModule],  
  templateUrl: './listagem-lojas.component.html',
  styleUrl: './listagem-lojas.component.css'
})
export class ListagemLojasComponent {
  form: FormGroup;
  camposLojas: IColunaTabela[] = [];
    
  lojas: Loja[] = [
    { id: '1', nome: 'Memórias do Esporte', site: 'memoriasdoesporte.com.br', parceira: true, ativa: true, urlBusca:'', times: [] },
    { id: '2', nome: 'BK Camisetas', site: 'bkcamisetas.com.br' , parceira: true, ativa: true, urlBusca:'', times: []},
    { id: '3', nome: 'Manto Sagrado', site: 'mantosagrado.com.br', parceira: true, ativa: true, urlBusca:'', times: [] },
    { id: '4', nome: 'Brechó do Futebol', site: 'brechodofut.com.br', parceira: false, ativa: true, urlBusca:'', times: [] },
    { id: '5', nome: 'Chanti Sports', site: 'chantisports.com.br' , parceira: true, ativa: false, urlBusca:'', times: []},
  ];

  constructor(private fb: FormBuilder,
              private readonly router: Router,
              private readonly tableService: LojaTableService) {
      this.form = this.criarForm();
      this.camposLojas = this.tableService.criarColunasTabelaLojaCompleta();
  }    

  private criarForm() {
    return this.fb.group({
      nome: [''],
      site: [''],
      parceira: [false],
      ativa: [true]
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

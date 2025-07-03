import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface IColunaTabela {
  key: string;
  label: string;
  bool: boolean;
}

@Component({
  selector: 'app-tabela-dinamica',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './tabela-dinamica.component.html',
  styleUrl: './tabela-dinamica.component.css'
})
export class TabelaDinamicaComponent {
  @Input() tituloLista: string = '';
  @Input() registros: any[] = [];
  @Input() colunas: IColunaTabela[] = [];
  @Input() exibirAcoes: boolean = true;
  @Input() exibirAdd: boolean = true;
  @Input() flutuante: boolean = true;
  
  @Output() edicaoClicked = new EventEmitter<any>();
  @Output() remocaoClicked = new EventEmitter<any>();
  @Output() adicicaoClicked = new EventEmitter<void>();
  
  obterValorCampo(objeto: any, campo: string): any {
    return campo.split('.').reduce((objetoCorrente, prop) => objetoCorrente?.[prop], objeto);
  }
  
  onEdit(item: any) {
    this.edicaoClicked.emit(item);
  }
  
  onDelete(item: any) {
    this.remocaoClicked.emit(item);
  }
  
  onAdd() {
    this.adicicaoClicked.emit();
  }
}

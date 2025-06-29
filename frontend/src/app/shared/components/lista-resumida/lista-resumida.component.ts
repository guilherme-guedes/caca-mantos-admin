import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface IColunaTabela {
  key: string;
  label: string;
}

@Component({
  selector: 'app-lista-resumida',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './lista-resumida.component.html',
  styleUrl: './lista-resumida.component.css'
})
export class ListaResumidaComponent {
  @Input() tituloLista: string = '';
  @Input() registros: any[] = [];
  @Input() colunas: IColunaTabela[] = [];
  @Input() exibirAcoes: boolean = true;
  @Input() exibirAdd: boolean = true;
  
  @Output() edicaoClicked = new EventEmitter<any>();
  @Output() remocaoClicked = new EventEmitter<any>();
  @Output() adicicaoClicked = new EventEmitter<void>();
  
  getValue(objeto: any, path: string): any {
    return path.split('.').reduce((current, prop) => current?.[prop], objeto);
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

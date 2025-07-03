import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-paginacao',
  imports: [CommonModule],
  templateUrl: './paginacao.component.html',
  styleUrl: './paginacao.component.css'
})
export class PaginacaoComponent {
  @Input() paginaAtual = 1;
  @Input() totalRegistros = 0;
  @Input() tamanhoPagina = 10;
  @Input() maxPaginasVisiveis = 5;

  @Output() paginaAlterada = new EventEmitter<number>();
  
  get totalPaginas(): number {
    return Math.ceil(this.totalRegistros / this.tamanhoPagina);
  }
  
  get paginas(): number[] {
    const total = this.totalPaginas;
    const atual = this.paginaAtual;
    const visiveis = this.maxPaginasVisiveis;

    let inicio = Math.max(1, atual - Math.floor(visiveis / 2));
    let fim = inicio + visiveis - 1;

    if (fim > total) {
      fim = total;
      inicio = Math.max(1, fim - visiveis + 1);
    }

    return Array.from({ length: fim - inicio + 1 }, (_, i) => i + inicio);
  }

  mudarPagina(pagina: number) {
    if (pagina >= 1 && pagina <= this.totalPaginas) {
      this.paginaAlterada.emit(pagina);
    }
  }
}

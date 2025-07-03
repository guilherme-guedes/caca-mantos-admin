export class Pagina<T> {
  paginaAtual: number = 1;
  totalPaginas: number = 1;
  itensPorPagina: number = 10;   
  quantidadeTotal: number = 0;
  itens: T[] = [];
}

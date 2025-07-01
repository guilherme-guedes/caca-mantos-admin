export interface Pagina<T> {
  paginaAtual: number,
  totalPaginas: number,
  itensPorPagina: number,    
  quantidadeTotal: number,
  itens: T[]
}
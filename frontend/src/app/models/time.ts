export interface Time {
  id: string;
  nome: string;
  nomeBusca: string;
  identificador: string;
  destaque: boolean;
  ativo: boolean;
  principal: boolean;
  termos: string[];
  homonimos: string[];
}
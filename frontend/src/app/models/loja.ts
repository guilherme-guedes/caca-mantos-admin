import { TimeResumido } from "./dto/time-resumido";

export interface Loja {
  id: string;
  nome: string;
  site: string;
  urlBusca: string;
  parceira: boolean;
  ativa: boolean;
  times: TimeResumido[];
}
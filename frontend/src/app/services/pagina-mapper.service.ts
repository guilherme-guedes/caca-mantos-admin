import { Injectable } from "@angular/core";
import { Pagina } from "../models/dto/pagina";

@Injectable({
  providedIn: 'root'
})
export class PaginaMapperService {

  constructor() { }

  paraModelos<T>(retornoAPI : any, conversaoItens: (itens: any) => T[]){
    let pagina: Pagina<T> = {} as Pagina<T>;
    Object.assign(pagina, retornoAPI);
    const modelos = conversaoItens(pagina.itens);
    return modelos;
  }
}
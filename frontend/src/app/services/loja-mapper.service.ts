import { Injectable } from '@angular/core';
import { Loja } from '../models/loja';

@Injectable({
  providedIn: 'root'
})
export class LojaMapperService {

  constructor() { }

  paraModelo(retornoAPI: any) : Loja{
    let loja: Loja = {} as Loja;
    Object.assign(loja, retornoAPI)
    return loja;
  }

  paraModelos(retornoAPI: any) : Loja[]{
    let lojas: Loja[] = [];
    Object.assign(lojas, retornoAPI);
    return lojas;
  }
}

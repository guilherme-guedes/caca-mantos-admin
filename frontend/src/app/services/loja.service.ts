import { Injectable } from '@angular/core';
import { environment } from '../environment/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LojaService {
  private endpoint: string = 'lojas';
  private urlBase: string = environment.urlAPI;

  constructor(private readonly httpClient: HttpClient) {}

  obter(id: string){
    return this.httpClient.get(`${this.urlBase}/${this.endpoint}/${id}`);
  }

  consultar(pagina: number,
                tamanhoPagina: number,
                trecho: string = "",
                ativa: boolean | null = null,
                parceira: boolean | null = null){
    return this.httpClient.get(`${this.urlBase}/${this.endpoint}?pagina=${pagina}&quantidade=${tamanhoPagina}&trecho=${trecho}&parceira=${parceira}&ativa=${ativa}`)
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environment/environment'

@Injectable({
  providedIn: 'root'
})
export class TimeService {
  private endpoint: string = 'times';
  private urlBase: string = environment.urlAPI;

  constructor(private readonly httpClient: HttpClient) {}

  obter(id: string){
    return this.httpClient.get(`${this.urlBase}/${this.endpoint}/${id}`);
  }

  consultar(pagina: number,
                tamanhoPagina: number,
                trecho: string = "",
                destaque: boolean | null = null,
                ativo: boolean | null = null,
                principal: boolean | null = null){
    return this.httpClient.get(`${this.urlBase}/${this.endpoint}?pagina=${pagina}&quantidade=${tamanhoPagina}&trecho=${trecho}&destaque=${destaque}&ativo=${ativo}&principal=${principal}`)
  }
}

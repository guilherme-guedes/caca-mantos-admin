import { Injectable } from '@angular/core';
import { environment } from '../environment/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private endpoint: string = 'dashboard';
  private urlBase: string = environment.urlAPI;

  constructor(private readonly httpClient: HttpClient) {}

  obterQuantidadeLojas(){
    return this.httpClient.get(`${this.urlBase}/${this.endpoint}/quantidade-lojas`);
  }

  obterQuantidadeTimes(){
    return this.httpClient.get(`${this.urlBase}/${this.endpoint}/quantidade-times`);
  }
}

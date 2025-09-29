import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LojaFormService } from '../../../services/loja-form.service';
import { FormLojaComponent } from '../components/form-loja/form-loja.component';
import { Loja } from '../../../models/loja';
import { LojaMapperService } from '../../../services/loja-mapper.service';
import { LojaService } from '../../../services/loja.service';
import { finalize, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-edicao-loja',
  imports: [
    CommonModule,
    FormLojaComponent
  ],
  templateUrl: './edicao-loja.component.html',
  styleUrl: './edicao-loja.component.css'
})
export class EdicaoLojaComponent  implements OnInit {
  private destroy$ = new Subject<void>();
  loja: Loja | null = null;  
  carregando = false;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private readonly lojaService: LojaService,
              private readonly mapper: LojaMapperService) { }

  ngOnInit(): void {
      const id = this.route.snapshot.params['id'];
      if (id) {
        this.lojaService.obter(id)
                      .pipe(takeUntil(this.destroy$), finalize(() => this.carregando = false))
                      .subscribe({
                        next: (dadosTime) => {
                          this.loja = this.mapper.paraModelo(dadosTime);
                        },
                        error: (error) => {
                          console.error('Erro:', error);
                          this.router.navigate(['/times']);
                        }
                      });
      }
      else{
        console.error('Erro: sem id informado');
        this.router.navigate(['/times']);
      }

    //const id = this.route.snapshot.params['id'];
    //if (id) {
      // this.loja = {
      //   id: id,
      //   nome: 'Loja Exemplo',
      //   site: 'https://www.exemplo.com',
      //   urlBusca: 'https://www.exemplo.com/busca?q=',
      //   times: [],
      //   parceira: true,
      //   ativa: true        
      // };
    //}
  }
}

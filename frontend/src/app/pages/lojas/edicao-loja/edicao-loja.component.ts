import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LojaFormService } from '../../../services/loja-form.service';
import { FormLojaComponent } from '../components/form-loja/form-loja.component';
import { Loja } from '../../../models/loja';

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
  loja: Loja | null = null;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      // this.lojaService.consultar(id).subscribe(loja => {
      //   this.loja = loja;
      // });
      this.loja = {
        id: id,
        nome: 'Loja Exemplo',
        site: 'https://www.exemplo.com',
        urlBusca: 'https://www.exemplo.com/busca?q=',
        times: [],
        parceira: true,
        ativa: true        
      };
    }
  }
}

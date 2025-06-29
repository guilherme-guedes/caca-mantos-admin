import { Component, OnInit } from '@angular/core';
import { FormTimeComponent } from '../components/form-time/form-time.component';
import { Time } from '../../../models/time';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edicao-time',
  imports: [FormTimeComponent],
  templateUrl: './edicao-time.component.html',
  styleUrl: './edicao-time.component.css'
})
export class EdicaoTimeComponent implements OnInit{  
  time: Time | null = null; 

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.time = {
        id: id,
        nome: 'Time Exemplo',
        identificador: 'fluminense-rj',
        nomeBusca: 'Fluminense',
        destaque: true,
        principal: true,
        termos:[],
        homonimos: [],
        ativo: true
      };
    }
  }
}

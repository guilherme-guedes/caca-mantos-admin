import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { LojaFormService } from '../../../../services/loja-form.service';
import { CommonModule } from '@angular/common';
import { InputListItemComponent } from '../../../../shared/components/input-list-item/input-list-item.component';
import { Loja } from '../../../../models/loja';

@Component({
  selector: 'app-form-loja',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, InputListItemComponent],
  templateUrl: './form-loja.component.html',
  styleUrl: './form-loja.component.css'
})
export class FormLojaComponent {
  form: FormGroup;
  
  @Input() dadosIniciais: Loja | null = null;
  @Output() onSubmited = new EventEmitter<Loja>();

  constructor(
    private readonly lojaFormService: LojaFormService) {
      this.form = this.lojaFormService.criarForm();
  }

  ngOnChanges(): void {
    if (this.dadosIniciais)
      this.form.patchValue(this.dadosIniciais);    
  }
  
  onSubmit(item: any) {
    if (this.form.valid) {
      const loja = Object.assign({}, this.form.value);
      loja.id = this.dadosIniciais ? this.dadosIniciais.id : null;
      
      this.onSubmited.emit(loja);
    }
  } 
}

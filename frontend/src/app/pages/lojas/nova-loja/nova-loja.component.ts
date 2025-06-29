import { Component } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { InputListItemComponent } from '../../../shared/components/input-list-item/input-list-item.component';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { LojaFormService } from '../../../services/loja-form.service';
import { Loja } from '../../../models/loja';
import { FormLojaComponent } from '../components/form-loja/form-loja.component';

@Component({
  selector: 'app-nova-loja',
  imports: [
    CommonModule,
    ReactiveFormsModule,
  FormLojaComponent],
  templateUrl: './nova-loja.component.html',
  styleUrl: './nova-loja.component.css'
})
export class NovaLojaComponent {

}

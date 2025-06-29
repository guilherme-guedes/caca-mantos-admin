import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TimeFormService } from '../../../../services/time-form.service';
import { InputListComponent } from '../../../../shared/components/input-list/input-list.component';
import { InputListItemComponent } from '../../../../shared/components/input-list-item/input-list-item.component';
import { Time } from '../../../../models/time';

@Component({
  selector: 'app-form-time',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    InputListComponent, 
    InputListItemComponent,
  ],
  templateUrl: './form-time.component.html',
  styleUrl: './form-time.component.css'
})
export class FormTimeComponent {
  form: FormGroup;
  
  @Input() dadosIniciais: Time  | null = null;
  @Output() onSubmited = new EventEmitter<any>();

  constructor(
    private readonly router: Router,
    private readonly timeFormService: TimeFormService) {
      this.form = this.timeFormService.criarForm();
  }
  
  ngOnChanges(): void{
    if(this.dadosIniciais) {
      this.form.patchValue(this.dadosIniciais);
    }
  }

  onSubmit(item: any) {
    if(this.form.valid) {
      const time = Object.assign({}, this.form.value);
      time.id = this.dadosIniciais ? this.dadosIniciais.id : null;

      this.onSubmited.emit(item);
    } 
  }
}

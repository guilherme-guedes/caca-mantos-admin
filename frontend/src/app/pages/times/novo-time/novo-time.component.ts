import { Component, OnInit } from '@angular/core';
import { InputListComponent } from '../../../shared/components/input-list/input-list.component';
import { InputListItemComponent } from '../../../shared/components/input-list-item/input-list-item.component';
import { Router } from '@angular/router';
import { Time } from '../../../models/time';
import { TimeFormService } from '../../../services/time-form.service';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormTimeComponent } from '../components/form-time/form-time.component';

@Component({
  selector: 'app-novo-time',
  imports: [FormTimeComponent],
  templateUrl: './novo-time.component.html',
  styleUrl: './novo-time.component.css'
})
export class NovoTimeComponent {  
}

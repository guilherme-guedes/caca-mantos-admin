import { Injectable } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Time } from "../models/time";

@Injectable({
  providedIn: 'root'
})
export class TimeFormService {
  
  constructor(private fb: FormBuilder) {}
  
  criarForm(time?: Partial<Time>): FormGroup {
    return this.fb.group({
      id: [time?.id],
      nome: [time?.nome || '', [Validators.required, Validators.minLength(3)]],
      identificador: [time?.identificador || '', [Validators.required, Validators.minLength(5)]],
      nomeBusca: [time?.identificador || '', [Validators.required, Validators.minLength(3)]],
      destaque: [time?.destaque || false],
      ativo: [time?.ativo || true],
      principal: [time?.principal || true],
      termos: [time?.termos || []],
      homonimos: [time?.homonimos || []]
    });
  }
  
  atualizarForm(form: FormGroup, time: Time): void {
    form.patchValue(time);
  }
}
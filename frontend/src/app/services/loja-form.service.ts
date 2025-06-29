import { Injectable } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Loja } from "../models/loja";

@Injectable({
  providedIn: 'root'
})
export class LojaFormService {
  
  constructor(private fb: FormBuilder) {}
  
  criarForm(loja?: Partial<Loja>): FormGroup {
    return this.fb.group({
      id: [loja?.id],
      nome: [loja?.nome || '', [Validators.required, Validators.minLength(5)]],
      site: [loja?.site || '', [Validators.required, Validators.minLength(12)]],
      urlBusca: [loja?.urlBusca || '', [Validators.required, Validators.minLength(12)]],
      ativa: [loja?.ativa || true],
      parceira: [loja?.parceira || false],
      times: [loja?.times || []]
    });
  }
  
  atualizarForm(form: FormGroup, loja: Loja): void {
    form.patchValue(loja);
  }
}
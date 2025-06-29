import { CommonModule  } from '@angular/common';
import { Component, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { ControlValueAccessor, FormsModule,  NG_VALUE_ACCESSOR } from '@angular/forms';

export interface ListItem {
  id: number | string;
  name: string;
}

@Component({
  selector: 'app-input-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './input-list.component.html',
  styleUrl: './input-list.component.css', 
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputListComponent),
      multi: true
    }
  ]
})
export class InputListComponent implements ControlValueAccessor {
  @Input() label: string = 'termo';
  @Input() labelPlural: string = 'termos';
  @Input() placeholder: string = `Digite um ${this.label} e pressione Enter ou clique em Adicionar`;
  @Input() maxItems: number = 10;
  
  termos: string[] = [];
  textoAtual?: string = '';
  
  private onChange = (value: string[]) => {};
  private onTouched = () => {};
  
  adicionar(): void {
    const trimmedInput = this.textoAtual?.trim();
    
    if (!trimmedInput) {
      return;
    }
    
    if (this.termos.includes(trimmedInput)) {
      alert(`Este ${this.label} já foi adicionado!`);
      return;
    }
    
    if (this.termos.length >= this.maxItems) {
      alert(`Você pode adicionar no máximo ${this.maxItems} ${this.labelPlural}!`);
      return;
    }
    
    this.termos.push(trimmedInput);
    this.textoAtual = '';
    
    this.onChange(this.termos);
    this.onTouched();
  }
  
  remover(index: number): void {
    this.termos.splice(index, 1);
    this.onChange(this.termos);
    this.onTouched();
  }
  
  // ControlValueAccessor
  writeValue(value: string[]): void {
    this.termos = value || [];
  }
  
  registerOnChange(fn: (value: string[]) => void): void {
    this.onChange = fn;
  }
  
  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }
}

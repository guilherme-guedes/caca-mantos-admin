import { CommonModule } from '@angular/common';
import { Component, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';

export interface ListItem {
  id: number | string;
  name: string;
}

@Component({
  selector: 'app-input-list-item',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './input-list-item.component.html',
  styleUrl: './input-list-item.component.css',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputListItemComponent),
      multi: true
    }
  ]
})
export class InputListItemComponent implements ControlValueAccessor {
  @Input() label: string = '';
  @Input() pluralLabel: string = '';
  @Input() placeholder: string = 'Selecione um item';
  @Input() maxItems: number = 10;
  @Input() allItems: ListItem[] = [];
  @Input() showEmptyAlert: boolean = true;
  
  selectedItems: ListItem[] = [];
  selectedItemId: string = '';  
  
  private onChange = (value: (number | string)[]) => {};
  private onTouched = () => {};

  get availableItems(): ListItem[] {
    const selectedIds = this.selectedItems.map(r => r.id.toString());
    return this.allItems.filter(item => !selectedIds.includes(item.id.toString()));
  }
  
  addItem(): void {
    if (!this.selectedItemId)
      return;
        
    if (this.selectedItems.length >= this.maxItems) {
      alert(`Você pode adicionar no máximo ${this.maxItems} ${this.label}!`);
      return;
    }
    
    const selectedItem = this.allItems.find(item => 
      item.id.toString() === this.selectedItemId
    );
    
    if (selectedItem) {
      this.selectedItems.push(selectedItem);
      this.selectedItemId = '';
      
      const ids = this.selectedItems.map(r => r.id);
      this.onChange(ids);
      this.onTouched();
    }
  }
  
  removeItem(index: number): void {
    this.selectedItems.splice(index, 1);
    const ids = this.selectedItems.map(r => r.id);
    this.onChange(ids);
    this.onTouched();
  }
  
  // ControlValueAccessor
  writeValue(value: (number | string)[]): void {
    if (value && Array.isArray(value)) {
      this.selectedItems = value
                            .map(id => this.allItems.find(item => item.id.toString() === id.toString()))
                            .filter(item => item !== undefined) as ListItem[];
    } else
        this.selectedItems = [];    
  }
  
  registerOnChange(fn: (value: (number | string)[]) => void): void {
    this.onChange = fn;
  }
  
  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }
}
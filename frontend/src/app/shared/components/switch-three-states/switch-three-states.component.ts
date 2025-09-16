import { Component, EventEmitter, forwardRef, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { NullableBool } from './switch-three-states.types';

@Component({
  selector: 'app-switch-three-states',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './switch-three-states.component.html',
  styleUrl: './switch-three-states.component.css',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SwitchThreeStatesComponent),
      multi: true
    }
  ]
})
export class SwitchThreeStatesComponent implements ControlValueAccessor {
  @Input() label: string = '';
  @Input() disabled: boolean = false;
  @Input() showStateText: boolean = false;
  @Input() trueText: string = 'Sim';
  @Input() falseText: string = 'Não';
  @Input() neutralText: string = '';
  
  @Output() valueChange = new EventEmitter<NullableBool>();

  value: NullableBool = null;
  
  private onChange = (value: NullableBool) => {};
  private onTouched = () => {};

  toggle(): void {
    if (this.disabled) return;

    switch (this.value) {
      case null:
        this.value = true;
        break;
      case true:
        this.value = false;
        break;
      case false:
        this.value = null;
        break;
    }

    this.onChange(this.value);
    this.onTouched();
    this.valueChange.emit(this.value);
  }

  getStateText(): string {
    switch (this.value) {
      case true: return this.trueText;
      case false: return this.falseText;
      case null: return this.neutralText;
      default: return this.neutralText;
    }
  }

  writeValue(value: NullableBool): void {
    this.value = value;
  }

  registerOnChange(fn: (value: NullableBool) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
}

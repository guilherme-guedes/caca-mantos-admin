import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-dashboard-counter',
  imports: [],
  templateUrl: './counter.component.html',
  styleUrl: './counter.component.css'
})
export class CounterComponent {
  @Input() valor: number = 0;
  @Input() titulo: string = '';
  @Input() icone: string = 'fas fa-chart-line';
  @Input() color: 'primary' | 'success' | 'warning' | 'danger' = 'primary';
  @Input() change: number = 0;
  @Input() changeLabel: string = 'desde o último mês';

  animatedValue: number = 0;

  ngOnInit() {
    this.animateValue();
  }

  ngOnChanges() {
    this.animateValue();
  }

  get iconClass(): string {
    return this.icone;
  }

  private animateValue() {
    const duration = 1000; // 1 segundo
    const startTime = performance.now();
    const startValue = this.animatedValue;
    const endValue = this.valor;

    const animate = (currentTime: number) => {
      const elapsed = currentTime - startTime;
      const progress = Math.min(elapsed / duration, 1);
      
      const easeOut = 1 - Math.pow(1 - progress, 3);
      
      this.animatedValue = Math.round(startValue + (endValue - startValue) * easeOut);

      if (progress < 1) {
        requestAnimationFrame(animate);
      }
    };

    requestAnimationFrame(animate);
  }
}

import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormTimeComponent } from '../components/form-time/form-time.component';
import { Time } from '../../../models/time';
import { ActivatedRoute, Router } from '@angular/router';
import { TimeService } from '../../../services/time.service';
import { finalize, Subject, takeUntil } from 'rxjs';
import { TimeMapperService } from '../../../services/time-mapper.service';

@Component({
  selector: 'app-edicao-time',
  imports: [FormTimeComponent],
  templateUrl: './edicao-time.component.html',
  styleUrl: './edicao-time.component.css'
})
export class EdicaoTimeComponent implements OnInit, OnDestroy{  
  private destroy$ = new Subject<void>();
  time: Time | null = null; 
  carregando = false;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private readonly timeService: TimeService,
              private readonly mapper: TimeMapperService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.timeService.obterTime(id)
                    .pipe(takeUntil(this.destroy$), finalize(() => this.carregando = false))
                    .subscribe({
                      next: (dadosTime) => {
                        this.time = this.mapper.paraModelo(dadosTime);
                      },
                      error: (error) => {
                        console.error('Erro:', error);
                        this.router.navigate(['/times']);
                      }
                    });
    }
    else{
      console.error('Erro: sem id informado');
      this.router.navigate(['/times']);
    }
  }
  
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}

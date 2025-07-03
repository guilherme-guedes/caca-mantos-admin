import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TabelaDinamicaComponent } from './tabela-dinamica.component';

describe('ListaResumidaComponent', () => {
  let component: TabelaDinamicaComponent;
  let fixture: ComponentFixture<TabelaDinamicaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TabelaDinamicaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TabelaDinamicaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

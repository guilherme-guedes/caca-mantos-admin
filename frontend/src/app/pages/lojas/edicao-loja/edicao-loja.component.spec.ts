import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EdicaoLojaComponent } from './edicao-loja.component';

describe('EdicaoLojaComponent', () => {
  let component: EdicaoLojaComponent;
  let fixture: ComponentFixture<EdicaoLojaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EdicaoLojaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EdicaoLojaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

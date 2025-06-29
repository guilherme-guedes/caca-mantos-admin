import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaResumidaComponent } from './lista-resumida.component';

describe('ListaResumidaComponent', () => {
  let component: ListaResumidaComponent;
  let fixture: ComponentFixture<ListaResumidaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListaResumidaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaResumidaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

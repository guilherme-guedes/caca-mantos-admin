import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListagemLojasComponent } from './listagem-lojas.component';

describe('ListagemLojasComponent', () => {
  let component: ListagemLojasComponent;
  let fixture: ComponentFixture<ListagemLojasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListagemLojasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListagemLojasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListagemCamisasComponent } from './listagem-camisas.component';

describe('ListagemCamisasComponent', () => {
  let component: ListagemCamisasComponent;
  let fixture: ComponentFixture<ListagemCamisasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListagemCamisasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListagemCamisasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

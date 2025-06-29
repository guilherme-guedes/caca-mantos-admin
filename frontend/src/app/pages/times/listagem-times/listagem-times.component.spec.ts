import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListagemTimesComponent } from './listagem-times.component';

describe('ListagemTimesComponent', () => {
  let component: ListagemTimesComponent;
  let fixture: ComponentFixture<ListagemTimesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListagemTimesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListagemTimesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

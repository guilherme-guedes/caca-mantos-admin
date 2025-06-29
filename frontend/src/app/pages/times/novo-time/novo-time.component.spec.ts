import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NovoTimeComponent } from './novo-time.component';

describe('NovoTimeComponent', () => {
  let component: NovoTimeComponent;
  let fixture: ComponentFixture<NovoTimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NovoTimeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NovoTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EdicaoTimeComponent } from './edicao-time.component';

describe('EdicaoTimeComponent', () => {
  let component: EdicaoTimeComponent;
  let fixture: ComponentFixture<EdicaoTimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EdicaoTimeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EdicaoTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SwitchThreeStatesComponent } from './switch-three-states.component';

describe('SwitchThreeStatesComponent', () => {
  let component: SwitchThreeStatesComponent;
  let fixture: ComponentFixture<SwitchThreeStatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SwitchThreeStatesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SwitchThreeStatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

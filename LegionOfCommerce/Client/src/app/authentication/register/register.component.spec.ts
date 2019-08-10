import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterComponent } from './register.component';

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [RegisterComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  /*
  it('should verify forms', () => {
    component.email.setValue('banana@gmail.com');
    component.password.setValue('12345');
    component.username.setValue('banana');
    component.confirmPassword.setValue('12345');

    const expected = false;
    const actual = component.register();

    // expect(actual === 'not valid');
  });
  */
});

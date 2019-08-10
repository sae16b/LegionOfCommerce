import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  FormControl,
  Validators
} from '@angular/forms';
import { AuthFormComponent } from '../auth-form/auth-form.component';
import { Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '@app/core/services/auth.service';
import { RegistrationInfo } from '@app/core/models/registration-info';
import { error } from 'util';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent extends AuthFormComponent implements OnInit {
  firstName = new FormControl('');
  lastName = new FormControl('');
  username = new FormControl('', Validators.required);
  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required]);
  confirmPassword = new FormControl('', [Validators.required]);

  formSubmitting = false;
  formDisabled = false;

  authInfo: RegistrationInfo;

  constructor(
    protected authService: AuthService,
    protected router: Router,
    protected fb: FormBuilder
  ) {
    super(authService, router, fb);
    this.form = fb.group({
      firstName: this.firstName,
      lastName: this.lastName,
      username: this.username,
      email: this.email,
      password: this.password,
      confirmPassword: this.confirmPassword
    });
  }

  isPasswordStrong() {
    const strong = AuthService.isPasswordStrong(this.password.value);
    if (!strong) {
      // display error
      console.log('password not strong enough');
    }
    return strong;
  }

  confirmPasswordCorrect() {
    const correct = this.password.value === this.confirmPassword.value;
    if (!correct) {
      // display error
      console.log('password != confirmPassword');
    }
    return correct;
  }

  formChecks() {
    return this.confirmPasswordCorrect() && this.isPasswordStrong();
  }

  ngOnInit() {
    this.quickRegister();
  }

  formToAuthInfo() {
    this.authInfo = {
      firstName: this.firstName.value,
      lastName: this.lastName.value,
      userName: this.username.value,
      email: this.email.value,
      password: this.password.value
    };
  }

  quickRegister() {
    this.firstName.setValue('bob');
    this.lastName.setValue('ross');
    this.email.setValue('bob@ro42rere43233ss.com');
    this.username.setValue('bo4223rereb');
    this.password.setValue('a123456');
    this.confirmPassword.setValue('a123456');
  }

  register() {
    if (!this.form.valid || !this.formChecks()) {
      // Install error handling later
      return;
    }

    this.formToAuthInfo();
    this._register();
  }

  _register() {
    this.authService
      .register(this.authInfo)
      .then(res => {
        this.registrationSuccess();
      })
      .catch(err => {
        this.registrationFailure();
      });
  }

  registrationSuccess() {
    console.log('registration success!');
    this.router.navigateByUrl('/');
  }

  registrationFailure() {
    console.log('registration failure :(');
  }
}

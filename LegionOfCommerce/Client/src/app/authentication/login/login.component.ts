import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { AuthService } from '@app/core/services/auth.service';
import { LoginInfo } from '@app/core/models/login-info';
import { Router } from '@angular/router';
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder
} from '@angular/forms';
import { AuthFormComponent } from '../auth-form/auth-form.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends AuthFormComponent implements OnInit {
  emailOrUserName = new FormControl('', Validators.required);
  password = new FormControl('', Validators.required);

  @ViewChild('emailOrUserNameInput', { static: false })
  emailOrUserNameInput: ElementRef;

  authInfo: LoginInfo = {
    emailOrUserName: '',
    password: ''
  };

  incorrectUserPassword = false;

  constructor(
    protected authService: AuthService,
    protected router: Router,
    protected fb: FormBuilder
  ) {
    super(authService, router, fb);
    this.form = fb.group({
      emailOrUserName: this.emailOrUserName,
      password: this.password
    });
  }

  ngOnInit() {
    this.signInQuickly();
  }

  formToAuthInfo() {
    this.authInfo.emailOrUserName = this.emailOrUserName.value;
    this.authInfo.password = this.password.value;
  }

  signInQuickly() {
    this.emailOrUserName.setValue('bobross12');
    this.password.setValue('123456');
  }

  signIn() {
    if (!this.form.valid) {
      return;
    }
    this.incorrectUserPassword = false;
    this.formSubmitting = true;
    this.disableForm();
    this.formToAuthInfo();
    this._signIn();
  }

  _signIn() {
    this.authService
      .login(this.authInfo)
      .then(_ => {
        this.signInSuccess();
      })
      .catch(err => {
        // do some error handling for this form
        this.signInFailure(err);
      });
  }

  signInSuccess() {
    this.router.navigateByUrl('/');
  }

  signInFailure(err) {
    this.enableForm();
    this.formSubmitting = false;
    this.incorrectUserPassword = true;
    setTimeout(() => {
      this.emailOrUserNameInput.nativeElement.focus();
    });
  }
}

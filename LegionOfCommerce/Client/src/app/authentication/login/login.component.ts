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

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  emailOrUserName = new FormControl('', Validators.required);
  password = new FormControl('', Validators.required);

  @ViewChild('emailOrUserNameInput', { static: false })
  emailOrUserNameInput: ElementRef;

  authInfo: LoginInfo = {
    emailOrUserName: '',
    password: ''
  };

  formSubmitting = false;
  formDisabled = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    fb: FormBuilder
  ) {
    this.loginForm = fb.group({
      emailOrUserName: this.emailOrUserName,
      password: this.password
    });
  }

  ngOnInit() {}

  submitOnEnter(key) {
    if (key === 'Enter') {
      this.signIn();
      return;
    }
  }

  disableForm() {
    this.formDisabled = true;
    for (const key in this.loginForm.controls) {
      if (this.loginForm.controls.hasOwnProperty(key)) {
        const control = this.loginForm.controls[key];
        control.disable();
      }
    }
  }
  enableForm() {
    for (const key in this.loginForm.controls) {
      if (this.loginForm.controls.hasOwnProperty(key)) {
        const control = this.loginForm.controls[key];
        control.enable();
      }
    }
    this.formDisabled = false;
  }

  formToAuthInfo() {
    this.authInfo.emailOrUserName = this.emailOrUserName.value;
    this.authInfo.password = this.password.value;
  }

  signIn() {
    if (!this.loginForm.valid) {
      return;
    }
    this.disableForm();
    this.formSubmitting = true;
    setTimeout(() => {
      this.formToAuthInfo();
      console.log(this.authInfo);
      this.authService.login(this.authInfo).subscribe(
        res => {
          console.log(res);
          // store token and navigate away
          this.router.navigateByUrl('/');
        },
        err => {
          this.enableForm();
          this.formSubmitting = false;
          setTimeout(() => {
            this.emailOrUserNameInput.nativeElement.focus();
          });
        }
      );
    }, 1000);
  }
}

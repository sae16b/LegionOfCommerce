import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '@app/core/services/auth.service';

export abstract class AuthFormComponent implements OnInit {
  form: FormGroup;
  formDisabled: boolean;
  formSubmitting = false;

  abstract authInfo: any;

  constructor(
    protected authService: AuthService,
    protected router: Router,
    protected fb: FormBuilder
  ) {}

  ngOnInit() {}

  abstract formToAuthInfo(): void;

  disableForm() {
    this.formDisabled = true;
    for (const key in this.form.controls) {
      if (this.form.controls.hasOwnProperty(key)) {
        const control = this.form.controls[key];
        control.disable();
      }
    }
  }
  enableForm() {
    for (const key in this.form.controls) {
      if (this.form.controls.hasOwnProperty(key)) {
        const control = this.form.controls[key];
        control.enable();
      }
    }
    this.formDisabled = false;
  }
}

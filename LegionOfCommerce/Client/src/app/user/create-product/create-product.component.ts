import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.scss']
})
export class CreateProductComponent implements OnInit {
  form: FormGroup;

  constructor(protected fb: FormBuilder) {
    this.form = fb.group({});
  }

  ngOnInit() {}

  testFunc() {}
}

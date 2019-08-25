import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-product-img-picker',
  templateUrl: './product-img-picker.component.html',
  styleUrls: ['./product-img-picker.component.scss']
})
export class ProductImgPickerComponent implements OnInit {
  maxImages = Array(12).fill(0);
  images = [];

  constructor() {}

  ngOnInit() {}
}

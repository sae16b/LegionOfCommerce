import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductImgPickerComponent } from './product-img-picker.component';

describe('ProductImgPickerComponent', () => {
  let component: ProductImgPickerComponent;
  let fixture: ComponentFixture<ProductImgPickerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductImgPickerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductImgPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

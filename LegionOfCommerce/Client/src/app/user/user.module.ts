import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { ProfileComponent } from './profile/profile.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SharedModule } from '@app/shared/shared.module';
import { CreateProductComponent } from './create-product/create-product.component';
import { ListProductsComponent } from './list-products/list-products.component';

@NgModule({
  declarations: [ProfileComponent, DashboardComponent, CreateProductComponent, ListProductsComponent],
  imports: [CommonModule, UserRoutingModule, SharedModule]
})
export class UserModule {}

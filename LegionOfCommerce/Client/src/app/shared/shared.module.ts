import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MatInputModule,
  MatButtonModule,
  MatProgressSpinnerModule,
  MatRadioModule
} from '@angular/material';
import { MatListModule } from '@angular/material/list';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { DropDownComponent } from '@app/shared/components/drop-down/drop-down.component';
import { GridItemComponent } from '@app/shared/components/grid-item/grid-item.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SocialMediaAuthComponent } from '@app/shared/components/social-media-auth/social-media-auth.component';
import { HitEnterDirective } from '@app/shared/directives/hit-enter/hit-enter.directive';
import { FormErrorDirective } from '@app/shared/directives/form-error/form-error.directive';
import { CoreModule } from '@app/core/core.module';
import { DashboardSidebarComponent } from './components/dashboard-sidebar/dashboard-sidebar.component';
import { TooltipDirective } from '@app/shared/directives/tooltip/tooltip.directive';
import { ProductImgPickerComponent } from './components/product-img-picker/product-img-picker.component';

/*
  NOTE: If certain html tags are incorrectly showing up as
  undefined (i.e. mat-form-field), it might be because VSCode
  doesn't like this module being imported using @app/...
  Just use a relative path instead.
*/

@NgModule({
  declarations: [
    DropDownComponent,
    GridItemComponent,
    SocialMediaAuthComponent,
    HitEnterDirective,
    FormErrorDirective,
    DashboardSidebarComponent,
    TooltipDirective,
    ProductImgPickerComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    MatButtonModule,
    MatInputModule,
    MatListModule,
    MatRadioModule,
    MatProgressSpinnerModule,
    AngularFontAwesomeModule
  ],
  exports: [
    CommonModule,
    CoreModule,
    MatButtonModule,
    MatInputModule,
    MatListModule,
    MatRadioModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule,
    AngularFontAwesomeModule,
    DropDownComponent,
    GridItemComponent,
    DashboardSidebarComponent,
    SocialMediaAuthComponent,
    ProductImgPickerComponent,
    HitEnterDirective,
    FormErrorDirective
  ]
})
export class SharedModule {}

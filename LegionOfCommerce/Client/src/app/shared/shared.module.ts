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
import { HitEnterDirective } from './directives/hit-enter.directive';
import { FormErrorDirective } from './directives/form-error.directive';

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
    FormErrorDirective
  ],
  imports: [
    CommonModule,
    MatButtonModule,
    MatInputModule,
    MatListModule,
    MatRadioModule,
    MatProgressSpinnerModule,
    AngularFontAwesomeModule
  ],
  exports: [
    CommonModule,
    MatButtonModule,
    MatInputModule,
    MatListModule,
    MatRadioModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule,
    AngularFontAwesomeModule,
    DropDownComponent,
    GridItemComponent,
    SocialMediaAuthComponent,
    HitEnterDirective,
    FormErrorDirective
  ]
})
export class SharedModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatInputModule, MatButtonModule } from '@angular/material';
import { MatListModule } from '@angular/material/list';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { DropDownComponent } from './drop-down/drop-down.component';
import { GridItemComponent } from './grid-item/grid-item.component';

/*
  NOTE: If certain html tags are incorrectly showing up as
  undefined (i.e. mat-form-field), it might be because VSCode
  doesn't like this module being imported using @app/...
  Just use a relative path instead.
*/

@NgModule({
  declarations: [DropDownComponent, GridItemComponent],
  imports: [
    CommonModule,
    MatButtonModule,
    MatInputModule,
    MatListModule,
    AngularFontAwesomeModule
  ],
  exports: [
    CommonModule,
    MatButtonModule,
    MatInputModule,
    MatListModule,
    AngularFontAwesomeModule,
    DropDownComponent,
    GridItemComponent
  ]
})
export class SharedModule {}

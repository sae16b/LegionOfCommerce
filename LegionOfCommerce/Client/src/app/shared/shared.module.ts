import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatInputModule, MatButtonModule } from '@angular/material';
import { MatListModule } from '@angular/material/list';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { DropDownComponent } from './drop-down/drop-down.component';
import { GridItemComponent } from './grid-item/grid-item.component';

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

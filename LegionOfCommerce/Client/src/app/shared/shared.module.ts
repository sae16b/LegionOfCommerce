import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatInputModule, MatButtonModule } from '@angular/material';
import { MatListModule } from '@angular/material/list';
import { AngularFontAwesomeModule } from 'angular-font-awesome';

@NgModule({
  declarations: [],
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
    AngularFontAwesomeModule
  ]
})
export class SharedModule {}

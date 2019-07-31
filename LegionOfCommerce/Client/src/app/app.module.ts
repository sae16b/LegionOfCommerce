import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './shared/navbar/navbar.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from '@shared/shared.module';
import { GridItemComponent } from './shared/grid-item/grid-item.component';

@NgModule({
  declarations: [AppComponent, NavbarComponent],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}

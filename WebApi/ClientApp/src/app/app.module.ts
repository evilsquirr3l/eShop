import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { MdbCheckboxModule } from 'mdb-angular-ui-kit/checkbox';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import {MdbDropdownModule} from "mdb-angular-ui-kit/dropdown";
import { ProductCardComponent } from './product-card/product-card.component';
import { CarouselComponent } from './carousel/carousel.component';
import {MdbCarouselModule} from "mdb-angular-ui-kit/carousel";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProductCardComponent,
    CarouselComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    MdbCheckboxModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
    ]),
    MdbDropdownModule,
    MdbCarouselModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {
}

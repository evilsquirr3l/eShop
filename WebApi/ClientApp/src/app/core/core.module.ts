import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarouselComponent } from "./components/carousel/carousel.component";
import { NavMenuComponent } from "./components/nav-menu/nav-menu.component";
import { MdbCarouselModule } from "mdb-angular-ui-kit/carousel";


@NgModule({
  declarations: [
    CarouselComponent,
    NavMenuComponent
  ],
  exports: [
    NavMenuComponent,
    CarouselComponent
  ],
  imports: [
    CommonModule,
    MdbCarouselModule
  ]
})
export class CoreModule { }

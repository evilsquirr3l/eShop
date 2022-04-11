import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MdbCarouselModule } from 'mdb-angular-ui-kit/carousel';
import { CarouselComponent } from './components/carousel/carousel.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { CoreRoutingModule } from './core-routing.module';
import { MdbDropdownModule } from "mdb-angular-ui-kit/dropdown";

@NgModule({
  declarations: [
    CarouselComponent,
    NavMenuComponent,
    NotFoundComponent,
  ],
  exports: [
    NavMenuComponent,
    CarouselComponent,
  ],
  imports: [
    CommonModule,
    MdbCarouselModule,
    CoreRoutingModule,
    MdbDropdownModule,
  ],
})
export class CoreModule { }

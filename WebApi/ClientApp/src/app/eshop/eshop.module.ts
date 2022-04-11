import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EshopRoutingModule } from './eshop-routing.module';
import { CoreRoutingModule } from '../core/core-routing.module';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { ProductDetailsPageComponent } from './pages/product-details-page/product-details-page.component';
import { CoreModule } from "../core/core.module";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";

@NgModule({
  declarations: [
    ProductDetailsComponent,
    ProductDetailsPageComponent
  ],
  imports: [
    CommonModule,
    EshopRoutingModule,
    CoreRoutingModule,
    CoreModule,
    FontAwesomeModule,
  ],
})
export class EshopModule {
}

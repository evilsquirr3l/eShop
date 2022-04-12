import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductCardComponent } from './components/product-card/product-card.component';
import { HomeComponent } from './pages/home/home.component';
import { CoreRoutingModule } from '../core/core-routing.module';
import { CoreModule } from '../core/core.module';
import { ProductDetailsPageComponent } from './pages/product-details-page/product-details-page.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'products/:id',
    component: ProductDetailsPageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes), CoreRoutingModule, CoreModule],
  exports: [RouterModule, ProductCardComponent],
  declarations: [
    ProductCardComponent,
    HomeComponent,
  ],
})
export class EshopRoutingModule { }

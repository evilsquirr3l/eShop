import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MdbCarouselModule } from 'mdb-angular-ui-kit/carousel';

const routes: Routes = [];

@NgModule({
  imports: [RouterModule.forChild(routes), MdbCarouselModule],
  exports: [RouterModule],
  declarations: [
  ],
})
export class CoreRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MdbCarouselModule } from 'mdb-angular-ui-kit/carousel';
import { NotFoundComponent } from './pages/not-found/not-found.component';

const routes: Routes = [
  {
    path: 'not-found',
    component: NotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes), MdbCarouselModule],
  exports: [RouterModule],
  declarations: [],
})
export class CoreRoutingModule {
}

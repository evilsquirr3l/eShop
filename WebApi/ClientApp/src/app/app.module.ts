import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { MdbCheckboxModule } from 'mdb-angular-ui-kit/checkbox';
import { MdbDropdownModule } from 'mdb-angular-ui-kit/dropdown';
import { MdbCarouselModule } from 'mdb-angular-ui-kit/carousel';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    MdbCheckboxModule,
    RouterModule.forRoot([
      {
        path: '',
        loadChildren: () => import('./eshop/eshop.module').then((m) => m.EshopModule),
      },
      {
        path: 'auth',
        loadChildren: () => import('./auth/auth.module').then((m) => m.AuthModule),
      },
      {
        path: '',
        pathMatch: 'full',
        loadChildren: () => import('./core/core.module').then((m) => m.CoreModule),
      },
      {
        path: '**',
        redirectTo: '/not-found',
      },
    ]),
    MdbDropdownModule,
    MdbCarouselModule,
    FontAwesomeModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {
}

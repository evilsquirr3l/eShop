import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './pages/login/login.component';
import { AuthRoutingModule } from './auth-routing.module';
import { RegistrationComponent } from './pages/registration/registration.component';

@NgModule({
  imports: [
    CommonModule,
    AuthRoutingModule,
  ],
  declarations: [
    LoginComponent,
    RegistrationComponent,
  ],
})
export class AuthModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EshopRoutingModule } from "./eshop-routing.module";
import { CoreRoutingModule } from "../core/core-routing.module";

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    EshopRoutingModule,
    CoreRoutingModule
  ]
})
export class EshopModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Header } from './layout/header/header';
import { Router, RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    Header
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    Header
  ]
})
export class CoreModule { }

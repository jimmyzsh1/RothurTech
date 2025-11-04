import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing-module';
import { Account } from './account';
import { Login } from './login/login';
import { Register } from './register/register';


@NgModule({
  declarations: [
    Account,
    Login,
    Register
  ],
  imports: [
    CommonModule,
    AccountRoutingModule
  ]
})
export class AccountModule { }

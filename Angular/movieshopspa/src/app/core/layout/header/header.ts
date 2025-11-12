import { Component } from '@angular/core';
import { Account } from '../../services/account';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  isUserLoggedIn: boolean = false;
  currentUser: any;
  constructor(private accountService: Account, private router: Router) {
    this.accountService.isLoggedIn.subscribe(resp => this.isUserLoggedIn = resp);
    this.accountService.currentUser.subscribe(resp => this.currentUser = resp);
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/account/login');
  }
}

import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable, of } from 'rxjs';
import { LoginModel } from '../../shared/models/LoginModel';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class Account {
  private currentUserSubject = new BehaviorSubject<any>(null);
  public currentUser = this.currentUserSubject.asObservable();

  private isLoggedInSubject = new BehaviorSubject<any>(false);
  public isLoggedIn = this.isLoggedInSubject.asObservable();

  private jwtHelper = new JwtHelperService();
  
  constructor(private http: HttpClient) { }

  login(login: LoginModel): Observable<boolean> {
    return this.http.post('https://localhost:7152/api/Account/login', login).pipe(map((response: any ) => {
      if (response && response.token) {
        localStorage.setItem('token', response.token);
        this.populateUserInfoFromJwtToken();
        return true;
      }
      return false;
    }), catchError((error: any) => {return of (false);}));
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSubject.next(null);
    this.isLoggedInSubject.next(false);
  }

  populateUserInfoFromJwtToken() {
    var tokenValue = localStorage.getItem('token');

    if (tokenValue && !this.jwtHelper.isTokenExpired(tokenValue)) {
      const decodedToken = this.jwtHelper.decodeToken(tokenValue);
      this.currentUserSubject.next(decodedToken);
      this.isLoggedInSubject.next(true);
    }
  }
}
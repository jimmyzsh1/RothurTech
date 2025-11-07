import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import { LoginModel } from '../../shared/models/LoginModel';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class Account {
  constructor(private http: HttpClient) { }

  login(login: LoginModel): Observable<boolean> {
    return this.http.post('http://localhost:7152/api/Account/login', login).pipe(map((response: any ) => {
      if (response && response.token) {
        localStorage.setItem('token', response.token);
        return true;
      }
      return false;
    }), catchError((error: any) => {return of (false);}));
  }

  logout() {
    localStorage.removeItem('token');
  }
}

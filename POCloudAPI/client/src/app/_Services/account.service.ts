import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { GlobalVariable } from '../global';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource = new ReplaySubject<User>(1)
  currentUser$ = this.currentUserSource.asObservable() 
  baseUrl = GlobalVariable.BASE_API_URL;
  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + "Account/login", model).pipe(
      map((response: User) => {
        const user = response
        if (user) {
          localStorage.setItem('user', JSON.stringify(user))
          this.currentUserSource.next(user)
        }
      })
    )
  }
  register(model: any) {
    return this.http.post(this.baseUrl + "Account/register", model).pipe(
      map((user: User) => {
        
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        
      })
      )
  }
  changepassword(model: any) {
    
    model.username = JSON.parse(localStorage.getItem('user')).username
    return this.http.post(this.baseUrl + "Account/changepassword", model).pipe(
      map((user: User) => {

        localStorage.setItem('user', JSON.stringify(user));
        this.currentUserSource.next(user);

      })
    )
  }
  setCurrentUser(user: User) {
    this.currentUserSource.next(user)
  }
  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null)
  }
}

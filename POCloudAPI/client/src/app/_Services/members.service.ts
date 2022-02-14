import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
  const HttpOptions = {
    headers: new HttpHeaders({
      Authorization: 'Bearer' + JSON.parse(localStorage.getItem('user')).token
    })
  }
export class MembersService {
  baseUrl = environment.apiUrl
  constructor(private http: HttpClient) { }

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'APIUsers', HttpOptions)
  }
  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + 'APIUsers/' + username, HttpOptions)
  }
}

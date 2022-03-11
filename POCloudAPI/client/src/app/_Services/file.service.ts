import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GlobalVariable } from '../global';
import { filesObj } from '../_models/filesObj';
import { fileDownload } from '../_models/fileDownload';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  baseUrl = GlobalVariable.BASE_API_URL;
  constructor(private http: HttpClient) { }
  uploadFile(file: filesObj): Observable<any> {
    return this.http.post<filesObj>(this.baseUrl + "File/uploadfile", file, {reportProgress: true, observe: 'events'}).pipe();
  }
  getFilesFromDB(model: User): Observable<any> {
    return this.http.post<User>(this.baseUrl + "File/getuserfiles",model).pipe();
  }
  downloadFile(model: fileDownload): Observable<any> {
    return this.http.post<fileDownload>(this.baseUrl + "File/downloadfile", model).pipe();
  }
}

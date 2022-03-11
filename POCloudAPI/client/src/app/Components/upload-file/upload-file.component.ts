import { HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, Subscription } from 'rxjs';
import { fileDownload } from '../../_models/fileDownload';
import { filesObj } from '../../_models/filesObj';
import { User } from '../../_models/user';
import { AccountService } from '../../_Services/account.service';
import { FileService } from '../../_Services/file.service';
@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent implements OnInit {
  model: User = {
    username: this.accountService.getUsername(), token: this.accountService.getUserToken()
  };
  fileName = '';
  FilesInDB: Array<string> = [];
  requiredFileType: string;
  theFile: filesObj = {
    fileName: "", fileAsBase64: "", fileSize: 0, token: "", username: "", ContentType: ""};
  messages: string[] = [];
  uploadProgress: number;
  uploadSub: Subscription;
  constructor(private uploadService: FileService, private dialog: MatDialog, private accountService: AccountService) { }
    async onFileSelected(event) {

    let file: File = event.target.files[0];

      if (file) {
       this.fileName = file.name;
       const formData = new FormData();
       formData.append("thumbnail", file);
        this.theFile.fileName = file.name;
        this.theFile.fileSize = file.size
        var reader = new FileReader();
        //reader.readAsBinaryString(file)
        reader.readAsDataURL(file)
        
        reader.onload = () => {
          let encoded = reader.result.toString().replace(/^data:(.*,)?/, '');
          //if ((encoded.length % 4) > 0) {
          //  encoded += '='.repeat(4 - (encoded.length % 4));
          //}
          let contentType = reader.result.toString().split(";");
          this.theFile.ContentType = contentType[0].toString();
          console.log(contentType[0]);
          console.log(encoded)
          this.theFile.fileAsBase64 = encoded
        };
        
        this.theFile.token = this.accountService.getUserToken()
        this.theFile.username = this.accountService.getUsername()
        

      const upload$ = this.uploadService.uploadFile(this.theFile).subscribe(
        event => {
          if (event.type == HttpEventType.UploadProgress) {
            this.uploadProgress = Math.round(100 * (event.loaded / event.total));
          }
        }, response => { this.reset(); } )
    

     
    }
  }
  cancelUpload() {
    this.uploadSub.unsubscribe();
    this.reset();
  }

  reset() {
    this.uploadProgress = null;
    this.uploadSub = null;
  }
  loadFiles() {
    this.uploadService.getFilesFromDB(this.model).subscribe(response => {
      var objArr = response
      for (var k in objArr) {
        this.FilesInDB.push(objArr[k].fullNameOfFile)
      }
      
      console.log(response)
    }, error => {
      console.log(error)

    })
  }
  downloadFile(str: string) {
    let modelDownload: fileDownload = {
      fileName: str, username: this.accountService.getUsername(), token: this.accountService.getUserToken()
    };
    this.uploadService.downloadFile(modelDownload).subscribe(response => {
      var objResp = response
      console.log("base64 : " + atob(objResp.fileAsBase64))
      var file = new File([atob(objResp.fileAsBase64)], objResp.fileName)
      var link = document.createElement('a');
      link.href = window.URL.createObjectURL(file);
      link.download = objResp.fileName
      link.click();
    }, error => { console.log(error) })
  }
  ngOnInit(): void {
    this.loadFiles()
  }
  
  
}


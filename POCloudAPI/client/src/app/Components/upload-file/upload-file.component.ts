import { HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, Subscription } from 'rxjs';
import { filesObj } from '../../_models/filesObj';
import { AccountService } from '../../_Services/account.service';
import { FileService } from '../../_Services/file.service';
@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent implements OnInit {
  fileName = '';
  
  requiredFileType: string;
  theFile: filesObj = {
    fileName: "", fileAsBase64: "", fileSize: 0, token: "", username: ""};
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
        //this.theFile.fileAsBase64 = file.text();
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
  ngOnInit(): void {
  }
  
  
}


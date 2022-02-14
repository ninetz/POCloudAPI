import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../../_Services/account.service';

@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrls: ['./changepassword.component.css']
})
export class ChangepasswordComponent implements OnInit {
  model: any = {}
  constructor(private accountService: AccountService, private router: Router, private dialog: MatDialog ) { }
  @ViewChild('ChangePasswordSuccessDialog') changePasswordSuccessDialog: TemplateRef<any>;
  ngOnInit(): void {
  }
  changepassword() {
    
    this.accountService.changepassword(this.model).subscribe(response => {
      setTimeout(() => { this.dialog.closeAll()}, 2500)
      this.dialog.open(this.changePasswordSuccessDialog).afterClosed().subscribe(response => {
        this.router.navigateByUrl("/")
      })
    }
    )
  }
}

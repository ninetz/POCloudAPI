  import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_Services/account.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-login-menu',
  templateUrl: './login-menu.component.html',
  styleUrls: ['./login-menu.component.css']
})
export class LoginMenuComponent implements OnInit {
  model: any = {}
  constructor(private accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }
  login() {
    this.accountService.login(this.model).subscribe(response => {
      this.router.navigateByUrl("/")
      console.log(response)
    }, error => {
      console.log(error)
      this.toastr.error(error.error)
    })
  }

}

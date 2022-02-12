  import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_Services/account.service';

@Component({
  selector: 'app-login-menu',
  templateUrl: './login-menu.component.html',
  styleUrls: ['./login-menu.component.css']
})
export class LoginMenuComponent implements OnInit {
  model: any = {}
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }
  login() {
    this.accountService.login(this.model).subscribe(response => {
      console.log(response)
      this.accountService.loggedIn = true
    }, error => { console.log(error) })
  }
  logout() {
    this.accountService.loggedIn = false
  }
}

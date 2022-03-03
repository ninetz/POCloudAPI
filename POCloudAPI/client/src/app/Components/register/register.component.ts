import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../_Services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {}
  constructor(private accountService: AccountService, private toastr: ToastrService, private router: Router) { }
  users: any;
  validationErrors: any = {};
  ngOnInit(): void {
  }
  register() {
    this.accountService.register(this.model).subscribe(response =>
    {
      this.router.navigateByUrl("/")
      console.log(response)
    }, error => {
      console.log(error)
      this.validationErrors = error;
      
    })
  }
  cancel() { console.log('cancelled') }
  getUsers() {}
}

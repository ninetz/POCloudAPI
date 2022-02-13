import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_Services/account.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {}
  constructor(private accountService: AccountService, private toastr: ToastrService) { }
  users: any;
  ngOnInit(): void {
  }
  register() {
    this.accountService.register(this.model).subscribe(response =>
    {
      console.log(response)
    }, error => {
      console.log(error)
      this.toastr.error(error.error)
    })
  }
  cancel() { console.log('cancelled') }
  getUsers() {}
}
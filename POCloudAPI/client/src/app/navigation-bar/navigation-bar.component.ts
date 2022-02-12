import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AccountService } from '../_Services/account.service';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent implements OnInit {
  
  constructor(public accountService: AccountService,private router: Router) { }

  ngOnInit(): void {
    
  }
  logout() {
    this.accountService.logout()
    this.router.navigateByUrl('/')
  }
  
}

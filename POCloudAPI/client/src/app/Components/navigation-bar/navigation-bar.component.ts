import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AccountService } from '../../_Services/account.service';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent implements OnInit {
  
  constructor(public accountService: AccountService, private router: Router
  ) { }
  hideDevComponents: boolean = false;
  ngOnInit(): void {
    if (environment.production) {
      this.hideDevComponents = true;
    }
  }
  logout() {
    this.router.navigateByUrl('/')
    this.accountService.logout()
    
  }
  
}

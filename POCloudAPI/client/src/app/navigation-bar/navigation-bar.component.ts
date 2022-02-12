import { Component, OnInit } from '@angular/core';

import { AccountService } from '../_Services/account.service';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent implements OnInit {
  
  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    
  }

  
}

import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_Services/account.service';
import { ToastrService } from 'ngx-toastr';
@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService, private toastr: ToastrService, private router: Router) { }
  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map(user => {
        if (user) {
          if (this.accountService.verifyUser(user).subscribe(response => {
            console.log(response);
            return true;
          }, error => {
            window.location.reload();
            this.router.navigateByUrl("/login")
            console.log(error)
            this.accountService.logout();
            return false;
          })) {
            return true;
          }
        }
        this.toastr.error("You do not have sufficient priviliges to access this component.")
      })
    )
  }
}
  
  

/*canActivate(): Observable < boolean > {
  return this.accountService.currentUser$.pipe(
    map(user => {
      if (user) {
        if (this.accountService.verifyUser().subscribe()) {
          return true
        }
      }
      this.toastr.error("You do not have sufficient priviliges to access this component.")
    })
  )
}
  
}*/

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginMenuComponent } from './login-menu/login-menu.component';
import { RegisterComponent } from './register/register.component';
import { UploadFileComponent } from './upload-file/upload-file.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'uploadfile', component: UploadFileComponent, canActivate: [AuthGuard] },
    ]
  },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginMenuComponent },

  { path: '**', component: HomeComponent, pathMatch: 'full' }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

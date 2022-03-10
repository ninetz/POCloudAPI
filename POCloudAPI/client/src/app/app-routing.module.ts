import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ServerErrorComponent } from './Components/errors/server-error/server-error.component';
import { HomeComponent } from './Components/home/home.component';
import { MyCertificationsComponent } from './Components/Info-Tabs/my-certifications/my-certifications.component';
import { MyCVComponent } from './Components/Info-Tabs/my-cv/my-cv.component';
import { MyProjectsComponent } from './Components/Info-Tabs/my-projects/my-projects.component';
import { ChangepasswordComponent } from './Components/login-menu/changepassword/changepassword.component';
import { LoginMenuComponent } from './Components/login-menu/login-menu.component';
import { NotFoundComponent } from './Components/not-found/not-found.component';
import { RegisterComponent } from './Components/register/register.component';
import { UploadFileComponent } from './Components/upload-file/upload-file.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'uploadfile', component: UploadFileComponent, canActivate: [AuthGuard] },
      { path: 'changepassword', component: ChangepasswordComponent, canActivate: [AuthGuard] },
    ]
  },
  { path: 'projects', component: MyProjectsComponent },
  { path: 'certificates', component: MyCertificationsComponent },
  { path: 'CV', component: MyCVComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginMenuComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },


  { path: '**', component: NotFoundComponent, pathMatch: 'full' }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

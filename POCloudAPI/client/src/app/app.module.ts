import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavigationBarComponent } from './Components/navigation-bar/navigation-bar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { LoginMenuComponent } from './Components/login-menu/login-menu.component';
import { HomeComponent } from './Components/home/home.component';
import { RegisterComponent } from './Components/register/register.component';
import { UploadFileComponent } from './Components/upload-file/upload-file.component'
import { SharedModule } from './shared.module';
import { TestErrorsComponent } from './Components/errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './Components/not-found/not-found.component';
import { ServerErrorComponent } from './Components/errors/server-error/server-error.component';
import { ChangepasswordComponent } from './Components/login-menu/changepassword/changepassword.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { MyCertificationsComponent } from './Components/Info-Tabs/my-certifications/my-certifications.component';
import { MyCVComponent } from './Components/Info-Tabs/my-cv/my-cv.component';
import { TermsAndConditionsComponent } from './Components/Info-Tabs/terms-and-conditions/terms-and-conditions.component';


@NgModule({
  declarations: [
    AppComponent,
    NavigationBarComponent,
    LoginMenuComponent,
    HomeComponent,
    RegisterComponent,
    UploadFileComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    ChangepasswordComponent,
    MyCertificationsComponent,
    MyCVComponent,
    TermsAndConditionsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgbModule,
    FormsModule,
    SharedModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

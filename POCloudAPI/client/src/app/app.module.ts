import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { LoginMenuComponent } from './login-menu/login-menu.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ToastrModule } from 'ngx-toastr';
import { UploadFileComponent } from './upload-file/upload-file.component'


@NgModule({
  declarations: [
    AppComponent,
    NavigationBarComponent,
    LoginMenuComponent,
    HomeComponent,
    RegisterComponent,
    UploadFileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgbModule,
    FormsModule,
    ToastrModule.forRoot({positionClass: "toast-middle-right"})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

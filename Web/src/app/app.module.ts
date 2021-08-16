import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ErrorInterceptor, JwtInterceptor } from './_helpers';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';
import { LeadComponent } from './lead/lead.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UserComponent,
    LoginComponent,
    LeadComponent
  ],
  imports: [    
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

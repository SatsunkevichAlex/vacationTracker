import { Component } from '@angular/core';
import { User, Role } from './_models';

import { AuthenticationService } from './_services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  user!: User;

  constructor(private authenticationService: AuthenticationService) {
    this.authenticationService.user.subscribe(x => this.user = x);
  }

  get isRegular() {
    return this.user && this.user.role === Role.User;
  }

  get isLead() {
    return this.user && this.user.role === Role.Lead;
  }

  get isAdmin() {
    var isAdmin = this.user && this.user.role === Role.Admin;
    var x = 2;
    return isAdmin 
  }

  logout() {
    this.authenticationService.logout();
  }
}

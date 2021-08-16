import { Component } from "@angular/core";
import { first } from 'rxjs/operators';

import { User } from '@app/_models';
import { UserService, AuthenticationService } from '@app/_services';

@Component({
    selector: 'user-component',
    templateUrl: 'user.component.html',
    styleUrls: ['user.component.css']
})
export class UserComponent {
    user: User;
    userFromApi!: User;

    constructor(
        private userService: UserService,
        private authenticataionService: AuthenticationService
    ) {
        this.user = this.authenticataionService.userValue;
    }

    ngOnInit() {
        this.userService.getById(this.user.id).pipe(first()).subscribe(user => {
            this.userFromApi = user;
        })
    }
}


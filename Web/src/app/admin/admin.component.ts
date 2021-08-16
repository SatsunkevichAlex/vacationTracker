import { Component, OnInit } from "@angular/core";
import { first } from "rxjs/operators";

import { User } from '@app/_models';
import { UserService } from "@app/_services";

@Component({ templateUrl: 'admin.component.html' })
export class AdminComponent implements OnInit {
    users: User[] = [];

    constructor(private userServcie: UserService) { }

    ngOnInit() {        
    }
}

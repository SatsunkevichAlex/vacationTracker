import { Role } from './role';

export class User {
    id!: number;
    firstname!: string;
    lastName!: string;
    email!: string;
    role!: Role;
    token?: string;
}
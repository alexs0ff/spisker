import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import {AuthentificateService} from "../AuthentificateService";

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthentificateService) {

    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.authService.isAuthorized;
    }
}
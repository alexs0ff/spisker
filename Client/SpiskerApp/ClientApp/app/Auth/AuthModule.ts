import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SignInComponent } from "./SignInComponent/SignInComponent";
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import {AuthentificateService} from "./AuthentificateService";
import { AuthResult } from "./AuthResult";
import {AuthEventType} from "./AuthFlow/AuthEventType";
import {AuthEvent} from "./AuthFlow/AuthEvent";
import { IfLoggedUserDirective} from "./Directives/IfLoggedUserDirective";
import {IfOwnedUserDirective} from "./Directives/IfOwnedUserDirective";
import { AuthGuard } from "./Guards/AuthGuard";
import {IfNotOwnedUserDirective} from "./Directives/IfNotOwnedUserDirective";

@NgModule({
    imports: [CommonModule, FormsModule, RouterModule, ReactiveFormsModule],
    declarations: [SignInComponent, IfLoggedUserDirective, IfOwnedUserDirective, IfNotOwnedUserDirective],
    exports: [SignInComponent, IfLoggedUserDirective, IfOwnedUserDirective, IfNotOwnedUserDirective]
})
class AuthModule { }

export {
    AuthModule,

    AuthentificateService,

    AuthResult,

    AuthEventType,

    AuthEvent,

    IfLoggedUserDirective,

    AuthGuard
}
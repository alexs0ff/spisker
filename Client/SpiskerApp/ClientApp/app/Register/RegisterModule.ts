import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import {RegisterComponent} from "./RegisterComponent/RegisterComponent";
import {RegistrationService} from "./RegistrationService";
import { RecoverComponent } from "./RecoverComponent/RecoverComponent";
import { ReCaptchaModule } from 'angular2-recaptcha';
import {UtilsModule} from "../Utils/UtilsModule";
import {ApproveRecoverComponent} from "./ApproveRecoverComponent/ApproveRecoverComponent";

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        ReCaptchaModule,
        UtilsModule

    ],
    declarations: [
        RegisterComponent,
        RecoverComponent,
        ApproveRecoverComponent
    ],
    providers: [RegistrationService]
})
class RegisterModule { }

export {
    RegisterModule,
    RegisterComponent,
    RecoverComponent,
    ApproveRecoverComponent
    }
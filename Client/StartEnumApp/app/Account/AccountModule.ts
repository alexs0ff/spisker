import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import {AccountRoutingModule} from "./AccountRoutingModule";
import {AccountComponent} from "./AccountComponent";
import {SettingsComponent} from "./Settings/SettingsComponent/SettingsComponent";
import {UserSettingsComponent} from "./Settings/UserSettingsComponent/UserSettingsComponent";
import {ChangePasswordComponent} from "./Settings/ChangePasswordComponent/ChangePasswordComponent";
import {ChangeStatusTextComponent} from "./Settings/ChangeStatusTextComponent/ChangeStatusTextComponent";
import {ChangeAvatarComponent} from "./Settings/ChangeAvatarComponent/ChangeAvatarComponent";
import {UtilsModule} from "../Utils/UtilsModule";
import {SocialModule} from "../Social/SocialModule";

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        AccountRoutingModule,
        SocialModule,
        UtilsModule


    ],
    declarations: [
        AccountComponent,
        SettingsComponent,
        UserSettingsComponent,
        ChangePasswordComponent,
        ChangeStatusTextComponent,
        ChangeAvatarComponent
    ]
})
export class AccountModule { }
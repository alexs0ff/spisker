import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountService } from "./AccountService";
import { AccountEvent } from "./AccountEvent";
import { AccountSettingsModel } from "./AccountSettingsModel";
import { AccountEventType } from "./AccountEventType";

@NgModule({
    imports: [CommonModule]
})
class AccountServiceModule { }

export {
    AccountServiceModule,
    AccountService,
    AccountEvent,
    AccountEventType,
    AccountSettingsModel
    }
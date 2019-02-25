import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SettingsService} from "./SettingsService";
import { SystemName } from "./SystemName";
import {Messages} from "./Messages";

@NgModule({
    imports: [
        CommonModule

    ],
    providers: [SettingsService]
})
class SettingsModule { }

export {
    SettingsModule,
    SettingsService,
    SystemName,
    Messages
    }
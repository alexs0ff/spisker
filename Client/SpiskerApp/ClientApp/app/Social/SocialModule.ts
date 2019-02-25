import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SocialService } from "./SocialService";
import { SocialEvent } from "./SocialEvent";
import { SocialEventType } from "./SocialEventType";
import {ProfileComponent} from "./ProfileComponent/ProfileComponent";
import { UtilsModule } from "../Utils/UtilsModule";
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [CommonModule, UtilsModule, RouterModule],
    declarations: [ProfileComponent],
    exports: [ProfileComponent]
})
class SocialModule { }

export {
    SocialModule,
    SocialService,
    SocialEvent,
    SocialEventType
}
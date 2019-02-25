import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import {WhoFollowComponent} from "./Cards/WhoFollowComponent/WhoFollowComponent";
import {UtilsModule} from "../Utils/UtilsModule";
import { StartenumComponent } from "./StartenumComponent/StartenumComponent";
import {HomeComponent} from "./HomeComponent/HomeComponent";
import {FeedModule} from "../Lists/FeedModule";
import {SocialModule} from "../Social/SocialModule";
import {FollowersComponent} from "./FollowersComponent/FollowersComponent";
import { FollowingsComponent } from "./FollowingsComponent/FollowingsComponent";
import {ProfileItemComponent} from "./ProfileItemComponent/ProfileItemComponent";

@NgModule({
    imports: [
        CommonModule, FeedModule, SocialModule, UtilsModule, RouterModule, ReactiveFormsModule, FormsModule
    ],
    declarations: [
        StartenumComponent, WhoFollowComponent, HomeComponent, FollowersComponent, ProfileItemComponent, FollowingsComponent
    ],
    exports: [StartenumComponent]
})
class HomeModule { }

export {
    HomeModule,
    StartenumComponent,
    HomeComponent,
    FollowersComponent,
    FollowingsComponent
    }
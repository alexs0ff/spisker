import { Component, OnInit, OnDestroy,Input } from '@angular/core';
import {SubscriptionsContainer} from "../../Utils/UtilsModule";
import {UserProfileModel} from "../../Social/Model/UserProfileModel";
import {SettingsService} from "../../Settings/SettingsModule";

@Component({
    selector: '[profile-item]',
    template: `

    <div class="row">
        <div class="col-md-4">
            <img [src]="profile.avatarUrl" class="profile-item-avatar" [empty-image]="'avatar'"/>
        </div>
        <div class="col-md-6">
            <div>
                <div class="row"> <h4 class="profile-item-user" [user-link]="profile.userName"><small>@{{profile.userName}}</small></h4> <a routerLink="/{{profile.userName}}"> {{getFio()}}</a></div>                
            </div>
        </div>
        <div class="col-md-2"></div>
    </div>
`


})
export class ProfileItemComponent implements OnInit, OnDestroy {

    @Input()
    profile: UserProfileModel;

    private subscriptionsContainer: SubscriptionsContainer = new SubscriptionsContainer();

    constructor(private settingsService: SettingsService) {

    }

    ngOnInit(): void {
        
    }

    ngOnDestroy(): void {
        this.subscriptionsContainer.clear();
    }

    getFio(): string {
        return this.settingsService.getFullName(this.profile);
    }
} 

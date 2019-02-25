import { Component, OnInit, OnDestroy } from '@angular/core';
import {AuthentificateService} from "../../../Auth/AuthModule";
import {SocialService, SocialEvent, SocialEventType } from "../../../Social/SocialModule";
import {UserProfileModel} from "../../../Social/Model/UserProfileModel";
import {StringExtentions} from "../../../Utils/Extentions/StringExtentions";
import {SubscriptionsContainer} from "../../../Utils/UtilsModule";
import {SystemName, SettingsService } from "../../../Settings/SettingsModule";
import * as jQuery from 'jquery';

@Component({
    selector: 'who-follow',
    template: `  
<div id="who-follow" class="card">
     <div class="card-header">Подписчики</div>
     <div class="card-block">
         <ul class="list-unstyled">
             <li *ngFor="let item of profiles">
                 <img src="{{item.avatarUrl}}" class="img-rounded" [empty-image]="'avatar'"/>
                 <div class="info">
                     <strong [user-link]="item.userName">@{{item.userName}}</strong>
                     <a *ngIf="showSubscribeButton" href="" class="btn btn-default" role="button" tabindex="-1" (click)="toggleFollowing($event,item)" title="Вы можете подписаться">
                         <span *ngIf="!item.isFollowing" class="glyphicon glyphicon-plus"> Подписаться</span>
                         <span *ngIf="item.isFollowing" class="glyphicon glyphicon-eye-close"> Отписаться</span>
                     </a>
                 </div>
             </li>             
             <li *ngIf="!profiles || profiles.length === 0">                 
                 <em>Нет подписчиков</em>                 
             </li>       
         </ul>
     </div>
 </div>
    
  `
})
export class WhoFollowComponent implements OnInit, OnDestroy {

    constructor(private socialService: SocialService, private authService: AuthentificateService, private settingsService: SettingsService) {
        this.profiles = new Array<UserProfileModel>();
    }

    profiles: Array<UserProfileModel>;

    private showSubscribeButton: boolean;

    private subscriptionsContainer: SubscriptionsContainer = new SubscriptionsContainer();

    ngOnInit(): void {
        this.showSubscribeButton = false;
        let subj:any = this.socialService.socialFeed.filter((e: SocialEvent) => e.eventType === SocialEventType.FollowersFetched && e.systemId === SystemName.whoFollowSystem)
            .subscribe((e: SocialEvent) => {

                if (e.data.profiles.length>this.settingsService.getMaxWhoFollowCount()) {
                    this.profiles = e.data.profiles.slice(0, this.settingsService.getMaxWhoFollowCount());
                } else {
                    this.profiles = e.data.profiles;    
                }

                this.processSubscription(e.data.forUser);
            });
        this.subscriptionsContainer.add(subj);
    }

    ngOnDestroy(): void {
        this.subscriptionsContainer.clear();
    }

    private processSubscription(forUser: string) {
        this.showSubscribeButton = false;
        if (StringExtentions.isNullOrWhiteSpace(forUser)) {
            return;
        }
        if (this.authService.isAuthorized) {
            this.showSubscribeButton = StringExtentions.compareOrdinalIgnoreCase(forUser, this.authService.userName);
        }
    }

    toggleFollowing(event: Event, item: UserProfileModel) {
        event.preventDefault();

        if (item.isFollowing) {
            if (this.authService.isAuthorized) {
                this.socialService.stopFollowing(this.authService.userName, item.userName);
            }
            
        } else {
            if (this.authService.isAuthorized) {
                this.socialService.startFollowing(this.authService.userName, item.userName);
            }
        }

        item.isFollowing = !item.isFollowing;
    }
}
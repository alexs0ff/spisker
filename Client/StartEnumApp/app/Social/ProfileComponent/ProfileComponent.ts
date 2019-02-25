import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import {SocialService} from "../SocialService";
import { SocialEvent } from "../SocialEvent";
import { SocialEventType } from "../SocialEventType";
import {SubscribeButtonState} from "./SubscribeButtonState";
import {AccountService, AccountEvent, AccountEventType } from "../../Account/AccountService/AccountServiceModule";
import {AuthentificateService} from "../../Auth/AuthModule";
import {StringExtentions} from "../../Utils/Extentions/StringExtentions";
import {UserProfileModel} from "../Model/UserProfileModel";
import {SubscriptionsContainer} from "../../Utils/UtilsModule";
import {SettingsService} from "../../Settings/SettingsModule";

@Component({
    selector: 'user-profile',
    template: `
<div class="card profile-resumme">
     <img class="card-img-top img-responsive" src="assets/imgs/landscape.jpg"/>
     <div class="card-block">
         <img [src]="avatarUrl" class="card-img" [empty-image]="'avatar'"/>
         <h4 class="card-title">{{fio}} <small>@{{userName}}</small></h4>
         <p class="card-text">{{statusText}}</p>
         <ul class="list-inline list-unstyled">
             <li id="card-tweets">
                 <a routerLink="/{{userName}}">
                     <span class="profile-stats">Списков</span>
                     <span class="profile-value">{{listCount}}</span>
                 </a>
             </li>
                      
             <li id="card-following">
                 <a routerLink="{{settingsService.getFollowingsUrl(userName)}}">
                     <span class="profile-stats">Подписан</span>
                     <span class="profile-value">{{following}}</span>
                 </a>
             </li>
             <li id="card-followers">
                 <a routerLink="{{settingsService.getFollowersUrl(userName)}}">
                     <span class="profile-stats">Подписчиков</span>
                     <span class="profile-value">{{followers}}</span>
                 </a>
             </li>
         </ul>
        
        <div [ngSwitch]="subscribeButtonView">
                 <a *ngSwitchCase="subscribeButtonState.Subscribe" href="" class="btn btn-default subscribe-btn" role="button" tabindex="-1" (click)="startFollow($event)">
                     <span class="glyphicon glyphicon-plus"> Подписаться</span>
                 </a>
            <a *ngSwitchCase="subscribeButtonState.Unsubscribe" href="" class="btn btn-default subscribe-btn" role="button" tabindex="-1" (click)="stopFollow($event)">
                <span class="glyphicon glyphicon-eye-close"> Отписаться</span>
            </a>
        </div>
     </div>
 </div>
`


})
export class ProfileComponent implements OnInit, OnDestroy {

    statusText: string;

    userName: string;

    fio: string;

    listCount: number;

    followers: number;

    following: number;

    avatarUrl: string;

    subscribeButtonView: SubscribeButtonState;

    subscribeButtonState = SubscribeButtonState;

    private subscriptionsContainer: SubscriptionsContainer = new SubscriptionsContainer();

    constructor(private socialService: SocialService, private authService: AuthentificateService, private accountService: AccountService, private router: Router, public settingsService: SettingsService) {
        
    }

    ngOnInit(): void {
        let subscription:any = this.socialService.socialFeed.filter((e: SocialEvent) => e.eventType === SocialEventType.ProfileFetched).map((e: SocialEvent) => e.data).subscribe(data => {
            this.setProfile(data);
        });

        this.subscriptionsContainer.add(subscription);

        subscription = this.socialService.socialFeed.filter((e: SocialEvent) => e.eventType === SocialEventType.UserStartedFollow).subscribe((data: SocialEvent) => {
            this.userStartedFollow(data.targetUserName, data.data);
        });

        this.subscriptionsContainer.add(subscription);

        subscription = this.socialService.socialFeed.filter((e: SocialEvent) => e.eventType === SocialEventType.UserStoppedFollow).subscribe((data: SocialEvent) => {
            this.userStoppedFollow(data.targetUserName, data.data);
        });
        this.subscriptionsContainer.add(subscription);

        subscription = this.accountService.accountFeed.filter((e: AccountEvent) => e.eventType === AccountEventType.StatusTextChanged && StringExtentions.compareOrdinalIgnoreCase(e.targetUserName, this.userName)).subscribe((data: AccountEvent) => {
            this.statusText = data.data;
        });
        this.subscriptionsContainer.add(subscription);

        subscription = this.accountService.accountFeed.filter((e: AccountEvent) => e.eventType === AccountEventType.AvatarChanged && StringExtentions.compareOrdinalIgnoreCase(e.targetUserName, this.userName)).subscribe((data: AccountEvent) => {
            this.avatarUrl = data.data;
        });
        this.subscriptionsContainer.add(subscription);
    }

    ngOnDestroy(): void {
        this.subscriptionsContainer.clear();
    }

    startFollow($event: Event) {
        $event.preventDefault();
        if (this.authService.isAuthorized) {
            this.socialService.startFollowing(this.authService.userName,this.userName);
        }
    }

    stopFollow($event: Event) {
        $event.preventDefault();
        if (this.authService.isAuthorized) {
            this.socialService.stopFollowing(this.authService.userName, this.userName);
        }
    }

    private setProfile(profile:UserProfileModel) {
        this.statusText = profile.statusText;
        this.userName = profile.userName;

       
        this.fio = this.settingsService.getFullName(profile);

        this.listCount = profile.listCount;
        this.followers = profile.followerCount;
        this.following = profile.followingCount;
        this.avatarUrl = profile.avatarUrl;

        if (this.authService.isAuthorized) {
            if (profile.isFollowing) {
                this.subscribeButtonView = SubscribeButtonState.Unsubscribe;
            } else {
                if (StringExtentions.compareOrdinalIgnoreCase(this.authService.userName, profile.userName)) {
                    this.subscribeButtonView = SubscribeButtonState.None;
                } else {
                    this.subscribeButtonView = SubscribeButtonState.Subscribe;
                }
            }
        } else {
            this.subscribeButtonView = SubscribeButtonState.None;
        }
    }

    private userStartedFollow(userName: string, toUser: string) {
        if (StringExtentions.compareOrdinalIgnoreCase(this.userName, toUser)
            && StringExtentions.compareOrdinalIgnoreCase(this.authService.userName, userName)
        ) {
            this.subscribeButtonView = SubscribeButtonState.Unsubscribe;
        }
    }

    private userStoppedFollow(userName: string, toUser: string) {
        if (StringExtentions.compareOrdinalIgnoreCase(this.userName, toUser)
            && StringExtentions.compareOrdinalIgnoreCase(this.authService.userName, userName)
        ) {
            this.subscribeButtonView = SubscribeButtonState.Subscribe;
        }
    }

    goToFollowers(event: Event) {
        event.preventDefault();
        let url: string = this.settingsService.getFollowersUrl(this.userName);
        this.router.navigateByUrl(url);
    }
} 

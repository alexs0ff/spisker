import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthentificateService } from "../../Auth/AuthModule";
import { FeedListService } from "../../Lists/FeedModule";
import { SocialService, SocialEvent, SocialEventType } from "../../Social/SocialModule";
import { StringExtentions } from "../../Utils/Extentions/StringExtentions";
import { SystemName, SettingsService } from "../../Settings/SettingsModule";
import {SubscriptionsContainer, QueryState } from "../../Utils/UtilsModule";
import { UserProfileModel } from "../../Social/Model/UserProfileModel";
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
    template: `
<div class="container">
    <div class="row">
        <div id="profile" class="col-md-3 hidden-sm hidden-xs">
            <user-profile></user-profile>           
        </div>
        
        <div class="col-sm-12 col-md-6"> 
            <div class="row hidden-md hidden-lg">                        
                <user-profile></user-profile>
            </div>
            <div class="row">                        
                <div class="card">
                    <form class="new-message" [formGroup]="searchForm"  novalidate>
                        <div class="input-group">
                            <input type="text"  formControlName="userName" class="form-control" placeholder="Поиск подписчиков" (keyup.enter)="searchTextChanged()" (click)="find($event)"/>
                            <span class="input-group-addon">                
                                <a href="" data-toggle="tooltip" data-placement="bottom"  title="Найти" >
                                    <span class="glyphicon glyphicon glyphicon-filter" aria-hidden="true" >
                                    </span>
                                </a>                                              
                            </span>

                        </div>
                        
                    </form>
                    <ul class="profile-list list-unstyled">
                        <li profile-item *ngFor="let profile of profiles" [profile]="profile" class="profile-item"></li> 
                        <li *ngIf="hasMoreRows" class="startenum-list-button" (click)="loadNext()">Показать еще…</li>
                    </ul>
                </div>
            </div>
        </div>

        
        <div id="right-content" class="col-md-3 hidden-xs hidden-sm">            
            <div id="app-info" class="card">
                <div class="card-block">
                    (c) Кулик Александр

                    <ul class="list-unstyled list-inline">
                        <li><a href="">Главная</a></li>
                        <li><a href="#">Условия использования</a></li>                        
                    </ul>
                </div>
                <div class="card-footer">
                    <a href="mailto:alexsoff@yandex.ru">Отзывы и предложения</a>
                </div>
            </div>
        </div>
    </div>
</div>
    
`


})
export class FollowersComponent implements OnInit, OnDestroy {

    searchForm: FormGroup;

    listsIsEmpty: boolean;

    hasMoreRows:boolean;

    constructor(private builder: FormBuilder,private route: ActivatedRoute, private authService: AuthentificateService, private socialService: SocialService, private router: Router, private settingsService: SettingsService) {
        this.profiles = new Array<UserProfileModel>();
        this.searchForm = builder.group({
            "userName": [""]
        });
    }

    private subscriptionsContainer: SubscriptionsContainer = new SubscriptionsContainer();

    private sub: any;

    profiles: Array<UserProfileModel>;

    private userName: string;

    private lastFollowerId: string;

    private queryState: QueryState = new QueryState();

    ngOnInit(): void {
        this.queryState.isSent = false;

        let sub: any = this.route.params.subscribe(params => {
            this.profiles.length = 0;
            
            this.userName = params["userName"];
            this.socialService.getProfile(this.userName, this.authService.userName);
            this.socialService.getFollowers(SystemName.followersComponent, this.userName, null, null);
            this.queryState.isSent = true;
        });

        this.subscriptionsContainer.add(sub);

        sub = this.socialService.socialFeed.filter((e: SocialEvent) => e.eventType === SocialEventType.FollowersFetched && e.systemId === SystemName.followersComponent)
            .subscribe((e: SocialEvent) => {
                for (var i = 0; i < e.data.profiles.length ; i++) {
                    this.profiles.push(e.data.profiles[i]);
                }
                
                this.lastFollowerId = e.data.lastFollowerId;
                this.hasMoreRows = (e.data.profiles.length !== 0 && e.data.profiles.length === this.settingsService.getMaxFollowersCount());
                this.queryState.isSent = false;
            });
        this.subscriptionsContainer.add(sub);

        sub = this.searchForm.valueChanges.subscribe(data =>
            this.searchTextChanged());

        this.subscriptionsContainer.add(sub);
        
    }

    ngOnDestroy(): void {
        this.subscriptionsContainer.clear();
    }

    private getSearchText(): string {
        return this.searchForm.controls["userName"].value;
    }

    loadNext() {
        this.socialService.getFollowers(SystemName.followersComponent, this.userName, this.getSearchText(), this.lastFollowerId);
        this.queryState.isSent = true;
    }

    searchTextChanged() {
        this.lastFollowerId = null;
        this.profiles.length = 0;

        this.loadNext();
    }

    find(event: Event) {
        event.preventDefault();
        this.searchTextChanged();
    }

} 

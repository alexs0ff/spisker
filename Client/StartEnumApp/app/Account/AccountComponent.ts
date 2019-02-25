import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {AuthentificateService} from "../Auth/AuthModule";
import {SocialService} from "../Social/SocialModule";
import {AccountService} from "./AccountService/AccountService";
import {AccountEvent} from "./AccountService/AccountEvent";
import {AccountEventType} from "./AccountService/AccountEventType";
import {StringExtentions} from "../Utils/Extentions/StringExtentions";

@Component({
    template: `  
<div class="container">
    <div class="row">
        <div id="profile" class="col-md-3 hidden-sm hidden-xs">
            <user-profile></user-profile>                             
          
        </div>
        
        <div id="main" class="col-sm-12 col-md-6">
            <router-outlet></router-outlet>
        </div>
        
        <div id="right-content" class="col-md-3 hidden-xs hidden-sm">
            <ul class="list-group">
                <li class="list-group-item list-group-item-info">
                    Статистика
                </li>
                <li class="list-group-item">
                    Лайков всего
                    <span class="label label-success">6</span>
                </li>
                <li class="list-group-item">
                    Лайков поставлено
                    <span class="label label-danger">87</span>
                </li>
                <li class="list-group-item">
                    Репостов моих списков
                    <span class="label label-default">2</span>
                </li>                
            </ul>
        </div>
    </div>
</div>

    
  `
})
export class AccountComponent implements OnInit, OnDestroy {
    constructor(private route: ActivatedRoute,
        private authService: AuthentificateService,
        private socialService: SocialService,
        private accountService:AccountService) {
        
    }

    private routeChanged: any;

    private accountUpdatedSubject: any;

    ngOnInit(): void {
        this.routeChanged = this.route.params.subscribe(params => {
            this.socialService.getProfile(this.authService.userName);
            this.accountService.getAccountSettings(this.authService.userName);
        });

        this.accountUpdatedSubject = this.accountService.accountFeed.filter((e: AccountEvent) => e.eventType === AccountEventType.AccountSettingsUpdated)
            .subscribe((e: AccountEvent) => {
                if (StringExtentions.compareOrdinalIgnoreCase(e.targetUserName, this.authService.userName)) {
                    this.socialService.getProfile(this.authService.userName);
                }
            });

    }

    ngOnDestroy(): void {
        this.routeChanged.unsubscribe();
        this.accountUpdatedSubject.unsubscribe();
    }
}
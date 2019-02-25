import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {AuthentificateService} from "../../Auth/AuthModule";
import {FeedListService} from "../../Lists/FeedModule";
import {SocialService} from "../../Social/SocialModule";
import {StringExtentions} from "../../Utils/Extentions/StringExtentions";
import {SystemName} from "../../Settings/SettingsModule";

@Component({
    selector: 'startenum-page',
    template: `
<div class="container">
    <div class="row">
        <div id="profile" class="col-md-3 hidden-sm hidden-xs">
            <user-profile></user-profile>
            
            <div id="profile-photo" class="card">
                <div class="card-header">Песочница</div>
                <div class="card-block">
                    <em>В разработке…</em>
                </div>
            </div>
        </div>
        
        <div id="main" class="col-sm-12 col-md-6">
            <div class="row hidden-md hidden-lg">                        
                <user-profile></user-profile>
            </div>
            <div class="row">                        
                <feed-card [showAddListInput]="userCanAddToFeed"></feed-card>
            </div>
        </div>

        
        <div id="right-content" class="col-md-3 hidden-xs hidden-sm">
            <who-follow></who-follow>
            <div id="app-info" class="card">
                <div class="card-block">
                    (c) {{currentYear}} Кулик Александр

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
export class StartenumComponent implements OnInit, OnDestroy {

    userCanAddToFeed: boolean;

    currentYear:number;

    constructor(private route: ActivatedRoute, private feedService: FeedListService, private authService: AuthentificateService, private socialService: SocialService, private router: Router) {
        
    }

    private sub: any;

    ngOnInit(): void {

        this.currentYear = (new Date()).getFullYear();
        
        this.sub = this.route.params.subscribe(params => {
            let userFeed: boolean = false;

            let userName = params["userName"];
            let listPublicId = params["listId"];
            if (StringExtentions.isNullOrWhiteSpace(userName)) {
                userName = this.authService.userName;
            }
            

            if (StringExtentions.compareOrdinalIgnoreCase(userName,"feed")) {
                userName = this.authService.userName;
                userFeed = true;
            }

            if (StringExtentions.isNullOrWhiteSpace(userName)) {
                this.router.navigateByUrl('');
            }

            if (userFeed) {
                this.feedService.startUserFeed();
                this.userCanAddToFeed = false;
            } else {
                this.feedService.startFetchUserList(userName, listPublicId);
                this.userCanAddToFeed = StringExtentions.compareOrdinalIgnoreCase(userName, this.authService.userName);
            }
            
            this.socialService.getProfile(userName, this.authService.userName);
            this.socialService.getFollowers(SystemName.whoFollowSystem, userName,null,null);
        });
    }

    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }
} 

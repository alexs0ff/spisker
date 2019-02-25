import { Component, OnInit, OnDestroy, HostListener, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { AuthentificateService, AuthEvent, AuthEventType} from "../Auth/AuthModule";
import {SocialService, SocialEvent, SocialEventType } from "../Social/SocialModule";
import {StringExtentions} from "../Utils/Extentions/StringExtentions";
import {ScreenDetector, WindowSize, SubscriptionsContainer } from "../Utils/UtilsModule";
import {RuLocale} from "../Utils/Moment/RuLocale";

@Component({
    selector: 'startenum-app',
    template: `

<nav class="navbar navbar-default navbar-fixed-top">
    <div class="navbar-header">
     <a class="navbar-brand" href="#"><img src="imgs/logo.png" class="img-responsive" /></a>
        
     <button #navbarToggler type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#nav-menu" aria-expanded="False">
            <span class="sr-only">Навигация</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
        </button>               
        <div id="smSearchPlace" class="pull-right">            
        </div> 
    </div>
   
    <div  class="collapse navbar-collapse" id="nav-menu">
        <ul class="nav navbar-nav">
            <li routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">
                <a (click)="collapseNav()" routerLink="/">
                    <span class="glyphicon glyphicon-home" aria-hidden="true"></span>
                    Главная
                </a>
            </li>
            <li *ngIf="userIsLoggedIn" routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">
                <a (click)="collapseNav()" href="#" routerLink="/feed">                    
                    <span class="glyphicon glyphicon-road" aria-hidden="true"></span>
                    Лента
                </a>
            </li>
            <li *ngIf="userIsLoggedIn" routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">
                <a (click)="collapseNav()" href="#" routerLink="{{userName}}">                    
                    <span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>
                    Мои списки
                </a>
            </li>
            <li *ngIf="userIsLoggedIn" routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">
                <a (click)="collapseNav()" href="#" routerLink="/i/account/settings">
                    <span class="glyphicon glyphicon-cog" aria-hidden="true"></span>
                    Настройки
                </a>
            </li>           
         
            <li *ngIf="userIsLoggedIn" class="visible-xs-inline">
                <a (click)="collapseNav()" href="#" (click)="logout($event);">
                    <span class="glyphicon glyphicon-off" aria-hidden="true"></span>
                    Выход
                </a>
            </li>

            <li *ngIf="!userIsLoggedIn" class="visible-xs-inline">
                <a (click)="collapseNav()" href="#" (click)="signinForm.showDialog()">
                    <span class="glyphicon glyphicon-off" aria-hidden="true"></span>
                    Вход
                </a>
            </li>
        </ul>
       
        <button *ngIf="!userIsLoggedIn" id="signin-link" class="btn btn-default pull-right hidden-xs nav-menu-button" href="#" role="button" data-nav="login" aria-haspopup="true" (click)="signinForm.showDialog()">
            <span>Войти</span>            
        </button>
        <div *ngIf="userIsLoggedIn" id="nav-options" class="btn-group pull-right hidden-xs">
            <button type="button" class="btn btn-primary dropdown-toggle thumbnail" data-toggle="dropdown" aria-expanded="False" aria-haspopup="true">
                <img [src]="avatarUrl" [empty-image]="'avatar'"/>
            </button>
            <ul class="dropdown-menu">
                <li *ngIf="userIsLoggedIn"><a href="" routerLink="/i/account/settings">Настройки</a></li>                
                <li role="separator" class="divider"></li>
                <li *ngIf="userIsLoggedIn"><a href="#" (click)="logout($event);">Выход</a></li>
            </ul>
        </div>
        <div id="lgSearchPlace">
            <main-search id="mainSearchComponent"></main-search>
        </div>
            
    </div>


</nav>
<error-flow-view></error-flow-view>
<router-outlet></router-outlet>
<signin-form #signinForm></signin-form>
`


})
export class AppComponent implements OnInit, OnDestroy {
    userIsLoggedIn: boolean;

    avatarUrl:string;

    userName:string;

    private subscriptionsContainer: SubscriptionsContainer = new SubscriptionsContainer();

    @ViewChild('navbarToggler')
    navbarToggler: ElementRef;

    constructor(private authService: AuthentificateService, private router: Router, private socialService: SocialService) {
        this.authService.reloadAuthData();
    }

    @HostListener('window:resize', ['$event'])
    onResize(event:Event) {
        //"Width: " + event.target.innerWidth;
        this.setSearchPlace();
    }

    ngOnInit(): void {
        RuLocale.defineLocale();
        RuLocale.setLocale();

        this.userIsLoggedIn = this.authService.isAuthorized;

        if (this.userIsLoggedIn) {
            this.userName = this.authService.userName;
        }

        let subscription: any = this.authService.authFeed.filter((e: AuthEvent) => e.eventType === AuthEventType.Logged)
            .map((e: AuthEvent) => e.data).subscribe(userId => {
                this.userIsLoggedIn = true;
                this.userName = this.authService.userName;
                this.getProfile();
                this.router.navigateByUrl(this.userName);
            });

        this.subscriptionsContainer.add(subscription);

        subscription = this.authService.authFeed.filter((e: AuthEvent) => e.eventType === AuthEventType.Loggedout)
            .subscribe(e => {
                this.userIsLoggedIn = false;
                this.router.navigateByUrl('');
            });
        this.subscriptionsContainer.add(subscription);
        subscription = this.socialService.socialFeed.filter((e: SocialEvent) => e.eventType === SocialEventType.ProfileFetched)
            .subscribe((e: SocialEvent) => {
                if (StringExtentions.compareOrdinalIgnoreCase(this.authService.userName, e.targetUserName)) {
                    this.avatarUrl = e.data.avatarUrl;
                }
            });

        this.subscriptionsContainer.add(subscription);

        this.getProfile();
        this.setSearchPlace();
    }

    ngOnDestroy(): void {
        this.subscriptionsContainer.clear();
    }

    private getProfile() {
        if (this.userIsLoggedIn) {
            this.socialService.getProfile(this.authService.userName);
        }
    }

    private setSearchPlace() {
        let width: number = window.innerWidth;
        let size: WindowSize = ScreenDetector.getSize(width);
        

        let component: Element = document.getElementById("mainSearchComponent");

        if (size === WindowSize.ScreenMd || size === WindowSize.ScreenLg || size === WindowSize.ScreenSm) {
            if (ScreenDetector.elementExists('#lgSearchPlace #mainSearchComponent')) {
                return;
            }
            document.getElementById("smSearchPlace").removeChild(component);
            document.getElementById("lgSearchPlace").appendChild(component);
        } else {

            if (ScreenDetector.elementExists('#smSearchPlace #mainSearchComponent')) {
                return;
            }
            document.getElementById("lgSearchPlace").removeChild(component);
            
            document.getElementById("smSearchPlace").appendChild(component);
        }
    }

    logout($event:Event) {

        $event.preventDefault();

        this.authService.logout().then(succes => {
            if (succes) {
                //
            }

        });
    }

    private navBarTogglerIsVisible() {
        return this.navbarToggler.nativeElement.offsetParent !== null;
    }

    collapseNav() {
        if (this.navBarTogglerIsVisible()) {
            this.navbarToggler.nativeElement.click();
        }
    }
}


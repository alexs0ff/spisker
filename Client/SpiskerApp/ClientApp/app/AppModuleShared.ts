import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from "./AppComponent/AppComponent";
import { AuthentificateService } from "./Auth/AuthentificateService";


/*App Modules*/
import { HomeModule } from "./Home/HomeModule";
import { AuthModule, AuthGuard } from "./Auth/AuthModule";
import { ErrorFlowModule } from "./ErrorFlow/ErrorFlowModule";
import { UtilsModule } from "./Utils/UtilsModule";
import { AppRoutingModule } from "./AppRoutingModule";
import { AccountModule } from "./Account/AccountModule";
import { RegisterModule } from "./Register/RegisterModule";
import { MainMenuModule } from "./Home/MainMenu/MainMenuModule";
import { SettingsModule } from "./Settings/SettingsModule";


/*Сервисы*/
import { ErrorFlowService } from "./ErrorFlow/ErrorFlowModule";
import { ClipboardService } from "./Utils/UtilsModule";
import { Sender } from "./Utils/Sender/SenderModule";
import { OAuth2Service } from "./OAuth2/OAuth2Module";
import { FeedListService, ListItemDragAndDropService, ListViewDialogService } from "./Lists/FeedModule";
import { SocialService } from "./Social/SocialModule";
import { AccountService } from "./Account/AccountService/AccountServiceModule";

/* RxJs*/
import "rxjs/add/operator/map";
import "rxjs/add/operator/filter";


@NgModule({
  imports: [BrowserModule.withServerTransition({ appId: 'my-app' }), HttpClientModule, AppRoutingModule, HomeModule, AuthModule, ErrorFlowModule, UtilsModule, RegisterModule, AccountModule, MainMenuModule, SettingsModule],
    declarations: [AppComponent],
    bootstrap: [AppComponent],
    providers: [AuthentificateService, Sender, OAuth2Service, FeedListService, ErrorFlowService, ListItemDragAndDropService, SocialService, AccountService, ListViewDialogService, ClipboardService]
})
export class AppModuleShared { }


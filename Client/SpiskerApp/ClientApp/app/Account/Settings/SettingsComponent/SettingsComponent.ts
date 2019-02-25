import { Component, OnInit, OnDestroy, ViewChild  } from '@angular/core';
import {AuthentificateService} from "../../../Auth/AuthModule";
import {UserSettingsComponent} from "../UserSettingsComponent/UserSettingsComponent";
import {ChangeStatusTextComponent} from "../ChangeStatusTextComponent/ChangeStatusTextComponent";
import {ChangeAvatarComponent} from "../ChangeAvatarComponent/ChangeAvatarComponent";
import {AccountEvent} from "../../AccountService/AccountEvent";
import {SocialService, SocialEvent, SocialEventType } from "../../../Social/SocialModule";
import {AccountEventType} from "../../AccountService/AccountEventType";
import {StringExtentions} from "../../../Utils/Extentions/StringExtentions";
import {UserProfileModel} from "../../../Social/Model/UserProfileModel";
import {AccountSettingsModel} from "../../AccountService/AccountSettingsModel";
import {AccountService} from "../../AccountService/AccountService";

@Component({
    template: `
<ul id="account-tabs" class="nav nav-tabs nav-justified">
        <li role="presentation" class="active">
            <a href="#account-user" data-toggle="tab">Пользователь</a>
        </li>
        <li role="presentation">
            <a href="#account-password" data-toggle="tab">Пароль</a>
        </li>
        <li role="presentation">
            <a href="#account-social" data-toggle="tab">Информация</a>
        </li>
    </ul>
            
<div class="tab-content card">
                <div role="tabpanel" class="tab-pane active" id="account-user">
                    <user-settings #userSettingsTab></user-settings>
                    <change-avatar #changeAvatarForm></change-avatar>
                </div>
                <div role="tabpanel" class="tab-pane" id="account-password">
                    <change-password></change-password>
                </div>
                <div role="tabpanel" class="tab-pane"  id="account-social">
                    <change-status-text #changeStatusForm></change-status-text>
                </div>
            </div>
  `
})
export class SettingsComponent implements OnInit, OnDestroy {

    @ViewChild('userSettingsTab') 
    settingsTab: UserSettingsComponent;

    @ViewChild('changeStatusForm')
    statusForm: ChangeStatusTextComponent;

    @ViewChild('changeAvatarForm')
    changeAvatarFormForm: ChangeAvatarComponent;

    constructor(private accountService: AccountService, private authService: AuthentificateService, private socialService:SocialService) {
        
    }

    private accountSettingsReceivedSubject: any;

    private profileFetchedSubject: any;

    ngOnInit(): void {
        this.accountSettingsReceivedSubject = this.accountService.accountFeed.filter((e: AccountEvent) => e.eventType === AccountEventType.AccountSettingsReceived)
            .subscribe((event: AccountEvent) => {
                if (StringExtentions.compareOrdinalIgnoreCase(event.targetUserName,this.authService.userName)) {
                    this.settingsReceived(event.data);
                }
            });

        this.profileFetchedSubject = this.socialService.socialFeed.filter((e: SocialEvent) => e.eventType === SocialEventType.ProfileFetched).map((e: SocialEvent) => e.data).subscribe(data => {
            this.setProfile(data);
        });
    }

    ngOnDestroy(): void {
        this.accountSettingsReceivedSubject.unsubscribe();
        this.profileFetchedSubject.unsubscribe();
    }

    private settingsReceived(data: AccountSettingsModel) {
        this.settingsTab.setData(data);
    }

    private setProfile(profile: UserProfileModel) {
        this.changeAvatarFormForm.setImage(profile.avatarUrl);
        this.statusForm.setCurrentStatusText(profile.statusText);
    }
}
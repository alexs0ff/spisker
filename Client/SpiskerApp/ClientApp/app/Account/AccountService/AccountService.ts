import { Injectable } from '@angular/core';
import { Subject } from "rxjs/rx";
import { ErrorFlowService } from "../../ErrorFlow/ErrorFlowModule";
import { AccountEvent } from "./AccountEvent";
import { AccountEventType } from "./AccountEventType";
import {AccountSettingsModel} from "./AccountSettingsModel";
import {ServiceBase} from "../../Utils/Services/ServiceBase";
import {Sender} from "../../Utils/Sender/SenderModule";

@Injectable()
export class AccountService extends ServiceBase{

    constructor(private sender: Sender, errorService: ErrorFlowService) {
        super(errorService);
        this.accountFeed = new Subject<AccountEvent>();
    }

    accountFeed: Subject<AccountEvent>;

    private getAccountSettingsUrl = "api/account/getsettings";

    private updateAccountSettingsUrl = "api/account/updatesettings";

    private changePasswordUrl = "api/account/changepassword";

    private changeStatusTextUrl = "api/account/changestatustext";

    private changeAvatarUrl = "api/account/changeavatar";

    private accountSettingsReceived(userName: string, data: AccountSettingsModel) {
        let event = new AccountEvent();
        event.eventType = AccountEventType.AccountSettingsReceived;
        event.targetUserName = userName;
        event.data = data;
        this.accountFeed.next(event);
    }

    private accountSettingsUpdated(userName: string) {
        let event = new AccountEvent();
        event.eventType = AccountEventType.AccountSettingsUpdated;
        event.targetUserName = userName;
        event.data = null;
        this.accountFeed.next(event);
    }

    private accountPasswordChanged(userName: string) {
        let event = new AccountEvent();
        event.eventType = AccountEventType.PasswordChanged;
        event.targetUserName = userName;
        event.data = null;
        this.accountFeed.next(event);
    }

    private accountPasswordChangeError(userName: string,response: any) {
        let event = new AccountEvent();
        event.eventType = AccountEventType.PasswordChangeError;
        event.targetUserName = userName;
        event.data = response;
        this.accountFeed.next(event);
    }

    private accountStatusTextChanged(userName: string, newStatus:string) {
        let event = new AccountEvent();
        event.eventType = AccountEventType.StatusTextChanged;
        event.targetUserName = userName;
        event.data = newStatus;
        this.accountFeed.next(event);
    }

    private accountAvatarChanged(userName: string, newUrl: string) {
        let event = new AccountEvent();
        event.eventType = AccountEventType.AvatarChanged;
        event.targetUserName = userName;
        event.data = newUrl;
        this.accountFeed.next(event);
    }

    getAccountSettings(userName: string) {

        let request: any = {
            UserName: userName
        };

        this.sender.getAuthJson(request, this.getAccountSettingsUrl).subscribe(response => {
            this.processError(response);

            if (response.settings) {
                this.accountSettingsReceived(userName, response.settings);
            };
        });
    }

    updateAccountSettings(userName: string, settings: AccountSettingsModel) {
        let request: any = {
            Settings: settings
        };

        this.sender.postAuthJson(request, this.updateAccountSettingsUrl).subscribe(response => {
            this.processError(response);

            if (response.userName) {
                this.accountSettingsUpdated(userName);
            };
        });
    }

    changeAccountPassword(userName: string, oldPassword:string, newPassword:string) {
        let request: any = {
            OldPassword: oldPassword,
            NewPassword:newPassword
        };

        this.sender.postAuthJson(request, this.changePasswordUrl).subscribe(response => {
            //Подумать как регистрировать ошибки
            //this.processError(response);

            if (response.userName) {
                this.accountPasswordChanged(userName);
            } else {
                this.accountPasswordChangeError(userName, response);
            }
        });
    }

    changeStatusText(userName: string, newStatus:string) {
        let request: any = {
            UserName: userName,
            NewStatus: newStatus
        };

        this.sender.postAuthJson(request, this.changeStatusTextUrl).subscribe(response => {
            this.processError(response);
            if (response.userName) {
                this.accountStatusTextChanged(response.userName, response.newStatus);
            } 
        });
    }

    changeAvatar(file:File) {
        this.sender.postFileAuthJson(file, this.changeAvatarUrl).subscribe(response => {
            this.processError(response);
            if (response.userName) {
                this.accountAvatarChanged(response.userName, response.imageUrl);
            }
        });
    }


}
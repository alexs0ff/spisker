import { Injectable } from '@angular/core';
import { Observable} from "rxjs/rx";
import { Sender } from "../Utils/Sender/SenderModule";
import {ServiceBase} from "../Utils/Services/ServiceBase";

@Injectable()
export class RegistrationService {
    constructor(private sender: Sender) {
        
    }

    private readonly registerUrl: string = "api/account/create";

    private readonly startRecoveryUrl: string = "api/account/startrecoverpassword";

    private readonly approveRecoveryUrl: string = "api/account/recoverpassword";

    register(userName: string, email: string, password:string): Observable<any> {
        var request = {
            UserName: userName,
            Email: email,
            Password:password
        };

        return this.sender.postJson(request, this.registerUrl);
    }

    startRecovery(userName: string, token:string): Observable<any> {
        var request = {
            UserName: userName,
            RecaptchaToken: token
        };

        return this.sender.postJson(request, this.startRecoveryUrl);
    }

    approveRecovery(userName: string, newPassword: string, number:string ,token: string): Observable<any> {
        var request = {
            UserName: userName,
            RecaptchaToken: token,
            NewPassword: newPassword,
            Number:number
        };

        return this.sender.postJson(request, this.approveRecoveryUrl);
    }
}
import { Injectable, EventEmitter } from '@angular/core';
import {AuthResult} from "./AuthModule";
import { AuthEvent } from "./AuthFlow/AuthEvent";
import { Subject } from "rxjs/rx";
import {AuthEventType} from "./AuthFlow/AuthEventType";
import {Sender} from "../Utils/Sender/SenderModule";
import {OAuth2Service} from "../OAuth2/OAuth2Module";

@Injectable()
export class AuthentificateService {

    authFeed: Subject<AuthEvent>;

    private _isAuthorized: boolean = false;

    private _userId: string;

    private _userName:string;

    private authUrl: string = "Token";
        
    constructor(private sender: Sender, private oauth2Service: OAuth2Service) {
        this.authFeed = new Subject<AuthEvent>();
    }

    login(userName: string, password: string): Promise<AuthResult> {
        return new Promise(resolve => {
            var result: AuthResult = new AuthResult();
            result.success = false;

            this._isAuthorized = false;
            this.oauth2Service.clear();

            this.sender.postAuth(userName, password, this.authUrl)
                .subscribe(response => {
                    this._isAuthorized = this.oauth2Service.setToken(response);
                    result.success = this._isAuthorized;
                    
                    if (this._isAuthorized) {
                        this._userId = this.oauth2Service.getTokenUserId();
                        this._userName = this.oauth2Service.getTokenUserName();
                        this.logged(this._userId);
                    } else {
                        result.description = "Произошла системная ошибка";
                    }
                    resolve(result);
                },error => {
                    result.description = "Неверный логин или пароль";
                    resolve(result);
                });


        });
    }

    logout(): Promise<boolean> {

        return new Promise(resolve => {
            if (this._isAuthorized) {
                this.oauth2Service.clear();
                this._isAuthorized = false;
                this._userName = null;
                this._userId = null;
                this.loggedout();
            };
            resolve(!this._isAuthorized);
        });
        
    }

    get isAuthorized(): boolean {
        return this._isAuthorized;
    }

    get userId(): string {
        if (this.isAuthorized) {
            return this._userId;
        }
        return null;
    }

    get userName(): string {
        if (this.isAuthorized) {
            return this._userName;
        }
        return null;
    }

    ///Необходимо вызывать при старте приложения.
    reloadAuthData() {
        if (this.oauth2Service.initToken()) {
            this._isAuthorized = true;
            this._userId = this.oauth2Service.getTokenUserId();
            this._userName = this.oauth2Service.getTokenUserName();
            this.logged(this._userId);
        }
    }

    private loggedout() {
        let event = new AuthEvent();
        event.eventType = AuthEventType.Loggedout;
        this.authFeed.next(event);
    }

    private logged(userId: string) {
        let event = new AuthEvent();
        event.data = userId;
        event.eventType = AuthEventType.Logged;
        this.authFeed.next(event);
    }

    public getToken():string {
        return this.oauth2Service.getTokenValue();
    }
    
}
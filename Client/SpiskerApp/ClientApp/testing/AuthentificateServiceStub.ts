import { AuthEvent, AuthResult, AuthEventType } from "../app/Auth/AuthModule";
import { Subject } from "rxjs/rx";

export class AuthentificateServiceStub {
    authFeed: Subject<AuthEvent>;

    private _isAuthorized: boolean = false;

    private _userId: string;

    private _userName: string;

    private authUrl: string = "Token";

    canReload:boolean;

    constructor() {
        this.authFeed = new Subject<AuthEvent>();
        this.canReload = false;
    }

    login(userName: string, password: string): Promise<AuthResult> {
        return new Promise(resolve => {
            var result: AuthResult = new AuthResult();
            result.success = false;

            this._isAuthorized = false;

            if (userName==='test' && password==='admin') {
                this._isAuthorized = true;
                result.success = this._isAuthorized;
                this._userId = 'userId';
                this._userName = userName;
                this.logged(this._userId);
               
                resolve(result);
            } else {
                result.description = "Неверный логин или пароль";
                resolve(result);
            }
           


        });
    }

    logout(): Promise<boolean> {

        return new Promise(resolve => {
            if (this._isAuthorized) {
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
        if (this.canReload) {
            this._isAuthorized = true;
            this._userId = 'userId';
            this._userName = 'test';
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
}

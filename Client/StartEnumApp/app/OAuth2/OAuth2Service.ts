import { Injectable } from '@angular/core';
import {Token} from "./Token";

//Реализация сервиса работы с токенами OAuth.
@Injectable() 
export class OAuth2Service {

    private tokenStorageKey: string ='OAuth2Service.OAuth2Token';

    private token: Token;

    initToken():boolean {
        var result = false;
        var data = window.localStorage.getItem(this.tokenStorageKey);
        if (data && data.length > 0) {
            this.token = <Token>JSON.parse(data, Token.parse);
            if (this.isValidToken()) {
                result = true;
            } else {
                this.clear();
            }
        }

        return result;
    }

    //Установка токена.
    setToken(token: any): boolean {
        if (token.access_token) {
            this.token = new Token();
            this.token.accessToken = token.access_token;
            this.token.userId = token.userId;
            this.token.userName = token.userName;
            var date = new Date();
            date.setTime(token.expiresOn);
            
            this.token.expiresOn = date;
            window.localStorage.setItem(this.tokenStorageKey, JSON.stringify(this.token));
            return true;
        }
        return false;
    }
    

    clear() {
        window.localStorage.removeItem(this.tokenStorageKey);
        this.token = null;
    }

    isValidToken(): boolean {
        if (this.token==null) {
            return false;
        }

        var today = new Date();
        var result = this.token.expiresOn > today;

        return result;
    }

    getTokenValue():string {
        if (this.token!=null) {
            return "Bearer " + this.token.accessToken;
        }

        return null;
    }

    getTokenUserId(): string {
        return this.token.userId;
    }

    getTokenUserName(): string {
        return this.token.userName;
    }
}
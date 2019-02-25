import { Injectable, Inject } from '@angular/core';
import {Token} from "./Token";

import { PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser, isPlatformServer } from '@angular/common';
import {SERVERPARAMS}  from "../Settings/serverparams";

//Реализация сервиса работы с токенами OAuth.
@Injectable() 
export class OAuth2Service {
  constructor( @Inject(PLATFORM_ID) private platformId: Object, @Inject(SERVERPARAMS) private serverParams: any,) {
    
  }

  private tokenStorageKey: string = 'OAuth2Service.OAuth2Token';

  private serverToken: string ='spiskerOAuth2Token';

    private token: Token;

  initToken(): boolean {
    let result:boolean = false;
    let data: any;
    if (isPlatformBrowser(this.platformId)) {
      data = localStorage.getItem(this.tokenStorageKey);
    } else {
      data = this.serverParams[this.serverToken];
    }
    

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

    if (isPlatformServer(this.platformId)) {
      return false;
    }
    if (token.access_token) {
      this.token = new Token();
      this.token.accessToken = token.access_token;
      this.token.userId = token.userId;
      this.token.userName = token.userName;
      var date = new Date();
      date.setTime(token.expiresOn);

      this.token.expiresOn = date;
      let raw = JSON.stringify(this.token);
      localStorage.setItem(this.tokenStorageKey, raw);
      this.setCookie(this.serverToken,
        raw,
        {
          expires:
            date
        });
            return true;
        }
        return false;
  }

  private setCookie(name: string, value: any, options: any) {
    options = options || {};

    var expires = options.expires;

    if (typeof expires == "number" && expires) {
      var d = new Date();
      d.setTime(d.getTime() + expires * 1000);
      expires = options.expires = d;
    }
    if (expires && expires.toUTCString) {
      options.expires = expires.toUTCString();
    }

    value = encodeURIComponent(value);

    var updatedCookie = name + "=" + value;

    for (var propName in options) {
      updatedCookie += "; " + propName;
      var propValue = options[propName];
      if (propValue !== true) {
        updatedCookie += "=" + propValue;
      }
    }

    document.cookie = updatedCookie;
  }


  clear() {
      if (isPlatformBrowser(this.platformId)) {
          window.localStorage.removeItem(this.tokenStorageKey);
        }
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

    getTokenValue(): string {
        if (isPlatformServer(this.platformId)) {
            if (this.token == null) {
                this.initToken();    
            }
        }

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
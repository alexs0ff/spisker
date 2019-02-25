import { Injectable } from '@angular/core';
import {UserProfileModel} from "../Social/Model/UserProfileModel";
import {Messages} from "./Messages";

@Injectable()
export class SettingsService {

    private _messages: Messages;

    constructor() {
        this._messages = new Messages();
    }

    getRecaptchaKey() {
        return "6Lc5FzEUAAAAAH6LmGz1JdjvewqoaW6vyuws6oeS";
    }

    getFollowersUrl(userName: string): string {
        return "/" + userName + "/followers";
    }

    getFollowingsUrl(userName: string): string {
        return "/" + userName + "/followings";
    }

    getFullName(profile: UserProfileModel):string {
        let middleName = "";

        if (profile.middleName) {
            middleName = profile.middleName;
        }

        let fullName = profile.lastName + " " + profile.firstName + " " + middleName;

        return fullName;
    }

    getMaxFollowersCount():number {
        return 10;
    }

    getMaxFollowingsCount(): number {
        return 10;
    }

    getMaxWhoFollowCount(): number {
        return 4;
    }

    get messages(): Messages { return this._messages; }
}


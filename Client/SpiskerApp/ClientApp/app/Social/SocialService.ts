import { Injectable } from '@angular/core';
import { Subject} from "rxjs/rx";
import { SocialEvent } from "./SocialEvent";
import { SocialEventType } from "./SocialEventType";
import {ServiceBase} from "../Utils/Services/ServiceBase";
import {UserProfileModel} from "./Model/UserProfileModel";
import {ErrorFlowService} from "../ErrorFlow/ErrorFlowModule";
import {Sender} from "../Utils/Sender/SenderModule";
import {StringExtentions} from "../Utils/Extentions/StringExtentions";

@Injectable()
export class SocialService extends ServiceBase {

    socialFeed: Subject<SocialEvent>;

    private readonly getProfileUrl = "api/profile";

    private readonly startFollowingUrl = "api/startfollowing";

    private readonly stopFollowingUrl = "api/stopfollowing";

    private readonly setLikeListUrl = "api/setlikelist";

    private readonly unsetLikeListUrl = "api/unsetlikelist";

    private readonly repostListUrl = "api/repostlist";

    private readonly getProfilesUrl = "api/findusers";

    private readonly getFollowersUrl = "api/getfollowers";

    private readonly getFollowingsUrl = "api/getfollowings";

    constructor(private sender: Sender, errorService: ErrorFlowService) {
        super(errorService);
        this.socialFeed = new Subject<SocialEvent>();
    }


    private followingsFetched(systemId: string, model: Array<UserProfileModel>, forUser: string, lastFollowingId: string) {
        let event = new SocialEvent();
        event.data = {
            profiles: model,
            forUser: forUser,
            lastFollowingId: lastFollowingId
        };
        event.systemId = systemId;
        event.eventType = SocialEventType.FollowingsFetched;
        this.socialFeed.next(event);
    }

    private followersFetched(systemId: string, model: Array<UserProfileModel>, forUser: string, lastFollowerId:string) {
        let event = new SocialEvent();
        event.data = {
            profiles: model,
            forUser: forUser,
            lastFollowerId: lastFollowerId
        };
        event.systemId = systemId;
        event.eventType = SocialEventType.FollowersFetched;
        this.socialFeed.next(event);
    }

    private profilesFetched(systemId: string, model: Array<UserProfileModel>) {
        let event = new SocialEvent();
        event.data = model;
        event.systemId = systemId;
        event.eventType = SocialEventType.ProfilesFetched;
        this.socialFeed.next(event);
    }

    private profileFetched(targetUserName:string, model: UserProfileModel) {
        let event = new SocialEvent();
        event.data = model;
        event.targetUserName = targetUserName;
        event.eventType = SocialEventType.ProfileFetched;
        this.socialFeed.next(event);
    }

    private userStartedFollow(userName: string, toUserName:string) {
        let event = new SocialEvent();
        event.data = toUserName;
        event.targetUserName = userName;
        event.eventType = SocialEventType.UserStartedFollow;
        this.socialFeed.next(event);
    }

    private userStoppedFollow(userName: string, toUserName: string) {
        let event = new SocialEvent();
        event.data = toUserName;
        event.targetUserName = userName;
        event.eventType = SocialEventType.UserStoppedFollow;
        this.socialFeed.next(event);
    }

    private likeListSet(listId: string, likeCount:number) {
        let event = new SocialEvent();
        event.data = {
            listId: listId,
            likeCount: likeCount
        };
        event.eventType = SocialEventType.SetLikeList;
        this.socialFeed.next(event);
    }

    private likeListUnset(listId: string, likeCount: number) {
        let event = new SocialEvent();
        event.data = {
            listId: listId,
            likeCount: likeCount
        };
        event.eventType = SocialEventType.UnsetLikeList;
        this.socialFeed.next(event);
    }

    private listReposted(listId: string, repostedListId:string, repostCount: number) {
        let event = new SocialEvent();
        event.data = {
            listId: listId,
            repostedListId: repostedListId,
            repostCount: repostCount
        };
        event.eventType = SocialEventType.ListReposted;
        this.socialFeed.next(event);
    }

    getProfile(userName: string, currentUserName:string=null) {

        let request: any = {
            UserName: userName
        };

        this.sender.getTryAuthJson(request, this.getProfileUrl).subscribe(response => {
            this.processError(response);

            if (response.profile) {
                this.profileFetched(userName, response.profile);
            };
        });
    }

    startFollowing(userName: string, toUserName: string) {

        let request: any = {
            UserName: userName,
            ToUserName: toUserName
        };

        this.sender.postAuthJson(request, this.startFollowingUrl).subscribe(response => {
            this.processError(response);

            if (response.subscriberUserId) {
                this.userStartedFollow(userName, toUserName);
            };
        });
    }

    stopFollowing(userName: string, toUserName: string) {

        let request: any = {
            UserName: userName,
            ToUserName: toUserName
        };

        this.sender.postAuthJson(request, this.stopFollowingUrl).subscribe(response => {
            this.processError(response);

            if (response.subscriberUserId) {
                this.userStoppedFollow(userName, toUserName);
            };
        });
    }

    setLikeList(listId: string) {

        let request: any = {
            ListId: listId
        };

        this.sender.postAuthJson(request, this.setLikeListUrl).subscribe(response => {
            this.processError(response);

            if (response.listId) {
                this.likeListSet(response.listId, response.likeCount);
            };
        });
    }

    unsetLikeList(listId: string) {
        let request: any = {
            ListId: listId
        };

        this.sender.postAuthJson(request, this.unsetLikeListUrl).subscribe(response => {
            this.processError(response);

            if (response.listId) {
                this.likeListUnset(response.listId, response.likeCount);
            };
        });
    }

    repostList(listId: string) {

        let request: any = {
            ListId: listId
        };

        this.sender.postAuthJson(request, this.repostListUrl).subscribe(response => {
            this.processError(response);

            if (response.listId) {
                this.listReposted(response.listId,response.repostedListId, response.repostCount);
            };
        });
    }

    getProfiles(systemId:string, name: string) {

        let request: any = {
            Name: name
        };

        this.sender.getTryAuthJson(request, this.getProfilesUrl).subscribe(response => {
            this.processError(response);

            if (response.profiles) {
                this.profilesFetched(systemId, response.profiles);
            };
        });
    }

    getFollowers(systemId: string, userName: string, search: string, lastFollowerId:string ) {

        let request: any = {
            UserName: userName,
            LastFollowerId: lastFollowerId
        };

        if (!StringExtentions.isNullOrWhiteSpace(search)) {
            request.Search = search;
        }

        this.sender.getTryAuthJson(request, this.getFollowersUrl).subscribe(response => {
            this.processError(response);

            if (response.profiles) {
                this.followersFetched(systemId, response.profiles, response.forUser, response.lastFollowerId);
            };
        });
    }

    getFollowings(systemId: string, userName: string, search: string, lastFollowingId: string) {

        let request: any = {
            UserName: userName,
            LastFollowingId: lastFollowingId
        };

        if (!StringExtentions.isNullOrWhiteSpace(search)) {
            request.Search = search;
        }

        this.sender.getTryAuthJson(request, this.getFollowingsUrl).subscribe(response => {
            this.processError(response);

            if (response.profiles) {
                this.followingsFetched(systemId, response.profiles, response.forUser, response.lastFollowerId);
            };
        });
    }
}
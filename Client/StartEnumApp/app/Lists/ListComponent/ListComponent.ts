import { Component, Input, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import {ListNameComponentState} from "./ListNameComponentState";
import {FeedListService} from "../FeedCardService/FeedListService";
import {ListViewDialogService} from "../ListViewDialog/ListViewDialogService";
import {ListModel} from "../Models/ListModel";
import {FeedEvent} from "../FeedCardService/FeedEvent";
import {FeedEventType} from "../FeedCardService/FeedEventType";
import {StringExtentions} from "../../Utils/Extentions/StringExtentions";
import {ListItemModel} from "../Models/ListItemModel";
import {SocialService, SocialEvent, SocialEventType } from "../../Social/SocialModule";
import { SubscriptionsContainer, DoubleClickListener } from "../../Utils/UtilsModule";
import {AuthentificateService, AuthEvent, AuthEventType } from "../../Auth/AuthModule";

@Component({
    selector: 'list-view',
    template: `
    <li class="startenum-list">
    <img [src]="list.avatarUrl" class="feed-avatar img-responsive" [empty-image]="'avatar'"/>    
     <div class="feed-post">
         <list-options [list]="list"></list-options>
         <h6 *ngIf="isReposted()"><span class="glyphicon glyphicon-transfer" aria-hidden="true"></span> {{list.ownerFullName}} репостнул(а)</h6>
         <h5>{{getFullName()}} <small [user-link]="getLogin()">@{{getLogin()}}- {{list.createEventTime | amFromUtc | amLocal | amTimeAgo  }}</small></h5>
        <div [ngSwitch]="listNameViewState">
            <div *ngSwitchCase="listNameState.View">
                <list-name-view (click)="toggleNameViewState($event)" [list]="list"></list-name-view>
            </div>
            <div *ngSwitchCase="listNameState.Edit">
                <list-name-edit [list]="list" (complete)="onEditNameCompleted()"></list-name-edit>
            </div>
        </div>
            <div class="listItems">
                <list-item-splitter [listId]="list.id" [userCanDrop]="userIsOwner"></list-item-splitter>
                <p *ngFor="let item of list.items" class="listing-item-container">
                    <list-item (removeItem)="removeFromList(item)" [item]="item" [listId]="list.id" [listKind]="list.listKind" [checkItemKind]="list.listCheckItemKind"></list-item>
                    <list-item-splitter [item]="item" [listId]="list.id" [userCanDrop]="userIsOwner"></list-item-splitter>
                </p>
                <p>
                    <a (click)="addNew($event)" href="#" data-toggle="tooltip" data-placement="bottom" title="Добавить" *iFOwnedUser="list.ownerId">
                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                    </a>
                </p>
            </div>        
     </div>
     <div class="action-list">         
         <a href="#" class="no-focus-border" data-toggle="tooltip" data-placement="bottom" title="Репост" (click)="startRepost($event)" *iFNotOwnedUser="list.ownerId;unLoggedVisible:false">
             <span class="glyphicon glyphicon-share" aria-hidden="true" [ngClass]="{'has-repost': currentUserHasRepost}"></span>
             <span class="repost-count">{{list.repostCount}}</span>
         </a>
         <a *ngIf="userIsLogged" href="" class="no-focus-border" data-toggle="tooltip" data-placement="bottom" title="Нравится" (click)="startSetLike($event)">
             <span class="glyphicon glyphicon-heart" aria-hidden="true" [ngClass]="{'has-like': list.currentUserHasLike}"></span>
             <span class="repost-count">{{list.likeCount}}</span>
         </a>
         <a *ngIf="userIsOwner" href="#" class="no-focus-border" data-toggle="tooltip" data-placement="bottom" title="Удалить" (click)="deleteList($event)">
             <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>             
         </a>

         <a *ngIf="userIsOwner" href="#" class="no-focus-border" data-toggle="tooltip" data-placement="bottom" [attr.title]="list.isPublished ? 'Скрыть' : 'Опубликовать'" (click)="changeIsPublishedList($event)">
             <span class="glyphicon"  [ngClass]="{'glyphicon-eye-open': list.isPublished,'glyphicon-eye-close':!list.isPublished}" aria-hidden="true"></span>             
         </a>

         <a *ngIf="showShare" href="#" class="no-focus-border" data-toggle="tooltip" data-placement="bottom" title="Открыть" (click)="openList($event)">
             <span class="glyphicon glyphicon glyphicon-open" aria-hidden="true"></span>             
         </a>
     </div>
 </li>
`

})
export class ListComponent implements OnInit, OnDestroy{
    @Input()
    list: ListModel;

    @Input()
    showShare: boolean;

    listNameState = ListNameComponentState;

    listNameViewState: ListNameComponentState;

    currentUserHasRepost: boolean;

    userIsLogged: boolean;

    userIsOwner:boolean;

    private subjectContainer: SubscriptionsContainer = new SubscriptionsContainer();

    private listNameClickListener: DoubleClickListener;
    
    constructor(private feedService: FeedListService,
        private ref: ChangeDetectorRef,
        private socialService: SocialService,
        private router: Router,
        private dialogService: ListViewDialogService,
        private authService: AuthentificateService) {
        
    }

    ngOnInit(): void {
        
        this.listNameViewState = ListNameComponentState.View;
        
        let subject:any = this.feedService.listFeed.filter((e: FeedEvent) => e.eventType === FeedEventType.ListItemAdded && e.parentId === this.list.id)
            .subscribe(e => {
                e.targetItem.isInserted = false;
                ListModel.addItemAfterItemId(this.list, e.afterId, e.targetItem);
                this.ref.detectChanges();
        
            });

        this.subjectContainer.add(subject);

        subject = this.feedService.listFeed.filter((e: FeedEvent) => e.eventType === FeedEventType.ListItemUpdated && e.parentId === this.list.id)
            .map((e: FeedEvent) => e.targetItem).subscribe(listItem => {
                ListModel.replaceItemInList(this.list,listItem, listItem);
            });
        this.subjectContainer.add(subject);

        subject = this.feedService.listFeed.filter((e: FeedEvent) => e.eventType === FeedEventType.ListItemRemoved && e.parentId === this.list.id)
            .map((e: FeedEvent) => e.targetId).subscribe(id => {
                ListModel.removeItemInList(this.list,id);
                ListModel.positionByIndex(this.list);
            });
        this.subjectContainer.add(subject);

        subject = this.socialService.socialFeed.filter((e: SocialEvent) => e.eventType === SocialEventType.SetLikeList && StringExtentions.compareOrdinalIgnoreCase(e.data.listId, this.list.id)).subscribe((data: SocialEvent) => {
            this.setLike(data.data.likeCount);
        });
        this.subjectContainer.add(subject);

        subject = this.socialService.socialFeed.filter((e: SocialEvent) => e.eventType === SocialEventType.UnsetLikeList && StringExtentions.compareOrdinalIgnoreCase(e.data.listId, this.list.id)).subscribe((data: SocialEvent) => {
            this.unsetLike(data.data.likeCount);
        });
        this.subjectContainer.add(subject);

        subject = this.socialService.socialFeed.filter((e: SocialEvent) => e.eventType === SocialEventType.ListReposted && StringExtentions.compareOrdinalIgnoreCase(e.data.listId, this.list.id)).subscribe((data: SocialEvent) => {
            this.setReposted(data.data.repostCount);
        });
        this.subjectContainer.add(subject);

        subject = this.feedService.listFeed.filter((e: FeedEvent) => e.eventType === FeedEventType.ListKindChanged && e.targetId === this.list.id)
            .map((e: FeedEvent) => e.targetItem).subscribe(data => {
                this.list.listKind = data.newKind;
            });
        this.subjectContainer.add(subject);

        subject = this.feedService.listFeed.filter((e: FeedEvent) => e.eventType === FeedEventType.ListCheckItemKindChanged && e.targetId === this.list.id)
            .map((e: FeedEvent) => e.targetItem).subscribe(data => {
                this.list.listCheckItemKind = data.newCheckItemKind;
            });
        this.subjectContainer.add(subject);

        subject = this.authService.authFeed.filter((e: AuthEvent) => e.eventType === AuthEventType.Logged || e.eventType === AuthEventType.Loggedout)
            .subscribe(e => {
                this.detectCurrentUserState();
            });

        this.subjectContainer.add(subject);

        
        subject = this.feedService.listFeed.filter((e: FeedEvent) => e.eventType === FeedEventType.ListIsPublishedChanged && e.targetId === this.list.id)
            .map((e: FeedEvent) => e.targetItem).subscribe(data => {
                this.setIsPublished(data.isPublished);
            });
        this.subjectContainer.add(subject);


        this.detectCurrentUserState();

        this.listNameClickListener = new DoubleClickListener();
    }

    ngOnDestroy(): void {
        this.subjectContainer.clear();

        if (this.listNameClickListener) {
            this.listNameClickListener.dispose();
        }
    }

    addNew(event: Event) {
        event.preventDefault();
        var newItem: ListItemModel = new ListItemModel();
        newItem.content = "";
        newItem.orderPosition = this.list.items.length + 1;
        newItem.isInserted = true;
        newItem.data = ListModel.getLastItemId(this.list);
        this.list.items.push(newItem);
    }


    isReposted(): boolean {
        return !StringExtentions.isNullOrWhiteSpace(this.list.originId);
    }

    getFullName():string {
        if (!this.isReposted()) {
            return this.list.ownerFullName;
        }
        return this.list.originFullName;
    }

    getLogin(): string {
        if (!this.isReposted()) {
            return this.list.ownerLogin;
        }
        return this.list.originLogin;
    }
    removeFromList(item: ListItemModel) {
        var index = this.list.items.indexOf(item, 0);
        if (index > -1) {
            this.list.items.splice(index, 1);
        }
    }

    toggleNameViewState(event:Event) {
        this.listNameClickListener.click(event,null,(x:any) => {
            if (this.listNameViewState === ListNameComponentState.View) {
                this.listNameViewState = ListNameComponentState.Edit;
            } else {
                this.listNameViewState = ListNameComponentState.View;
            }
        });

        
    }

    onEditNameCompleted(saved: boolean) {
        this.listNameViewState = ListNameComponentState.View;
    }

    deleteList(event: Event) {
        event.preventDefault();
        this.feedService.removeList(this.list.id);
    }

    startSetLike(event:Event) {
        event.preventDefault();

        if (this.list.currentUserHasLike) {
            this.socialService.unsetLikeList(this.list.id);   
        } else {
            this.socialService.setLikeList(this.list.id);    
        }

        this.list.currentUserHasLike = !this.list.currentUserHasLike;
    }

    startRepost(event: Event) {
        event.preventDefault();

        if (!this.currentUserHasRepost) {
            this.socialService.repostList(this.list.id);
        } 

        this.currentUserHasRepost = true;
    }

    setLike(likeCount: number) {
        this.list.likeCount = likeCount;
    }

    unsetLike(likeCount:number) {
        this.list.likeCount = likeCount;
    }

    setReposted(repostCount: number) {
        this.list.repostCount = repostCount;
    }

    setIsPublished(isPublished: boolean) {
        this.list.isPublished = isPublished;
    }

    openList(event: Event) {
        event.preventDefault();
        let url = this.list.ownerLogin + "/" + this.list.publicId;
        if ("undefined" !== typeof window.history.pushState) {
            let oldPath = window.location.pathname;
            window.history.pushState("new url" + url, this.list.name, url);
            this.dialogService.showDialog(this.list, oldPath);
        } else {
            this.router.navigateByUrl(url);
        }
    }

    private detectCurrentUserState() {
        if (this.authService.isAuthorized) {
            this.userIsLogged = false;
            this.userIsOwner = StringExtentions.compareOrdinalIgnoreCase(this.list.ownerId, this.authService.userId);
        } else {
            this.userIsOwner = false;
            this.userIsLogged = false;
        }
    }

    private changeIsPublishedList(event: Event) {
        event.preventDefault();
        this.feedService.updateListIsPublished(this.list.id,!this.list.isPublished);
    }
} 

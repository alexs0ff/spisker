import { Injectable } from '@angular/core';
import { Subject } from "rxjs/rx";
import {ErrorFlowService} from "../../ErrorFlow/ErrorFlowModule";
import {ServiceBase} from "../../Utils/Services/ServiceBase";
import {FeedEvent} from "./FeedEvent";
import {ListModel} from "../Models/ListModel";
import {FeedEventType} from "./FeedEventType";
import {ListItemModel} from "../Models/ListItemModel";
import {StringExtentions} from "../../Utils/Extentions/StringExtentions";
import {Sender} from "../../Utils/Sender/SenderModule";

//Сервис управления списками в ленте
@Injectable()
export class FeedListService extends ServiceBase {

    listFeed: Subject<FeedEvent>;

    private fetchUrl = "api/list";

    private feedUrl = "api/feed";

    private saveNewListUrl = "api/list";

    private updateListUrl = "api/list/update";

    private removeListUrl = "api/list/remove";

    private saveNewListItemUrl = "api/list/item";

    private updateListItemUrl = "api/list/item/update";

    private removeListItemUrl = "api/list/item/remove";

    private moveToListItemUrl = "api/list/item/moveto";

    private listKindUpdateUrl = "api/list/updatekind";

    private listCheckItemKindUrl = "api/list/updatecheckitemkind";
    
    private updateListItemCheckUrl = "api/list/item/updatecheck";

    private listIsPublishedUpdateUrl = "api/list/updatepublished";

    constructor(private sender: Sender, errorService: ErrorFlowService) {
        super(errorService);
        this.listFeed = new Subject<FeedEvent>();
    }
   
    
    private listsFetched(lists: Array<ListModel>, hasMore:boolean, lastListId:string, userName:string, feedKind:number,selectedListId:string) {
        let event = new FeedEvent();
        event.targetItem = {
            lists: lists,
            hasMore: hasMore,
            lastListId: lastListId,
            userName: userName,
            feedKind: feedKind,
            selectedListId:selectedListId
        };
        event.eventType = FeedEventType.ListsFetched;
        this.listFeed.next(event);
    }

    private listItemRemoved(listId: string, listItemId: string) {
        let event = new FeedEvent();
        event.parentId = listId;
        event.targetId = listItemId;
        event.eventType = FeedEventType.ListItemRemoved;
        this.listFeed.next(event);
    }

    private listItemUpdated(listId: string, listItemId: string, listItem: ListItemModel) {
        let event = new FeedEvent();
        event.targetItem = listItem;
        event.parentId = listId;
        event.targetId = listItemId;
        event.eventType = FeedEventType.ListItemUpdated;
        this.listFeed.next(event);
    }

    private addListItem(listId:string,afterItemId:string,listItem: ListItemModel) {
        let event = new FeedEvent();
        event.targetItem = listItem;
        event.parentId = listId;
        event.afterId = afterItemId;
        event.eventType = FeedEventType.ListItemAdded;
        this.listFeed.next(event);
    }

    private addList(list: ListModel) {
        let event = new FeedEvent();
        event.targetItem = list;
        event.eventType = FeedEventType.ListAdded;
        this.listFeed.next(event);
    }

    private listUpdated(list: ListModel) {
        if (!list) {
            return;
        }
        let event = new FeedEvent();
        event.targetItem = list;
        event.eventType = FeedEventType.ListUpdated;
        this.listFeed.next(event);
    }

    private deleteList(listId: string) {
        let event = new FeedEvent();
        event.targetId = listId;
        event.eventType = FeedEventType.ListRemoved;
        this.listFeed.next(event);
    }

    private cleanList() {
        let event = new FeedEvent();
        event.eventType = FeedEventType.ListsCleaned;
        this.listFeed.next(event);
    }

    private listItemsSwapped(targetListId: string, itemId: string, afterItemId: string, copy:boolean, copiedListItem:ListItemModel) {
        let event = new FeedEvent();
        event.targetItem = {
            itemId: itemId,
            afterItemId: afterItemId,
            targetListId: targetListId,
            copy: copy,
            copiedItem: copiedListItem
        };
        event.eventType = FeedEventType.ListItemsSwapped;
        this.listFeed.next(event);
    }

    private listKindChanged(listId: string, newKind:number) {
        let event = new FeedEvent();
        event.targetId = listId;
        event.eventType = FeedEventType.ListKindChanged;
        event.targetItem = {
            newKind: newKind
        };
        this.listFeed.next(event);
    }

    private listCheckItemKindChanged(listId: string, newCheckItemKind: number) {
        let event = new FeedEvent();
        event.targetId = listId;
        event.eventType = FeedEventType.ListCheckItemKindChanged;
        event.targetItem = {
            newCheckItemKind: newCheckItemKind
        };
        this.listFeed.next(event);
    }

    private listItemCheckUpdated(listId: string, listItemId: string, listItem: ListItemModel) {
        let event = new FeedEvent();
        event.targetItem = listItem;
        event.parentId = listId;
        event.targetId = listItemId;
        event.eventType = FeedEventType.ListItemCheckUpdated;
        this.listFeed.next(event);
    }

    private listIsPublishedChanged(listId: string, isPublished: boolean) {
        let event = new FeedEvent();
        event.targetId = listId;
        event.eventType = FeedEventType.ListIsPublishedChanged;
        event.targetItem = {
            isPublished: isPublished
        };
        this.listFeed.next(event);
    }

    //Старт выборки ленты пользователя.
    startUserFeed() {
        this.cleanList();
        this.userFeed(null);
    }

    userFeed(lastId: string) {
        let request: any = {
            userName: "",
            lastListId: lastId
        };

        this.sender.getAuthJson(request, this.feedUrl).subscribe(response => {
            if (response != null && response.lists != null && response.lists !== undefined) {
                this.listsFetched(response.lists, response.hasMore, response.lastListId, response.userName, response.feedKind,null);
            }
        });
    }

    //Старт выборки списков для пользователя.
    startFetchUserList(userName: string, selectedListNumber: number = 0) {
        this.cleanList();
        this.fetchUserList(userName, null, selectedListNumber);
    }

    fetchUserList(userName: string, lastId: string, selectedListNumber:number=0) {
        let request: any = {
            userName: userName,
            lastListId: lastId,
            selectedListNumber: selectedListNumber
        };
        this.sender.getTryAuthJson(request, this.fetchUrl).subscribe(response => {
            if (response != null && response.lists != null && response.lists!==undefined) {
                this.listsFetched(response.lists, response.hasMore, response.lastListId, userName, response.feedKind,response.selectedListId);
            }
        });
    }

    addNewList(listName: string) {
        let request: any = {
            Name:listName
        };

        this.sender.postAuthJson(request, this.saveNewListUrl).subscribe(response => {
            this.processError(response);

            if (response.list) {
                this.addList(response.list);    
            };
        });
    }

    updateList(listId: string, name: string) {
        let request: any = {
            ListId: listId,
            Name:name
        };

        this.sender.postAuthJson(request, this.updateListUrl).subscribe(response => {
            this.processError(response);

            this.listUpdated(response.list);
        });
    }

    removeList(listId: string) {
        let request: any = {
            ListId: listId
        };

        this.sender.postAuthJson(request, this.removeListUrl).subscribe(response => {
            this.processError(response);

            this.deleteList(response.listId);
        });
    }

    addNewListItem(listId:string,itemContent: string, afterListItemId:string) {
        let request: any = {
            Content: itemContent,
            ListId: listId,
            AfterListItemId: afterListItemId
        };

        this.sender.postAuthJson(request, this.saveNewListItemUrl).subscribe(response => {
            this.processError(response);
            
            if (response.listItem) {
                this.addListItem(listId, afterListItemId, response.listItem);
            };
        });
    }

    updateListItem(listId: string, listItemId: string, itemContent: string) {
        let request: any = {
            Content: itemContent,
            ListId: listId,
            ListItemId: listItemId
        };

        this.sender.postAuthJson(request, this.updateListItemUrl).subscribe(response => {
            this.processError(response);


            if (response.listItem) {
                this.listItemUpdated(listId, listItemId,response.listItem);
            };
        });
    }

    removeListItem(listId: string, listItemId: string) {
        let request: any = {
            ListId: listId,
            ListItemId: listItemId
        };

        this.sender.postAuthJson(request, this.removeListItemUrl).subscribe(response => {
            this.processError(response);

            if (response.listItemId) {
                this.listItemRemoved(listId, listItemId);
            };
        });
    }

    swapItems(targetListId: string, itemId: string, afterItemId: string, copy: boolean) {
        if (StringExtentions.compareOrdinalIgnoreCase(itemId, afterItemId)) {
            return;
        }

        let request: any = {
            TargetListId: targetListId,
            ListItemId: itemId,
            AfterListItemId: afterItemId,
            Copy: copy
        };

        this.sender.postAuthJson(request, this.moveToListItemUrl).subscribe(response => {
            this.processError(response);

            if (response.listItemId) {
                this.listItemsSwapped(targetListId, itemId, afterItemId, copy, response.listItem);
            };
        });
    }

    updateListKind(listId: string, listKind: number) {
        let request: any = {
            ListId: listId,
            ListKind: listKind
        };

        this.sender.postAuthJson(request, this.listKindUpdateUrl).subscribe(response => {
            this.processError(response);

            if (response.listId) {
                this.listKindChanged(response.listId, response.listKind);
            };
        });
    }

    updateListCheckItemKind(listId: string, listCheckItemKind: number) {
        let request: any = {
            ListId: listId,
            ListCheckItemKind: listCheckItemKind
        };

        this.sender.postAuthJson(request, this.listCheckItemKindUrl).subscribe(response => {
            this.processError(response);

            if (response.listId) {
                this.listCheckItemKindChanged(response.listId, response.listCheckItemKind);
            };
        });
    }

    updateListItemCheck(listId: string, listItemId: string, isChecked: boolean) {
        let request: any = {
            IsChecked: isChecked,
            ListId: listId,
            ListItemId: listItemId
        };

        this.sender.postAuthJson(request, this.updateListItemCheckUrl).subscribe(response => {
            this.processError(response);
            if (response.listItem) {
                this.listItemCheckUpdated(listId, listItemId, response.listItem);
            };
        });
    }

    updateListIsPublished(listId: string, isPublished: boolean) {
        let request: any = {
            ListId: listId,
            IsPublished: isPublished
        };

        this.sender.postAuthJson(request, this.listIsPublishedUpdateUrl).subscribe(response => {
            this.processError(response);

            if (response.listId) {
                this.listIsPublishedChanged(response.listId, response.isPublished);
            };
        });
    }
}
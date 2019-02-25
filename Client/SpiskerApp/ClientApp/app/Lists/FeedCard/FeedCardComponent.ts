import { Component, OnInit, OnDestroy, ChangeDetectorRef, Input  } from '@angular/core';

import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import {FeedListService} from "../FeedCardService/FeedListService";
import {FeedKind} from "./FeedKind";
import {ListViewDialogService} from "../ListViewDialog/ListViewDialogService";

import {FeedEvent} from "../FeedCardService/FeedEvent";
import {FeedEventType} from "../FeedCardService/FeedEventType";
import {ListModel} from "../Models/ListModel";
import {ListItemModel} from "../Models/ListItemModel";

@Component({
    selector: 'feed-card',
    template: `
<div class="card">
    <form class="new-message" [formGroup]="editForm" novalidate>
        <div *ngIf="showAddListInput" class="input-group">
            <input formControlName="listNameInput" type="text" class="form-control" placeholder="Название нового списка…" />
            <span class="input-group-addon">                
                <a *ngIf="!editForm.invalid" href="" data-toggle="tooltip" data-placement="bottom"  title="Создать новый" (click)="addNew($event)">
                    <span class="glyphicon glyphicon glyphicon-plus" aria-hidden="true" >
                    </span>
                </a>               
                <span *ngIf="editForm.invalid" class="glyphicon glyphicon-th-list" aria-hidden="true" title="Введите название для списка">

                </span>
            </span>

        </div>
        <div *ngIf="!showAddListInput" class="empty-lists">
        <em *ngIf="this.lists.length===0">
            Нет списков для отображения
        </em>
        </div>
    </form>
    <ul id="feed" class="feed list-unstyled">
        <list-view *ngFor="let list of lists" [list]="list" [showShare]="true"></list-view>   
        <list-button *ngIf="hasMoreRows" [title]="'Показать еще'" (buttonClick)="nextFeedClick()"></list-button>
    </ul>
</div>
<list-view-dialog></list-view-dialog>
`
})
export class FeedCardComponent implements OnInit, OnDestroy {
    lists: Array<ListModel> = [];

    @Input()
    showAddListInput:boolean;

    editForm: FormGroup;

    hasMoreRows: boolean;

    private feedKind: FeedKind;

    private lastListId:string;

    private userName:string;

    private listAddedSubject:any;

    private listUpdatedSubject: any;

    private listsCleanedSubject: any;

    private listRemovedSubject: any;

    private listItemsSwappedSubject: any;

    private listsFetchedSubject:any;

    constructor(private formBuilder: FormBuilder, private feedService: FeedListService, private dialogService: ListViewDialogService,private ref: ChangeDetectorRef) {
        
        this.editForm = formBuilder.group({
            "listNameInput": ["", [Validators.required, Validators.maxLength(100)]]
        });

        this.hasMoreRows = false;

        this.lastListId = null;

        this.userName = null;

        this.feedKind = FeedKind.Unknown;
    }

    ngOnInit(): void {

        this.listsCleanedSubject = this.feedService.listFeed.filter((e: FeedEvent) => e.eventType === FeedEventType.ListsCleaned).subscribe(e => {
            this.lists.length = 0;
        });

        this.listsFetchedSubject = this.feedService.listFeed.filter((e: FeedEvent) => e.eventType === FeedEventType.ListsFetched)
            .map((e: FeedEvent) => e.targetItem).subscribe(data => {

                for (var i = 0; i < data.lists.length; i++) {
                    ListModel.sortItems(data.lists[i]);
                    this.lists.push(data.lists[i]);
                }

                this.hasMoreRows = data.hasMore;
                this.lastListId = data.lastListId;
                this.userName = data.userName;
                this.feedKind = <FeedKind>data.feedKind;

                if (data.selectedListId) {
                    let list: ListModel = this.findList(data.selectedListId);
                    if (list != null) {
                        this.dialogService.showDialog(list, null);
                    }
                }
            });

        this.listAddedSubject = this.feedService.listFeed.filter((e: FeedEvent) => e.eventType === FeedEventType.ListAdded)
            .map((e: FeedEvent) => e.targetItem).subscribe(list => {
                ListModel.sortItems(list);
                this.lists.splice(0, 0, list);
            });

        this.listUpdatedSubject = this.feedService.listFeed.filter((e: FeedEvent) => e.eventType === FeedEventType.ListUpdated)
            .map((e: FeedEvent) => e.targetItem).subscribe((list: ListModel) => {
                let index: number = this.findIndexInList(list.id);
                if (index >= 0) {
                    this.lists[index] = list;
                }
            });
        
        this.listRemovedSubject = this.feedService.listFeed.filter((e: FeedEvent) => e.eventType === FeedEventType.ListRemoved)
            .map((e: FeedEvent) => e.targetId).subscribe((id: string) => {
                let index: number = this.findIndexInList(id);
                if (index >= 0) {
                    this.lists.splice(index, 1);
                }
            });

        this.listItemsSwappedSubject = this.feedService.listFeed.filter((e: FeedEvent) => e.eventType === FeedEventType.ListItemsSwapped)
            .map((e: FeedEvent) => e.targetItem).subscribe((item: any) => {
                this.swapItems(item.itemId, item.afterItemId, item.targetListId, item.copiedItem, item.copy);
            });
    }

    ngOnDestroy(): void {
        this.listsCleanedSubject.unsubscribe();
        this.listUpdatedSubject.unsubscribe();
        this.listAddedSubject.unsubscribe();
        this.listRemovedSubject.unsubscribe();
        this.listItemsSwappedSubject.unsubscribe();
        this.listsFetchedSubject.unsubscribe();
    }

    nextFeedClick() {
        if (this.feedKind===FeedKind.Feed) {
            this.feedService.userFeed(this.lastListId);
        } else if(this.feedKind === FeedKind.UserLists) {
            this.feedService.fetchUserList(this.userName, this.lastListId);
        }
        
    }

    private findIndexInList(id: string):number {
        return this.lists.findIndex(el => el.id === id);
    }

    private findList(id: string): ListModel {
        let index = this.lists.findIndex(el => el.id === id);
        if (index>=0) {
            return this.lists[index];
        }

        return null;
    }

    private findListByItemId(id: string): ListModel {
        for (var i = 0; i < this.lists.length; i++) {
            if (ListModel.findIndexInList(this.lists[i],id)>=0) {
                return this.lists[i];
            }
        }
        return null;
    }

    addNew(event: Event) {
        event.preventDefault();
        this.feedService.addNewList(this.editForm.controls["listNameInput"].value);
        this.editForm.controls["listNameInput"].setValue("");
    }

    private swapItems(itemId: string, afterItemId: string, targetListId: string, copiedItem: ListItemModel, copy: boolean) {
        
        if (copy && (copiedItem==null || copiedItem===undefined)) {
            return;
        }

        let toList = this.findList(targetListId);

        let fromList: ListModel;

        if (toList != null) {

            let item = ListModel.findItemInList(toList, itemId);

            if (item==null) {
                fromList = this.findListByItemId(itemId);
                if (fromList!=null) {
                    item = ListModel.findItemInList(fromList,itemId);    
                }
            }

            if (item == null) {
                return;
            }

            if (copy) {
                ListModel.addItemAfterItemId(toList,afterItemId,copiedItem);
            } else {
                ListModel.addItemAfterItemId(toList, afterItemId, item);
                ListModel.positionByIndex(toList);

                if (fromList!=null) {
                    ListModel.removeItemInList(fromList, item.id);
                    ListModel.positionByIndex(fromList);
                }
            }

            this.ref.detectChanges();
        }
    }
} 

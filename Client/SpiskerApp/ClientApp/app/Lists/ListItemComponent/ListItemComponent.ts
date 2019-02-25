import { Component, Input, OnInit, Output, EventEmitter, OnDestroy } from '@angular/core';

import {ListItemComponentState} from "./ListItemComponentState";
import {ListItemModel} from "../Models/ListItemModel";

@Component({
    selector: 'list-item',
    template: `
    <div [ngSwitch]="state">
        <div *ngSwitchCase="listItemState.Edit">
            <list-item-edit [item]="item" [listId]="listId" (complete)="onEditCompleted($event)"></list-item-edit>
        </div>
        <div *ngSwitchDefault>
           <list-item-view [item]="item" [listId]="listId" [listKind]="listKind" [checkItemKind]="checkItemKind" (editRequest)="onEditRequest()"></list-item-view>
        </div>
    </div>
`

})
export class ListItemComponent implements OnInit, OnDestroy {
    @Input()
    item: ListItemModel;

    @Input()
    listId: string;

    @Input()
    listKind: number;

    @Input()
    checkItemKind: number;

    listItemState = ListItemComponentState;

    state: ListItemComponentState;

    @Output()
    removeItem: EventEmitter<any> = new EventEmitter<any>();

    ngOnInit(): void {
        if (this.item.isInserted) {
            this.state = ListItemComponentState.Edit;    
        } else {
            this.state = ListItemComponentState.View;
        }
        
    }

    ngOnDestroy(): void {
        
    }

    onEditRequest() {
        this.state = ListItemComponentState.Edit;
    }

    onEditCompleted(wasInserted: boolean) {
        this.state = ListItemComponentState.View;
        if (wasInserted) {
            this.removeItem.emit(true);
        }
    }
} 

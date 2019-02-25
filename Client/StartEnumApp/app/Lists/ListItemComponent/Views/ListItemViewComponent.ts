
import { Component, Input, Output, EventEmitter, OnInit, ViewChild, OnChanges, SimpleChanges, AfterViewChecked, ElementRef  } from '@angular/core';
import {FeedListService} from "../../FeedCardService/FeedListService";
import {ListItemModel} from "../../Models/ListItemModel";

declare var linkifyElement: any;

@Component({
    selector: 'list-item-view',
    template: `
<div (click)="toogleShowControls($event)" list-item-draggable draggable="true" class="list-item-content" [item]="item"
     >    

    <a *ngIf="checkItemKind!==1" href="" class="no-focus-border" (click)="toggleCheck($event)"><span class="glyphicon" [ngClass]="{'glyphicon-check': item.isChecked,'glyphicon-unchecked': !item.isChecked}"></span></a>
    <span *ngIf="listKind===2">&#9656;</span>

    <span *ngIf="listKind===3">
        <strong>{{item.orderPosition + 1}}.</strong>
    </span>
<span [ngClass]="{'selected-list-item':item.isChecked && checkItemKind===3}" #contentParagraph>
    {{item.content}}
</span>
</div>
<div *ngIf="showControls" class="action-list">
                <a (click)="startRemove($event)" href="#" data-toggle="tooltip" data-placement="bottom" title="Удалить" *iFOwnedUser="item.ownerId" >
                    <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                </a>
                <a (click)="startEdit($event)" href="#" data-toggle="tooltip" data-placement="bottom" title="Редактировать" *iFOwnedUser="item.ownerId" >
                    <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>            
                </a>
                <a (click)="toogleShowControls($event)" href="#" data-toggle="tooltip" data-placement="bottom" title="Отмена">
                    <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                </a>
            </div>
`
})
export class ListItemViewComponent implements OnInit, AfterViewChecked  {

    constructor(private feedService: FeedListService) {
        
    }

    @ViewChild("contentParagraph")
    contentElement:ElementRef;

    @Input()
    item: ListItemModel;

    @Input()
    listId: string;

    @Input()
    listKind: number;

    @Input()
    checkItemKind: number;

    @Output() editRequest: EventEmitter<any> = new EventEmitter<any>();

    showControls: boolean;

    private contentIsChanged:boolean;

    ngOnInit(): void {
        this.showControls = false;
        this.contentIsChanged = true;
    }

    ngAfterViewChecked(): void {
        if (this.contentIsChanged) {
            linkifyElement(this.contentElement.nativeElement, null, document);
            this.contentIsChanged = false;
        }
    }

    toogleShowControls(event: Event) {
        let target: any = event.target;
        //проверяем клики по ссылке
        if (target.localName !== "a") {
            event.preventDefault();
            this.showControls = !this.showControls;    
        }
        
    }

    startEdit(event: Event) {
        event.preventDefault();
        this.editRequest.emit(null);
    }

    startRemove(event: Event) {
        event.preventDefault();
        this.feedService.removeListItem(this.listId,this.item.id);
    }

    toggleCheck(event: Event) {
        event.preventDefault();
        event.stopPropagation();
        this.item.isChecked = !this.item.isChecked;
        this.feedService.updateListItemCheck(this.listId, this.item.id, this.item.isChecked);
    }
}
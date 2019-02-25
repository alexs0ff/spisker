import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import {AuthentificateService} from "../../Auth/AuthModule";
import {FeedListService} from "../FeedCardService/FeedListService";
import {ClipboardService, SubscriptionsContainer } from "../../Utils/UtilsModule";
import {RadioMenuModel, MenuItemModel, MenuItemDivider } from "../../Utils/Menu/MenuModule";
import {ListModel} from "../Models/ListModel";
import {FeedEvent} from "../FeedCardService/FeedEvent";
import {FeedEventType} from "../FeedCardService/FeedEventType";

@Component({
    selector: 'list-options',
    template: `
<div class="list-context-menu btn-group pull-right">
    <a type="button" class="dropdown-toggle" data-toggle="dropdown" aria-expanded="False" aria-haspopup="true">
        <span class="glyphicon glyphicon-option-vertical"></span>
    </a>
    <ul class="dropdown-menu">
        <liradio-menu class="menu-items" [model]="listKindMenuModel" (idChanged)="onListKindIdChanged($event)" *iFOwnedUser="list.ownerId"></liradio-menu>        
        <liradio-menu class="menu-items" [model]="listCheckItemKindMenuModel" (idChanged)="onListCheckItemKindIdChanged($event)" *iFOwnedUser="list.ownerId"></liradio-menu>        
        <li><a href=""><span class="glyphicon glyphicon-duplicate"></span> Копировать</a></li>
        <li><a href="" (click)="copyUrl($event)"><span class="glyphicon glyphicon-globe"></span> Ссылка</a></li>
    </ul>
</div>
  `
})
export class ListOptionsComponent implements OnInit, OnDestroy {

    @Input()
    list: ListModel;

    listKindMenuModel: RadioMenuModel;
    listCheckItemKindMenuModel: RadioMenuModel;

    private subjectContainer: SubscriptionsContainer = new SubscriptionsContainer();

    constructor(private authService: AuthentificateService, private feedService: FeedListService, private clipboardService: ClipboardService) {
       
    }

    ngOnInit(): void {
        this.listKindMenuModel = new RadioMenuModel();
        this.listKindMenuModel.addItem(new MenuItemModel("1", "Простой", true));
        this.listKindMenuModel.addItem(new MenuItemModel("2", "Маркер"));
        this.listKindMenuModel.addItem(new MenuItemModel("3", "Нумерация"));
        this.listKindMenuModel.addItem(new MenuItemDivider());

        if (this.list && this.list.listKind !== undefined) {
            this.listKindMenuModel.setChecked(this.list.listKind.toString());
        }


        this.listCheckItemKindMenuModel = new RadioMenuModel();
        this.listCheckItemKindMenuModel.addItem(new MenuItemModel("1", "Без выделения", true));
        this.listCheckItemKindMenuModel.addItem(new MenuItemModel("2", "Простой"));
        this.listCheckItemKindMenuModel.addItem(new MenuItemModel("3", "Зачеркнутый"));
        this.listCheckItemKindMenuModel.addItem(new MenuItemDivider());

        if (this.list && this.list.listCheckItemKind !== undefined) {
            this.listCheckItemKindMenuModel.setChecked(this.list.listCheckItemKind.toString());
        }

        let subject:any = this.feedService.listFeed.filter((e: FeedEvent) => e.eventType === FeedEventType.ListKindChanged && e.targetId === this.list.id)
            .map((e: FeedEvent) => e.targetItem).subscribe(data => {
                this.listKindMenuModel.setChecked(data.newKind);
            });

        this.subjectContainer.add(subject);

        subject = this.feedService.listFeed.filter((e: FeedEvent) => e.eventType === FeedEventType.ListCheckItemKindChanged && e.targetId === this.list.id)
            .map((e: FeedEvent) => e.targetItem).subscribe(data => {
                this.listCheckItemKindMenuModel.setChecked(data.newCheckItemKind);
            });

        this.subjectContainer.add(subject);
    }

    ngOnDestroy(): void {
        this.subjectContainer.clear();
    }

    onListKindIdChanged(newId: string) {
        this.feedService.updateListKind(this.list.id, +newId);
    }

    onListCheckItemKindIdChanged(newId: string) {
        this.feedService.updateListCheckItemKind(this.list.id, +newId);
    }

    copyUrl(event: Event) {
        event.preventDefault();
        let url = window.location.origin + '/' + this.list.ownerLogin + '/' + this.list.publicId;
        this.clipboardService.copyTextToClipboard(url);
    }
}
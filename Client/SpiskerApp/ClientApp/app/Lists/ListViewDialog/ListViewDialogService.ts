import { Injectable } from '@angular/core';
import { ListViewDialogEvent } from "./ListViewDialogEvent";
import { Subject } from "rxjs/rx";
import {ListViewDialogEventType} from "./ListViewDialogEventType";
import {ErrorFlowService} from "../../ErrorFlow/ErrorFlowModule";
import {ServiceBase} from "../../Utils/Services/ServiceBase";
import {ListModel} from "../Models/ListModel";

@Injectable()
export class ListViewDialogService extends ServiceBase {
    dialogFeed: Subject<ListViewDialogEvent>;
    constructor(private erServ: ErrorFlowService) {
        super(erServ);

        this.dialogFeed = new Subject<ListViewDialogEvent>();
    }

    showDialog(list: ListModel, backUrl:string) {
        let event = new ListViewDialogEvent();
        event.eventType = ListViewDialogEventType.Open;
        event.data = list;
        event.backUrl = backUrl;

        this.dialogFeed.next(event);
    }

    closeDialog() {
        let event = new ListViewDialogEvent();
        event.eventType = ListViewDialogEventType.Close;

        this.dialogFeed.next(event);
    }

    closedDialog(list: ListModel) {
        let event = new ListViewDialogEvent();
        event.eventType = ListViewDialogEventType.Closed;
        event.data = list;
        this.dialogFeed.next(event);
    }
}
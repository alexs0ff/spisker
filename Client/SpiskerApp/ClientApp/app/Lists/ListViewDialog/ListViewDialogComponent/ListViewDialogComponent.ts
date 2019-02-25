import { Component, Input, OnInit,OnDestroy, Output, EventEmitter } from '@angular/core';
import {ListViewDialogService} from "../ListViewDialogService";
import {ListViewDialogEvent} from "../ListViewDialogEvent";
import {ListViewDialogEventType} from "../ListViewDialogEventType";
import {ListModel} from "../../Models/ListModel";
import {StringExtentions} from "../../../Utils/Extentions/StringExtentions";
import * as jQuery from 'jquery';

@Component({
    selector: 'list-view-dialog',
    template: `
   
<div class="modal fade" id="list-view-dialog" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-label="close"><span>&times;</span></button>
                <h4 class="modal-title"><span class="glyphicon glyphicon-folder-open"></span> Просмотр списка</h4>
            </div>
            <div class="modal-body">
                <div class="container-fluid">       
                    <ul class="feed list-unstyled">
                        <list-view *ngIf="list" [list]="list" ></list-view> 
                    </ul>
                </div>
            </div>            
        </div>
    </div>
</div>
`

})
export class ListViewDialogComponent implements OnInit, OnDestroy {

    list:ListModel;

    private dialogSubject: any;

    private backUrl:string;

    constructor(private dialogService: ListViewDialogService) {
        
    }
   

    ngOnInit(): void {
        this.dialogSubject = this.dialogService.dialogFeed.subscribe((event: ListViewDialogEvent) => {
                if (event.eventType === ListViewDialogEventType.Open) {
                    this.list = <ListModel>event.data;
                    this.backUrl = event.backUrl;
                    this.showDialog();
                } else if (event.eventType === ListViewDialogEventType.Close) {
                    this.closeDialog();
                }
        });

        jQuery('#list-view-dialog').on('hidden.bs.modal',
            () => {
                this.onDialogClosed();
            });
    }

    ngOnDestroy(): void {
        this.dialogSubject.unsubscribe();
    }

    private onDialogClosed() {
        this.dialogService.closedDialog(this.list);

        if (!StringExtentions.isNullOrWhiteSpace(this.backUrl)) {
            if ("undefined" !== typeof window.history.pushState) {
                window.history.pushState("restore url" + this.backUrl, this.list.name, this.backUrl);
            }
        }
    }


    showDialog() {
        jQuery('#list-view-dialog').modal();
    }

    closeDialog() {
        jQuery('#list-view-dialog').modal('hide');
    }
} 

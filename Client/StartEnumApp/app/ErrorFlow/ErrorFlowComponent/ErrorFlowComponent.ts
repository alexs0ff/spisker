
import { Component, OnInit, OnDestroy } from '@angular/core';
import {ErrorFlowService} from "../ErrorFlowService";
import {ErrorFlowEvent} from "../ErrorFlowEvent";
import {ErrorFlowEventType} from "../ErrorFlowEventType";

@Component({
    selector: 'error-flow-view',
    template: `
<div *ngIf="alertIsVisible" class="alert alert-danger error-flow">
     <button type="button" class="close error-flow-close" aria-label="close" (click)="closeAlert()"><span  aria-hidden="true">&times;</span></button>     
     <strong>{{currentEvent.errorType | enumToString:errorEnum | i18nSelect:errorTitleMap}}</strong> {{currentEvent.description}}
     
 </div>
`
})
export class ErrorFlowComponent implements OnInit, OnDestroy {

    private errorRegistred: any;

    private currentEvent: ErrorFlowEvent;

    alertIsVisible:any;

    private timerId:any;

    private errorShowInterval:number = 5000;

    private errorEnum = ErrorFlowEventType;

    private errorTitleMap:any ={
        "Communication":"Сетевая ошибка",
        "System": "Системная ошибка",
        "other":"Ошибка"
    }

    constructor(private errorFlowService: ErrorFlowService) {
        this.timerId = -1;
    }

    private updateError(newError: ErrorFlowEvent) {
        this.currentEvent = newError;
    }

    ngOnInit(): void {
        this.errorRegistred = this.errorFlowService.errorFeed.subscribe(er => {
            this.updateError(er);
            this.showAlert();
        });
    }

    ngOnDestroy(): void {
        this.errorRegistred.unsubscribe();
    }

    private showAlert() {
        if (this.alertIsVisible) {
            this.cleanTimer();
        }
        this.alertIsVisible = true;
        this.timerId = setTimeout(() => { this.closeAlert(); }, this.errorShowInterval);
    }

    private closeAlert() {
        this.alertIsVisible = false;
        this.cleanTimer();
    }

    private cleanTimer() {
        if (this.timerId > 0) {
            clearTimeout(this.timerId);
            this.timerId = -1;
        }
    }
}
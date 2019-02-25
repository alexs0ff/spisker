import { Injectable } from '@angular/core';
import { Subject } from "rxjs/rx";
import { ErrorFlowEvent } from "./ErrorFlowEvent";
import { ErrorFlowEventType } from "./ErrorFlowEventType";

@Injectable()
export class ErrorFlowService {
    errorFeed: Subject<ErrorFlowEvent>;

    constructor() {
        this.errorFeed = new Subject<ErrorFlowEvent>();
    }

    private registerFlowError(code: number, description: string, data: any, errorType: ErrorFlowEventType) {
        let errorEvent = new ErrorFlowEvent();
        errorEvent.data = data;
        errorEvent.description = description;
        errorEvent.errorType = errorType;
        errorEvent.code = code;
        this.errorFeed.next(errorEvent);
    }

    registerCommunicationError(code: number, description: string, data: any) {
        this.registerFlowError(code, description, data, ErrorFlowEventType.Communication);
    }

    registerSystemError(code: number, description: string, data: any) {
        this.registerFlowError(code, description, data, ErrorFlowEventType.System);
    }
}
import { ErrorFlowEventType } from "./ErrorFlowEventType";

export class ErrorFlowEvent {
    errorType: ErrorFlowEventType;

    code: number;

    description: string;

    data: any;
}
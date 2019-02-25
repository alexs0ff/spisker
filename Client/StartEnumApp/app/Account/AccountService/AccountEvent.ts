
import { AccountEventType } from "./AccountEventType";

export class AccountEvent {

    eventType: AccountEventType;

    targetUserName: string;

    data: any;
}
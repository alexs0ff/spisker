import {FeedEventType} from "./FeedEventType";

export class FeedEvent {

    eventType: FeedEventType;

    targetId: string;

    parentId: string;

    afterId: string;

    targetItem:any;
}
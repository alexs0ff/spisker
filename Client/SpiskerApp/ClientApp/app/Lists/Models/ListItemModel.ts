export class ListItemModel {

    id: string;

    content: string;

    orderPosition: number;

    createEventTime: Date;

    editEventTime: Date;

    likeCount: number;

    ownerId: string;

    isInserted: boolean;

    isChecked: boolean;

    data: any;

    static compareByPosition(a: ListItemModel, b: ListItemModel):number {
        return a.orderPosition - b.orderPosition;
    }
}
import {ListItemModel} from "./ListItemModel";

export class ListModel {
    id: string;

    name: string;

    createEventTime: Date;

    likeCount: number;

    repostCount: number;

    items: Array<ListItemModel> = [];

    ownerId:string;

    ownerFullName: string;

    ownerLogin: string;

    originId: string;

    originFullName: string;

    originLogin: string;

    currentUserHasLike: boolean;

    listKind: number;

    listCheckItemKind: number;

    avatarUrl: string;

    publicId: number;

    isPublished: boolean;

    static sortItems(list: ListModel) {
        list.items.sort((a: ListItemModel, b: ListItemModel) => ListItemModel.compareByPosition(a,b));
    }

    static positionByIndex(list: ListModel) {
        for (var i = 0; i < list.items.length; i++) {
            list.items[i].orderPosition = i;
        }
    }

    static findIndexInList(list: ListModel,id: string): number {
        return list.items.findIndex(el => el.id === id);
    }

    static findItemInList(list: ListModel,id: string): ListItemModel {
        let index = ListModel.findIndexInList(list, id);
        if (index >= 0) {
            return list.items[index];
        }

        return null;
    }

    static removeItemInList(list: ListModel,id: string) {
        let index: number = ListModel.findIndexInList(list, id);
        if (index >= 0) {
            list.items.splice(index, 1);
        }
    }

    static replaceItemInList(list: ListModel,replacedItem: ListItemModel, newItem: ListItemModel) {
        let index: number = ListModel.findIndexInList(list, replacedItem.id);
        if (index >= 0) {
            list.items[index] = newItem;
        }
    }

    static addItem(list: ListModel,newItem: ListItemModel) {
        list.items.push(newItem);
    }

    static addItemAfter(list: ListModel,index:number, item: ListItemModel) {
        if (list.items.length<index) {
            ListModel.addItem(list, item);
        } else {
            list.items.splice(index, 0, item);
        }
    }

    static addItemAfterItemId(list: ListModel,afterItemId: string, item: ListItemModel) {
        var addingIndex = ListModel.findIndexInList(list, item.id);

        if (addingIndex>=0) {
            ListModel.removeItemInList(list,item.id);
        }

        let index = ListModel.findIndexInList(list, afterItemId);

        if (index >= 0) {
            ListModel.addItemAfter(list,index + 1, item);
        } else {
            ListModel.addItemAfter(list, 0, item);
        }
    }

    static getLastItemId(list: ListModel):string {
        if (list.items.length === 0) {
            return null;
        }

        return list.items[list.items.length - 1].id;
    }
}
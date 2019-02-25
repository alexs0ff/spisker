import { Injectable } from '@angular/core';
import {FeedListService} from "../FeedCardService/FeedListService";
import {ListItemModel} from "../Models/ListItemModel";
import {StringExtentions} from "../../Utils/Extentions/StringExtentions";

@Injectable()
export class ListItemDragAndDropService{

    private textDataTypeConst = "text/plain";

    get textDataType(): string { return this.textDataTypeConst; }

    constructor(private feedService: FeedListService) {
        this.item = null;
    }

    private item: ListItemModel;

    private copy:boolean;

    startDrag(item: ListItemModel, copy:boolean) {
        this.item = item;
        this.copy = copy;
    }

    complete(content: string, listId: string, targetItem: ListItemModel) {
        if (StringExtentions.isNullOrWhiteSpace(content) || StringExtentions.isNullOrWhiteSpace(listId)) {
            return;
        }

        let tragetItemId: string = null;

        if (targetItem != null) {
            tragetItemId = targetItem.id;
        }

        //Если пункт не определен или контенты не равны, тогда добавляем пункт списка
        if (this.item == null || !StringExtentions.compareOrdinalIgnoreCase(this.item.content, content)) {
            this.feedService.addNewListItem(listId, content, tragetItemId);
            return;
        }
        

        if (this.item!=null) {
            this.feedService.swapItems(listId, this.item.id, tragetItemId, this.copy);
        }
    }
}
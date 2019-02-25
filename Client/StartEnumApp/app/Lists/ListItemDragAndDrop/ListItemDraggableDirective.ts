import { Input, Directive, HostListener } from '@angular/core';
import {ListItemDragAndDropService} from "./ListItemDragAndDropService";
import {ListItemModel} from "../Models/ListItemModel";

@Directive(
    {
        selector: '[list-item-draggable]'
    })
export class ListItemDraggableDirective {

    @Input()
    item: ListItemModel;

    constructor(private dropSrvice: ListItemDragAndDropService) {
        
    }
   
    @HostListener('dragstart', ['$event'])
    onDragStart(event: DragEvent) {
        if (this.item ==null || this.item==undefined) {
            return;
        }
        let copy = false;
        if (event.ctrlKey) {
            event.dataTransfer.effectAllowed = 'copyMove';
            copy = true;
        } else {
            event.dataTransfer.effectAllowed = 'move';
        }

        
        //Для FF
        event.dataTransfer.setData(this.dropSrvice.textDataType, this.item.content);
        this.dropSrvice.startDrag(this.item, copy);
    }
}
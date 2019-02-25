import { Component, Input, OnInit, HostListener } from '@angular/core';
import {ListItemDragAndDropService} from "../ListItemDragAndDrop/ListItemDragAndDropService";
import {ListItemModel} from "../Models/ListItemModel";

@Component({
    selector: 'list-item-splitter',
    template: `
<div [ngClass]="{'listing-item-splitter':true,'listing-item-splitter-expanded':expanded}">
</div>
`
})
export class ListItemSplitterComponent implements OnInit {

    expanded: boolean;

    @Input()
    item: ListItemModel;

    @Input()
    listId: string;

    @Input()
    userCanDrop: boolean;

    constructor(private dropSrvice: ListItemDragAndDropService) {

    }

    ngOnInit(): void {

    }

    private processDropData(data: string) {
        this.dropSrvice.complete(data,this.listId, this.item);
    }
  
    @HostListener('dragenter', ['$event'])
    onDragEnter(event: any) {
        event.preventDefault();
        if (this.userCanDrop) {
            this.expanded = true;    
        }
        return true;
    }

    @HostListener('dragover', ['$event'])
    onDragOver(event: any) {
        event.preventDefault();
    }

    @HostListener('drop', ['$event'])
    onDrop(event: any) {
        event.preventDefault();
        if (!this.userCanDrop) {
            return;
        }
        this.expanded = false;

        let data:any = event.dataTransfer.items;
        if (data) {
            for (var i = 0; i < data.length; i += 1) {
                if ((data[i].kind == 'string') &&
                    (data[i].type.match('^text/plain'))) {
                    // This item is the target node
                    data[i].getAsString((s:any) => {
                        this.processDropData(s);
                    });
                } else if ((data[i].kind == 'string') &&
                    (data[i].type.match('^text/html'))) {
                    //data[i].getAsString(s => {
                    //    alert(s);
                    //});
                } else if ((data[i].kind == 'string') &&
                    (data[i].type.match('^text/uri-list'))) {
                    // Drag data item is URI
                } else if ((data[i].kind == 'file')) {
                    // Drag data item is an image file
                    //var f = data[i].getAsFile();
                }
            }
        } else {
            if (event.dataTransfer) {
                let value = event.dataTransfer.getData('text/plain');
                if (value) {
                    this.processDropData(value);
                }
            }
        }
    }

    @HostListener("dragleave")
    onMouseOut() {
        this.expanded = false;
    }
}
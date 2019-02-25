
import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import {ListModel} from "../../Models/ListModel";

@Component({
    selector: 'list-name-view',
    template: `
<p class="list-name">{{list.name}}</p>
`
})
export class ListNameViewComponent implements OnInit {

    @Input()
    list: ListModel;

    ngOnInit(): void {
        
    }
}
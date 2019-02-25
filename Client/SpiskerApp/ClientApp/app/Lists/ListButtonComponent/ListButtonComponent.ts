import { Component, EventEmitter, OnInit, Output, Input } from '@angular/core';


@Component({
    selector: 'list-button',
    template: `
    <li class="startenum-list-button" (click)="handleClick()">     
    {{title}}
 </li>
`

})
export class ListButtonComponent implements OnInit {

    @Input()
    title: string;

    @Output()
    buttonClick:EventEmitter<boolean>;

    constructor() {
        this.buttonClick = new EventEmitter<boolean>();
    }

    ngOnInit(): void {
        
    }

    handleClick() {
        this.buttonClick.emit(true);
    }
    
} 

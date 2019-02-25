import { Component, OnInit, OnDestroy, Input, EventEmitter,Output } from '@angular/core';
import {RadioMenuModel} from "../RadioMenuModel";

@Component({
    selector: 'liradio-menu',
    template: `

       <li *ngFor="let item of model.items" [ngClass]="{'divider':item.type===0}" [attr.role]="item.type===0 ? 'separator': null">

        <a *ngIf="item.type===1" href="" (click)="selectItem($event,item.id)"><span class="glyphicon" [ngClass]="{'glyphicon-ok':item.checked,'glyphicon-asterisk':!item.checked}"></span> {{item.name}}</a>
</li>      
  `,
    styles: [`
        a {
        clear: both;
        color: #333;
        display: block;
        font-weight: 400;
        line-height: 1.42857;
        padding: 3px 20px;
        white-space: nowrap;
    }
    
    a:focus, a:hover {
        color: #262626;
        text-decoration: none;
        background-color: #f5f5f5;
    }
`]
})
export class LiRadioMenuComponent implements OnInit, OnDestroy {

    @Input()
    model: RadioMenuModel;

    @Output()
    idChanged:EventEmitter<string> = new EventEmitter<string>();

    ngOnInit(): void {
        
    }

    ngOnDestroy(): void {
        
    }

    selectItem(event:Event,id:string) {
        event.preventDefault();
        if (!this.model.isChecked(id)) {
            this.model.setChecked(id);    
            this.idChanged.emit(id);
        }
    }
}
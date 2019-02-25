
import { Component, Input,Output,EventEmitter, OnInit,OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import {FeedListService} from "../../FeedCardService/FeedListService";
import {ListItemModel} from "../../Models/ListItemModel";

@Component({
    selector: 'list-item-edit',
    template: `
<form [formGroup]="editForm" novalidate>
    <div class="input-group" >
        <textarea name="content" formControlName="contentInput" class="form-control" rows="3" style="resize:none" placeholder="Еще один пункт" maxlength=500 set-item-focus></textarea>                
        <span class="input-group-addon glyphicon-vertical">
            <a *ngIf="!editForm.invalid" href="#" data-toggle="tooltip" data-placement="bottom"  title="Сохранить" (click)="save($event)">
                <span class="glyphicon glyphicon-ok" aria-hidden="true" >
                </span>
            </a>
            <a href="#" data-toggle="tooltip" data-placement="bottom"  title="Отмена" (click)="cancel($event)">
                <span class="glyphicon glyphicon-remove" aria-hidden="true">
                </span>
            </a>
        </span>

    </div>
</form>
`
})
export class ListItemEditComponent implements OnInit{

    @Input()
    item: ListItemModel;

    @Input()
    listId: string;

    //Вызывается когда пользователь закончил редактирование пункта меню.
    @Output() complete: EventEmitter<boolean> = new EventEmitter<boolean>();
    

    editForm: FormGroup;

    ngOnInit(): void {
        this.editForm.controls["contentInput"].setValue(this.item.content);
    }
    

    constructor(private formBuilder: FormBuilder, private feedService: FeedListService) {
        this.editForm = formBuilder.group({
            "contentInput": ["", [Validators.required]]
        });
    }

    cancel(event: Event) {
        event.preventDefault();
        var wasInserted = this.item.isInserted;
        this.complete.emit(wasInserted);
    }

    save(event: Event) {
        event.preventDefault();
        let value = this.editForm.controls["contentInput"].value;
        if (this.item.isInserted) {
            this.feedService.addNewListItem(this.listId, value, this.item.data);
        } else {
            this.feedService.updateListItem(this.listId, this.item.id, value);
        }
        var wasInserted = this.item.isInserted;
        this.complete.emit(wasInserted);
    }
}
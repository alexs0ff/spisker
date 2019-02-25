
import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FeedListService } from "../../FeedCardService/FeedListService";
import {ListModel} from "../../Models/ListModel";

@Component({
    selector: 'list-name-edit',
    template: `
<form [formGroup]="editForm" novalidate>
    <div class="input-group" >
        <textarea name="content" formControlName="listNameInput" class="form-control" rows="2" style="resize:none" placeholder="Еще один пункт" maxlength=100 set-item-focus></textarea>                
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
export class ListNameEditComponent implements OnInit {

    @Input()
    list: ListModel;

    //Вызывается когда пользователь закончил редактирование названия меню.
    @Output() complete: EventEmitter<boolean> = new EventEmitter<boolean>();


    editForm: FormGroup;
    ngOnInit(): void {
        this.editForm.controls["listNameInput"].setValue(this.list.name);
    }

    constructor(private formBuilder: FormBuilder, private feedService: FeedListService) {
        this.editForm = formBuilder.group({
            "listNameInput": ["", [Validators.required,Validators.maxLength(100)]]
        });
    }

    cancel(event: Event) {
        event.preventDefault();
        this.complete.emit(false);
    }

    save(event: Event) {
        event.preventDefault();
        this.complete.emit(true);
        this.feedService.updateList(this.list.id, this.editForm.controls["listNameInput"].value);
    }
}
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

import { AuthentificateService } from "../../../Auth/AuthModule";
import {FormBase} from "../../../Utils/Forms/FormBase";
import {StringExtentions} from "../../../Utils/Extentions/StringExtentions";
import {AccountService} from "../../AccountService/AccountService";
import {AccountEvent} from "../../AccountService/AccountEvent";
import {AccountEventType} from "../../AccountService/AccountEventType";

@Component({
    selector: 'change-status-text',
    template: `
<form class="form-horizontal" [formGroup]="statusTextForm" (ngSubmit)="submit()" novalidate>
    <div class="form-group">
        <label class="col-sm-3 control-label">Статус</label>
         <div class="col-sm-9">
            <textarea cols="3" style="resize:none" class="form-control" name="statusText" formControlName="statusText" placeholder="Статус"></textarea>
            <div *ngIf="formErrors.oldPassword" class="alert alert-danger input-error-msg">
                {{ formErrors.oldPassword }}
            </div>
        </div>
    </div>    
    
    <div class="form-group">                            
        <div class="col-sm-offset-3 col-sm-9">
            <button type="submit" class="btn btn-primary" [disabled]="statusTextForm.invalid">Изменить</button>
        </div>
    </div>

    <div class="row"  *ngIf="hasStatusTextChanged" >
           
    <div class="alert alert-info">
        <h3>Статус успешно изменен</h3>               
    </div>
        
    </div>
</form>
  `
})
export class ChangeStatusTextComponent extends FormBase implements OnInit, OnDestroy {

    statusTextForm: FormGroup;

    constructor(private formBuilder: FormBuilder, private accountService: AccountService, private authService: AuthentificateService) {
        super();
    }

    private statusTextChangedSubject: any;

    hasStatusTextChanged: boolean;

    ngOnInit(): void {
        this.hasStatusTextChanged = false;

        this.statusTextForm = this.formBuilder.group({
            "statusText": [""]
        });

        this.formErrors = {
            'statusText': ''
        };

        this.validationMessages = {
            'statusText': {
                'required': 'Статус обязателен'
            }
        };

        this.setForm(this.statusTextForm);

        this.statusTextChangedSubject = this.accountService.accountFeed.filter((e: AccountEvent) => e.eventType === AccountEventType.StatusTextChanged)
            .subscribe((event: AccountEvent) => {
                if (StringExtentions.compareOrdinalIgnoreCase(event.targetUserName, this.authService.userName)) {
                    this.hasStatusTextChanged = true;
                }
            });
    }

    ngOnDestroy(): void {
        this.statusTextChangedSubject.unsubscribe();
    }

    submit(): void {
        this.hasStatusTextChanged = false;
        this.accountService.changeStatusText(this.authService.userName, this.getFormValue("statusText"));
    }

    setCurrentStatusText(statusText: string) {
        this.setFormValue("statusText",statusText);
    }
}
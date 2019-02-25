import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import {AuthentificateService} from "../../../Auth/AuthModule";
import {FormBase} from "../../../Utils/Forms/FormBase";
import {AccountService} from "../../AccountService/AccountService";
import {AccountSettingsModel} from "../../AccountService/AccountSettingsModel";

@Component({
    selector:'user-settings',
    template: `
<form class="form-horizontal" [formGroup]="accountForm" (ngSubmit)="submit()" novalidate>
    <div class="form-group">
        <label class="col-sm-3 control-label">Фамилия</label>
        <div class="col-sm-9">
            <input type="text" class="form-control" name="lastName" formControlName="lastName"/>
            <div *ngIf="formErrors.lastName" class="alert alert-danger input-error-msg">
                {{ formErrors.lastName }}
            </div>
        </div>
    </div>    
    <div class="form-group">
        <label class="col-sm-3 control-label">Имя</label>
        <div class="col-sm-9">
            <input type="text" class="form-control" name="firstName" formControlName="firstName"/>
            <div *ngIf="formErrors.firstName" class="alert alert-danger input-error-msg">
                {{ formErrors.firstName }}
            </div>
        </div>
    </div>    

    <div class="form-group">
        <label class="col-sm-3 control-label">Отчество</label>
        <div class="col-sm-9">
            <input type="text" class="form-control" name="middleName" formControlName="middleName"/>
            <div *ngIf="formErrors.middleName" class="alert alert-danger input-error-msg">
                {{ formErrors.middleName }}
            </div>
        </div>
    </div>    
                   
    <div class="form-group">
        <label class="col-sm-3 control-label">Email</label>
        <div class="col-sm-9">
            <input type="email" class="form-control" name="email" formControlName="email"/>
            <div *ngIf="formErrors.email" class="alert alert-danger input-error-msg">
                {{ formErrors.email }}
            </div>
        </div>
    </div>
    <div class="form-group">
                            
        <div class="col-sm-offset-3 col-sm-9">
            <button type="submit" class="btn btn-primary" [disabled]="accountForm.invalid">Сохранить</button>
        </div>
    </div>
</form>
  `
})
export class UserSettingsComponent extends FormBase implements OnInit, OnDestroy {

    accountForm: FormGroup;

    constructor(private formBuilder: FormBuilder, private accountService: AccountService, private authService: AuthentificateService ) {
        super();
    }

    ngOnInit(): void {
        this.accountForm = this.formBuilder.group({
            "lastName": ["", [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
            "firstName": ["", [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
            "middleName": [""],
            "email": ["", [Validators.required, Validators.email]]
        });

        this.formErrors = {
            'lastName': '',
            'firstName': '',
            'middleName': '',
            'email': ''
        };

        this.validationMessages = {
            'firstName': {
                'required': 'Имя пользователя обязательно.',
                'minlength': 'Имя пользователя должна составлять минимум 1 символ.',
                'maxlength': 'Имя пользователя должна составлять максимум 100 символов.'
            },
            'lastName': {
                'required': 'Фамилия пользователя обязательно.',
                'minlength': 'Фамилия пользователя должна составлять минимум 1 символ.',
                'maxlength': 'Фамилия пользователя должна составлять максимум 100 символов.'
            },
            'email': {
                'required': 'Email пользователя обязателен.',
                'email': 'Формат email ошибочен.'
            }
        };

        this.setForm(this.accountForm);
    }

    ngOnDestroy(): void {
         
    }

    submit(): void {
        let model = new AccountSettingsModel();
        model.email = this.getFormValue("email");
        model.firstName = this.getFormValue("firstName");
        model.lastName = this.getFormValue("lastName");
        model.middleName = this.getFormValue("middleName");

        this.accountService.updateAccountSettings(this.authService.userName, model);
    }

    setData(data: AccountSettingsModel) {
        this.setFormValue("firstName", data.firstName);
        this.setFormValue("lastName", data.lastName);
        this.setFormValue("middleName", data.middleName);
        this.setFormValue("email", data.email);
    }
}
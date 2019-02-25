import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AuthentificateService } from "../../../Auth/AuthModule";
import {AccountService} from "../../AccountService/AccountService";
import {FormBase} from "../../../Utils/Forms/FormBase";
import {fieldsAreEqualValidator} from "../../../Utils/Validators/fieldsAreEqualValidator";
import {StringExtentions} from "../../../Utils/Extentions/StringExtentions";
import {FormsExtentions} from "../../../Utils/Forms/FormsExtentions";
import {AccountEvent} from "../../AccountService/AccountEvent";
import {AccountEventType} from "../../AccountService/AccountEventType";

@Component({
    selector: 'change-password',
    template: `
<form class="form-horizontal" [formGroup]="passwordForm" (ngSubmit)="submit()" novalidate>
    <div class="form-group">
        <label class="col-sm-3 control-label">Старый пароль</label>
         <div class="col-sm-9">
            <input type="password" class="form-control" name="oldPassword" formControlName="oldPassword" placeholder="Старый пароль"/>
            <div *ngIf="formErrors.oldPassword" class="alert alert-danger input-error-msg">
                {{ formErrors.oldPassword }}
            </div>
        </div>
    </div>
    <div class="form-group">  
        <label class="col-sm-3 control-label">Новый пароль</label>
        <div class="col-sm-9">
        <input type="password" class="form-control" name="newPassword" formControlName="newPassword" placeholder="Новый пароль"/>
        <div *ngIf="formErrors.newPassword" class="alert alert-danger input-error-msg">
            {{ formErrors.newPassword }}
        </div>
    </div>
    </div>

    <div class="form-group">  
        <label class="col-sm-3 control-label">Повторение пароля</label>
        <div class="col-sm-9">
        <input type="password" class="form-control" name="passwordCopy" formControlName="passwordCopy" placeholder="Повторите пароль"/>
        <div *ngIf="formErrors.passwordCopy" class="alert alert-danger input-error-msg">
            {{ formErrors.passwordCopy }}
        </div>
    </div>
    </div>
    <div class="form-group">                            
        <div class="col-sm-offset-3 col-sm-9">
            <button type="submit" class="btn btn-primary" [disabled]="passwordForm.invalid">Изменить</button>
        </div>
    </div>

    <div class="row"  *ngIf="hasPasswordChanged" >
           
    <div class="alert alert-info">
        <h4>Пароль успешно изменен</h4>               
    </div>
        
    </div>
</form>
  `
})
export class ChangePasswordComponent extends FormBase implements OnInit, OnDestroy {

    passwordForm: FormGroup;

    constructor(private formBuilder: FormBuilder, private accountService: AccountService, private authService: AuthentificateService) {
        super();
    }

    private changePasswordErrorSubject: any;

    private passwordChangedSubject: any;

    hasPasswordChanged:boolean;

    ngOnInit(): void {
        this.hasPasswordChanged = false;

        this.passwordForm = this.formBuilder.group({
            "oldPassword": ["", [Validators.required, Validators.minLength(8)]],
            "newPassword": ["", [Validators.required, Validators.minLength(8)]],
            "passwordCopy": ["", [Validators.required, fieldsAreEqualValidator("newPassword")]]
        });

        this.formErrors = {
            'oldPassword': '',
            'newPassword': '',
            'passwordCopy': ''
        };

        this.validationMessages = {
            'oldPassword': {
                'required': 'Пароль обязателен',
                'minlength': 'Пароль должен составлять не менее 8 символов.',
                '104': "Пароль должен иметь латинские буквы, разный регистр и цифры",
                '109': "Ошибочный пароль"
            },
            'newPassword': {
                'required': 'Пароль обязателен',
                'minlength': 'Пароль должен составлять не менее 8 символов.',
                '108': "Пароль должен иметь латинские буквы, разный регистр и цифры"
            },
            'passwordCopy': {
                'required': 'Повторение пароля обязателено',
                'confirmPassword': 'Пароли должны быть одинаковыми.',
                'key': 'Пароли должны быть одинаковыми.',
                '108': 'Пароль должен иметь латинские буквы, разный регистр и цифры',
            }
        };

        this.setForm(this.passwordForm);

        this.changePasswordErrorSubject = this.accountService.accountFeed.filter((e: AccountEvent) => e.eventType === AccountEventType.PasswordChangeError)
            .subscribe((event: AccountEvent) => {
                if (StringExtentions.compareOrdinalIgnoreCase(event.targetUserName, this.authService.userName)) {
                    this.processErrors(event.data.errors);
                }
            });

        this.passwordChangedSubject = this.accountService.accountFeed.filter((e: AccountEvent) => e.eventType === AccountEventType.PasswordChanged)
            .subscribe((event: AccountEvent) => {
                if (StringExtentions.compareOrdinalIgnoreCase(event.targetUserName, this.authService.userName)) {
                    this.hasPasswordChanged = true;
                    this.setFormValue("oldPassword","");
                }
            });
    }

    ngOnDestroy(): void {
        this.changePasswordErrorSubject.unsubscribe();
        this.passwordChangedSubject.unsubscribe();
    }

    submit(): void {
        this.accountService.changeAccountPassword(this.authService.userName,
            this.getFormValue("oldPassword"),
            this.getFormValue("newPassword"));
    }

    processErrors(errors:any) {
        for (var i = 0; i < errors.length; i++) {
            FormsExtentions.setFormError(this.passwordForm, errors[i].code, this.formErrors, this.validationMessages);
        }
    }
}
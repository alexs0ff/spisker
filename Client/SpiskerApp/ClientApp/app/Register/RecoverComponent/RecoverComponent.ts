import { Component, OnInit, ViewChild  } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { RegistrationService } from "../RegistrationService";
import { AuthentificateService } from "../../Auth/AuthModule";
import { FormBase } from "../../Utils/Forms/FormBase";
import { FormsExtentions } from "../../Utils/Forms/FormsExtentions";
import { SettingsService } from "../../Settings/SettingsModule";
import { ReCaptchaComponent } from 'angular2-recaptcha/lib/captcha.component';

@
Component({
    template: `
<div class="container">
<div class="row" *ngIf="!numberSent">
<div class="col-md-3 hidden-sm hidden-xs"></div>

    <div class="col-sm-12 col-md-6">    
        <form [formGroup]="recoverForm" (ngSubmit)="submit()" novalidate>
        <p>Для восстановления пароля введите имя пользователя и пройдите тест:</p>        
            <div class="form-group">        
                <input class="form-control" name="name" formControlName="userName" placeholder="Имя пользователя" set-item-focus/>                       
                <div *ngIf="formErrors.userName" class="alert alert-danger input-error-msg">
                    {{ formErrors.userName }}
                </div>
            </div>
            <div class="form-group">        
                <re-captcha [site_key]="googlePublicKey" [language]="'ru'" (captchaResponse)="handleCorrectCaptcha($event)" (captchaExpired)="handleCaptchaExpired()"></re-captcha>
            </div>           

            <div class="form-group">        
                <button class="btn btn-default" [disabled]="recoverForm.invalid || googleCaptchaIsNotSolved || sendingRequest">
                    Восстановить
                </button>
            </div>
        </form>
        
    </div>
    <div class="col-md-3 hidden-xs hidden-sm"></div>
    </div>
    <div class="row"  *ngIf="numberSent" >
        <div class="col-md-3 hidden-sm hidden-xs"></div>

        <div class="col-sm-12 col-md-6">    
            <div class="alert alert-info">
                <h5>Номер восстановления для пользователя <strong>{{getFormValue("userName")}}</strong>  успешно отправлен, проверьте почту и следуйте инструкциям.</h5>                
            </div>
        </div>
        <div class="col-md-3 hidden-xs hidden-sm"></div>
    </div>
</div>
`
})
export class RecoverComponent extends FormBase implements OnInit {
    recoverForm: FormGroup;

    googlePublicKey: string;

    googleCaptchaIsNotSolved: boolean;

    sendingRequest: boolean;

    numberSent:boolean;

    private responseToken: string;

    @ViewChild(ReCaptchaComponent)
    captcha: ReCaptchaComponent;
    
    constructor(private formBuilder: FormBuilder, private router: Router, private registrationService: RegistrationService, private settingsService: SettingsService) {
        super();
        this.sendingRequest = false;
        this.numberSent = false;
    }

    ngOnInit(): void {
        this.recoverForm = this.formBuilder.group({
            "userName": [null, [Validators.required, Validators.pattern(/^[A-Za-z][A-Za-z0-9_]{3,20}$/)]]
        });

        this.formErrors = {
            'userName': '',
            'number': ''
        };

        this.validationMessages = {
            'userName': {
                '5': 'Пользователь не существует',
                'required': 'Имя пользователя обязательно.',
                'pattern': 'Допустимы латинские буквы или цифры от 4 до 20 символов.'
            }
        };

        this.setForm(this.recoverForm);

        this.googlePublicKey = this.settingsService.getRecaptchaKey();
        this.googleCaptchaIsNotSolved = true;
    }

    handleCorrectCaptcha(event: any) {
        this.googleCaptchaIsNotSolved = false;
        this.responseToken = event;
    }

    handleCaptchaExpired(event: any) {
        this.googleCaptchaIsNotSolved = true;
    }

    submit(): void {
        this.sendingRequest = true;
        this.numberSent = false;
        this.registrationService.startRecovery(this.getFormValue("userName"), this.responseToken)
            .subscribe(response => {
                this.sendingRequest = false;
                if (response.errors.length > 0) {
                    this.captcha.reset();
                    this.googleCaptchaIsNotSolved = true;
                    this.processResponseErrors(response.errors);
                } else {

                    this.numberSent = true;
                }
            });
    }

    
}
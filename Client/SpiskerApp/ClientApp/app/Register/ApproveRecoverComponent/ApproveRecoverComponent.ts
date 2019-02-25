import { Component, OnInit, ViewChild,OnDestroy } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { RegistrationService } from "../RegistrationService";
import { AuthentificateService } from "../../Auth/AuthModule";
import { FormBase } from "../../Utils/Forms/FormBase";
import { fieldsAreEqualValidator } from "../../Utils/Validators/fieldsAreEqualValidator";
import { FormsExtentions } from "../../Utils/Forms/FormsExtentions";
import { SettingsService } from "../../Settings/SettingsModule";
import { ReCaptchaComponent } from 'angular2-recaptcha/lib/captcha.component';
import {SubscriptionsContainer} from "../../Utils/UtilsModule";
import {ErrorFlowService} from "../../ErrorFlow/ErrorFlowModule";
import {passwordValidator} from "../../Utils/Validators/passwordValidator";

@
Component({
    template: `
<div class="container">
<div class="row" *ngIf="!passwordChanged">
<div class="col-md-3 hidden-sm hidden-xs"></div>

    <div class="col-sm-12 col-md-6">    
        <form [formGroup]="approveForm" (ngSubmit)="submit()" novalidate>
        <p>Для смены пароля проверьте правильность данных и пройдите тест:</p>        
            <div class="form-group">        
                <input class="form-control" name="name" formControlName="userName" placeholder="Имя пользователя"/>                       
                <div *ngIf="formErrors.userName" class="alert alert-danger input-error-msg">
                    {{ formErrors.userName }}
                </div>
            </div>
            <div class="form-group">        
                <input class="form-control" name="number" formControlName="number" placeholder="Номер восстановления"/>                       
                <div *ngIf="formErrors.number" class="alert alert-danger input-error-msg">
                    {{ formErrors.number }}
                </div>
            </div>
            <div class="form-group">               
                <input type="password" class="form-control" name="password" formControlName="password" placeholder="Пароль" set-item-focus/>
                <div *ngIf="formErrors.password" class="alert alert-danger input-error-msg">
                    {{ formErrors.password }}
                </div>
            </div>

            <div class="form-group">               
                <input type="password" class="form-control" name="passwordCopy" formControlName="passwordCopy" placeholder="Повторите пароль"/>
                <div *ngIf="formErrors.passwordCopy" class="alert alert-danger input-error-msg">
                    {{ formErrors.passwordCopy }}
                </div>
            </div>
            <div class="form-group">        
                <re-captcha [site_key]="googlePublicKey" [language]="'ru'" (captchaResponse)="handleCorrectCaptcha($event)" (captchaExpired)="handleCaptchaExpired()"></re-captcha>
            </div>           

            <div class="form-group">        
                <button class="btn btn-default" [disabled]="approveForm.invalid || googleCaptchaIsNotSolved || sendingRequest">
                    Восстановить
                </button>
            </div>
        </form>
    </div>
    <div class="col-md-3 hidden-xs hidden-sm"></div>
    </div>
    <div class="row"  *ngIf="passwordChanged" >
        <div class="col-md-3 hidden-sm hidden-xs"></div>

        <div class="col-sm-12 col-md-6">    
            <div class="alert alert-info">
                <h5>Смена пароля для пользователя <strong>{{registredUserName}}</strong>  успешно завершена.</h5>
                <a class="btn btn-default" href="#" (click)="startAuth($event)">Нажмите</a> для входа в систему.
            </div>
        </div>
        <div class="col-md-3 hidden-xs hidden-sm"></div>
    </div>
</div>
`
})
export class ApproveRecoverComponent extends FormBase implements OnInit, OnDestroy {
    approveForm: FormGroup;

    googlePublicKey: string;

    googleCaptchaIsNotSolved: boolean;

    sendingRequest: boolean;

    passwordChanged:boolean;

    private responseToken: string;

    @ViewChild(ReCaptchaComponent)
    captcha: ReCaptchaComponent;

    constructor(private formBuilder: FormBuilder,
        private router: Router,
        private authService: AuthentificateService,
        private registrationService: RegistrationService,
        private settingsService: SettingsService,
        private route: ActivatedRoute,
        private errorFlowService: ErrorFlowService) {
        super();
        this.sendingRequest = false;
        this.passwordChanged = false;
        this.subscriptionContainer = new SubscriptionsContainer();
    }

    private subscriptionContainer: SubscriptionsContainer;

    ngOnInit(): void {
        this.approveForm = this.formBuilder.group({
            "userName": [null, [Validators.required]],
            "number": [null, [Validators.required]],
            "password": [null, [Validators.required, passwordValidator()]],
            "passwordCopy": [null, [Validators.required, fieldsAreEqualValidator("password")]]
        });

        this.formErrors = {
            'userName': '',
            'number': '',
            'password': '',
            'passwordCopy': ''
        };

        this.validationMessages = {
            'userName': {
                '5': 'Пользователь не существует',
                'required': 'Имя пользователя обязательно.',
                'pattern': 'Допустимы латинские буквы или цифры от 4 до 20 символов.'
            },
            'number': {
                'required': 'Номер обязателен.',
                '115': 'Ошибочный номер восстановления.'
            },
            'password': {
                'required': 'Пароль обязателен',
                'password': this.settingsService.messages.passwordRequired,
                '104': "Пароль должен иметь заглавные и строчные латинские буквы и цифры",
                '108': "Пароль должен иметь заглавные и строчные латинские буквы и цифры"
            },
            'passwordCopy': {
                'required': 'Повторение пароля обязателено',
                'confirmPassword': 'Пароли должны быть одинаковыми.',
            }
        };

        this.setForm(this.approveForm);

        this.googlePublicKey = this.settingsService.getRecaptchaKey();
        this.googleCaptchaIsNotSolved = true;

        let subscription: any = this.route.params.subscribe(params => {
            let userName = params["userName"];
            let number = params["number"];

            this.setFormValue("userName",userName);
            this.setFormValue("number", number);

            this.passwordChanged = false;
        });

        this.subscriptionContainer.add(subscription);
    }

    ngOnDestroy(): void {
        this.subscriptionContainer.clear();
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
        this.passwordChanged = false;
        this.registrationService.approveRecovery(this.getFormValue("userName"), this.getFormValue("password"), this.getFormValue("number"), this.responseToken)
            .subscribe(response => {
                this.sendingRequest = false;
                if (response.errors.length > 0) {
                    this.captcha.reset();
                    this.googleCaptchaIsNotSolved = true;
                    this.processResponseErrors(response.errors);
                } else {
                    
                    this.passwordChanged = true;
                }
            });
    }

    startAuth(event: Event) {
        event.preventDefault();
        let userName = this.getFormValue("userName");
        this.authService.login(userName, this.getFormValue("password")).then(result => {
            if (result.success) {
                this.router.navigateByUrl(userName);
            } else {
                this.errorFlowService.registerSystemError(0, result.description,null);
            }
        });
    }

}
import { Component,OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import {RegistrationService} from "../RegistrationService";
import {AuthentificateService} from "../../Auth/AuthModule";
import {FormBase} from "../../Utils/Forms/FormBase";
import {fieldsAreEqualValidator} from "../../Utils/Validators/fieldsAreEqualValidator";
import {FormsExtentions} from "../../Utils/Forms/FormsExtentions";
import {passwordValidator} from "../../Utils/Validators/passwordValidator";
import {SettingsService} from "../../Settings/SettingsModule";

@
Component({
    template: `
<div class="container">
<div class="row"  *ngIf="!hasSuccessfullRegistration" >
<div class="col-md-3 hidden-sm hidden-xs"></div>

    <div class="col-sm-12 col-md-6">    
        <form [formGroup]="registerForm" (ngSubmit)="submit()" novalidate>
        <p>Пожалуйста, введите данные по вашей учетной записи ниже:</p>        
            <div class="form-group">        
                <input class="form-control" name="name" formControlName="userName" placeholder="Имя пользователя"/>                       
                <div *ngIf="formErrors.userName" class="alert alert-danger input-error-msg">
                    {{ formErrors.userName }}
                </div>
            </div>
            <div class="form-group">        
                <input class="form-control" name="email" formControlName="email" placeholder="Email пользователя"/>                       
                <div *ngIf="formErrors.email" class="alert alert-danger input-error-msg">
                    {{ formErrors.email }}
                </div>
            </div>
            <div class="form-group">               
                <input type="password" class="form-control" name="password" formControlName="password" placeholder="Пароль"/>
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
                <button class="btn btn-default" [disabled]="registerForm.invalid">
                    Зарегистрировать
                </button>
            </div>
        </form>
    </div>
    <div class="col-md-3 hidden-xs hidden-sm"></div>
    </div>

    <div class="row"  *ngIf="hasSuccessfullRegistration" >
        <div class="col-md-3 hidden-sm hidden-xs"></div>

        <div class="col-sm-12 col-md-6">    
           <div class="alert alert-info">
            <h5>Регистрация для пользователя <strong>{{registredUserName}}</strong>  успешно завершена.</h5>
            <a class="btn btn-default" href="#" (click)="startAuth($event)">Нажмите</a> для входа в систему.
           </div>
        </div>
        <div class="col-md-3 hidden-xs hidden-sm"></div>
    </div>
</div>
`
})
export class RegisterComponent extends FormBase implements OnInit {
    registerForm: FormGroup;

    isNotValidCredentials: boolean = false;

    hasSuccessfullRegistration: boolean = false;

    private registredUserName:string;
    
    constructor(private formBuilder: FormBuilder, private router: Router, private registrationService: RegistrationService, private authService: AuthentificateService, private settingsService: SettingsService,) {
        super();
    }

    ngOnInit(): void {
        this.registerForm = this.formBuilder.group({
            "userName": [null,[Validators.required, Validators.pattern(/^[A-Za-z][A-Za-z0-9_]{3,20}$/)]],
            "email": [null,[Validators.required, Validators.email]],
            "password": [null, [passwordValidator()]],
            "passwordCopy": [null,[Validators.required, fieldsAreEqualValidator("password")]]
        });
        
        this.formErrors = {
            'userName': '',
            'email': '',
            'password': '',
            'passwordCopy': ''
        };

        this.validationMessages = {
            'userName': {
                'required': 'Имя пользователя обязательно.',
                'minlength': 'Имя пользователя должно составлять минимум 4 символа.',
                'maxlength': 'Имя пользователя должно составлять максимум 15 символов.',
                'pattern': 'Допустимы латинские буквы или цифры от 4 до 20 символов.',
                "102": "Логин имеет ошибочный формат",
                "103": "Логин уже существует"
            },
            'email': {
                'required': 'Email пользователя обязателен.',
                'email': 'Формат email ошибочен.',
                "100": "Email уже существует",
                "101": "Email имеет ошибочный формат"
            },
            'password': {
                'required': 'Пароль обязателен',
                'password': this.settingsService.messages.passwordRequired,
                '104': "Пароль должен иметь заглавные и строчные латинские буквы и цифры"
            },
            'passwordCopy': {
                'required': 'Повторение пароля обязателено',
                'confirmPassword': 'Пароли должны быть одинаковыми.',
            }
        };

        this.setForm(this.registerForm);
    }

    submit(): void {
        this.registrationService.register(this.getFormValue("userName"), this.getFormValue("email"), this.getFormValue("password"))
            .subscribe(response=> {
                if (response.errors.length > 0) {
                    this.processErrors(response.errors);
                } else {
                    this.registredUserName = response.userName;
                    this.hasSuccessfullRegistration = true;
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
                alert(result.description);
            }
        });
    }

    processErrors(errors:any) {
        for (var i = 0; i < errors.length; i++) {

            if (errors[i].code === 105) {
                this.formErrors.userName = errors[i].text;
                continue;
            }

            FormsExtentions.setFormError(this.registerForm, errors[i].code, this.formErrors, this.validationMessages);    
        }
        
    }
}
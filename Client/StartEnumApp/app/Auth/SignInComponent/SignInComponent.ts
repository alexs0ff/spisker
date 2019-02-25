import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import {AuthentificateService} from "../AuthentificateService";
import { AuthResult } from "../AuthModule";
import {passwordValidator} from "../../Utils/Validators/passwordValidator";
import {FormBase} from "../../Utils/Forms/FormBase";
import {SettingsService} from "../../Settings/SettingsModule";
declare var jQuery: any;

@Component({
    selector: 'signin-form',
    template: `
   
<div class="modal fade" id="sign-in-dialog" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-label="close"><span>&times;</span></button>
                <h4 class="modal-title">Войти в систему <span class="glyphicon glyphicon-exclamation-sign"></span></h4>
            </div>
            <div class="modal-body">
                <div class="container-fluid">
                    <form class="form-horizontal" [formGroup]="loginForm" >                   
                       
                        <p>Заполните поля логин и пароль</p>                   
                        <div class="form-group">                            
                            <input class="form-control" name="name" placeholder="Логин" formControlName="userName" set-item-focus/> 
                            <div *ngIf="formErrors.userName" class="alert alert-danger input-error-msg">
                                {{ formErrors.userName }}
                            </div>
                        
                        </div>
                        <div class="form-group">                            
                            <input type="password" class="form-control" placeholder="Пароль" name="password" formControlName="password" />                    
                            <div *ngIf="formErrors.password" class="alert alert-danger input-error-msg">
                                {{ formErrors.password }}
                            </div>
                        </div>                        
                    
                    </form>
                    <div class="alert alert-danger"
                         *ngIf="hasError">
                        {{errorText}}
                    </div>
                    <div class="row">
                    <h5>Если Вас нет в системе, то Вы можете <a href="#" (click)="gotoRegister($event)">зарегистрироваться</a></h5>
                    </div>
                    <div class="row">
                        <h5>Если Вы забыли пароль, то Вы можете его <a href="#" (click)="gotoRecover($event)">восстановить</a></h5>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button class="btn btn-info" [disabled]="loginForm.invalid" (click)="startLogin($event)">
                    Войти
                </button>
                <button class="btn btn-default" data-dismiss="modal">Закрыть</button>
            </div>
        </div>
    </div>
</div>
`

})
export class SignInComponent extends FormBase implements OnInit {

    loginForm: FormGroup;

    constructor(private formBuilder: FormBuilder, private authService: AuthentificateService, private router: Router, private settings:SettingsService) {
        super();
        

    }

    hasError: boolean;

    errorText:string;
    
    ngOnInit(): void {
       
        this.loginForm = this.formBuilder.group({
            "userName": ["", [Validators.required]],
            "password": ["", [passwordValidator()]]
        });

        this.clearFormErrors();

        this.validationMessages = {
            'userName': {
                'required': 'Имя пользователя обязательно.'
            },
            'password': {
                'password': this.settings.messages.passwordRequired
    }
        };

        this.setForm(this.loginForm);
    }

    private clearFormErrors() {
        this.formErrors = {
            'userName': '',
            'password': ''
        };
    }

    startLogin($event:Event) {
        this.authService.login(this.loginForm.controls["userName"].value, this.loginForm.controls["password"].value)
            .then((result:AuthResult)=> {
                if (result.success) {
                    this.clearInputs();
                    this.closeDialog();
                } else {
                    this.hasError = true;
                    this.errorText = result.description;
                }
                });
    }

    gotoRegister(event:Event) {
        event.preventDefault();
        this.closeDialog();
        this.router.navigate(['i/account/register']);
    }

    gotoRecover(event: Event) {
        event.preventDefault();
        this.closeDialog();
        this.router.navigate(['i/account/recover']);
    }

    showDialog() {
        jQuery('#sign-in-dialog').modal();
        this.clearInputs();
        this.clearFormErrors();
    }

    closeDialog() {
        jQuery('#sign-in-dialog').modal('hide');
    }

    private clearInputs() {
        this.loginForm.controls["userName"].setValue(null);
        this.loginForm.controls["userName"].markAsUntouched();
        this.loginForm.controls["password"].setValue(null);
        this.loginForm.controls["password"].markAsUntouched();
    }
} 

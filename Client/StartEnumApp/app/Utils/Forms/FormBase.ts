import {  } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import {FormsExtentions} from "./FormsExtentions";

export class FormBase {
    mainForm: FormGroup;

    formErrors: any;

    validationMessages: any;

    protected setForm(form: FormGroup) {
        if (form==null) {
            throw new Error("Форма не задана");
        }

        this.mainForm = form;
        //TODO удаление подписки
        this.mainForm.valueChanges
            .subscribe(data => FormsExtentions.onValueChanged(this.mainForm,
                this.formErrors,
                this.validationMessages,
                data));

        FormsExtentions.onValueChanged(this.mainForm,
            this.formErrors,
            this.validationMessages); 
        //from https://angular.io/guide/form-validation#live-example
        //and https://angular.io/guide/form-validation
    }

    protected getFormValue(name: string): any {
        return this.mainForm.controls[name].value;
    }

    protected setFormValue(name: string, value:any) {
        this.mainForm.controls[name].setValue(value);
    }

    protected processResponseErrors(errors: any) {
        for (var i = 0; i < errors.length; i++) {
            FormsExtentions.setFormError(this.mainForm, errors[i].code, this.formErrors, this.validationMessages);
        }
    }
}
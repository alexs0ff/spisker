import { AbstractControl, ValidatorFn } from '@angular/forms';

export function passwordValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } => {
        const regex: RegExp = /^(?=.*\d)(?=.*[a-zA-Z])[a-zA-Z0-9]{7,}$/;
        const checkPassword:boolean = regex.test(control.value);
        return checkPassword ? null : { 'password': { value: control.value } };
    };
}
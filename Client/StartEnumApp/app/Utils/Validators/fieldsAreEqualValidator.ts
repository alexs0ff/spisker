import { AbstractControl, ValidatorFn } from '@angular/forms';

export function fieldsAreEqualValidator(filedToCompare:string ): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } => {

        let group = control.parent;

        if (!group) {
            return null;
        }
        
        var f1 = group.get(filedToCompare);
        if (!f1) {
            return null;
        }
        

        var equal = control.value === f1.value;
        
        return equal ? null : { 'confirmPassword': { name } } ;
    };
}
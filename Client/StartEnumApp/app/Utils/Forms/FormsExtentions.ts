import { FormGroup } from '@angular/forms';

export class FormsExtentions {

    static onValueChanged(form: FormGroup, formErrors: any, validationMessages: any, data ?: any) {
        if (!form) {
            return;
        }

        for (const field in formErrors) {
            // clear previous error message (if any)
            formErrors[field] = '';
            const control = form.get(field);

            if (control && control.dirty && !control.valid) {
                const messages = validationMessages[field];
                for (const key in control.errors) {
                    formErrors[field] += messages[key] + ' ';
                    if (messages[key]===undefined) {
                        alert(key +' is undefined');
                    }
                }
            }
        }
    }

    static setFormError(form: FormGroup, key: string, formErrors: any, validationMessages: any) {
        for (let controlName in validationMessages) {
            const control = validationMessages[controlName];
            for (const error in control) {
                if (error == key) {
                    formErrors[controlName] += control[key] + ' ';
                    form.controls[controlName].setErrors({ key: true });
                };
            }
        }
    }

}

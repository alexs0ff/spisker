import { Input, Directive, ElementRef } from '@angular/core';

@Directive(
    {
        selector: '[set-item-focus]'
    })
export class SetFocusDirective {
    constructor(el: ElementRef) {
        setTimeout(() => {
            el.nativeElement.focus();
        }, 0);
    }
}
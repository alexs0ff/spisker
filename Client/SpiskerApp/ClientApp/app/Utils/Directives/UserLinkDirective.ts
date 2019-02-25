import { Input, Directive, HostListener } from '@angular/core';
import { Router } from '@angular/router';

@Directive(
    {
        selector: '[user-link]',
        host: {
            '[style.cursor]': '"pointer"'
        }
    })
export class UserLinkDirective {
    constructor(private router: Router) {

    }

    @Input("user-link")
    userLogin: string;

    @HostListener("click")
    handleClick() {
        this.router.navigate(['/' + this.userLogin]);
    }
}
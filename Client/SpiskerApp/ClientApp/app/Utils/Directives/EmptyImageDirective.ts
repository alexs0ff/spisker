import { Input, Directive, ElementRef, DoCheck  } from '@angular/core';
import {StringExtentions} from "../Extentions/StringExtentions";

@Directive(
    {
        selector: '[empty-image]'
    })
export class EmptyImageDirective implements DoCheck  {

    private images: any = {
        'avatar':'/assets/imgs/account.png'
        };

    @Input("empty-image")
    emptyImage: string;

    constructor(el: ElementRef) {
        el.nativeElement.onerror = ()=> {
            let imgSrc = this.images[this.emptyImage];
            if (!StringExtentions.isNullOrWhiteSpace(imgSrc)) {
                el.nativeElement.src = imgSrc;    
            }
        }
    }

    ngDoCheck(): void {
        
    }
}
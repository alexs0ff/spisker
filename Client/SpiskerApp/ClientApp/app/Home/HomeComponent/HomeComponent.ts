import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthentificateService } from "../../Auth/AuthModule";
import { Router } from '@angular/router';
import * as jQuery from 'jquery';

//declare var jQuery: any;

@Component({    
    selector: 'main-page',
    /* prod**       
    templateUrl: './HomeComponent.html',
    styleUrls: ['./HomeComponent.css']
           **end-prod */
    /* dev */
    templateUrl: 'HomeComponent.html',
    styleUrls: ['HomeComponent.css']
    /* end-dev */

})
export class HomeComponent implements OnInit, OnDestroy {

constructor(private authService: AuthentificateService, private router: Router) {

}

ngOnInit(): void {
    jQuery('.level-bar-inner').css('width', '0');

    jQuery('.level-bar-inner').each(function (key:any, value:any ) {

        let itemWidth = jQuery(this).data('level');

        jQuery(this).animate({
            width: itemWidth
        }, 800);

    });


    jQuery('.pop-preview').on('click', function () {
        jQuery('.imagepreview').attr('src', jQuery(this).find('img').attr('src'));
        jQuery('#imagemodal').modal('show');
    });		
}

ngOnDestroy(): void {
    
}

goToTry(event: Event) {
    event.preventDefault();
    let url: string = 'i/account/register';
    if (this.authService.isAuthorized) {
        url = this.authService.userName;
    }

    this.router.navigateByUrl(url);
}
} 

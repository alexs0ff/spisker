import { Directive, Input, TemplateRef, ViewContainerRef, OnInit, OnDestroy } from '@angular/core';
import { AuthEvent } from "../AuthFlow/AuthEvent";
import { AuthEventType } from "../AuthFlow/AuthEventType";
import { AuthentificateService } from "../AuthentificateService";
import {SubscriptionsContainer} from "../../Utils/UtilsModule";

@Directive(
    {
        selector: '[iFOwnedUser]'
    })
export class IfOwnedUserDirective implements OnInit, OnDestroy {
    
    @Input('iFOwnedUser') ownedUserId: string;

    private container: SubscriptionsContainer = new SubscriptionsContainer();

    constructor(
        private templateRef: TemplateRef<any>,
        private viewContainer: ViewContainerRef,
        private authService: AuthentificateService
    ) {

    }

    ngOnInit(): void {
        let subscr:any = this.authService.authFeed.filter((e: AuthEvent) => e.eventType === AuthEventType.Logged || e.eventType === AuthEventType.Loggedout)
            .subscribe(e => {
                this.setState();
            });

        this.container.add(subscr);
        this.setState();
    }

    ngOnDestroy(): void {
        this.container.clear();
    }

    @Input() set ifLoggedUser(visible: boolean) {


    }

    private setState() {
        let owned = this.authService.userId === this.ownedUserId;
        this.setVisible(owned);
    }

    private setVisible(state: boolean) {
        if (state) {
            // If condition is true add template to DOM
            this.viewContainer.createEmbeddedView(this.templateRef);
        } else {
            // Else remove template from DOM
            this.viewContainer.clear();
        }
    }
}
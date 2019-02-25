import { Directive, Input, TemplateRef, ViewContainerRef, OnInit, OnDestroy } from '@angular/core';
import { AuthEvent } from "../AuthFlow/AuthEvent";
import { AuthEventType } from "../AuthFlow/AuthEventType";
import { AuthentificateService } from "../AuthentificateService";

import { SubscriptionsContainer } from "../../Utils/UtilsModule";

@Directive(
    {
        selector: '[iFNotOwnedUser]'
    })
export class IfNotOwnedUserDirective implements OnInit, OnDestroy {

    private authFeedSubject: any;

    private subscriptionsContainer: SubscriptionsContainer = new SubscriptionsContainer();

    constructor(
        private templateRef: TemplateRef<any>,
        private viewContainer: ViewContainerRef,
        private authService: AuthentificateService
    ) {

    }


    private ownedUserId: string;

    @Input()
    set iFNotOwnedUser(value: string) {
        this.ownedUserId = value;
    }

    private unLoggedVisible: boolean;
    @Input()
    set iFNotOwnedUserUnLoggedVisible(value: boolean) {
        this.unLoggedVisible = value;
    }

    ngOnInit(): void {
        var sub = this.authService.authFeed.filter((e: AuthEvent) => e.eventType === AuthEventType.Logged || e.eventType === AuthEventType.Loggedout)
            .subscribe(e => {
                this.setState();
            });

        this.subscriptionsContainer.add(sub);
        this.setState();
    }

    ngOnDestroy(): void {
        this.subscriptionsContainer.clear();
    }

    private setState() {

        if (!this.authService.isAuthorized) {
            this.setVisible(this.unLoggedVisible);
        } else {
            let owned = this.authService.userId === this.ownedUserId;

            this.setVisible(!owned);    
        }
        
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
import { Directive, Input, TemplateRef, ViewContainerRef, OnInit, OnDestroy } from '@angular/core';
import { AuthEvent } from "../AuthFlow/AuthEvent";
import { AuthEventType } from "../AuthFlow/AuthEventType";
import {AuthentificateService} from "../AuthentificateService";
import { SubscriptionsContainer} from "../../Utils/UtilsModule";

@Directive(
    {
        selector: '[iFLoggedUser]'
    })
export class IfLoggedUserDirective implements OnInit, OnDestroy {

    private authFeedSubject: any;

    private subscriptionsContainer: SubscriptionsContainer = new SubscriptionsContainer();

    @Input('iFLoggedUser') visibleIfLogged: boolean;
    
    constructor(
        private templateRef: TemplateRef<any>,
        private viewContainer: ViewContainerRef,
        private authService: AuthentificateService
    ) {

    }
    
    ngOnInit(): void {
        let container = this.authService.authFeed.filter((e: AuthEvent) => e.eventType === AuthEventType.Logged || e.eventType === AuthEventType.Loggedout)
            .subscribe(e => {
                this.setState();
            });
        this.subscriptionsContainer.add(container);
        this.setState();
    }

    ngOnDestroy(): void {
        this.subscriptionsContainer.clear();
    }

    @Input() set ifLoggedUser(visible: boolean) {
        

    }

    private setState() {
        let logged = this.authService.isAuthorized;
        if (logged) {
            this.setVisible(this.visibleIfLogged);
        } else {
            this.setVisible(!this.visibleIfLogged);
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
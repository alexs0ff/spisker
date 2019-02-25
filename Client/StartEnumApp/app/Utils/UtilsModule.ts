import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {EnumToStringPipe} from "./Pipes/EnumToStringPipe";
import { QueryState } from "./Forms/QueryState";
import {SetFocusDirective} from "./Directives/SetFocusDirective";
import {EmptyImageDirective} from "./Directives/EmptyImageDirective";
import { UserLinkDirective } from "./Directives/UserLinkDirective";
import {ClipboardService} from "./ClipboardService";
import { SubscriptionsContainer } from "./Subscriptions/SubscriptionsContainer";
import { WindowSize } from "./Html/WindowSize";
import { ScreenDetector } from "./Html/ScreenDetector";
import { DoubleClickListener } from "./Mobile/DoubleClickListener";

@NgModule({
    imports: [CommonModule],
    declarations: [EnumToStringPipe, SetFocusDirective, EmptyImageDirective, UserLinkDirective],
    exports: [EnumToStringPipe, SetFocusDirective, EmptyImageDirective, UserLinkDirective]
})
class UtilsModule {
}

export {
    EnumToStringPipe,
    UtilsModule,
    ClipboardService,
    SubscriptionsContainer,
    ScreenDetector,
    WindowSize,
    QueryState,
    DoubleClickListener
    }
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorFlowComponent } from "./ErrorFlowComponent/ErrorFlowComponent";
import { ErrorFlowService } from "./ErrorFlowService";
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import {UtilsModule} from "../Utils/UtilsModule";

@NgModule({
    imports: [CommonModule, FormsModule, ReactiveFormsModule,UtilsModule],
    declarations: [ErrorFlowComponent],
    exports: [ErrorFlowComponent]
})
class ErrorFlowModule {
}

export {
    ErrorFlowModule,

    ErrorFlowService
   
    }
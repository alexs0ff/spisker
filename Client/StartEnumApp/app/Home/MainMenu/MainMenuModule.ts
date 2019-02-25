import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchComponent } from "./SearchComponent/SearchComponent";
import { ReactiveFormsModule } from '@angular/forms';
import {UtilsModule} from "../../Utils/UtilsModule";

@NgModule({
    imports: [
        CommonModule, ReactiveFormsModule, UtilsModule
    ],
    declarations: [
        SearchComponent
    ],
    exports: [SearchComponent]
})
export class MainMenuModule { }
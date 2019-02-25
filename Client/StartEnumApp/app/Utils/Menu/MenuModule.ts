import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {LiRadioMenuComponent} from "./LiRadioMenuComponent/LiRadioMenuComponent";
import { MenuItemModel } from "./MenuItemModel";
import { RadioMenuModel } from "./RadioMenuModel";
import { MenuItemBase } from "./MenuItemBase";
import { MenuItemDivider } from "./MenuItemDivider";

@NgModule({
    imports: [CommonModule],
    declarations: [LiRadioMenuComponent],
    exports: [LiRadioMenuComponent]
})
class MenuModule { }

export {
    MenuModule,
    LiRadioMenuComponent,
    MenuItemModel,
    RadioMenuModel,
    MenuItemBase,
    MenuItemDivider
    }
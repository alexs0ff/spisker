import {MenuModel} from "./MenuModel";
import {MenuItemModel} from "./MenuItemModel";
import {StringExtentions} from "../Extentions/StringExtentions";
import {MenuItemBase} from "./MenuItemBase";

export class RadioMenuModel extends MenuModel {
    public addItem(item: MenuItemBase) {
        let hasChecked:boolean = false;
        for (var i = 0; i < this.items.length; i++) {
            if (this.isMenuItem(i)) {
                if (this.getItemModel(i).checked) {
                    hasChecked = true;
                    break;
                }    
            };
        }

        if (hasChecked && item.type === this.menuItemType) {
            let itemMenu = <MenuItemModel>item;
            itemMenu.checked = false;
        }
        super.addItem(item);
    }

    public isChecked(id: string): boolean {
        let result = false;
        for (var i = 0; i < this.items.length; i++) {
            if (this.isMenuItem(i)) {
                if (StringExtentions.compareOrdinalIgnoreCase(this.getItemModel(i).id, id)) {
                    result = this.getItemModel(i).checked;
                    break;
                }
            }
        }
        return result;
    }

    public setChecked(id: string): MenuItemModel {
        let result: MenuItemModel = null;

        for (var i = 0; i < this.items.length; i++) {
            if (this.isMenuItem(i)) {
                if (StringExtentions.compareOrdinalIgnoreCase(this.getItemModel(i).id, id)) {
                    this.getItemModel(i).checked = true;
                    result = this.getItemModel(i);
                } else {
                    this.getItemModel(i).checked = false;
                }
            }
        }

        return result;
    }
}
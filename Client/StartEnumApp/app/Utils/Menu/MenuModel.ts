import { MenuItemBase } from "./MenuItemBase";
import {MenuItemModel} from "./MenuItemModel";

export class MenuModel {
    constructor() {
        this.items = new Array<MenuItemBase>();
    }

    public items: Array<MenuItemBase>;

    protected addItem(item: MenuItemBase) {
        this.items.push(item);
    }

    protected deviderType:number = 0;

    protected menuItemType: number = 1;

    isMenuItem(index:number):boolean {
        if (index >= this.items.length) {
            return false;
        }

        return this.items[index].type === this.menuItemType;
    }

    getItemModel(index: number): MenuItemModel {
        if (index >= this.items.length) {
            throw new Error("Ошибочный индекс пункта меню");
        }

        if (this.items[index].type === this.deviderType) {
            throw new Error("Ошибочный тип пункта меню");
        }

        let item = <MenuItemModel>this.items[index];

        return item;
    }
}
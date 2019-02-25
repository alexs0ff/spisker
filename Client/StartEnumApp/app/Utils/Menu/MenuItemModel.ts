import {MenuItemBase} from "./MenuItemBase";

export class MenuItemModel extends MenuItemBase {
    constructor(id: string = null, name: string = null, checked: boolean = false) {
        super();
        this.name = name;
        this.id = id;
        this.checked = checked;
        this.type = 1;//Пункт меню
    }

    name: string;

    id: string;

    checked:boolean;
}
import {WindowSize} from "./WindowSize";

export class ScreenDetector {
    static getSize(width: number): WindowSize {
        let size: WindowSize = WindowSize.ScreenXs;

        if (width >= WindowSize.ScreenLg) {
            size = WindowSize.ScreenLg;
        } else if (width >= WindowSize.ScreenMd) {
            size = WindowSize.ScreenMd;
        }
        else if (width >= WindowSize.ScreenSm) {
            size = WindowSize.ScreenSm;
        }

        return size;
    }

    static elementExists(query: string): boolean {
        var element = document.querySelector(query);
        if (typeof (element) != 'undefined' && element != null) {
            return true;
        }

        return false;
    }
}
export class StringExtentions {

    static isNullOrWhiteSpace(value: string): boolean {
        return value === null || value==undefined || value.match(/^ *$/) !== null;
    }

    static compareOrdinalIgnoreCase(string1:string, string2:string): boolean {
        return this.compareStrings(string1, string2, true, false);
    }

    static compareStrings(string1: string, string2: string, ignoreCase:boolean, useLocale:boolean): boolean {
        if (string1==null && string2==null) {
            return true;
        }

        if (string1==null || string2==null) {
            return false;
        }

        if (ignoreCase) {
            if (useLocale) {
                string1 = string1.toLocaleLowerCase();
                string2 = string2.toLocaleLowerCase();
            }
            else {
                string1 = string1.toLowerCase();
                string2 = string2.toLowerCase();
            }
        }

        return string1 === string2;
    }
}
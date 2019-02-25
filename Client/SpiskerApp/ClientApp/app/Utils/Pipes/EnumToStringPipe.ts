import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'enumToString' })
export class EnumToStringPipe implements PipeTransform {
    transform(value: any, enumType: any): string {
        let result: string = "";
        if (value === undefined || value===null) {
            return result;
        }

        if (enumType === undefined || enumType===null) {
            return result;
        }

        let val = enumType[value];

        if (val===undefined || val===null || val.length===0) {
            return result;
        }

        result = val;

        return result;
    }
}
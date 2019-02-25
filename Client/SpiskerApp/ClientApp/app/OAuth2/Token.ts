export class Token {
    accessToken: string;

    expiresOn: Date;

    userId: string;

    userName:string;

    static parse(key: any, value: any): any {
        if (key==="expiresOn") {
            return new Date(value);
        }

        return value;
    };
}
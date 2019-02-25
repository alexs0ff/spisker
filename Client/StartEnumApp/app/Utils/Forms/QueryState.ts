export class QueryState {
    constructor() {
        this.querySentDate = new Date();
        this._isSent = false;
    }

    private readonly queryTimeout:number = 10;

    private _isSent: boolean;

    private querySentDate: Date;

    get isSent(): boolean { return this._isSent; }

    set isSent(value: boolean) {
        this._isSent = value;
        if (value) {
            this.querySentDate = new Date();
        }
    }

    canSend() {
        let now: Date = new Date();
        var difference = (+now - +this.querySentDate) / 1000;

        return !this._isSent || difference > this.queryTimeout;
    }
}
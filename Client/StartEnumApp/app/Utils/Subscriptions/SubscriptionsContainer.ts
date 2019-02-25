import { Subscription } from "rxjs/rx";

export class SubscriptionsContainer {
    private list: Array<Subscription> = new Array<Subscription>();

    add(subscription: Subscription) {
        this.list.push(subscription);
    }

    clear() {
        for (let i = 0; i < this.list.length; i++) {
            this.list[i].unsubscribe();
        }
    }
}
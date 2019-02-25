import {Action} from "../Delegates/Action";

enum ButtonState {

    Empty,

    Click,

    DblClick,
}

//На мобильных девайсах dbl клик не работает, используем timer
export class DoubleClickListener {

    private timer: any;

    private readonly delay:number = 300;

    private currentState: ButtonState;

    click(event:Event, clickAction: Action<Event>, dblClickAction: Action<Event>) {

        
        if (dblClickAction == null) {
            if (clickAction) {
                clickAction(event);
            }
            return;
        }
        if (this.currentState === ButtonState.DblClick) {
            return;
        }

        if (this.currentState === ButtonState.Click) {
            this.currentState = ButtonState.DblClick;
            return;
        }

        this.currentState = ButtonState.Click;

        if (this.timer) {
            clearTimeout(this.timer);
        }

        this.timer = setTimeout(() => {
                if (this.currentState === ButtonState.Click && clickAction) {
                    clickAction(event);
                } else if (this.currentState === ButtonState.DblClick) {
                    dblClickAction(event);
                }

                this.currentState = ButtonState.Empty;
            },
            this.delay);
        
    }


    dispose() {
        if (this.timer) {
            clearTimeout(this.timer);
        }
    }
}
import {ErrorFlowService} from "../../ErrorFlow/ErrorFlowModule";

export class ServiceBase {

    protected errorService: ErrorFlowService;

    constructor(erServ: ErrorFlowService) {
        this.errorService = erServ;
    }

    protected  processError(response: any) {
        if (response.errors && response.errors.length > 0) {
            for (var i = 0; i < response.errors.length; i++) {
                var err = response.errors[i];
                this.errorService.registerSystemError(err.code, err.text, err);
            }
        }
    }
}
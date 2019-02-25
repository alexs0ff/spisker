import { Injectable } from '@angular/core';

@Injectable()
export class ClipboardService {

    public copyTextToClipboard(text: string) {
        let txtArea: any = document.createElement("textarea");

        txtArea.style.position = 'fixed';
        txtArea.style.top = 0;
        txtArea.style.left = 0;
        txtArea.style.opacity = 0;
        txtArea.value = text;
        document.body.appendChild(txtArea);
        txtArea.select();
        try {
            let successful = document.execCommand('copy');
            let msg = successful ? 'successful' : 'unsuccessful';
            console.log('Copying text command was ' + msg);
            if (successful) {
                return true;
            }
        } catch (err) {
            console.log('Oops, unable to copy');
        }
        document.body.removeChild(txtArea);
        return false;
    }
}
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AuthentificateService } from "../../../Auth/AuthModule";
import {AccountService} from "../../AccountService/AccountService";
import {AccountEvent} from "../../AccountService/AccountEvent";
import {AccountEventType} from "../../AccountService/AccountEventType";
import {StringExtentions} from "../../../Utils/Extentions/StringExtentions";

@Component({
    selector: 'change-avatar',
    template: `

<div class="row text-center">
    <img [src]="imageSrc" class="settings-avatar img-rounded" [empty-image]="'avatar'"/>    
</div>
<div class="row text-center">
    <div class="btn btn-primary fileUpload">  
        <span>Выбрать аватар</span>  
        <input type="file" id="btnUpload" value="Upload" (change)="fileChange($event)" class="upload"  />  
    </div> 

</div>
<div class="row" *ngIf="errorText" ><div class="alert alert-danger">{{errorText}}</div></div>
  `,
    styles: [`
    .fileUpload {  
position: relative;  
overflow: hidden;  
margin: 10px;  
}  
.fileUpload input.upload {  
position: absolute;  
top: 0;  
right: 0;  
margin: 0;  
padding: 0;  
font-size: 20px;  
cursor: pointer;  
opacity: 0;  
filter: alpha(opacity=0);  
}  
`]
})
export class ChangeAvatarComponent implements OnInit, OnDestroy {

    imageSrc:string;

    errorText: string;

    private maxFileSize:number = 1000000;

    constructor(private accountService: AccountService, private authService: AuthentificateService) {
        
    }

    private avatarChangedSubject: any;

    private hasStatusTextChanged: boolean;

    ngOnInit(): void {
        this.hasStatusTextChanged = false;

        this.avatarChangedSubject = this.accountService.accountFeed.filter((e: AccountEvent) => e.eventType === AccountEventType.AvatarChanged)
            .subscribe((event: AccountEvent) => {
                if (StringExtentions.compareOrdinalIgnoreCase(event.targetUserName, this.authService.userName)) {
                    this.setImage(event.data);
                }
            });
    }

    ngOnDestroy(): void {
        this.avatarChangedSubject.unsubscribe();
    }

    
      
    fileChange(event:any) {
        let fileList: FileList = event.target.files;
        if (fileList.length>0) {
            let file: File = fileList[0];
            if (file.size > this.maxFileSize) {
                this.errorText = "Файл должен быть до 1 Мб (текущий вес -" + file.size+ ")";
            } else {
                this.errorText = null;
                this.accountService.changeAvatar(file);
            }
            
        } else {
            this.errorText = "Необходимо выбрать файл";
        }
    }

    setImage(url: string) {
        this.imageSrc = url;
    }
}  

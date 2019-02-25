import { Component, OnInit, OnDestroy } from '@angular/core';
import {  Router } from '@angular/router';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import {FormBase} from "../../../Utils/Forms/FormBase";
import {UserProfileModel} from "../../../Social/Model/UserProfileModel";
import {StringExtentions} from "../../../Utils/Extentions/StringExtentions";
import {SocialService, SocialEvent, SocialEventType } from "../../../Social/SocialModule";
import {SystemName, SettingsService } from "../../../Settings/SettingsModule";
import {QueryState} from "../../../Utils/UtilsModule";
import * as jQuery from 'jquery';

@Component({
    selector: 'main-search',
    template: `  
<form id="searchUsers" role="search" [formGroup]="searchForm" class="pull-right">

    <div class="btn-group" >   
        <div class="input-group dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="True">
            <input type="text" class="form-control"  formControlName="searchInput" (keyup.enter)="searchTextChanged()" placeholder="Поиск…"/>
            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
        </div>
        <ul id="found-user-list" class="dropdown-menu pull-right">            
            <li  *ngFor="let item of profiles">
                <a href="#" (click)=gotoUser($event,item.userName)>       
                <img src="{{item.avatarUrl}}" class="img-responsive" [empty-image]="'avatar'"/>                                            
                    <div class="notification-message">
                        {{getFio(item)}} <small>@{{item.userName}}</small>
                    </div>                
                </a>
            </li>          
            <li *ngIf="!profiles || profiles.length === 0">
                <div class="notification-message">
                    <em>Не найдено</em>
                </div>  
            </li>                 
        </ul>
        
    </div>
     
 </form>
    
  `
})
export class SearchComponent extends FormBase implements OnInit, OnDestroy {

    searchForm: FormGroup;

    constructor(private formBuilder: FormBuilder, private socialService: SocialService, private router: Router, private settingsService: SettingsService) {
        super();
    }

    profiles:Array<UserProfileModel>;

    private profilesFetchedSubject: any;

    private textChanges: any;

    private queryState: QueryState = new QueryState();

    ngOnInit(): void {
        this.searchForm = this.formBuilder.group({
            "searchInput": [""]
        });

        this.formErrors = {
           
        };

        this.validationMessages = {
            
        };

        this.setForm(this.searchForm);

        this.textChanges = this.searchForm.valueChanges.subscribe(data =>
            this.searchTextChanged());

        this.profilesFetchedSubject = this.socialService.socialFeed.filter((e: SocialEvent) => e.eventType === SocialEventType.ProfilesFetched)
            .subscribe((e: SocialEvent) => {
                if (StringExtentions.compareOrdinalIgnoreCase(e.systemId, SystemName.mainSearchComponent)) {
                    this.queryState.isSent = false;
                    this.profiles = e.data;
                    this.showSearchResults();
                }
            });
    }

    ngOnDestroy(): void {
        this.profilesFetchedSubject.unsubscribe();
        this.textChanges.unsubscribe();
    }

    searchTextChanged() {
        let val = this.getFormValue("searchInput");

        if (!val || val.length < 3) {
            this.profiles = null;
            return;
        }
        if (this.queryState.canSend()) {
            this.socialService.getProfiles(SystemName.mainSearchComponent, val);
            this.queryState.isSent = true;
        }
    }

    showSearchResults() {
        if (jQuery('#found-user-list').is(":hidden")) {
            jQuery('#searchUsers .dropdown-toggle').dropdown('toggle');
        }
    }

    getFio(item: UserProfileModel):string {
       return this.settingsService.getFullName(item);
    }

    gotoUser(event: Event, userName: string) {
        event.preventDefault();
        this.router.navigateByUrl(userName);
    }
}
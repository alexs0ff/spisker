import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MomentModule } from 'angular2-moment/moment.module';
import { DebugElement } from '@angular/core';
import {ListComponent} from "./ListComponent";
import {ListModel} from "../Models/ListModel";
import {SenderStub} from "../../../testing/SenderStub";
import {AuthentificateServiceStub} from "../../../testing/AuthentificateServiceStub";
import { RouterStub } from "../../../testing/RouterStub";
import {ListItemViewComponent} from "../ListItemComponent/Views/ListItemViewComponent";
import {IfOwnedUserDirective} from "../../Auth/Directives/IfOwnedUserDirective";
import {ListItemModel} from "../Models/ListItemModel";
import {FeedListService, FeedModule } from "../FeedModule";
import {ErrorFlowService} from "../../ErrorFlow/ErrorFlowModule";
import {Sender} from "../../Utils/Sender/SenderModule";
import { AuthentificateService, AuthResult } from "../../Auth/AuthModule";
import {SocialService} from "../../Social/SocialModule";
import {ListViewDialogService} from "../ListViewDialog/ListViewDialogService";
import {ClipboardService} from "../../Utils/UtilsModule";
import { ListItemDragAndDropService } from "../ListItemDragAndDrop/ListItemDragAndDropService";
describe('ListComponent tests', () => {

    let comp: ListComponent;
    let fixture: ComponentFixture<ListComponent>;
    let model: ListModel;
    let sender: SenderStub;
    let router: RouterStub;
    let authService: AuthentificateServiceStub;

    beforeEach(() => {

        TestBed.configureTestingModule({
            imports: [FeedModule, FormsModule, ReactiveFormsModule, MomentModule],
            providers: [FeedListService, ListItemDragAndDropService,ErrorFlowService, SocialService, ListViewDialogService,
                ClipboardService,
                { provide: Sender, useClass: SenderStub },
                { provide: AuthentificateService, useClass: AuthentificateServiceStub },
                { provide: Router, useClass: RouterStub }
            ]

        });

        sender = TestBed.get(Sender);
        authService = TestBed.get(AuthentificateService);
        router = TestBed.get(Router);

        fixture = TestBed.createComponent(ListComponent);

        comp = fixture.componentInstance; // BannerComponent test instance
        model = new ListModel();
        
        model.ownerId = "userId";
        model.createEventTime = new Date();
        model.ownerFullName = "owner full name";
        model.originFullName = "origin full name";
        model.likeCount = 3;
        model.name = "List";
        model.publicId = 111;
        model.id = "listId";
        model.listKind = 1;
        model.repostCount = 2;
        model.ownerLogin = "user";

        let listItemModel: ListItemModel = new ListItemModel();

        listItemModel.content = "item 1";
        listItemModel.ownerId = model.ownerId;
        listItemModel.createEventTime = new Date();

        model.items.push(listItemModel);

        comp.list = model;
    });

    it('should display list name', () => {
        fixture.detectChanges();
        let de = fixture.debugElement.query(By.css('.feed-post h5'));
        let el = de.nativeElement;
        expect(el.textContent).toContain(model.ownerFullName);
    });

    it('should only one item', () => {
        fixture.detectChanges();
        let lists: DebugElement[] = fixture.debugElement.queryAll(By.css('.listItems list-item'));
        
        expect(lists.length === 1).toBeTruthy("Ithem should be one");
    });

    it('should only two item', () => {
        fixture.detectChanges();

        let listItemModel: ListItemModel = new ListItemModel();

        listItemModel.content = "item 2";
        listItemModel.ownerId = model.ownerId;
        listItemModel.createEventTime = new Date();

        model.items.push(listItemModel);

        fixture.detectChanges();

        let lists: DebugElement[] = fixture.debugElement.queryAll(By.css('.listItems list-item'));

        expect(lists.length === 2).toBeTruthy("Ithem should be two");
    });


    it('Show like button', (done: any) => {
        fixture.detectChanges();

        let de: DebugElement = fixture.debugElement.query(By.css('.action-list .glyphicon-heart'));
        expect(de == null).toBeTruthy("Like button is not visible for unlogged users");

        authService.login('test', 'admin').then((result: AuthResult) => {
            expect(result.success).toBeTruthy('fault login');

            fixture.detectChanges();
            de = fixture.debugElement.query(By.css('.action-list .glyphicon-heart'));
            expect(de != null).toBeTruthy('like not exists');
            done();
        });
    });
});
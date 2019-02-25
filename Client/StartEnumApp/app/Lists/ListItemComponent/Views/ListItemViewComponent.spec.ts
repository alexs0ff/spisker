import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ListItemViewComponent } from './ListItemViewComponent';
import {ListItemModel} from "../../Models/ListItemModel";

import {FeedListService} from "../../FeedModule";
import {IfOwnedUserDirective} from "../../../Auth/Directives/IfOwnedUserDirective";
import {Sender} from "../../../Utils/Sender/SenderModule";
import { ErrorFlowService } from "../../../ErrorFlow/ErrorFlowModule";
import {SenderStub} from "../../../../testing/SenderStub";
import {AuthentificateService, AuthResult } from "../../../Auth/AuthModule";
import { AuthentificateServiceStub } from "../../../../testing/AuthentificateServiceStub";
import {ListItemDraggableDirective} from "../../ListItemDragAndDrop/ListItemDraggableDirective";
describe('ListItemViewComponent (inline template)', () => {

    let comp: ListItemViewComponent;
    let fixture: ComponentFixture<ListItemViewComponent>;
    let de: DebugElement;
    let el: HTMLElement;
    let model: ListItemModel;
    let sender: SenderStub;
    let authService: AuthentificateServiceStub;

    beforeEach(() => {

        let senderStub = new SenderStub();
        let authServiceStub = new AuthentificateServiceStub();

        TestBed.configureTestingModule({
            declarations: [ListItemViewComponent, IfOwnedUserDirective, ListItemDraggableDirective],
            providers: [FeedListService, ErrorFlowService, { provide: Sender, useValue: senderStub }, { provide: AuthentificateService, useValue: authServiceStub}]

        });

        sender = TestBed.get(Sender);
        authService = TestBed.get(AuthentificateService);

        fixture = TestBed.createComponent(ListItemViewComponent);

        comp = fixture.componentInstance; // BannerComponent test instance
        model = new ListItemModel();
        model.content = "Test content";
        model.ownerId = "userId";
        comp.item = model;
        comp.listId = 'test list id';
        comp.checkItemKind = 1;

        // query for the title <h1> by CSS element selector
        de = fixture.debugElement.query(By.css('.list-item-content span'));
        el = de.nativeElement;
    });

    it('no conntent in the DOM until manually call `detectChanges`', () => {
        expect(el.textContent.trim()).toEqual('');
    });

    it('should display original title', () => {
        fixture.detectChanges();
        expect(el.textContent).toContain(model.content);
    });

    it('should display a different test title', () => {
        model.content = 'Test Title';
        fixture.detectChanges();
        expect(el.textContent).toContain(model.content);
    });

    it('Test show controls', () => {
        fixture.detectChanges();
        let elem: DebugElement = fixture.debugElement.query(By.css('.action-list'));
        expect(elem == null).toBeTruthy('Element exists');
        comp.showControls = true;
        fixture.detectChanges();
        elem = fixture.debugElement.query(By.css('.action-list'));
        expect(elem != null).toBeTruthy('Element not exists');
    });

    it('Test visible edit button', (done: any) => {
        fixture.detectChanges();//Обязательно вызывать
        comp.showControls = true;
        fixture.detectChanges();
        let elem: DebugElement = fixture.debugElement.query(By.css('.action-list .glyphicon-edit'));
        expect(elem == null).toBeTruthy('edit exists');


        authService.login('test', 'admin').then((result: AuthResult) => {
            expect(result.success).toBeTruthy('fault login');

            fixture.detectChanges();
            elem = fixture.debugElement.query(By.css('.action-list .glyphicon-edit'));
            expect(elem != null).toBeTruthy('edit not exists');
            done();
        });
        
    });
});
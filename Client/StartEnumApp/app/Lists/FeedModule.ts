import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import {FeedCardComponent} from "./FeedCard/FeedCardComponent";
import {ListComponent} from "./ListComponent/ListComponent";
import { FeedListService } from "./FeedCardService/FeedListService";
import {ListItemComponent} from "./ListItemComponent/ListItemComponent";
import {ListItemEditComponent} from "./ListItemComponent/Views/ListItemEditComponent";
import {ListItemViewComponent} from "./ListItemComponent/Views/ListItemViewComponent";

import {ListNameViewComponent} from "./ListComponent/NameViews/ListNameViewComponent";
import {ListNameEditComponent} from "./ListComponent/NameViews/ListNameEditComponent";
import {AuthModule} from "../Auth/AuthModule";
import { ListItemDraggableDirective } from "./ListItemDragAndDrop/ListItemDraggableDirective";
import { ListItemDragAndDropService } from "./ListItemDragAndDrop/ListItemDragAndDropService";
import {ListItemSplitterComponent} from "./ListItemSplitterComponent/ListItemSplitterComponent";
import {ListButtonComponent} from "./ListButtonComponent/ListButtonComponent";
import {ListOptionsComponent} from "./ListOptionsComponent/ListOptionsComponent";
import { UtilsModule } from "../Utils/UtilsModule";
import { MomentModule } from 'angular2-moment/moment.module';
import {ListViewDialogService} from "./ListViewDialog/ListViewDialogService";
import {ListViewDialogComponent} from "./ListViewDialog/ListViewDialogComponent/ListViewDialogComponent";
import {MenuModule} from "../Utils/Menu/MenuModule";

@NgModule({
    imports: [
        CommonModule, FormsModule, ReactiveFormsModule, AuthModule, MenuModule, UtilsModule, MomentModule
    ],
    declarations: [
        FeedCardComponent,
        ListComponent,
        ListItemComponent,
        ListItemEditComponent,
        ListItemViewComponent,
        ListNameViewComponent,
        ListNameEditComponent,
        ListItemDraggableDirective,
        ListItemSplitterComponent,
        ListButtonComponent,
        ListOptionsComponent,
        ListViewDialogComponent
    ],
    exports: [FeedCardComponent]
})
class FeedModule {}

export {
    FeedModule,
    FeedListService,
    ListItemDragAndDropService,
    ListViewDialogService,
    FeedCardComponent
    }
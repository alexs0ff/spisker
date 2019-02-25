
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import {AccountComponent} from "./AccountComponent";
import {SettingsComponent} from "./Settings/SettingsComponent/SettingsComponent";
import { AuthGuard } from "../Auth/AuthModule";

const accountRoutes: Routes = [
    {
        path: 'i/account',
        component: AccountComponent,
        children: [
            {
                path: 'settings',
                component:SettingsComponent
            }
        ],
        canActivate: [AuthGuard]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(accountRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AccountRoutingModule { }
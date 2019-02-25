import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartenumComponent, HomeComponent, FollowersComponent, FollowingsComponent } from "./Home/HomeModule";
import { RegisterComponent, RecoverComponent, ApproveRecoverComponent } from "./Register/RegisterModule";


/* Url для компонента*/

const appRoutes: Routes = [
    { path: '', pathMatch: 'full', component: HomeComponent},
    { path: ':userName', pathMatch: 'full', component: StartenumComponent },
    { path: ':userName/followers', pathMatch: 'full', component: FollowersComponent },
    { path: ':userName/followings', pathMatch: 'full', component: FollowingsComponent },
    { path: ':userName/:listId', pathMatch: 'full', component: StartenumComponent },
    { path: 'i/account/register', pathMatch: 'full', component: RegisterComponent },
    { path: 'i/account/recover', pathMatch: 'full', component: RecoverComponent },
    { path: 'i/account/approve/:userName/:number', pathMatch: 'full', component: ApproveRecoverComponent }
   
    //{ path: '**', component: PageNotFoundComponent }
];

@NgModule({
    imports: [
        RouterModule.forRoot(appRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }
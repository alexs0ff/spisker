import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { APP_BASE_HREF } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ORIGIN_URL, REQUEST } from '@nguniversal/aspnetcore-engine/tokens';
import { AppModuleShared } from './AppModuleShared';
import { AppModuleSharedTest } from './AppModuleTest';
import { AppComponentTest } from './AppComponent/AppComponentTest';
import { AppComponent } from './AppComponent/AppComponent';
import { BrowserTransferStateModule } from '@angular/platform-browser';
import { BrowserPrebootModule } from 'preboot/browser';
import { SERVERPARAMS} from "./Settings/serverparams";

export function getOriginUrl() {
  return window.location.origin;
}

export function getRequest() {
  // the Request object only lives on the server
  return { cookie: document.cookie };
}

export function getServerParameters() {
  return window['TRANSFER_CACHE'];
}

@NgModule({
  bootstrap: [AppComponent],
  imports: [
    BrowserPrebootModule.replayEvents(),
    BrowserAnimationsModule,

    // Our Common AppModule
    AppModuleShared
    //AppModuleSharedTest

  ],
  providers: [
    {
      // We need this for our Http calls since they'll be using an ORIGIN_URL provided in main.server
      // (Also remember the Server requires Absolute URLs)
      provide: ORIGIN_URL,
      useFactory: (getOriginUrl)
    }, {
      // The server provides these in main.server
      provide: REQUEST,
      useFactory: (getRequest)
    }, {
      provide: SERVERPARAMS,
      useFactory: (getServerParameters)
    }
  ]
})
export class AppModule { }

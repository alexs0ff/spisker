import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { BrowserModule } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';


import { ServerTransferStateModule } from '@angular/platform-server';

import { ServerPrebootModule } from 'preboot/server';
import { AppModuleShared } from './AppModuleShared';
import { AppComponent } from './AppComponent/AppComponent';

@NgModule({
  bootstrap: [AppComponent],
  imports: [
    // Our Common AppModule
    AppModuleShared,

    ServerModule,
    ServerPrebootModule.recordEvents({ appRoot: 'startenum-app' }),
    NoopAnimationsModule,

    // HttpTransferCacheModule still needs fixes for 5.0
    //   Leave this commented out for now, as it breaks Server-renders
    //   Looking into fixes for this! - @MarkPieszak
    // ServerTransferStateModule // <-- broken for the time-being with ASP.NET
  ]
})
export class AppModule {

  constructor() { }

}

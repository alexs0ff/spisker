import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { BrowserModule } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { AppModuleSharedTest } from './AppModuleTest';

import { AppModuleShared } from './AppModuleShared';
import { AppComponent } from './AppComponent/AppComponent';
import { AppComponentTest } from './AppComponent/AppComponentTest';
import { ServerTransferStateModule } from '@angular/platform-server';

import { ServerPrebootModule } from 'preboot/server';

@NgModule({
  bootstrap: [AppComponent],
  //bootstrap: [AppComponentTest],
  imports: [
    // Our Common AppModule
    AppModuleShared,
    //AppModuleSharedTest,

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

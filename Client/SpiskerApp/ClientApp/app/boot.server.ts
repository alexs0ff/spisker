import 'zone.js/dist/zone-node';
import './polyfills/server.polyfills';
import { enableProdMode } from '@angular/core';
import { createServerRenderer } from 'aspnet-prerendering';
import { SERVERPARAMS } from "./Settings/serverparams";

// Grab the (Node) server-specific NgModule
import { AppModule } from './app.module.server';
import { ngAspnetCoreEngine, IEngineOptions, createTransferScript } from '@nguniversal/aspnetcore-engine';

enableProdMode();

export default createServerRenderer((params) => {

  // Platform-server provider configuration
  const setupOptions: IEngineOptions = {
    appSelector: '<startenum-app></startenum-app>',
    ngModule: AppModule,
    request: params,
    providers: [
      { provide: SERVERPARAMS, useValue: params.data }
    ]
  };

  return ngAspnetCoreEngine(setupOptions).then(response => {

    // Apply your transferData to response.globals
    response.globals.transferData = createTransferScript({
      //fromDotnet: params.data.thisCameFromDotNET // example of data coming from dotnet, in HomeController
        //testData:params.data
    });

    return ({
      html: response.html, // our <app-root> serialized
      globals: response.globals // all of our styles/scripts/meta-tags/link-tags for aspnet to serve up
    });
  });
});

import 'zone.js/dist/zone-node';
import './polyfills/server.polyfills';
import { enableProdMode } from '@angular/core';
import { createServerRenderer } from 'aspnet-prerendering';
import { SERVERPARAMS } from "./Settings/serverparams";

// Grab the (Node) server-specific NgModule
const { AppModuleNgFactory } = require('./app.module.server.ngfactory'); // <-- ignore this - this is Production only
import { ngAspnetCoreEngine, IEngineOptions, createTransferScript } from '@nguniversal/aspnetcore-engine';

enableProdMode();

export default createServerRenderer((params) => {

    // Platform-server provider configuration
    const setupOptions: IEngineOptions = {
      appSelector: '<startenum-app></startenum-app>',
        ngModule: AppModuleNgFactory,
        request: params,
        providers: [
          { provide: SERVERPARAMS, useValue: params.data }
        ]
    };

    return ngAspnetCoreEngine(setupOptions).then(response => {

        // Apply your transferData to response.globals
        response.globals.transferData = createTransferScript({
           
        });

        return ({
            html: response.html, // our <app-root> serialized
            globals: response.globals // all of our styles/scripts/meta-tags/link-tags for aspnet to serve up
        });
    });
});

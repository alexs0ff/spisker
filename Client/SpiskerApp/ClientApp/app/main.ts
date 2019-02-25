import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModuleShared } from "./AppModuleShared";

const platform = platformBrowserDynamic();
platform.bootstrapModule(AppModuleShared);


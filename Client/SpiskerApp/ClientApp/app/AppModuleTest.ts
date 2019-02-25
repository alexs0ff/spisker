import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponentTest } from './AppComponent/AppComponentTest';
@NgModule({
  //imports: [BrowserModule, FormsModule],
  imports: [BrowserModule.withServerTransition({ appId: 'my-app' }), FormsModule],
  declarations: [AppComponentTest],
  bootstrap: [AppComponentTest]
})
export class AppModuleSharedTest { }
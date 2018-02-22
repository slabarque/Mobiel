import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CodemirrorModule } from 'ng2-codemirror';

import { AppComponent } from './app.component';
import { DrawingComponent } from './drawing/drawing.component';
import { PartComponent } from './part/part.component';
import { DrawingService } from './service/drawing.service';
import { ArrowComponent } from './arrow/arrow.component';
import { ScriptComponent } from './script/script.component';



@NgModule({
  declarations: [
    AppComponent,
    DrawingComponent,
    PartComponent,
    ArrowComponent,
    ScriptComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    CodemirrorModule,
    FormsModule
  ],
  providers: [DrawingService],
  bootstrap: [AppComponent]
})
export class AppModule { }

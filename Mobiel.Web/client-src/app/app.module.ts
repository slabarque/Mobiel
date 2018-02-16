import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';


import { AppComponent } from './app.component';
import { DrawingComponent } from './drawing/drawing.component';
import { PartComponent } from './part/part.component';
import { DrawingService } from './drawing/service/drawing.service';
import { ArrowComponent } from './arrow/arrow.component';



@NgModule({
  declarations: [
    AppComponent,
    DrawingComponent,
    PartComponent,
    ArrowComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [DrawingService],
  bootstrap: [AppComponent]
})
export class AppModule { }

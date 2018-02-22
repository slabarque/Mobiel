import { Component, OnInit } from '@angular/core';
import { DrawingService, Object2D, Point } from './service/drawing.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';
  drawing: Object2D;

  constructor(private drawingService: DrawingService) {

  }

  ngOnInit(): void {
    
  }

  recalculateDrawing(code: string) {
    this.drawingService.getDrawing(code)
      .subscribe(data => {
        this.drawing = data;
      });
  }
}

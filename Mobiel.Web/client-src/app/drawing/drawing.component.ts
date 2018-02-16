import { Component, OnInit } from '@angular/core';
import { PartComponent } from '../part/part.component';
import { DrawingService, Object2D, Point } from './service/drawing.service';

@Component({
  selector: 'app-drawing',
  templateUrl: './drawing.component.html',
  styleUrls: ['./drawing.component.css']
})
export class DrawingComponent implements OnInit {
  width: number = 800;
  height: number = 1300;
  sx: number = 0.40;
  sy: number = -0.40;
  cx: number = 0;
  cy: number = 10;
  transform: string;
  viewbox: string;
  drawing: Object2D;
  colors: string[];
  centerOfGravityArrow: string;

  constructor(private drawingService: DrawingService) {
    this.transform = "matrix(" + this.sx + ", 0, 0, " + this.sy + ", " + (this.cx - this.sx * this.cx) + ", " + (this.cy - this.sy * this.cy) + ")";
    this.viewbox = "-300 -100 " + (this.width + 100) + " " + (this.height + 100);
  }

  ngOnInit() {
    this.createDrawing();
  }

  createDrawing() {
    this.drawingService.getDrawing()
      .subscribe(data => {
        this.drawing = data;
        this.colors = [];
        for (var i = 0; i < this.drawing.parts.length; i++) {
          this.colors.push(this.getColorByIndex(i));
        }
      });
  }

  getColorByIndex(index: number) {
    var colors = ["black", "red", "green", "grey", "blue", "purple", "yellow", "orange", "cyan"];
    return colors[index % colors.length];
  }

  getColor(weight: number) {
    var maxWeight = this.drawing.parts.reduce(function (p: number, c, i, arr) { return p < c.weight ? c.weight : p; }, 0)
    var rgbValue = 255 - ((255 / maxWeight) * weight) * 4 | 0;
    return "rgb(" + (255 - rgbValue) + "," + rgbValue + "," + rgbValue + ")";
  }

}

import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { PartComponent } from '../part/part.component';
import { Object2D, Point } from '../service/drawing.service';

@Component({
  selector: 'app-drawing',
  templateUrl: './drawing.component.html',
  styleUrls: ['./drawing.component.css']
})
export class DrawingComponent implements OnInit, OnChanges {
  @Input() drawing: Object2D;

  width: number = 800;
  height: number = 1300;
  sx: number = 1;//0.40;
  sy: number = -1;//-0.40;
  cx: number = 0;
  cy: number = 0;
  xmin: number = -300;
  ymin: number = -300;
  transform: string;
  viewbox: string;
  colors: string[];
  centerOfGravityArrow: string;

  constructor() {
    this.transform = "matrix(" + this.sx + ", 0, 0, " + this.sy + ", " + (this.cx - this.sx * this.cx) + ", " + (this.cy - this.sy * this.cy) + ")";
    this.viewbox = this.xmin + " " + this.ymin + " " + (this.width + 600) + " " + (this.height + 600);
  }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.drawing) {
      this.colors = [];
      for (var i = 0; i < this.drawing.parts.length; i++) {
        this.colors.push(this.getColorByIndex(i));
      }
    }
  }

  getColorByIndex(index: number) {
    var colors = ["black", "red", "green", "grey", "blue", "purple", "orange", "cyan"];
    return colors[index % colors.length];
  }

  getColor(weight: number) {
    var maxWeight = this.drawing.parts.reduce(function (p: number, c, i, arr) { return p < c.weight ? c.weight : p; }, 0)
    var rgbValue = 255 - ((255 / maxWeight) * weight) * 4 | 0;
    return "rgb(" + (255 - rgbValue) + "," + rgbValue + "," + rgbValue + ")";
  }

}

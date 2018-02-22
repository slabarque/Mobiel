import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Point } from '../service/drawing.service';

@Component({
  selector: '[app-arrow]',
  templateUrl: './arrow.component.html',
  styleUrls: ['./arrow.component.css']
})
export class ArrowComponent implements OnInit, OnChanges {
  @Input() point: Point;
  @Input() length: number;
  @Input() color: string;

  arrowString: string;
  constructor() { }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.arrowString = this.getTriangle();
  }

  getTriangle() {
    var yend = this.point.y - this.length;
    return (this.point.x - 3) + "," + yend + " " + (this.point.x + 3) + "," + yend + " " + this.point.x + "," + (yend - 6);
  }
}

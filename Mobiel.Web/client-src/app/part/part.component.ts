import { Component, OnInit, OnChanges, Input } from '@angular/core';
import { Part, Point } from '../drawing/service/drawing.service';

@Component({
  selector: '[app-part]',
  templateUrl: './part.component.html',
  styleUrls: ['./part.component.css']
})
export class PartComponent implements OnInit, OnChanges {
  @Input() part: Part;
  @Input() color: string;
  pointsString: string;

  constructor() {
    //this.pointsString = "125,30 125,30 125,30 31.9,63.2 46.1,186.3 125,230 125,230 125,230 203.9,186.3 218.1,63.2"
  }

  ngOnInit() {
    //this.pointsString = this.points[0][0] + "," + this.points[0][1] + " " + this.points[1][0] + "," + this.points[1][1] + " " + this.points[2][0] + "," + this.points[2][1] + " " + this.points[3][0] + "," + this.points[3][1];
  }

  ngOnChanges() {
    var p = this.part.polygon;
    this.pointsString = p[0].x + "," + p[0].y + " " + p[1].x + "," + p[1].y + " " + p[2].x + "," + p[2].y + " " + p[3].x + "," + p[3].y;
  }

}

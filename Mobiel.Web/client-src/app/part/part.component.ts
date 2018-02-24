import { Component, OnInit, OnChanges, Input } from '@angular/core';
import { Part, Point } from '../service/drawing.service';

@Component({
  selector: '[app-part]',
  templateUrl: './part.component.html',
  styleUrls: ['./part.component.css']
})
export class PartComponent implements OnInit, OnChanges {
  @Input() part: Part;
  @Input() old: boolean;
  @Input() color: string = "black";
  pointsString: string;

  constructor() {
    this.pointsString = "";
  }

  ngOnInit() {
  }

  ngOnChanges() {
    var points = this.part.polygon;
    for(var point of points)
      this.pointsString += point.x + "," + point.y + " ";
  }

}

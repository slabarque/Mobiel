import { Injectable} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

export class Point {
  x: number;
  y: number;
}

export class Part {
  polygon: Point[];
  centroid: Point;
  weight: number;
}

export class Object2D {
  parts: Part[];
  centerOfGravity: Point;
  weight: number;
}

@Injectable()
export class DrawingService {

  constructor(private http: HttpClient) { }

  drawingUrl = 'api/shapes';

  getDrawing(): Observable<Object2D> {
    return this.http.get<Object2D>(this.drawingUrl);
  }

}

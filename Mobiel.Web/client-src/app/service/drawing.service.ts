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
  oldParts: Part[];
  parts: Part[];
  oldCenterOfGravity: Point;
  centerOfGravity: Point;
  weight: number;
  ankerPoint: Point;
}

@Injectable()
export class DrawingService {

  constructor(private http: HttpClient) { }

  drawingUrl = 'api/drawing';

  getDrawing(code:string): Observable<Object2D> {
    return this.http.post<Object2D>(this.drawingUrl, { code: code });
  }

}

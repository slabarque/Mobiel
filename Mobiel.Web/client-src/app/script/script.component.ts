import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CodemirrorComponent } from 'ng2-codemirror';

@Component({
  selector: 'app-script',
  templateUrl: './script.component.html',
  styleUrls: ['./script.component.css']
})
export class ScriptComponent implements OnInit {
  @Output() codeUpdated = new EventEmitter<string>();
  code: string;
  options: any;
  constructor() {
    this.options = {
      lineNumbers: true
    };
    this.code =
      `A (225.0,430.0)
R(200, 200), -20, 200, 100
R(200, 200), 200, 20, 100
R(400, 200), 20, 200, 100
R(400, 400), -200, -20, 100
`;
    this.code =
      `A (280,444)
R(200, 200), -20, 200, 100
R(200, 200), 200, 20, 100
R(400, 200), 20, 200, 100
R(400, 400), -200, -20, 1000
R(280, 400), 40, 300, 1000
`;
    this.code =
      `A (280,478)
R(200, 200), -20, 200, 100
R(200, 200), 200, 20, 100
R(400, 200), 20, 200, 100
R(400, 400), -200, -20, 100
R(280, 400), 40, 530, 1000
R(340,220),60,60,1000
R(250, 930), 100, 40, 300
`;
  }

  ngOnInit() {
    this.updateCode();
  }

  updateCode() {
    this.codeUpdated.emit(this.code);
  }
}

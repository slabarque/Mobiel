webpackJsonp(["main"],{

/***/ "../../../../../client-src/$$_lazy_route_resource lazy recursive":
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncatched exception popping up in devtools
	return Promise.resolve().then(function() {
		throw new Error("Cannot find module '" + req + "'.");
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = "../../../../../client-src/$$_lazy_route_resource lazy recursive";

/***/ }),

/***/ "../../../../../client-src/app/app.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/app.component.html":
/***/ (function(module, exports) {

module.exports = "<!--The content below is only a placeholder and can be replaced.-->\n<div style=\"text-align:center\">\n  <app-drawing></app-drawing>\n</div>\n\n"

/***/ }),

/***/ "../../../../../client-src/app/app.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var AppComponent = /** @class */ (function () {
    function AppComponent() {
        this.title = 'app';
    }
    AppComponent = __decorate([
        core_1.Component({
            selector: 'app-root',
            template: __webpack_require__("../../../../../client-src/app/app.component.html"),
            styles: [__webpack_require__("../../../../../client-src/app/app.component.css")]
        })
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;


/***/ }),

/***/ "../../../../../client-src/app/app.module.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var platform_browser_1 = __webpack_require__("../../../platform-browser/esm5/platform-browser.js");
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var http_1 = __webpack_require__("../../../common/esm5/http.js");
var app_component_1 = __webpack_require__("../../../../../client-src/app/app.component.ts");
var drawing_component_1 = __webpack_require__("../../../../../client-src/app/drawing/drawing.component.ts");
var part_component_1 = __webpack_require__("../../../../../client-src/app/part/part.component.ts");
var drawing_service_1 = __webpack_require__("../../../../../client-src/app/drawing/service/drawing.service.ts");
var arrow_component_1 = __webpack_require__("../../../../../client-src/app/arrow/arrow.component.ts");
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            declarations: [
                app_component_1.AppComponent,
                drawing_component_1.DrawingComponent,
                part_component_1.PartComponent,
                arrow_component_1.ArrowComponent,
            ],
            imports: [
                platform_browser_1.BrowserModule,
                http_1.HttpClientModule
            ],
            providers: [drawing_service_1.DrawingService],
            bootstrap: [app_component_1.AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;


/***/ }),

/***/ "../../../../../client-src/app/arrow/arrow.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/arrow/arrow.component.html":
/***/ (function(module, exports) {

module.exports = "<svg:line [attr.x1]=\"point.x\" [attr.y1]=\"point.y\" [attr.x2]=\"point.x\" [attr.y2]=\"point.y-length\" [attr.stroke]=\"color\"  stroke-width=\"2\"/>\r\n<svg:polygon [attr.points]=\"arrowString\" [attr.stroke]=\"color\" [attr.fill]=\"color\" />\r\n"

/***/ }),

/***/ "../../../../../client-src/app/arrow/arrow.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var drawing_service_1 = __webpack_require__("../../../../../client-src/app/drawing/service/drawing.service.ts");
var ArrowComponent = /** @class */ (function () {
    function ArrowComponent() {
    }
    ArrowComponent.prototype.ngOnInit = function () {
    };
    ArrowComponent.prototype.ngOnChanges = function (changes) {
        this.arrowString = this.getTriangle();
    };
    ArrowComponent.prototype.getTriangle = function () {
        var yend = this.point.y - this.length;
        return (this.point.x - 3) + "," + yend + " " + (this.point.x + 3) + "," + yend + " " + this.point.x + "," + (yend - 6);
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", drawing_service_1.Point)
    ], ArrowComponent.prototype, "point", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Number)
    ], ArrowComponent.prototype, "length", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", String)
    ], ArrowComponent.prototype, "color", void 0);
    ArrowComponent = __decorate([
        core_1.Component({
            selector: '[app-arrow]',
            template: __webpack_require__("../../../../../client-src/app/arrow/arrow.component.html"),
            styles: [__webpack_require__("../../../../../client-src/app/arrow/arrow.component.css")]
        }),
        __metadata("design:paramtypes", [])
    ], ArrowComponent);
    return ArrowComponent;
}());
exports.ArrowComponent = ArrowComponent;


/***/ }),

/***/ "../../../../../client-src/app/drawing/drawing.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/drawing/drawing.component.html":
/***/ (function(module, exports) {

module.exports = "<div *ngIf=\"drawing\">\r\n  <svg [attr.width]=\"width\" [attr.height]=\"height\" [attr.viewBox]=\"viewbox\" [attr.transform]=\"transform\">\r\n    <svg:g *ngFor=\"let part of drawing.parts; let i=index\" app-part [part]=\"part\" [color]=\"colors[i]\" />\r\n    <svg:circle fill=\"red\" r=\"7\" [attr.cx]=\"drawing.centerOfGravity.x\" [attr.cy]=\"drawing.centerOfGravity.y\" />\r\n    <svg:g app-arrow [point]=\"drawing.centerOfGravity\" [length]=\"drawing.weight/30\" [color]=\"'red'\" />\r\n  </svg>\r\n</div>\r\n"

/***/ }),

/***/ "../../../../../client-src/app/drawing/drawing.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var drawing_service_1 = __webpack_require__("../../../../../client-src/app/drawing/service/drawing.service.ts");
var DrawingComponent = /** @class */ (function () {
    function DrawingComponent(drawingService) {
        this.drawingService = drawingService;
        this.width = 800;
        this.height = 1300;
        this.sx = 0.40;
        this.sy = -0.40;
        this.cx = 0;
        this.cy = 10;
        this.transform = "matrix(" + this.sx + ", 0, 0, " + this.sy + ", " + (this.cx - this.sx * this.cx) + ", " + (this.cy - this.sy * this.cy) + ")";
        this.viewbox = "-300 -100 " + (this.width + 100) + " " + (this.height + 100);
    }
    DrawingComponent.prototype.ngOnInit = function () {
        this.createDrawing();
    };
    DrawingComponent.prototype.createDrawing = function () {
        var _this = this;
        this.drawingService.getDrawing()
            .subscribe(function (data) {
            _this.drawing = data;
            _this.colors = [];
            for (var i = 0; i < _this.drawing.parts.length; i++) {
                _this.colors.push(_this.getColorByIndex(i));
            }
        });
    };
    DrawingComponent.prototype.getColorByIndex = function (index) {
        var colors = ["black", "red", "green", "grey", "blue", "purple", "yellow", "orange", "cyan"];
        return colors[index % colors.length];
    };
    DrawingComponent.prototype.getColor = function (weight) {
        var maxWeight = this.drawing.parts.reduce(function (p, c, i, arr) { return p < c.weight ? c.weight : p; }, 0);
        var rgbValue = 255 - ((255 / maxWeight) * weight) * 4 | 0;
        return "rgb(" + (255 - rgbValue) + "," + rgbValue + "," + rgbValue + ")";
    };
    DrawingComponent = __decorate([
        core_1.Component({
            selector: 'app-drawing',
            template: __webpack_require__("../../../../../client-src/app/drawing/drawing.component.html"),
            styles: [__webpack_require__("../../../../../client-src/app/drawing/drawing.component.css")]
        }),
        __metadata("design:paramtypes", [drawing_service_1.DrawingService])
    ], DrawingComponent);
    return DrawingComponent;
}());
exports.DrawingComponent = DrawingComponent;


/***/ }),

/***/ "../../../../../client-src/app/drawing/service/drawing.service.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var http_1 = __webpack_require__("../../../common/esm5/http.js");
var Point = /** @class */ (function () {
    function Point() {
    }
    return Point;
}());
exports.Point = Point;
var Part = /** @class */ (function () {
    function Part() {
    }
    return Part;
}());
exports.Part = Part;
var Object2D = /** @class */ (function () {
    function Object2D() {
    }
    return Object2D;
}());
exports.Object2D = Object2D;
var DrawingService = /** @class */ (function () {
    function DrawingService(http) {
        this.http = http;
        this.drawingUrl = 'api/shapes';
    }
    DrawingService.prototype.getDrawing = function () {
        return this.http.get(this.drawingUrl);
    };
    DrawingService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], DrawingService);
    return DrawingService;
}());
exports.DrawingService = DrawingService;


/***/ }),

/***/ "../../../../../client-src/app/part/part.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/part/part.component.html":
/***/ (function(module, exports) {

module.exports = "<svg:polygon [attr.points]=\"pointsString\" fill=\"none\" [attr.stroke]=\"color\" />\r\n<svg:circle fill=\"black\" r=\"5\" [attr.cx]=\"part.centroid.x\" [attr.cy]=\"part.centroid.y\" />\r\n<svg:g app-arrow [point]=\"part.centroid\" [length]=\"part.weight/30\" [color]=\"'black'\" />\r\n\r\n"

/***/ }),

/***/ "../../../../../client-src/app/part/part.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var drawing_service_1 = __webpack_require__("../../../../../client-src/app/drawing/service/drawing.service.ts");
var PartComponent = /** @class */ (function () {
    function PartComponent() {
        //this.pointsString = "125,30 125,30 125,30 31.9,63.2 46.1,186.3 125,230 125,230 125,230 203.9,186.3 218.1,63.2"
    }
    PartComponent.prototype.ngOnInit = function () {
        //this.pointsString = this.points[0][0] + "," + this.points[0][1] + " " + this.points[1][0] + "," + this.points[1][1] + " " + this.points[2][0] + "," + this.points[2][1] + " " + this.points[3][0] + "," + this.points[3][1];
    };
    PartComponent.prototype.ngOnChanges = function () {
        var p = this.part.polygon;
        this.pointsString = p[0].x + "," + p[0].y + " " + p[1].x + "," + p[1].y + " " + p[2].x + "," + p[2].y + " " + p[3].x + "," + p[3].y;
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", drawing_service_1.Part)
    ], PartComponent.prototype, "part", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", String)
    ], PartComponent.prototype, "color", void 0);
    PartComponent = __decorate([
        core_1.Component({
            selector: '[app-part]',
            template: __webpack_require__("../../../../../client-src/app/part/part.component.html"),
            styles: [__webpack_require__("../../../../../client-src/app/part/part.component.css")]
        }),
        __metadata("design:paramtypes", [])
    ], PartComponent);
    return PartComponent;
}());
exports.PartComponent = PartComponent;


/***/ }),

/***/ "../../../../../client-src/environments/environment.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
Object.defineProperty(exports, "__esModule", { value: true });
exports.environment = {
    production: false
};


/***/ }),

/***/ "../../../../../client-src/main.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var platform_browser_dynamic_1 = __webpack_require__("../../../platform-browser-dynamic/esm5/platform-browser-dynamic.js");
var app_module_1 = __webpack_require__("../../../../../client-src/app/app.module.ts");
var environment_1 = __webpack_require__("../../../../../client-src/environments/environment.ts");
if (environment_1.environment.production) {
    core_1.enableProdMode();
}
platform_browser_dynamic_1.platformBrowserDynamic().bootstrapModule(app_module_1.AppModule)
    .catch(function (err) { return console.log(err); });


/***/ }),

/***/ 0:
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__("../../../../../client-src/main.ts");


/***/ })

},[0]);
//# sourceMappingURL=main.bundle.js.map
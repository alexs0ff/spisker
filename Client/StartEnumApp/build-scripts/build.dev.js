var gulp = require('gulp');
var gutil = require('gulp-util');
var colors = gutil.colors;
var streamqueue = require('streamqueue');
var es = require('event-stream');
var print = require('gulp-print');
var nodePath = require('path');
var rename = require("gulp-rename");
var cssmin = require("gulp-cssmin");
var concat = require("gulp-concat");
var vinylPaths = require('vinyl-paths');
var del = require('del');


var compilation = require('./compilation.js');
var bundling = require('./bundling.js');
var pathTools = require('./path-tools.js');

//var pathTools = require('./build-scripts/path-tools.js');

var paths = require('./variables.js');

var build = {};


build.compile = function (context) {

    var predprocessorTs = function () {
        return compilation.confTSPredprocessor(paths.appTs, paths.app,false);
    };

    var compileHomeLess = function () {
        return compilation.compileLess(paths.homeComponent);
    };
    
    return streamqueue({ objectMode: true },
        predprocessorTs,
        compileHomeLess
        
    );
};

module.exports = build;

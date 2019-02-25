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
var mkdirp = require('mkdirp');


var compilation = require('./compilation.js');
var bundling = require('./bundling.js');
var pathTools = require('./path-tools.js');

//var pathTools = require('./build-scripts/path-tools.js');

var paths = require('./variables.js');

var build = {};


build.clean = function (context) {
  gutil.log("Cleaning.." + colors.magenta(context.destDir));

  return gulp.src([
      context.destDir + "**/*.*"
  ], { read: false })
    .pipe(vinylPaths(del))
    .on('end', () => gutil.log("Cleaning done."));
};

build.createDebugDir = function (context) {

  return gulp.src('*.*', { read: false })
    .pipe(gulp.dest(context.destDir + "app/"))
    .pipe(gulp.dest(paths.devassets))
    .pipe(gulp.dest(context.destDir + "scripts/"))
    .pipe(gulp.dest(context.destDir + "testing/"))
    .on('end', () => gutil.log("Creating dirs done."));
}

build.copy = function (context) {
  gutil.log("coping.." + colors.magenta(context.destDir));

  return streamqueue({ objectMode: true },
    () => {
      return gulp.src(
          paths.webroot + "ClientApp/app/**/*.*"
        )
        .pipe(gulp.dest(context.destDir + "app/"));
    },

    () => {
      return gulp.src(
          paths.webroot + "ClientApp/scripts/**/*.*"
        )
        .pipe(gulp.dest(context.destDir + "scripts/"));
    },
    
    () => {
      return gulp.src(
        paths.webroot + "ClientApp/assets/**/*.*"
        )
        .pipe(gulp.dest(paths.devassets));
    },

    () => {
      return gulp.src(
        paths.webroot + "ClientApp/test/**/*.*"
        )
        .pipe(gulp.dest(context.destDir));
    },

    () => {
      return gulp.src(
          paths.webroot + "ClientApp/testing/**/*.*"
        )
        .pipe(gulp.dest(context.destDir + "testing/"));
    }
  );
}

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

build.prepareToLaunch = function (context) {
  var createDest = function () {
    return build.createDebugDir(context);
  };

  var cleanDest = function () {
    return build.clean(context);
  };

  var copyDest = function () {
    return build.copy(context);
  };
  
  var compileTs = function() {
    return compilation.compileTypescript(paths.debugTsConfig, paths.devdest);
  }

  return streamqueue({ objectMode: true },
    cleanDest,
    createDest,
    copyDest,
    compileTs
  );
};



module.exports = build;

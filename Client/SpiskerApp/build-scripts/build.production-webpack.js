var gulp = require('gulp');
var gutil = require('gulp-util');
var colors = gutil.colors;
var streamqueue = require('streamqueue');
var vinylPaths = require('vinyl-paths');
var del = require('del');
var compilation = require('./compilation.js');

var paths = require('./variables.js');

var build = {};


build.compile = function (context) {
  
    var predprocessorTs = function () {
        return compilation.confTSPredprocessor(paths.appTs, paths.app,true);
    };
    
    var compileVendorWebpack = function () {
      return compilation.webpack(paths.webpackVendorConf, context.par);
    }

    var compileServerWebpack = function () {
      return compilation.webpack(paths.webpackServerConf, context.par);
    }

    var compileBaseLess = function () {
        return compilation.compileLess(paths.assestsCssPath);
    };

    return streamqueue({ objectMode: true },
        //predprocessorTs,
      compileVendorWebpack,
      compileServerWebpack
    );
};

build.copy = function (context) {
  gutil.log("coping.." + colors.magenta(context.destDir));

  return streamqueue({ objectMode: true },
    () => {
      return gulp.src(
        paths.webroot + "ClientApp/assets/imgs/**/*.*"
        )
        .pipe(gulp.dest(context.destDir +'assets/imgs/'));
    }
  );
}

build.clean = function (context) {
  gutil.log("Cleaning.." + colors.magenta(context.destDir));
  return gulp.src([
      context.destDir + "**/*.*"
    ], { read: false })
    .pipe(vinylPaths(del))
    .on('end', () => gutil.log("Cleaning done."));
};

build.build = function (context) {
    return streamqueue({ objectMode: true },
        () => build.clean(context),
        () => build.copy(context),
        () => build.compile(context)
    );
}

module.exports = build;

var gulp = require('gulp');
var gutil = require('gulp-util');

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
    
    var compileProdWebpack = function () {
        return compilation.webpack(paths.webpackProdConf);
    }

    var compileBaseLess = function () {
        return compilation.compileLess(paths.assestsCssPath);
    };

   

    return streamqueue({ objectMode: true },
        predprocessorTs,
        compileBaseLess,
        compileProdWebpack
    );
};

build.clean = function (context) {
    gutil.log("Cleaning...");
    return gulp.src([
        paths.app + "/**/*.js",
            paths.app + '/**/*.js.map',
            paths.app + '/**/*.metadata.json',
            paths.aot,
            context.destDir
        ])
        .pipe(vinylPaths(del))
        .on('end', () => gutil.log("Cleaning done."));
};

build.build = function (context) {
    return streamqueue({ objectMode: true },
        () => build.clean(context),
        () => build.compile(context)
    );
}

module.exports = build;

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


build.commonBundle = function (context) {
    var packages = [
        paths.shimJs,
        pathTools.resolvePackagePath("zone.js"),
        pathTools.resolvePackagePath("jquery"),
        paths.bootstrapJs,
        paths.linkifyJs,
        paths.linkifyElementJs,
        paths.scripts
    ];

    var bundle = bundling.concatBundle("common.min.js", packages,
        { destDir: context.destDir, uglify: true, cache: true });
    return bundle.stream;
};


build.bundle = function (context) {
    return es.merge(
        build.commonBundle(context),
        build.html(context),
        build.css(context),
        build.assets(context),
        build.fonts(context),
        build.buildjs(context)
    );
}

build.assets = function (context) {
    return gulp.src("./assets/imgs/**/*.*")
        .pipe(gulp.dest(nodePath.join(context.destDir, "assets/imgs")));
}

build.css = function (context) {
    return gulp.src([paths.bootstrapCss, paths.fontawesomeCss, paths.assestsCss])
        .pipe(cssmin())
        .pipe(concat("bundle.css"))
        .pipe(gulp.dest(nodePath.join(context.destDir, "assets/css")));
}

build.fonts = function (context) {
    return gulp.src([paths.bootstrapFonts, paths.fontawesomeFonts])
        .pipe(gulp.dest(nodePath.join(context.destDir, "assets/fonts")));
}

build.buildjs = function (context) {
    return gulp.src(paths.buildjs)
        .pipe(gulp.dest(context.destDir));
}

build.html = function (context) {
    return gulp.src(context.indexPage)
        .pipe(rename("index.html"))
        .pipe(gulp.dest(context.destDir));
}

build.compile = function (context) {

    var predprocessorTs = function () {
        return compilation.confTSPredprocessor(paths.appTs, paths.app,true);
    };

    var compileProdLess = function () {
        return compilation.compileLess(paths.homeComponent);
    };

    var compileBaseLess = function () {
        return compilation.compileLess(paths.assestsCssPath);
    };

    var compileProdAot = function () {
        return compilation.compileAot(paths.tsAotConfig);
    }
    var compileProdRollup = function () {
        return compilation.rollup(paths.rollupConfig);
    }

    return streamqueue({ objectMode: true },
        predprocessorTs,
        compileProdLess,
        compileBaseLess,
        compileProdAot,
        compileProdRollup
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
        () => build.compile(context),
        () => build.bundle(context)
    );
}

module.exports = build;

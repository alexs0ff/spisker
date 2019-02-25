var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    del = require('del'),
    less = require("gulp-less"),
    runSequence = require('run-sequence'),
    rename = require("gulp-rename");

var compilation = require('./build-scripts/compilation.js');
var build = require('./build-scripts/build.production-aot.js');

var builddev = require('./build-scripts/build.dev.js');

var buildwebpack = require('./build-scripts/build.production-webpack.js');

var pathTools = require('./build-scripts/path-tools.js');

var paths = require('./build-scripts/variables.js');

var prodContext = {
    destDir: paths.dest,
    indexPage:"./index-aot.html"
};

gulp.task("prod:build", function (cb) {
    return build.build(prodContext);
});

gulp.task("prod:webpack", function (cb) {
    return buildwebpack.build(prodContext);
});

var devContext = {
  destDir: paths.devdest
};

gulp.task("dev:compile", function (cb) {
    return builddev.compile(devContext);
});

gulp.task("less:base", function () {
    return compilation.compileLess(paths.assestsCssPath);;
});


gulp.task("test", function (cb) {
    //console.log(pathTools.resolvePackagePath("zone.js"));
    //return compilation.compileLess(paths.homeComponent);
    // return compilation.compileTypescript(paths.tsconfig, paths.app);

    //return compilation.compileAot(cb, paths.tsAotConfig);
    //return compilation.rollup(cb, paths.rollupConfig);

    //return build.commonBundle(prodContext);
    //return builddev.compile(prodContext);
    //return build.fonts(prodContext);
    //return build.clean(prodContext);

});
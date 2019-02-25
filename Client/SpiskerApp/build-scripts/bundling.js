var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var print = require('gulp-print');
var es = require('event-stream');
var fileExists = require('file-exists');
var gutil = require('gulp-util');
var colors = gutil.colors;
var nodePath = require('path');


var gulpif = require('gulp-if');



var bundling = {};



/** Performs concatenation of specified packages to single bundle */
bundling.concatBundle = function (name, paths, options) {
    return bundleSingleFileHelper(name, options, (outputFileName) => {
        return gulp.src(paths)
            .pipe(print(f => bundleIn(name, f)))
            .pipe(concat(outputFileName))
            .pipe(gulpif(options.uglify, uglify()))
            .pipe(gulp.dest(options.destDir))
            .pipe(print(f => bundleOut(f, 'concatenation done!')));
    });
}


/* -------------------- Helpers ----------------------------*/

function bundleIn(name, file) {
    return colors.gray("[" + name + "] <-- ") + file;
}

function bundleOut(name, file) {
    return colors.gray("[" + name + "] --> ") + colors.magenta(file);
}

function bundleSingleFileHelper(name, options, bundleFunc) {
    var result = {};
    var outputFileName = name;
    var outputFilePath = nodePath.join(options.destDir || "./", outputFileName);

    result.resultFiles = [outputFilePath];
    if (!fileExists.sync(outputFilePath) || options.cache === false) {
        result.stream = bundleFunc(outputFileName);
    } else {
        gutil.log(bundleOut(name, outputFilePath) + colors.gray(" already exists, skipping..."));
        result.stream = es.merge();
    }
    return result;
}


module.exports = bundling;

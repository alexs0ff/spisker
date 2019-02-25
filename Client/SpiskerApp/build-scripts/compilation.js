var gulp = require('gulp');
var ts = require('gulp-typescript');
var print = require('gulp-print');
var less = require('gulp-less');
var gutil = require('gulp-util');
var colors = gutil.colors;
var exec = require('child_process').exec;
var through2 = require('through2');
var gulpif = require('gulp-if');
var replace = require('gulp-replace');


var compilation = {};

compilation.execute = function(command){
    return through2.obj(function (file, enc, cb) {
        
        var that = this;

        exec(command, function (err, stdout, stderr) {

            gutil.log(gutil.colors.white(stdout));
            gutil.log(gutil.colors.red(stderr));
            cb(err);
        });
    });
}

compilation.compileTypescript = function (tsconfigFile, destDir) {
    gutil.log("Compiling typescript " + colors.magenta(tsconfigFile));
    var tsProject = ts.createProject(tsconfigFile);
    return tsProject.src()
        .pipe(tsProject())
        .js
        .pipe(gulp.dest(destDir))
        .pipe(print())
        .on('end', () => gutil.log(gutil.colors.gray('[' + tsconfigFile + '] --> ') +
            "compilation of typescript done (dest dir "
            + gutil.colors.magenta(destDir) + ')'));
}

compilation.compileLess = function (sourceDir, targetDir) {
    gutil.log("Compiling LESS in " + colors.magenta(sourceDir));
    return gulp.src(sourceDir + '/**/*.less')
        .pipe(less())
        .pipe(gulp.dest(targetDir || sourceDir))
        .pipe(print())
        .on('end', () => gutil.log("Compilation of less done."));
}

compilation.compileAot = function (configFilePath) {
    gutil.log("AOT compiling  with " + gutil.colors.magenta(configFilePath));

    return gulp.src(configFilePath).pipe(compilation.execute('"node_modules/.bin/ngc" -p ' + configFilePath))
        .pipe(print()).on('end', () => gutil.log("AOT compiling is done."));
}

compilation.rollup = function (configFilePath) {
    gutil.log("Run Rollup with " + gutil.colors.magenta(configFilePath));
    return gulp.src(configFilePath).pipe(compilation.execute('"node_modules/.bin/rollup" -c ' + configFilePath))
        .pipe(print()).on('end', () => gutil.log("Rollup is done."));
}

compilation.webpack = function (configFilePath, otherParam) {
  gutil.log("Webpack compiling  with " + gutil.colors.magenta(configFilePath));

  if (!otherParam) {
    otherParam = "";
  }
  return gulp.src(configFilePath).pipe(compilation.execute('"node_modules/.bin/webpack" --config ' + configFilePath + ' --bail ' + otherParam))
        .pipe(print()).on('end', () => gutil.log(gutil.colors.green("Webpack compiling is done.")));
}

compilation.confHtmlPredprocessor = function (targetParh, destDir, isProd) {
    gutil.log("Predprocessor HTML compiling  with " + gutil.colors.magenta(targetParh));
    return gulp.src(targetParh)
        .pipe(gulpif(isProd, replace('<!-- prod --', '<!-- prod -->'), replace('<!-- prod -->', '<!-- prod --')))
        .pipe(gulpif(isProd, replace('-- end-prod -->', '<!-- end-prod -->'), replace('<!-- end-prod -->', '-- end-prod -->')))
        .pipe(gulpif(isProd, replace('<!-- dev -->', '<!-- dev --'), replace('<!-- dev --', '<!-- dev -->')))
        .pipe(gulpif(isProd, replace('<!-- end-dev -->', '-- end-dev -->'), replace('-- end-dev -->', '<!-- end-dev -->')))
        .pipe(gulp.dest(destDir | targetParh))
        .pipe(print(file => { gutil.log('Processed --> ' + file) }));
}

compilation.confTSPredprocessor = function (targetParh, destDir, isProd) {
    gutil.log("Predprocessor TS compiling  with " + gutil.colors.magenta(targetParh));
    return gulp.src(targetParh)
        .pipe(print(file => { gutil.log('processing --> ' + gutil.colors.magenta(file)) }))
        .pipe(gulpif(isProd, replace('/* prod**', '/* prod */'), replace('/* prod */', '/* prod**')))
        .pipe(gulpif(isProd, replace('**end-prod */', '/* end-prod */'), replace('/* end-prod */', '**end-prod */')))
        .pipe(gulpif(isProd, replace('/* dev */', '/* dev**'), replace('/* dev**', '/* dev */')))
        .pipe(gulpif(isProd, replace('/* end-dev */', '**end-dev */'), replace('**end-dev */', '/* end-dev */')))
        .pipe(gulp.dest(destDir))
        .pipe(print(file => { gutil.log('Processed --> ' + file) }))
        .on('end', () => gutil.log("Predprocessor TS compiling is done."));
    
        ;
}

module.exports = compilation;

(function (global) {
    System.config({
        map: {
            app: 'App',
            '@angular/animations': 'node_modules/@angular/animations/bundles/animations.umd.js',
            '@angular/animations/browser': 'node_modules/@angular/animations/bundles/animations-browser.umd.js',
            '@angular/platform-browser/animations': 'node_modules/@angular/platform-browser/bundles/platform-browser-animations.umd.js',
            '@angular/core': 'node_modules/@angular/core/bundles/core.umd.js',
            '@angular/common/http': 'node_modules/@angular/common/bundles/common-http.umd.js',
            '@angular/common': 'node_modules/@angular/common/bundles/common.umd.js',
            '@angular/compiler': 'node_modules/@angular/compiler/bundles/compiler.umd.js',
            '@angular/platform-browser': 'node_modules/@angular/platform-browser/bundles/platform-browser.umd.js',
            '@angular/platform-browser-dynamic': 'node_modules/@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
            '@angular/http': 'node_modules/@angular/http/bundles/http.umd.js',
            '@angular/router': 'node_modules/@angular/router/bundles/router.umd.js',
            '@angular/forms': 'node_modules/@angular/forms/bundles/forms.umd.js',
            'tslib': 'node_modules/tslib/tslib.js',
            'moment':'node_modules/moment/',
            'angular2-moment':'node_modules/angular2-moment/',
            'rxjs': 'node_modules/rxjs',
            'angular2-recaptcha': 'node_modules/angular2-recaptcha',
            'linkifyjs': 'node_modules/linkifyjs/'
        },
        paths: {
            // paths serve as alias
            'npm:': 'https://unpkg.com/'
        },
        packages: {
            'app': {
                main: 'main.js',
                defaultExtension: 'js'
                
            },
            'rxjs': {
                defaultExtension: 'js'
            },
            'moment': {
                main: 'moment.js',
                defaultExtension: 'js'
            },
            'angular2-moment': {
                main: 'index.js',
                defaultExtension: 'js'
            },
            'angular2-recaptcha': {
                defaultExtension: 'js',
                main: 'index'
            }
            ,
            'linkifyjs': {
                defaultExtension: 'js',
                main: 'index'
            }
        }
    });
})(this); 
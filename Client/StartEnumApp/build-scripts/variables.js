var paths = {
    webroot: "./",
    app: "./app/",
    dest: "./dest/",
    aot: "./aot/",
    noderoot: "./node_modules/"
};

paths.appCss = paths.webroot + 'assets/css/';
paths.appTs = paths.app + '/**/*.ts';

paths.shimJs = paths.noderoot + 'core-js/client/shim.js';
paths.bootstrapJs = paths.noderoot + 'bootstrap/dist/js/bootstrap.js';
paths.linkifyJs = paths.noderoot + 'linkifyjs/dist/linkify.js';
paths.linkifyElementJs = paths.noderoot + 'linkifyjs/dist/linkify-element.js';
paths.scripts = paths.webroot + 'scripts/*.js';

paths.assestsCssPath = paths.webroot + 'assets/css/';
paths.assestsCss = paths.assestsCssPath + '*.css';

paths.bootstrapCss = paths.noderoot + 'bootstrap/dist/css/bootstrap.css';
paths.bootstrapFonts = paths.noderoot + 'bootstrap/dist/fonts/*.*';
paths.fontawesomeCss = paths.noderoot + 'font-awesome/css/font-awesome.css';
paths.fontawesomeFonts = paths.noderoot + 'font-awesome/fonts/*.*';

paths.tsconfig = paths.webroot + "tsconfig.json";
paths.tsWebpackConfig = paths.webroot + "tsconfig-webpack.json";
paths.tsAotConfig = paths.webroot + "tsconfig-aot.json";
paths.rollupConfig = paths.webroot + "build-scripts/rollup-config.js";

/*Home component*/
paths.home = paths.app + "Home/";
paths.homeComponent = paths.home + "HomeComponent/";


paths.buildjs = paths.app + "build.js";

paths.webpackIndex = paths.webroot + "index-webpack.html";
paths.polyfills = paths.app + "polyfills.ts";
paths.vendor = paths.app + "vendor.ts";
paths.mainTs = paths.app + "main.ts";


paths.webpackProdConf = paths.webroot + "build-scripts/webpack.prod.js";

module.exports = paths;
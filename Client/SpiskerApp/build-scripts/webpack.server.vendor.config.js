const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const merge = require('webpack-merge');

var paths = require('./variables.js');

const treeShakableModules = [
   'zone.js',
  '@angular/animations',
  '@angular/common',
  '@angular/compiler',
  '@angular/core',
  '@angular/forms',
  '@angular/http',
  '@angular/platform-browser',
  '@angular/platform-browser-dynamic',
  '@angular/router',
  'ngx-bootstrap',
];
const nonTreeShakableModules = [
    'bootstrap',
    'bootstrap/dist/css/bootstrap.css',
    'assets/css/base.css',
    'core-js',
    // 'es6-promise',
    // 'es6-shim',
    'event-source-polyfill',
    'jquery',
    paths.linkifyElementJs
];
const allModules = treeShakableModules.concat(nonTreeShakableModules);

var dirname = __dirname + '/../';

dirname = path.normalize(dirname);

module.exports = (env) => {
    console.log(`env = ${JSON.stringify(env)}`);
    console.log(`dir = ${JSON.stringify(dirname)}`);
    const extractCSS = new ExtractTextPlugin('vendor.css');
    const isDevBuild = !(env && env.prod);
    const sharedConfig = {
        stats: { modules: false },
        resolve: { extensions: ['.js'], modules: [path.resolve(dirname, "ClientApp"), "node_modules"] },
        module: {
            rules: [
                { test: /\.(png|woff|woff2|eot|ttf|svg)(\?|$)/, use: 'url-loader?limit=100000' }
            ]
        },
        output: {
            publicPath: 'dist/',
            filename: '[name].js',
            library: '[name]_[hash]'
        },
        plugins: [
            new webpack.ProvidePlugin(
                {
                    $: 'jquery',
                    jQuery: 'jquery',
                    'window.jQuery': 'jquery',
                    'window.$': 'jquery'
                }), // Maps these identifiers to the jQuery package (because Bootstrap expects it to be a global variable)
            new webpack.ContextReplacementPlugin(/\@angular\b.*\b(bundles|linker)/,
                path.join(dirname, paths.client)), // Workaround for https://github.com/angular/angular/issues/11580
            new webpack.ContextReplacementPlugin(/(.+)?angular(\\|\/)core(.+)?/,
                path.join(dirname, paths.client)), // Workaround for https://github.com/angular/angular/issues/14898
            new webpack.IgnorePlugin(/^vertx$/) // Workaround for https://github.com/stefanpenner/es6-promise/issues/100
        ]
    };

    const clientBundleConfig = merge(sharedConfig,
        {
            entry: {
                // To keep development builds fast, include all vendor dependencies in the vendor bundle.
                // But for production builds, leave the tree-shakable ones out so the AOT compiler can produce a smaller bundle.
                vendor: isDevBuild ? allModules : nonTreeShakableModules
            },
            output: { path: path.join(dirname, 'wwwroot', 'dist') },
            module: {
                rules: [
                    {
                        test: /\.css(\?|$)/,
                        use: extractCSS.extract({ use: isDevBuild ? 'css-loader' : 'css-loader?minimize' })
                    }
                ]
            },
            plugins: [
                extractCSS,
                new webpack.DllPlugin({
                    path: path.join(dirname, 'wwwroot', 'dist', '[name]-manifest.json'),
                    name: '[name]_[hash]'
                })
            ].concat(isDevBuild
                ? []
                : [
                    new webpack.optimize.UglifyJsPlugin()
                ])
        });

    const serverBundleConfig = merge(sharedConfig,
        {
            target: 'node',
            resolve: { mainFields: ['main'] },
            entry: { vendor: allModules.concat(['aspnet-prerendering']) },
            output: {
                path: path.join(dirname, paths.client, 'dist'),
                libraryTarget: 'commonjs2',
            },
            module: {
                rules: [
                    {
                        test: /\.css(\?|$)/,
                        use: ['to-string-loader', isDevBuild ? 'css-loader' : 'css-loader?minimize']
                    }
                ]
            },
            plugins: [
                new webpack.DllPlugin({
                    path: path.join(dirname, paths.client, 'dist', '[name]-manifest.json'),
                    name: '[name]_[hash]'
                })
            ].concat(isDevBuild
                ? []
                : [
                    new webpack.optimize.UglifyJsPlugin()
                ])
        });

    return [clientBundleConfig, serverBundleConfig];
}

var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var paths = require('./variables.js');
var pathtools = require('./path-tools.js');

module.exports = {
	entry: {
        'polyfills': paths.polyfills,
        'vendor': paths.vendor,
        'app': paths.mainTs
	},

	resolve: {
		extensions: ['.ts', '.js']
	},

	module: {
		rules: [
            {
            	test: /\.ts$/,
            	loaders: [
                    {
                    	loader: 'awesome-typescript-loader',
                        options: { configFileName: paths.tsWebpackConfig }
                    }, 'angular2-template-loader'
            	]
            },
            {
            	test: /\.html$/,
                loader: 'html-loader',
	            options: {
	                root: pathtools.root('')
	            }
            },
            {
            	test: /\.(png|jpe?g|gif|svg|woff|woff2|ttf|eot|ico)$/,
                loader: 'file-loader?name=assets/[name].[hash].[ext]'
            },
            {
            	test: /\.css$/,
                exclude: pathtools.root('app'),
            	loader: ExtractTextPlugin.extract({ fallbackLoader: 'style-loader', loader: 'css-loader?sourceMap' })
            },
            {
            	test: /\.css$/,
                include: pathtools.root('app'),
            	loader: 'raw-loader'
            }
		]
	},

	plugins: [
        // Workaround for angular/angular#11580
        new webpack.ContextReplacementPlugin(
            // The (\\|\/) piece accounts for path separators in *nix and Windows
            /angular(\\|\/)core(\\|\/)@angular/,
            paths.app, // location of your src
            {} // a map of your routes
        ),

        new webpack.optimize.CommonsChunkPlugin({
        	name: ['app', 'vendor', 'polyfills']
        }),

        new HtmlWebpackPlugin({
            template: paths.webpackIndex
        })
	]
};
import nodeResolve from 'rollup-plugin-node-resolve';
import commonjs from 'rollup-plugin-commonjs';
import uglify from 'rollup-plugin-uglify';

export default {
    input: 'app/main-aot.js',
    output: {
        file: 'app/build.js',
        format:'iife'
    },
    sourceMap: false,
    onwarn: function (warning) {
        // Skip certain warnings

        // should intercept ... but doesn't in some rollup versions
        if (warning.code === 'THIS_IS_UNDEFINED') { return; }

        // console.warn everything else
        console.warn(warning.message);
    },
    plugins: [
        nodeResolve({ jsnext: true, module: true }),
        commonjs({
            include: ['node_modules/rxjs/**', 'node_modules/angular2-moment/**', 'node_modules/moment/**','node_modules/angular2-recaptcha/**']
        }),
        uglify()
    ]
};
var nodePath = require('path');
var resolve = require('browser-resolve');
var fs = require("fs");
var path = require('path');

var _root = path.resolve(__dirname, '..');

var pathTools = {};

pathTools.root = function (args) {
    args = Array.prototype.slice.call(arguments, 0);
    return path.join.apply(path, [_root].concat(args));
}

pathTools.normalizePathSeparators = function (path) {
    return path.split('\\').join('/');
}

pathTools.resolvePackagePath = function (packageName, relativeDir) {
    var pkgPath = resolve.sync(packageName);
    var resolvedPath = nodePath.relative(relativeDir || "./", pkgPath);
    var normalizedPath = pathTools.normalizePathSeparators(resolvedPath);
    return normalizedPath;
}

pathTools.resolvePackage = function (packageMap, packageName, packagePath) {
    packageMap[packageName] = pathTools.resolvePackagePath(packagePath || packageName);
}


pathTools.walkFilesSync = function (dir, visitor) {
    var files = fs.readdirSync(dir);
    files.forEach(function (file) {
        if (fs.statSync(path.join(dir, file)).isDirectory()) {
            pathTools.walkFilesSync(path.join(dir, file), visitor);
        }
        else {
            let filePath = path.join(dir, file);
            visitor(filePath);
        }
    });
};

module.exports = pathTools;


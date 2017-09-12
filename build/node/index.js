var less = require("less");

module.exports = function (callback, input, filename) {
    less.render(input,
        {
            //paths: ['.', './lib'],  // Specify search paths for @import directives
            filename: filename, // Specify a filename, for better error messages
        },
        function (e, output) {
            callback(/* error */ null, output.css);
        });
};
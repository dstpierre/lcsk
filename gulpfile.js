var gulp = require('gulp');
var minify = require("gulp-minify");
var uglify = require("gulp-uglify");
var concat = require("gulp-concat");

gulp.task("default", function() {
	return gulp.src("./public/js/widget/*.js")
		.pipe(concat("parle.js"))
		.pipe(gulp.dest("./public/js/widget/dist"));
})

/*gulp.task("widget-init", function () {
  return gulp.src("public/js/widget/init.js")
    .pipe(minify())
    .pipe(uglify())
    .pipe(gulp.dest("public/js/widget/dist"));
});
gulp.task("widget-load", function () {
  return gulp.src("public/js/widget/load.js")
    .pipe(minify())
    .pipe(uglify())
    .pipe(gulp.dest("public/js/widget/dist"));
});
gulp.task("widget", ["widget-init", "widget-load"], function () {

});

*/
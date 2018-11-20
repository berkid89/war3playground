/// <binding BeforeBuild='jscripts, min:css' Clean='jscripts, min:css' />
var gulp = require('gulp'),
    sass = require('gulp-sass'),
    cssmin = require("gulp-cssmin"),
    rename = require("gulp-rename"),
    babel = require('gulp-babel'),
    concat = require('gulp-concat');

gulp.task('min:css', function () {
    return gulp.src('assets/scss/style.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(cssmin())
        .pipe(rename({
            suffix: ".min"
        }))
        .pipe(gulp.dest('wwwroot/assets/css'));
});

gulp.task('jscripts', function () {
    return gulp.src('assets/js/**/*.js')
        .pipe(babel({
            presets: ['@babel/env']
        }))
        .pipe(concat('app.js'))
        .pipe(gulp.dest('wwwroot/assets/js'));
});
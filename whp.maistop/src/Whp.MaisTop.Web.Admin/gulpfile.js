// SCRIPTS
const gulp = require('gulp');
const browserSync = require('browser-sync').create();
//
const autoprefixer = require('gulp-autoprefixer');
const babel = require('gulp-babel');
const clean = require('gulp-clean');
const concat = require('gulp-concat');
const cssComb = require('gulp-csscomb');
const cssmin = require('gulp-cssmin');
const fileinclude = require('gulp-file-include');
const htmlmin = require('gulp-htmlmin');
const imagemin = require('gulp-imagemin');
const jshint = require('gulp-jshint');
const plumber = require('gulp-plumber');
const sass = require('gulp-sass');
const uglify = require('gulp-uglify');

//--------------------------------------------------------------------------------------------------------
// PATHS
//
const origem = {
  dev: {
    html: './dev/**/*.html',
    sass: ['./dev/assets/sass/**/*.scss', './dev/assets/sass/style.scss'],
    js: [
      './dev/assets/js/core/_nameSpace.js',
      './dev/assets/js/core/_core.js',
      './dev/assets/js/components/*.js',
      './dev/assets/js/controller/*.js',
      './dev/assets/js/controller/**/*.js',
      './dev/assets/js/vendor/**/*.js'
    ],
    vendor: './dev/assets/js/vendor/**/*',
    email: './dev/assets/email/**/*',
    fonts: './dev/assets/fonts/**/*',
    img: './dev/assets/img/**/*',
    pdf: './dev/assets/pdf/**/*'
  },
  server: {
    html: ['./server/**/*.html', '!./server/_shared', '!./server/_shared/**/*'],
    css: './server/assets/css/style.css',
    js: './server/assets/js/scripts.js',
    vendor: './server/assets/js/vendor/**/*',
    email: './server/assets/email/**/*',
    fonts: './server/assets/fonts/**/*',
    img: './server/assets/img/**/*',
    pdf: './server/assets/pdf/**/*'
  }
};
const destino = {
  server: {
    html: './server',
    css: './server/assets/css',
    js: './server/assets/js',
    vendor: './server/assets/js/vendor',
    email: './server/assets/email',
    fonts: './server/assets/fonts',
    img: './server/assets/img',
    pdf: './server/assets/pdf'
  },
  build: {
    html: './build',
    css: './build/assets/css',
    js: './build/assets/js',
    vendor: './build/assets/js/vendor',
    email: './build/assets/email',
    fonts: './build/assets/fonts',
    img: './build/assets/img',
    pdf: './build/assets/pdf'
  }
};
//--------------------------------------------------------------------------------------------------------
// FUNÇÕES DEV
//
// Função para incluir os templates no server
function devTemplate() {
  return gulp
    .src(origem.dev.html, {allowEmpty: true})
    .pipe(plumber())
    .pipe(fileinclude({prefix: '@@', basepath: '@file'}))
    .pipe(gulp.dest(destino.server.html))
    .pipe(browserSync.stream());
}
// SASS
function devSass() {
  return gulp
    .src(origem.dev.sass, {allowEmpty: true})
    .pipe(plumber())
    .pipe(sass({ outputStyle: 'compressed' }).on('error', sass.logError))
    .pipe(autoprefixer({browsers: ['last 2 versions'], cascade: true}))
    .pipe(gulp.dest(destino.server.css))
    .pipe(browserSync.stream());
}
// Concat JS
function devJs() {
  return gulp
    .src(origem.dev.js, {allowEmpty: true})
    .pipe(plumber())
    .pipe(babel({presets: ['env']}))
    .pipe(concat('scripts.js'))
    .pipe(jshint())
    .pipe(gulp.dest(destino.server.js))
    .pipe(browserSync.stream());
}
// Vendor para server
function devVendor() {
  return gulp
    .src(origem.dev.vendor, {allowEmpty: true})
    .pipe(gulp.dest(destino.server.vendor))
    .pipe(browserSync.stream());
}
// Imagens para server
function devImg() {
  return gulp
    .src(origem.dev.img, {allowEmpty: true})
    .pipe(gulp.dest(destino.server.img))
    .pipe(browserSync.stream());
}

function devPdf() {
  return gulp
    .src(origem.dev.pdf, {allowEmpty: true})
    .pipe(gulp.dest(destino.server.pdf))
    .pipe(browserSync.stream());
}
//Fontes para server
function devFonts() {
  return gulp
    .src(origem.dev.fonts, { allowEmpty: true })
    .pipe(gulp.dest(destino.server.fonts))
    .pipe(browserSync.stream());
}
// Emails para server
function devEmails() {
  return gulp
    .src(origem.dev.email, {allowEmpty: true})
    .pipe(gulp.dest(destino.server.email))
    .pipe(browserSync.stream());
}
// Função para iniciar o browser
function browser() {
  browserSync.init({
    server: {
      // Pasta padrão que será carregada
      baseDir: './server',
      // Index que será carregada primeiro
      index: 'index.html'
      //proxy: "localhost:3000"
    }
  });
}
// Função de watch do Gulp
function watch() {
  // Watch geral
  gulp.watch('./dev/**/*').on('change', browserSync.reload);
  gulp.watch(origem.dev.html, devTemplate);
  gulp.watch(origem.dev.sass, devSass);
  gulp.watch(origem.dev.js, devJs);
  gulp.watch(origem.dev.vendor, devVendor);
  gulp.watch(origem.dev.img, devImg);
  gulp.watch(origem.dev.fonts, devFonts);
  gulp.watch(origem.dev.email, devEmails);
  gulp.watch(origem.dev.pdf, devPdf);
}
//--------------------------------------------------------------------------------------------------------
// FUNÇÕES BUILD
//
// HTML
function buildHtml() {
  return gulp
    .src(origem.server.html, {allowEmpty: true})
    .pipe(htmlmin({collapseWhitespace: true, removeComments: true}))
    .pipe(gulp.dest(destino.build.html));
}
// CSS
function buildCss() {
  return gulp
    .src(origem.server.css, {allowEmpty: true})
    .pipe(cssComb())
    .pipe(cssmin())
    .pipe(gulp.dest(destino.build.css));
}

function buildPdf() {
  return gulp
    .src(origem.server.pdf, {allowEmpty: true})
    .pipe(gulp.dest(destino.build.pdf));
}
// JS
function buildJs() {
  return gulp
    .src(origem.server.js, {allowEmpty: true})
    .pipe(uglify())
    .pipe(gulp.dest(destino.build.js));
}
// Vendor
function buildVendor() {
  return gulp.src(origem.server.vendor, {allowEmpty: true}).pipe(gulp.dest(destino.build.vendor));
}
// Imagens para server
function buildImg() {
  return gulp
    .src(origem.server.img, {allowEmpty: true})
    .pipe(
      imagemin([
        imagemin.gifsicle({interlaced: true}),
        imagemin.jpegtran({progressive: true}),
        imagemin.optipng({optimizationLevel: 7}),
        imagemin.svgo({
          plugins: [{removeViewBox: true}, {cleanupIDs: false}]
        })
      ])
    )
    .pipe(gulp.dest(destino.build.img));
}
// Fontes para server
// function buildFonts() {
//   return gulp.src(origem.server.fonts, { allowEmpty: true }).pipe(gulp.dest(destino.build.fonts));
// }
// Emails para server
function buildEmails() {
  return gulp.src(origem.server.email, {allowEmpty: true}).pipe(gulp.dest(destino.build.email));
}
//--------------------------------------------------------------------------------------------------------
// TASKS
//
// DEV
gulp.task('html-dev', devTemplate);
gulp.task('sass-dev', devSass);
gulp.task('js-dev', devJs);
gulp.task('vendor-dev', devVendor);
gulp.task('img-dev', devImg);
gulp.task('fonts-dev', devFonts);
gulp.task('email-dev', devEmails);
gulp.task('pdf-dev', devPdf)

//BUILD
gulp.task('html-build', buildHtml);
gulp.task('css-build', buildCss);
gulp.task('js-build', buildJs);
gulp.task('pdf-build', buildPdf);
gulp.task('vendor-build', buildVendor);
gulp.task('img-build', buildImg);
// gulp.task('fonts-build', buildFonts);
gulp.task('email-build', buildEmails);
// Inicia Browser
gulp.task('browser-sync', browser);
gulp.task('watch', watch);
// --------------------------------------------------------------------------------------------------------
// Tarefas de Limpeza
//
// Clean server folder
gulp.task('clean:server', function() {
  return gulp.src('./server', {allowEmpty: true}).pipe(clean());
});
// Clean build folder
gulp.task('clean:build', function() {
  return gulp.src('./build', {allowEmpty: true}).pipe(clean());
});
//--------------------------------------------------------------------------------------------------------
// $ GULP DEFAULT
//
gulp.task(
  'default',
  gulp.series(
    'clean:server',
    'clean:build',
    gulp.parallel(
      'watch',
      'browser-sync',
      'html-dev',
      'sass-dev',
      'js-dev',
      'vendor-dev',
      'img-dev',
      'fonts-dev',
      'email-dev',
      'pdf-dev'
    )
  )
);
//--------------------------------------------------------------------------------------------------------
// $ GULP BUILD
//
gulp.task(
  'build',
  gulp.series(
    'clean:server',
    'clean:build',
    gulp.parallel('html-dev', 'sass-dev', 'js-dev', 'vendor-dev', 'img-dev', 'fonts-dev', 'email-dev', 'pdf-dev'),
    gulp.parallel('html-build', 'css-build', 'js-build', 'vendor-build', 'img-build', /*'fonts-build',*/ 'email-build', 'pdf-build')
  )
);

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
    htmlPaginas: './dev/paginas/**/*.html',
    htmlIncludes: ['./dev/**/*.html', '!./dev/paginas', '!./dev/paginas/**/*'],
    sass: ['./dev/_assets/sass/**/*.scss', './dev/_assets/sass/style.scss'],
    js: [
      './dev/_assets/js/core/_nameSpace.js',
      './dev/_assets/js/core/_core.js',
      './dev/_assets/js/components/*.js',
      './dev/_assets/js/controller/*.js'
    ],
    json: './dev/json/**/*',
    vendor: './dev/_assets/js/vendor/**/*',
    email: './dev/_assets/email/**/*',
    // fonts: './dev/_assets/fonts/**/*',
    img: './dev/_assets/img/**/*'
  },
  server: {
    html: ['./server/**/*.html', '!./server/_shared', '!./server/_shared/**/*'],
    css: './server/_assets/css/style.css',
    js: './server/_assets/js/scripts.js',
    vendor: './server/_assets/js/vendor/**/*',
    email: './server/_assets/email/**/*',
    // fonts: './server/_assets/fonts/**/*',
    img: './server/_assets/img/**/*'
  }
};
const destino = {
  server: {
    html: './server',
    css: './server/_assets/css',
    js: './server/_assets/js',
    vendor: './server/_assets/js/vendor',
    email: './server/_assets/email',
    // fonts: './server/_assets/fonts',
    img: './server/_assets/img',
    json: './server/json'
  },
  build: {
    html: './build',
    css: './build/_assets/css',
    js: './build/_assets/js',
    vendor: './build/_assets/js/vendor',
    email: './build/_assets/email',
    // fonts: './build/_assets/fonts',
    img: './build/_assets/img'
  }
};
//--------------------------------------------------------------------------------------------------------
// FUNÇÕES DEV
//
// Função para incluir os templates no server
function devPaginas() {
  return gulp
    .src(origem.dev.htmlPaginas, {allowEmpty: true})
    .pipe(plumber())
    .pipe(fileinclude({prefix: '@@', basepath: '@file'}))
    .pipe(gulp.dest(destino.server.html))
    .pipe(browserSync.stream());
}
function devIncludes() {
  return gulp
    .src(origem.dev.htmlIncludes, {allowEmpty: true})
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
// JSON para server
function devJson() {
  return gulp
    .src(origem.dev.json, {allowEmpty: true})
    .pipe(gulp.dest(destino.server.json))
    .pipe(browserSync.stream());
}
// Fontes para server
// function devFonts() {
//   return gulp
//     .src(origem.dev.fonts, { allowEmpty: true })
//     .pipe(gulp.dest(destino.server.fonts))
//     .pipe(browserSync.stream());
// }
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
  gulp.watch('./dev/**/*.html', gulp.series(devPaginas, devIncludes));
  gulp.watch(origem.dev.sass, devSass);
  gulp.watch(origem.dev.js, devJs);
  gulp.watch(origem.dev.vendor, devVendor);
  gulp.watch(origem.dev.img, devImg);
  gulp.watch(origem.dev.json, devJson);
  // gulp.watch(origem.dev.fonts, devFonts);
  gulp.watch(origem.dev.email, devEmails);
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
    // .pipe(
    //   imagemin([
    //     imagemin.gifsicle({interlaced: true}),
    //     imagemin.jpegtran({progressive: true}),
    //     imagemin.optipng({optimizationLevel: 7}),
    //     imagemin.svgo({
    //       plugins: [{removeViewBox: true}, {cleanupIDs: false}]
    //     })
    //   ])
    // )
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
gulp.task('paginas-dev', devPaginas);
gulp.task('includes-dev', devIncludes);
gulp.task('sass-dev', devSass);
gulp.task('js-dev', devJs);
gulp.task('vendor-dev', devVendor);
gulp.task('json-dev', devJson);
gulp.task('img-dev', devImg);
// gulp.task('fonts-dev', devFonts);
gulp.task('email-dev', devEmails);
//BUILD
gulp.task('html-build', buildHtml);
gulp.task('css-build', buildCss);
gulp.task('js-build', buildJs);
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
      'paginas-dev',
      'includes-dev',
      'sass-dev',
      'js-dev',
      'vendor-dev',
      'json-dev',
      'img-dev',
      // 'fonts-dev',
      'email-dev'
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
    gulp.parallel('paginas-dev', 'includes-dev', 'sass-dev', 'js-dev', 'vendor-dev', 'img-dev', /*'fonts-dev',*/ 'email-dev'),
    gulp.parallel('html-build', 'css-build', 'js-build', 'vendor-build', 'img-build', /*'fonts-build',*/ 'email-build')
  )
);

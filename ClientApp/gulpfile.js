// Импорт основного модуля
const gulp = require("gulp");

const fs = require("fs");
const fonter = require("gulp-fonter-fix");
const ttf2woff2 = require("gulp-ttf2woff2");

const del = require("del");

// Пути к папке с исходниками и папке с результатом
const srcFolder = `./src`;
const isFontsReW = process.argv.includes("--rewrite");

// Пути к папкам и файлам проекта
const path = {
  src: {
    fonts: `${srcFolder}/fonts`,
  },
  srcAssets: {
    fonts: `${srcFolder}/assets/fonts/`,
  },
};

// Импорт задач
const otfToTtf = () => {
  del(path.srcAssets.fonts);
  // Ищем файлы шрифтов .otf
  return (
    gulp
      .src(`${path.src.fonts}/*.otf`, {})

      // Конвертируем в .ttf
      .pipe(
        fonter({
          formats: ["ttf"],
        })
      )
      // Выгружаем в исходную папку
      .pipe(gulp.dest(`${path.src.fonts}/`))
  );
};
const ttfToWoff = () => {
  // Ищем файлы шрифтов .ttf
  return (
    gulp
      .src(`${path.src.fonts}/*.ttf`, {})
      // Конвертируем в .woff
      .pipe(
        fonter({
          formats: ["woff"],
        })
      )
      // Выгружаем в папку с результатом
      .pipe(gulp.dest(`${path.srcAssets.fonts}`))
      // Ищем файлы шрифтов .ttf
      .pipe(gulp.src(`${path.src.fonts}/*.ttf`))
      // Конвертируем в .woff2
      .pipe(ttf2woff2())
      // Выгружаем в папку с результатом
      .pipe(gulp.dest(`${path.srcAssets.fonts}`))
      // Ищем файлы шрифтов .woff и woff2
      .pipe(gulp.src(`${path.src.fonts}/*.{woff,woff2}`))
      // Выгружаем в папку с результатом
      .pipe(gulp.dest(`${path.srcAssets.fonts}`))
  );
};
const fonstStyle = () => {
  let fontsFile = `${srcFolder}/styles/fonts/fonts.scss`;
  // Если передан флаг --rewrite удаляем файл подключения шрифтов
  isFontsReW ? fs.unlink(fontsFile, cb) : null;
  // Проверяем существуют ли файлы шрифтов
  fs.readdir(path.srcAssets.fonts, function (err, fontsFiles) {
    if (fontsFiles) {
      // Проверяем существует ли файл стилей для подключения шрифтов
      if (!fs.existsSync(fontsFile)) {
        // Если файла нет, создаем его
        fs.writeFile(fontsFile, "", cb);
        let newFileOnly;
        for (var i = 0; i < fontsFiles.length; i++) {
          // Записываем подключения шрифтов в файл стилей
          let fontFileName = fontsFiles[i].split(".")[0];
          if (newFileOnly !== fontFileName) {
            let fontName = fontFileName.split("-")[0]
              ? fontFileName.split("-")[0]
              : fontFileName;
            let fontWeight = fontFileName.split("-")[1]
              ? fontFileName.split("-")[1]
              : fontFileName;
            if (fontWeight.toLowerCase() === "thin") {
              fontWeight = 100;
            } else if (fontWeight.toLowerCase() === "extralight") {
              fontWeight = 200;
            } else if (fontWeight.toLowerCase() === "light") {
              fontWeight = 300;
            } else if (fontWeight.toLowerCase() === "medium") {
              fontWeight = 500;
            } else if (fontWeight.toLowerCase() === "semibold") {
              fontWeight = 600;
            } else if (fontWeight.toLowerCase() === "bold") {
              fontWeight = 700;
            } else if (
              fontWeight.toLowerCase() === "extrabold" ||
              fontWeight.toLowerCase() === "heavy"
            ) {
              fontWeight = 800;
            } else if (fontWeight.toLowerCase() === "black") {
              fontWeight = 900;
            } else {
              fontWeight = 400;
            }
            fs.appendFile(
              fontsFile,
              `@font-face {\n\tfont-family: ${fontName};\n\tfont-display: swap;\n\tsrc: url("/assets/fonts/${fontFileName}.woff2") format("woff2"), url("/assets/fonts/${fontFileName}.woff") format("woff");\n\tfont-weight: ${fontWeight};\n\tfont-style: normal;\n}\r\n`,
              cb
            );
            newFileOnly = fontFileName;
          }
        }
      } else {
        // Если файл есть, выводим сообщение
        console.log(
          "Файл scss/fonts/fonts.scss уже существует. Для обновления файла нужно его удалить!"
        );
      }
    } else {
      // Если шрифтов нет
      fs.unlink(fontsFile, cb);
    }
  });
  return gulp.src(`${srcFolder}`);
};
function cb() {}

// Последовательная обработака шрифтов
const fonts = gulp.series(otfToTtf, ttfToWoff, fonstStyle);

// Выполнение сценария по умолчанию
gulp.task("fonts", fonts);

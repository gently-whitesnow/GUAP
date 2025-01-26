<!DOCTYPE html>
<html lang="ru">
  <head>
    <meta charset="UTF-8" />
    <meta
      name="description"
      content="Сайт, посвященный антивирусным программам и их обзору."
    />
    <meta
      name="keywords"
      content="антивирусы, безопасность, программы, защита, Kaspersky, Avast"
    />
    <meta name="author" content="Александр Зайцев" />
    <meta property="og:title" content="Антивирусные программы" />
    <meta
      property="og:description"
      content="Обзор современных антивирусов и их возможностей."
    />
    <meta property="og:image" content="logo.png" />
    <meta property="og:type" content="website" />
    <title>Антивирусные программы - Главная</title>
    <!-- Подключение внешней таблицы стилей -->
    <link rel="stylesheet" href="style.css" />
  </head>
  <body>
    <div id="header">
      <h1 id="header-text">Антивирусные программы</h1>
      <img src="logo.png" alt="Логотип сайта" width="100" id="author-btn"/>
    </div>

    <div id="menu">
      <ul>
        <li>
          <li><a href="index.html">Главная</a></li>
          <li><a href="html5.html">HTML5 версия</a></li>
          <li><a href="sources.html">Использованные источники</a></li>
        </li>
      </ul>
    </div>

    <div id="content">
      <h2>История антивирусов</h2>
      <p>
        Антивирусные программы появились в конце 1980-х годов в ответ на
        стремительный рост количества компьютерных вирусов. Первые вирусы были
        относительно простыми, их цель заключалась в создании помех в работе
        операционных систем. Однако с развитием технологий и увеличением
        популярности интернета началась новая эра, характеризующаяся появлением
        более сложных угроз. Антивирусы начали эволюционировать от простых
        сканеров к комплексным программным решениям, способным обнаруживать
        вредоносное программное обеспечение в реальном времени. В настоящее
        время антивирусные программы играют ключевую роль в обеспечении
        информационной безопасности как для домашних пользователей, так и для
        корпоративных клиентов.
      </p>

      <h3>Ранние этапы развития</h3>
      <p>
        В 1990-х годах появление вирусов, распространяющихся через электронную
        почту, поставило под угрозу миллионы пользователей. Антивирусы стали
        оснащаться функциями проверки почтовых вложений и защиты от
        макровирусов. Одной из первых широко известных программ стала Norton
        Antivirus, предложившая автоматическое обновление вирусных баз. С начала
        2000-х годов производители антивирусного ПО стали активно внедрять
        технологии эвристического анализа, что позволило идентифицировать ранее
        неизвестные угрозы.
      </p>

      <h3>Современные технологии</h3>
      <p>
        Сегодня антивирусы предлагают множество функций: защита от фишинга,
        сканирование веб-страниц, блокировка рекламного ПО и даже обеспечение
        безопасности мобильных устройств. Ведущие разработчики, такие как
        Kaspersky, McAfee, Avast и ESET, интегрируют технологии машинного
        обучения для прогнозирования поведения вредоносного программного
        обеспечения. Использование облачных технологий позволяет мгновенно
        обновлять базы данных и обеспечивать высокий уровень защиты.
      </p>

      <h3>Основные типы угроз:</h3>
      <ol class="custom-list">
        <li>Вирусы</li>
        <li>
          Трояны
          <ol>
            <li>Банковские трояны</li>
            <li>Руткиты</li>
          </ol>
        </li>
        <li>Черви</li>
        <li>Шпионское ПО</li>
        <li>Рекламное ПО</li>
      </ol>

      <hr />
    <table id="antivirus-table">
        <caption>Таблица сравнения популярных антивирусов</caption>
        <colgroup>
            <col class="program-column" />
            <col class="price-column" />
            <col />
        </colgroup>
        <thead>
            <tr>
                <th>Программа</th>
                <th>Цена</th>
                <th>Особенности</th>
            </tr>
        </thead>
        <tbody>
            <?php include 'script.php'; ?>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="3">Цены актуальны на 2025 год</td>
            </tr>
        </tfoot>
    </table>
    </div>

    <div id="footer">
      <p>© 2025, Все права защищены. Контакты: info@example.com</p>
    </div>
    <script src="author_popap.js"></script>
    <script src="empty_row.js"></script>
    <script src="header_interactive.js"></script>
    <script>
      
      // Функция для создания и отображения часов
      function createClock() {
        let clock = document.getElementById("clock");
        if (!clock) {
          clock = document.createElement("div");
          clock.id = "clock";
          clock.style.position = "fixed";
          clock.style.top = "10px";
          clock.style.right = "10px";
          clock.style.padding = "10px";
          clock.style.backgroundColor = "rgba(0, 0, 0, 0.7)";
          clock.style.color = "white";
          clock.style.fontSize = "16px";
          clock.style.borderRadius = "5px";
          clock.style.zIndex = "1000";
          document.body.appendChild(clock);
        }
        
        function updateClock() {
          const now = new Date();
          clock.textContent = `Время: ${now.toLocaleTimeString()}`;
        }
        
        updateClock(); // Первичное обновление времени
        setInterval(updateClock, 1000); // Обновление каждую секунду
      }
      
      // Создаем флаг для отслеживания состояния видимости часов
      let isClockVisible = true;
      // Функция для переключения видимости часов
      function toggleClock() {
        const clock = document.getElementById("clock");
        if (clock) {
          isClockVisible = !isClockVisible;
          clock.style.display = isClockVisible ? "block" : "none";
        }
      }

      let lastScrollTop = 0; // Предыдущая позиция прокрутки
      function slideClock() {
        const clock = document.getElementById("clock");
        if (!clock) {
          return
        }    
        const currentScrollTop = window.scrollY || document.documentElement.scrollTop;

        if (currentScrollTop > lastScrollTop) {
          clock.classList.add("slide-down"); 
          setTimeout(() => {
            clock.classList.remove("slide-down"); 
          }, 300);
        } else {
          clock.classList.add("slide-up"); 
          setTimeout(() => {
            clock.classList.remove("slide-up"); 
          }, 300);
        }

        lastScrollTop = currentScrollTop; // Обновляем предыдущую позицию
      }


      window.addEventListener("scroll", () => {
        slideClock();
      });
    
      // Добавление обработчика события keydown
      document.addEventListener("keydown", function (event) {
        if (event.key === "t" || event.key === "T" || event.key === "е" || event.key === "Е") {
          toggleClock();
        }
      });
    
      // Вызов createClock при загрузке страницы
      window.addEventListener("load", () => {
        createClock();
      });
    </script>   
    
  </body>
</html>

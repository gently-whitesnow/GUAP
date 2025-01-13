const colors = ["red", "orange", "yellow", "green", "blue", "indigo", "violet"];
let currentColorIndex = 0;
let currentLogoSize = 100; // Начальный размер логотипа
let increasingLogo = true; // Флаг для увеличения/уменьшения размера логотипа

function changeHeaderStyle() {
  const header = document.getElementById("header-text");
  const logo = document.getElementById("author-btn");

  // Изменяем цвет заголовка с плавным переходом
  header.style.color = colors[currentColorIndex];
  currentColorIndex = (currentColorIndex + 1) % colors.length;

  // Изменяем размер логотипа плавно
  if (increasingLogo) {
    currentLogoSize += 2; // Увеличиваем размер логотипа
    if (currentLogoSize >= 150) increasingLogo = false; // Максимальный размер логотипа
  } else {
    currentLogoSize -= 2; // Уменьшаем размер логотипа
    if (currentLogoSize <= 100) increasingLogo = true; // Минимальный размер логотипа
  }

  logo.style.width = currentLogoSize + "px";
  logo.style.height = "auto"; // Сохраняем пропорции
}

// Устанавливаем интервал для изменения стиля
setInterval(changeHeaderStyle, 100);

const addEmptyRow = () => {
  const table = document
    .getElementById("antivirus-table")
    .getElementsByTagName("tbody")[0]; // Находим тело таблицы
  const newRow = document.createElement("tr"); // Создаем новую строку

  // Добавляем три пустых ячейки в строку
  for (let i = 0; i < 3; i++) {
    const newCell = document.createElement("td");
    newCell.textContent = ""; // Пустое содержимое
    newRow.appendChild(newCell);
  }

  newRow.className = "manual-row";
  table.appendChild(newRow); // Добавляем строку в таблицу
};

// Пример вызова функции для демонстрации
document.addEventListener("keydown", function (event) {
  if (
    event.key === "a" ||
    event.key === "A" ||
    event.key === "Ф" ||
    event.key === "ф"
  ) {
    addEmptyRow(); // Добавляем пустую строку при нажатии клавиши "A"
  }
});

const deleteManualEmptyRow = () => {
  const elements = document.querySelectorAll(".manual-row");
  if (elements.length === 0) return;

  const lastElement = elements[elements.length - 1];
  lastElement.remove();
};

document.addEventListener("keydown", function (event) {
  if (
    event.key === "d" ||
    event.key === "D" ||
    event.key === "в" ||
    event.key === "В"
  ) {
    deleteManualEmptyRow(); // Добавляем пустую строку при нажатии клавиши "A"
  }
});

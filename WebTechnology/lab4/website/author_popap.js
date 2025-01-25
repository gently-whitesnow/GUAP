document.getElementById("author-btn").addEventListener("click", function () {
  // Создание фона для окна
  const overlay = document.createElement("div");
  overlay.id = "modal-overlay";
  overlay.style.position = "fixed";
  overlay.style.top = "0";
  overlay.style.left = "0";
  overlay.style.width = "100%";
  overlay.style.height = "100%";
  overlay.style.backgroundColor = "rgba(0, 0, 0, 0.5)";
  overlay.style.zIndex = "999";

  // Создание самого окна
  const modal = document.createElement("div");
  modal.id = "author-modal";
  modal.style.position = "fixed";
  modal.style.top = "50%";
  modal.style.left = "50%";
  modal.style.transform = "translate(-50%, -50%)";
  modal.style.padding = "20px";
  modal.style.backgroundColor = "white";
  modal.style.border = "2px solid black";
  modal.style.zIndex = "1000"; // Поверх overlay
  modal.innerHTML = `
        <p>Автор: Александр Зайцев</p>
        <p>Группа: Z1431K</p>
        <button id="close-modal">Закрыть</button>
      `;

  document.body.appendChild(overlay);
  document.body.appendChild(modal);

  // Закрытие по кнопке "Закрыть"
  document.getElementById("close-modal").addEventListener("click", function () {
    document.body.removeChild(modal);
    document.body.removeChild(overlay);
  });

  // Закрытие по клику на overlay
  overlay.addEventListener("click", function () {
    document.body.removeChild(modal);
    document.body.removeChild(overlay);
  });
});

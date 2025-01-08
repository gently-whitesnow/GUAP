document.getElementById("addBtn").addEventListener("click", addBus);

function fetchSchedule() {
  fetch("cgi-bin/schedule.py", {
    method: "GET",
  })
    .then((response) => response.json())
    .then((data) => {
      const tbody = document.querySelector("tbody");
      tbody.innerHTML = "";
      data.forEach((bus) => {
        const row = document.createElement("tr");
        row.innerHTML = `
          <td>${bus.number}</td>
          <td>${bus.route}</td>
          <td>${bus.departure}</td>
          <td><button class="deleteBtn" data-id="${bus.id}">❌</button></td>
        `;
        tbody.appendChild(row);
      });
      // Добавляем обработчики на кнопки удаления
      document.querySelectorAll(".deleteBtn").forEach((btn) =>
        btn.addEventListener("click", (e) => {
          const id = e.target.dataset.id;
          deleteBus(id);
        })
      );
    })
    .catch((err) => console.error("Error:", err));
}

function addBus() {
  const number = document.getElementById("busNumber").value;
  const route = document.getElementById("busRoute").value;
  const departure = document.getElementById("busDeparture").value;

  fetch("cgi-bin/schedule.py", {
    method: "POST",
    headers: {
      "Content-Type": "application/x-www-form-urlencoded",
    },
    body: `number=${encodeURIComponent(number)}&route=${encodeURIComponent(
      route
    )}&departure=${encodeURIComponent(departure)}`,
  })
    .then((response) => response.json())
    .then((data) => {
      console.log(data.message);
      fetchSchedule(); // Обновляем расписание
    })
    .catch((err) => console.error("Error:", err));
}

function deleteBus(id) {
  fetch("cgi-bin/schedule.py", {
    method: "POST",
    headers: {
      "Content-Type": "application/x-www-form-urlencoded",
    },
    body: `action=delete&id=${encodeURIComponent(id)}`,
  })
    .then((response) => response.json())
    .then((data) => {
      console.log(data.message);
      fetchSchedule(); // Обновляем расписание
    })
    .catch((err) => console.error("Error:", err));
}

// Инициализация
document.addEventListener("DOMContentLoaded", fetchSchedule);

fish_icons = ["🐟", "🐠", "🐡", "🦈", "🐙", "🦑", "🦐", "🦀", "🐬", "🐳"];
fishes = new Set([]);
aquarium = document.querySelector(".aquarium");

async function fetchWords() {
  try {
    const response = await fetch("http://localhost:1234/words");
    const words = await response.json();
    words.forEach((word) => {
      let lower = word.toLowerCase();
      if (fishes.has(lower)) return;
      fishes.add(lower);
      const fishContainer = document.createElement("div");
      fishContainer.style.position = "absolute"; // Позиционирование для движения
      fishContainer.style.display = "flex";
      fishContainer.style.flexDirection = "column";
      fishContainer.style.alignItems = "center";

      let fontSize = getRandomArbitrary(20, 70) + "px";

      fishContainer.style.fontSize = fontSize;

      const text = document.createElement("div");
      text.textContent = word;
      text.style.marginBottom = "5px";

      const icon = document.createElement("div");
      icon.textContent =
        fish_icons[getRandomArbitrary(0, fish_icons.length - 1)];
      icon.style.fontSize = fontSize;

      fishContainer.appendChild(text);
      fishContainer.appendChild(icon);
      aquarium.appendChild(fishContainer);

      // Начальная позиция
      const aquariumRect = aquarium.getBoundingClientRect();
      fishContainer.style.left =
        getRandomArbitrary(0, aquariumRect.width - fishContainer.offsetWidth) +
        "px";
      fishContainer.style.top =
        getRandomArbitrary(
          0,
          aquariumRect.height - fishContainer.offsetHeight
        ) + "px";

      // Анимация движения
      moveFish(fishContainer, aquariumRect);
    });
  } catch (error) {
    console.error("Error fetching words:", error);
  }
}

function moveFish(fish, aquariumRect) {
  let dx = getRandomArbitrary(1, 3); // Скорость по оси X
  let dy = getRandomArbitrary(1, 2); // Скорость по оси Y

  let currentLeft = parseInt(fish.style.left || 0);
  let currentTop = parseInt(fish.style.top || 0);

  const fishWidth = fish.offsetWidth; // Реальная ширина контейнера
  const fishHeight = fish.offsetHeight; // Реальная высота контейнера

  setInterval(() => {
    currentLeft += dx;
    currentTop += dy;

    // Проверка на столкновение с границами аквариума
    if (currentLeft <= 0 || currentLeft >= aquariumRect.width - fishWidth) {
      dx = -dx; // Меняем направление по X
    }

    if (currentTop <= 0 || currentTop >= aquariumRect.height - fishHeight) {
      dy = -dy; // Меняем направление по Y
    }

    // Применяем новые координаты
    fish.style.left = `${currentLeft}px`;
    fish.style.top = `${currentTop}px`;
  }, 10); // Интервал обновления позиции
}

// Submit a new word to the server
async function submitWord() {
  const wordInput = document.getElementById("wordInput");
  const word = wordInput.value.trim();
  if (!word) return;

  try {
    await fetch("http://localhost:1234/words", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ word }),
    });
    wordInput.value = "";
    fetchWords();
  } catch (error) {
    console.error("Error submitting word:", error);
  }
}

function getRandomArbitrary(min, max) {
  return Math.floor(Math.random() * (max - min + 1)) + min;
}

// Load words on page load
document.addEventListener("DOMContentLoaded", fetchWords);

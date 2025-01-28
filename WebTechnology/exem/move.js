let x = 0;
let y = 0;

const heading = document.getElementById("greeting");

document.addEventListener("keydown", (event) => {
  const step = 10;

  switch (event.key) {
    case "ArrowUp":
      y -= step;
      break;
    case "ArrowDown":
      y += step;
      break;
    case "ArrowLeft":
      x -= step;
      break;
    case "ArrowRight":
      x += step;
      break;
  }

  heading.style.left = `${x}px`;
  heading.style.top = `${y}px`;
});

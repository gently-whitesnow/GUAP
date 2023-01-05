from PySide6 import QtCore, QtGui, QtWidgets
from PySide6.QtCore import Qt

from polygon import Polygon


class MainWindow(QtWidgets.QMainWindow):
    def __init__(self):
        super().__init__()

        # Создаем графический объект
        self.label = QtWidgets.QLabel()
        # Полотно на котором будем рисовать
        self.width = 600
        self.height = 600
        canvas = QtGui.QPixmap(self.width, self.height)
        canvas.fill(Qt.white)
        # Передаем полотно графическому объекту
        self.label.setPixmap(canvas)
        # Размещаем полотно по центру приложения
        self.setCentralWidget(self.label)

        # треугольник
        # struct = [(200,100),(150,300),(300,300)]
        # стрелка 7 точек
        self.struct = [(0, 0), (50, 0), (50, -20), (100, 30), (50, 80), (50, 60), (0, 60)]
        # Начало координат в центре экрана
        self.dx = self.width/2
        self.dy = self.height/2
        # градус на который повернута фигура
        self.degree = 0
        # Наша фигура
        self.arrow = Polygon(self.label, self.struct, self.dx, self.dy, self.degree)
        # шаг перемещения
        self.step = 90

    # обработчик нажатия клавиш (только для английской раскладки)
    def keyPressEvent(self, event):
        key = event.key()
        print(f"Found key {key}")
        if key == QtCore.Qt.Key.Key_W:
            self.dy -= self.step*2
            self.arrow = Polygon(self.label, self.struct, self.dx, self.dy, self.degree)
        if key == QtCore.Qt.Key.Key_S:
            self.dy += self.step*2
            self.arrow = Polygon(self.label, self.struct, self.dx, self.dy, self.degree)
        if key == QtCore.Qt.Key.Key_A:
            self.dx -= self.step*2
            self.arrow = Polygon(self.label, self.struct, self.dx, self.dy, self.degree)
        if key == QtCore.Qt.Key.Key_D:
            self.dx += self.step*2
            self.arrow = Polygon(self.label, self.struct, self.dx, self.dy, self.degree)
        if key == QtCore.Qt.Key.Key_Q:
            self.degree -= self.step
            self.arrow = Polygon(self.label, self.struct, self.dx, self.dy, self.degree)
        if key == QtCore.Qt.Key.Key_E:
            self.degree += self.step
            self.arrow = Polygon(self.label, self.struct, self.dx, self.dy, self.degree)

# запуск приложения
app = QtWidgets.QApplication()
window = MainWindow()
window.show()
app.exec_()
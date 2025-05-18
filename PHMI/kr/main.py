import sys
from PyQt5.QtWidgets import (
    QApplication, QMainWindow, QWidget,
    QPushButton, QHBoxLayout, QVBoxLayout,
)
from PyQt5.QtGui import (
    QPainter, QPolygonF, QPen,
    QLinearGradient, QBrush
)
from PyQt5.QtCore import QPointF, Qt


class TrapezoidWidget(QWidget):
    def __init__(self):
        super().__init__()
        self.setMinimumSize(400, 300)
        self._init_shape()
        # запоминание состояния отражения
        self._reflected = False  # track reflection state

    def _init_shape(self):
        """Инициализация вершин трапеции"""
        w_bottom, w_top, height = 200, 120, 120
        self.points = [
            QPointF(-w_top / 2, -height / 2),   
            QPointF( w_top / 2, -height / 2),   
            QPointF( w_bottom / 2,  height / 2),  
            QPointF(-w_bottom / 2,  height / 2),  
        ]


    def paintEvent(self, _event):  
        """Отрисовка трапеции"""
        painter = QPainter(self)

        painter.setRenderHint(QPainter.Antialiasing)
        # перемещение трапеции в центр экрана
        painter.translate(self.width() / 2, self.height() / 2)

        polygon = QPolygonF(self.points)
        bounds = polygon.boundingRect()

        # --- Линейный градиент ---
        # Переворачивание градиента при отражении, чтобы разница была видна
        grad = QLinearGradient(bounds.left(), 0, bounds.right(), 0)
        if self._reflected:
            grad.setColorAt(0.0, Qt.magenta)
            grad.setColorAt(1.0, Qt.cyan)
        else:
            grad.setColorAt(0.0, Qt.cyan)
            grad.setColorAt(1.0, Qt.magenta)

        painter.setBrush(QBrush(grad))
        painter.setPen(QPen(Qt.black, 2))
        painter.drawPolygon(polygon)

    def scale_horizontal(self, factor: float):
        """Масштабирование трапеции по горизонтали"""
        self.points = [QPointF(p.x() * factor, p.y()) for p in self.points]
        self.update()

    def reflect_vertical(self):
        """Отражение трапеции по вертикали"""
        self.points = [QPointF(-p.x(), p.y()) for p in self.points]
        self._reflected = not self._reflected  # toggle state
        self.update()


class MainWindow(QMainWindow):
    """Главное окно с областью рисования и кнопками управления"""

    def __init__(self):
        super().__init__()
        self.setWindowTitle("Трапеция")

        self.trap_widget = TrapezoidWidget()

        inc_btn  = QPushButton("1 – Увеличить по горизонтали")
        dec_btn  = QPushButton("2 – Уменьшить по горизонтали")
        refl_btn = QPushButton("3 – Отразить по вертикали")

        # подключение кнопок к функциям
        inc_btn.clicked.connect(lambda: self.trap_widget.scale_horizontal(1.2))
        dec_btn.clicked.connect(lambda: self.trap_widget.scale_horizontal(0.8))
        refl_btn.clicked.connect(self.trap_widget.reflect_vertical)

        # размещение кнопок в одной строке
        btn_layout = QHBoxLayout()
        btn_layout.addWidget(inc_btn)
        btn_layout.addWidget(dec_btn)
        btn_layout.addWidget(refl_btn)

        # размещение трапеции и кнопок в одной строке
        main_layout = QVBoxLayout()
        main_layout.addWidget(self.trap_widget)
        main_layout.addLayout(btn_layout)

        container = QWidget()
        container.setLayout(main_layout)
        self.setCentralWidget(container)


if __name__ == "__main__":
    app = QApplication(sys.argv)
    window = MainWindow()
    window.show()
    sys.exit(app.exec_())

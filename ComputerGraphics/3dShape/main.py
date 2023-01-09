import sys
from PySide6.QtGui import (QGuiApplication, QVector3D, QColor)
from PySide6.Qt3DCore import (Qt3DCore)
from PySide6.Qt3DExtras import (Qt3DExtras)


class Window(Qt3DExtras.Qt3DWindow):
    def __init__(self):
        super().__init__()

        # Устанавливаем камеру
        self.camera().setPosition(QVector3D(0, 0, 40))
        self.camera().setViewCenter(QVector3D(0, 0, 0))
        # метод создания сцены
        self.createScene()

        self.setRootEntity(self.rootEntity)

    def createScene(self):
        # Корневой элемент к которому все будет прикрепляться
        self.rootEntity = Qt3DCore.QEntity()

        # Создание объекта
        self.sphereEntity = Qt3DCore.QEntity(self.rootEntity)

        # Форма объекта - сфера
        self.sphereMesh = Qt3DExtras.QSphereMesh()
        self.sphereMesh.setRadius(3)

        # Задание материала
        self.material = Qt3DExtras.QDiffuseSpecularMaterial(self.sphereEntity)
        # Окружающий цвет - белый
        self.material.setAmbient(QColor('#FFFFFF'))
        # Цвет объекта - голубой
        self.material.setDiffuse(QColor("#59B9E0"))

        # Присваииваем объекту форму и материал
        self.sphereEntity.addComponent(self.sphereMesh)
        self.sphereEntity.addComponent(self.material)

if __name__ == '__main__':
    app = QGuiApplication(sys.argv)
    view = Window()
    view.show()
    sys.exit(app.exec())

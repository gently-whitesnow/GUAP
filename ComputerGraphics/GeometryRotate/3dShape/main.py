import sys
from turtle import color
from PySide6.QtCore import (Property, QObject, QPropertyAnimation, Signal)
from PySide6.QtGui import (QGuiApplication, QMatrix4x4, QQuaternion, QVector3D, QColor)
from PySide6.Qt3DCore import (Qt3DCore)
from PySide6.Qt3DExtras import (Qt3DExtras)
from PySide6.Qt3DRender import (Qt3DRender)

class Window(Qt3DExtras.Qt3DWindow):
    def __init__(self):
        super().__init__()
        
        # Устанавливаем камеру
        # self.camera().lens().setPerspectiveProjection(45, 16 / 9, 0.1, 1000)
        self.camera().setPosition(QVector3D(0, 0, 40))
        self.camera().setViewCenter(QVector3D(0, 0, 0))

        

        # метод создания сцены
        self.createScene()

        self.setRootEntity(self.rootEntity)

    def createScene(self):
        
        self.rootEntity = Qt3DCore.QEntity()

        # Задание материала
        self.material = Qt3DExtras.QDiffuseSpecularMaterial(self.rootEntity)
        # Окружающий цвет - белый
        self.material.setAmbient(QColor('#FFFFFF'))
        # Цвет объекта - голубой
        self.material.setDiffuse(QColor("#59B9E0"))



        # Создание сферы
        self.sphereEntity = Qt3DCore.QEntity(self.rootEntity)
        self.sphereMesh = Qt3DExtras.QSphereMesh()
        self.sphereMesh.setRadius(3)
  
        self.sphereEntity.addComponent(self.sphereMesh)
        self.sphereEntity.addComponent(self.material)


if __name__ == '__main__':
    app = QGuiApplication(sys.argv)
    view = Window()
    view.show()
    sys.exit(app.exec())
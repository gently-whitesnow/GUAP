import sys
from PySide6 import (QtCore)
from PySide6.QtGui import (QGuiApplication, QMatrix4x4,
                           QQuaternion, QVector3D, QColor)
from PySide6.Qt3DCore import (Qt3DCore)
from PySide6.Qt3DExtras import (Qt3DExtras)
from PySide6.Qt3DRender import (Qt3DRender)


class Window(Qt3DExtras.Qt3DWindow):
    def __init__(self):
        super().__init__()

        # Устанавливаем камеру
        self.camera().lens().setPerspectiveProjection(45, 16 / 9, 0.1, 1000)
        # Точка за которой будет следить камера
        self.camera().setViewCenter(QVector3D(0, 0, 0))
        # Позиция камеры
        self.cameraVector = QVector3D(0, 0, 40)
        self.camera().setPosition(self.cameraVector)

        self.rootEntity = Qt3DCore.QEntity()

        # Добавляем объекты
        self.createSphere(QVector3D(-10, 0, 5))
        self.createCube(QVector3D(10, 0, 3))
        self.createTorus(QVector3D(0, 0, 0))

        # Добавляем источники света
        self.createLightOne(QVector3D(0, 10, 0), 0.8, "blue")
        self.createLightTwo(QVector3D(0, -10, 0), 0.8, "red")
        self.createLightThree(QVector3D(0, 0, 20), 0.8, "yellow")

        self.setRootEntity(self.rootEntity)

    # функция для вращения источников света
    def rotateLight(self, light, quaternion):
        light.components()[0].setTranslation(quaternion.rotatedVector(light.components()[0].translation()))

    # источник света 1
    def createLightOne(self, position, intensity, color):
        # создаем узел
        self.lightOneEntity = Qt3DCore.QEntity(self.rootEntity)
        # создаем источник света
        self.lightOnePoint = Qt3DRender.QPointLight(self.lightOneEntity)
        # цвет источник света
        self.lightOnePoint.setColor(color)
        # интенсивность источник света
        self.lightOnePoint.setIntensity(intensity)
        # объект трансформации
        self.lightOneTransform = Qt3DCore.QTransform()
        # x y z
        self.lightOneTransform.setTranslation(position)
        # добавляем на узел источник и его позицию
        self.lightOneEntity.addComponent(self.lightOneTransform)
        self.lightOneEntity.addComponent(self.lightOnePoint)
        
    # источник света 2
    def createLightTwo(self, position, intensity, color):

        self.lightTwoEntity = Qt3DCore.QEntity(self.rootEntity)
        self.lightTwoPoint = Qt3DRender.QPointLight(self.lightTwoEntity)
        self.lightTwoPoint.setColor(color)
        self.lightTwoPoint.setIntensity(intensity)

        self.lightTwoTransform = Qt3DCore.QTransform()
        self.lightTwoTransform.setTranslation(position)

        self.lightTwoEntity.addComponent(self.lightTwoTransform)
        self.lightTwoEntity.addComponent(self.lightTwoPoint)

    # источник света 3
    def createLightThree(self, position, intensity, color):

        self.lightThreeEntity = Qt3DCore.QEntity(self.rootEntity)
        self.lightThreePoint = Qt3DRender.QPointLight(self.lightThreeEntity)
        self.lightThreePoint.setColor(color)
        self.lightThreePoint.setIntensity(intensity)

        self.lightThreeTransform = Qt3DCore.QTransform()
        self.lightThreeTransform.setTranslation(position)

        self.lightThreeEntity.addComponent(self.lightThreeTransform)
        self.lightThreeEntity.addComponent(self.lightThreePoint)

    # сфера
    def createSphere(self, position):

        # Создание узла для сферы
        self.sphereEntity = Qt3DCore.QEntity(self.rootEntity)

        # создания сферы
        self.sphereMesh = Qt3DExtras.QSphereMesh()
        self.sphereMesh.setRadius(3)

        # позиция сферы
        self.sphereTransform = Qt3DCore.QTransform()
        self.sphereTransform.setTranslation(position)
        
        # материал сферы
        self.sphereMaterial = getBaseMaterial(self.sphereEntity, 1000)

        # добавляем на узел сферу, ее позицию и материал
        self.sphereEntity.addComponent(self.sphereMesh)
        self.sphereEntity.addComponent(self.sphereTransform)
        self.sphereEntity.addComponent(self.sphereMaterial)

    # куб
    def createCube(self, position):

        # создаем узел для куба
        self.cubeEntity = Qt3DCore.QEntity(self.rootEntity)

        # создание куба
        self.cubeMesh = Qt3DExtras.QCuboidMesh()
        self.cubeMesh.setXExtent(2.0)
        self.cubeMesh.setYExtent(2.0)
        self.cubeMesh.setZExtent(2.0)

        # создание позиции для куба вместе с начальным поворотом
        self.cubeTransform = Qt3DCore.QTransform()
        matrix = QMatrix4x4()
        matrix.rotate(30.0, QVector3D(0, 1, 1))
        self.cubeTransform.setMatrix(matrix)
        self.cubeTransform.setTranslation(position)

        # материал куба
        self.cubeMaterial = getBaseMaterial(self.cubeEntity, 100)

        # добавляем на узел куб, его позицию и материал
        self.cubeEntity.addComponent(self.cubeMesh)
        self.cubeEntity.addComponent(self.cubeTransform)
        self.cubeEntity.addComponent(self.cubeMaterial)

    # тор
    def createTorus(self, position):

        # создание узла тора
        self.torusEntity = Qt3DCore.QEntity(self.rootEntity)

        # создание тора
        self.torusMesh = Qt3DExtras.QTorusMesh()
        self.torusMesh.setRadius(4.0)
        self.torusMesh.setMinorRadius(2.1)

        # создание позиции и начального поворота для тора
        self.torusTransform = Qt3DCore.QTransform()
        self.torusTransform.setTranslation(position)
        self.matrix = QMatrix4x4()
        self.matrix.rotate(-30.0, QVector3D(1, 0, 0))
        self.torusTransform.setMatrix(self.matrix)

        # создание материала для тора
        self.torusMaterial = getBaseMaterial(self.torusEntity, 50)

        # добавляем на узел тор, его позицию и материал
        self.torusEntity.addComponent(self.torusMesh)
        self.torusEntity.addComponent(self.torusTransform)
        self.torusEntity.addComponent(self.torusMaterial)

    #  обработка событий нажатия клавиш (английская раскладка)
    def keyPressEvent(self, event):
        # шаг поворота
        step = 2
        # вращаем вектор в сторону, согласно клавише
        self.rotation = QQuaternion.fromAxisAndAngle(QVector3D(0, 0, 0), 0)
        if event.key() == QtCore.Qt.Key_A:
            self.rotation = QQuaternion.fromAxisAndAngle(QVector3D(0, 1, 0), -step)
        elif event.key() == QtCore.Qt.Key_W:
            self.rotation = QQuaternion.fromAxisAndAngle(QVector3D(1, 0, 0), step)
        elif event.key() == QtCore.Qt.Key_D:
            self.rotation = QQuaternion.fromAxisAndAngle(QVector3D(0, 1, 0), step)
        elif event.key() == QtCore.Qt.Key_S:
            self.rotation = QQuaternion.fromAxisAndAngle(QVector3D(1, 0, 0), -step)

        # вращаем камеру
        self.cameraVector = self.rotation.rotatedVector(self.cameraVector)
        self.camera().setPosition(self.cameraVector)
        # вращаем свет 
        self.rotateLight(self.lightOneEntity, self.rotation)
        self.rotateLight(self.lightTwoEntity, self.rotation)
        self.rotateLight(self.lightThreeEntity, self.rotation)
        # при вращении камеры на такой же угол что и камера,
        # свет остается на своих позициях, а все остальная сцена вращается


# задание общего материала для объектов с различным коэффициентом отражения
def getBaseMaterial(entity, shiness):

    # Задание материала
    # Световой эффект фонга основан на сочетании трех компонентов освещения: окружающего, рассеянного и зеркального.
    #  Относительные силы этих компонентов контролируются с помощью их коэффициентов отражательной способности,
    #  которые моделируются как триплеты RGB:
    material = Qt3DExtras.QPhongMaterial(entity)
    # Цвет материала - салатовый
    material.setDiffuse(QColor("white"))
    # Блеск поверхности - коэффициент отражения от 0 до 100
    material.setShininess(shiness)
    return material


if __name__ == '__main__':
    app = QGuiApplication(sys.argv)
    view = Window()
    view.show()
    sys.exit(app.exec())

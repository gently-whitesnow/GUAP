from PySide6 import QtGui
import math

class Polygon():
    def __init__(self, label, points, dx=0, dy=0, degree=0):
        canvas = label.pixmap()
        painter = QtGui.QPainter(canvas)
        slow = 0
        fast = 1
        # переводим из градусов в радианы
        degree = degree*math.pi/180
        # Соединяем все точки в списке
        while (slow != len(points)):
            # х* = x cos  - y sin ,
            # у* = х sin  + у cos . 
            x1 = points[slow][0]*math.cos(degree)-points[slow][1]*math.sin(degree) + dx
            y1 = points[slow][0]*math.sin(degree)+points[slow][1]*math.cos(degree) + dy
            x2 = points[fast][0]*math.cos(degree)-points[fast][1]*math.sin(degree) + dx
            y2 = points[fast][0]*math.sin(degree)+points[fast][1]*math.cos(degree) + dy

            painter.drawLine(x1, y1, x2, y2)
            slow += 1
            fast += 1
            # Соединяем первую и последнюю точку
            if (fast == len(points)):
                fast=0

        painter.end()
        label.setPixmap(canvas)




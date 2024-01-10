#pragma once

#include <QGroupBox>
#include <QMainWindow>

#include "views/root.h"

class MainWindow : public QMainWindow {
   public:
   // инициализация приложения
    MainWindow() : QMainWindow() {
        QGroupBox* group = new QGroupBox("Зайцев Александр Сергеевич Z1431");
        auto root = new Root();
        group->setLayout(root);
        setCentralWidget(group);
    }
};
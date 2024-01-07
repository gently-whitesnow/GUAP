#pragma once

#include <QCheckBox>
#include <QComboBox>
#include <QGroupBox>
#include <QHBoxLayout>
#include <QLabel>
#include <QMainWindow>
#include <QPushButton>
#include <QRadioButton>
#include <QVBoxLayout>
#include <QtSql>
#include <iostream>
#include <map>
#include "views/root.h"

class MainWindow : public QMainWindow {
   public:
    MainWindow() : QMainWindow() {
        QGroupBox* group = new QGroupBox("Зайцев Александр Сергеевич Z1431");
        auto root = new Root();
        group->setLayout(root);
        setCentralWidget(group);
    }

   private:
    QSqlDatabase db = QSqlDatabase::addDatabase("QSQLITE");
};
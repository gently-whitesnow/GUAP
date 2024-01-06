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

class MainWindow : public QMainWindow {
   public:
    MainWindow() : QMainWindow() {
        db.setDatabaseName("my_db.sqlite");
        if (!db.open()) {
            qDebug() << "Что-то пошло не так!";
        }
        
    }

   private:
    QSqlDatabase db = QSqlDatabase::addDatabase("QSQLITE");
};
#pragma once

#include <QGroupBox>
#include <QHBoxLayout>
#include <QLabel>
#include <QLineEdit>
#include <QMainWindow>
#include <QPushButton>
#include <QVBoxLayout>

class MainWindow : public QMainWindow {
   public:
    MainWindow() : QMainWindow() {
        QGroupBox* group = new QGroupBox("Зайцев Александр Сергеевич Z1431");
        QVBoxLayout* mainLayout = new QVBoxLayout();

        QLabel* greeting = new QLabel("Здравствуй QT! Начинаем тебя изучать");
        mainLayout->addWidget(greeting);
        QHBoxLayout* horizontal = new QHBoxLayout();
        QLineEdit* lineEdit = new QLineEdit();
        horizontal->addWidget(lineEdit);
        QPushButton* button = new QPushButton("Start");

        QLabel* text = new QLabel("Text");

        QObject::connect(button, QOverload<bool>::of(&QPushButton::clicked),
                         [text, lineEdit](bool) {
                             text->setText(lineEdit->text());
                             lineEdit->clear();
                         });

        horizontal->addWidget(button);
        mainLayout->addLayout(horizontal);
        mainLayout->addWidget(text);
        mainLayout->setAlignment(text, Qt::AlignHCenter);
        group->setLayout(mainLayout);
        setCentralWidget(group);
    }
};
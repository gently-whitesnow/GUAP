#pragma once

#include <QHBoxLayout>
#include <QLabel>

class CurrentUser : public QHBoxLayout {
   public:
    CurrentUser(const uint userId) : QHBoxLayout() {
        QString title = QString::fromStdString("Текущий пользователь: ");
        title.append(QString::number(userId));
        QLabel* label = new QLabel(title);
        addWidget(label);
    }
};
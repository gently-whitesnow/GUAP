#pragma once

#include <QHBoxLayout>

class CurrentUser : public QHBoxLayout {
   public:
    // конструктор информации о пользователе
    CurrentUser(const uint userId);
};
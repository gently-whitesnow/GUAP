#pragma once

#include <service/repository.h>

#include <QVBoxLayout>

class Content : public QVBoxLayout {
   public:
    // конструктор контента
    Content(int userId);

   private:
    Repository repo;
};
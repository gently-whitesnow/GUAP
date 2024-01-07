#pragma once

#include <QString>

class Book {
   public:
    Book(int id, QString name, QString author) {
        this->id = id;
        this->name = name;
        this->author = author;
    }

    uint32_t id;
    QString name;
    QString author;
};
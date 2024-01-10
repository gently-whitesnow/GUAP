#pragma once

#include <QString>

class Book {
   public:
    uint32_t id;
    QString name;
    QString author;

    // запрос на создание таблицы
    QString makeCreateTableQuery() const;

    // запрос на получение всех данных
    QString makeSelectAllQuery() const;

    // запрос на получение данного по id книги
    QString makeSelectQuery(decltype(id) book_id) const;

    // запрос на добавление данных
    QString makeInsertQuery() const;

    // запрос на удаление данных
    QString makeDeleteQuery() const;
};

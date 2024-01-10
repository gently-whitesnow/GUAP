#pragma once

#include "book.h"
#include "user.h"

struct Reservation {
    User user;
    Book book;

    // запрос на создание таблицы
    QString makeCreateTableQuery() const;

    // запрос на получение всех данных
    QString makeSelectAllQuery() const;

    // запрос на добавление данных
    QString makeInsertQuery() const;

    // запрос на удаление данных
    QString makeDeleteQuery() const;
};

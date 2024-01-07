#pragma once

#include "book.h"
#include "user.h"

struct Reservation {
    User user;
    Book book;

    QString makeCreateTableQuery() const {
        return "CREATE TABLE IF NOT EXISTS reservations ("
               "id integer PRIMARY KEY NOT NULL, "
               "user_id integer, "
               "book_id integer"
               ");";
    }

    QString makeSelectAllQuery() const { return "SELECT user_id, book_id FROM reservations"; }

    QString makeInsertQuery() const {
        return QString(
                   "INSERT INTO reservations(user_id, book_id) "
                   "VALUES (%1, %2);")
            .arg(user.id).arg(book.id);
    }

    QString makeDeleteQuery() const {
        static QString kInsertQuery =
            "DELETE FROM reservations WHERE user_id = %1 AND book_id = %2;";
        return kInsertQuery.arg(user.id).arg(book.id);
    }
};

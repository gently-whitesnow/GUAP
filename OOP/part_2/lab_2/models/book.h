#pragma once

#include <QString>

class Book {
   public:
    uint32_t id;
    QString name;
    QString author;

    QString makeCreateTableQuery() const {
        return "CREATE TABLE IF NOT EXISTS books ("
               "id integer PRIMARY KEY NOT NULL, "
               "name VARCHAR(255), "
               "author VARCHAR(255)"
               ");";
    }

    QString makeSelectAllQuery() const { return "SELECT * FROM books"; }

    QString makeSelectQuery(decltype(id) book_id) const {
        return QString("SELECT id, name, author FROM books WHERE id = %1").arg(book_id);
    }

    QString makeInsertQuery() const {
        return QString(
                   "INSERT INTO books(name, author) "
                   "VALUES ('%1', '%2');")
            .arg(name, author);
    }

    QString makeDeleteQuery() const {
        static QString kInsertQuery = "DELETE FROM books WHERE id = %1;";
        return kInsertQuery.arg(id);
    }
};

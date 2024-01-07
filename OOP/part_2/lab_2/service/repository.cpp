#include "repository.h"

#include <exception>

Repository::Repository() : db_(QSqlDatabase::addDatabase("QSQLITE")) {
    db_.setDatabaseName("repository.sqlite");
    if (!db_.open()) {
        throw std::runtime_error("Невозможно открыть базу данных");
    }
    QSqlQuery query;
    if (!query.exec(Book().makeCreateTableQuery())) {
        throw std::runtime_error("Не удается создать таблицу книг");
    }
    if (!query.exec(Reservation().makeCreateTableQuery())) {
        throw std::runtime_error("Не удается создать таблицу бронирования");
    }
}

void Repository::addBook(const Book& book) {
    QSqlQuery query;
    if (!query.exec(book.makeInsertQuery())) {
        throw std::runtime_error("Не удается добавить книгу");
    }
}

void Repository::removeBook(const Book& book) {
    QSqlQuery query;
    if (!query.exec(book.makeDeleteQuery())) {
        throw std::runtime_error("Не удается удалить книгу");
    }
}

std::vector<Book> getBooks() {
    QSqlQuery query;
    if (!query.exec(Book().makeSelectAllQuery())) {
        throw std::runtime_error("Не удается получить список книг");
    }
    QSqlRecord rec = query.record();

    std::vector<Book> books;
    while (query.next()) {
        Book book;
        book.id = query.value(rec.indexOf("id")).toInt();
        book.name = query.value(rec.indexOf("name")).toString();
        book.author = query.value(rec.indexOf("author")).toString();
        books.emplace_back(std::move(book));
    }
    return books;
}

void Repository::makeReservation(const User& user, const Book& book) {
    QSqlQuery query;
    if (!query.exec(
            Reservation{.user = user, .book = book}.makeInsertQuery())) {
        throw std::runtime_error("Не удается добавить бронь в базу данных");
    }
}

void Repository::freeReservation(const Reservation& reservation) {
    QSqlQuery query;
    if (!query.exec(reservation.makeDeleteQuery())) {
        throw std::runtime_error("Не удается удалить бронь из базы данных");
    }
}

std::vector<Reservation> Repository::getReservations() {
    QSqlQuery query;
    if (!query.exec(Reservation().makeSelectAllQuery())) {
        throw std::runtime_error("Не удается получить список брони");
    }
    QSqlRecord rec = query.record();

    std::vector<Reservation> reservations;
    while (query.next()) {
        Reservation reservation;

        uint32_t bookId = query.value(rec.indexOf("book_id")).toInt();
        uint32_t userId = query.value(rec.indexOf("user_id")).toInt();

        Book book;
        {
            QSqlQuery book_query;
            if (!book_query.exec(Book().makeSelectQuery(bookId))) {
                continue;
            }
            QSqlRecord rec = book_query.record();
            query.next();
            book.id = query.value(rec.indexOf("id")).toInt();
            book.name = query.value(rec.indexOf("name")).toString();
            book.author = query.value(rec.indexOf("author")).toString();
        }
        reservation.book = std::move(book);
        reservation.user = User{.id = userId};
        reservations.emplace_back(std::move(reservation));
    }
    return reservations;
}

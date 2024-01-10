#pragma once

#include <QtSql>

#include <models/book.h>
#include <models/reservation.h>
#include <models/user.h>
#include <vector>

class Repository {
public:

    Repository();

    // добавить книгу
    void addBook(const Book& book);
    // удалить книгу
    void removeBook(const Book& book);
    // получить все книги
    std::vector<Book> getBooks();
    // зарезервировать книгу
    void makeReservation(const User& user, const Book& book);
    // разрезервировать книгу
    void freeReservation(const Reservation& reservation);
    // получить все резервации
    std::vector<Reservation> getReservations();

private:
    QSqlDatabase db_;
};
#pragma once

#include <QtSql>

#include <models/book.h>
#include <models/reservation.h>
#include <models/user.h>
#include <vector>

class Repository {
public:

    Repository();

    void addBook(const Book& book);
    void removeBook(const Book& book);
    std::vector<Book> getBooks();
    void makeReservation(const User& user, const Book& book);
    void freeReservation(const Reservation& reservation);
    std::vector<Reservation> getReservations();

private:
    QSqlDatabase db_;
};
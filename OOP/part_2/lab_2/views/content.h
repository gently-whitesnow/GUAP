#pragma once

#include <service/repository.h>

#include <QHBoxLayout>
#include <QInputDialog>
#include <QLabel>
#include <QMessageBox>
#include <QPushButton>

#include "../models/book.h"
#include "table.h"

class Content : public QVBoxLayout {
   public:
    Content(int userId) : QVBoxLayout() {
        QPushButton* addButton = new QPushButton("Добавить книгу");

        addWidget(addButton);

        auto values = new QList<Book>;

        Table* table = new Table(repo.getBooks(), repo.getReservations(), userId);
        table->setRemoveCallback([table, this](Book book) {
            repo.removeBook(book);
            table->updateData(repo.getBooks(), repo.getReservations());
        }).setRemoveReservationCallback([table, this](Reservation reservation) {
            repo.freeReservation(reservation);
            table->updateData(repo.getBooks(), repo.getReservations());
        }).setAddReservationCallback([table, this](User user, Book book) {
            repo.makeReservation(user, book);
            table->updateData(repo.getBooks(), repo.getReservations());
        });

        QObject::connect(
            addButton, QOverload<bool>::of(&QPushButton::clicked),
            [table, this](bool) {
                bool ok;
                QString bookName = QInputDialog::getText(
                    nullptr, "Добавить книгу",
                    "Введите название книги:", QLineEdit::Normal, QString(),
                    &ok, Qt::WindowFlags(), Qt::ImhNone);
                if (!ok) {
                    return;
                }
                if (bookName.isEmpty()) {
                    QMessageBox::warning(
                        nullptr, "Ошибка",
                        "Нельзя добавить книгу с пустым названием");
                    return;
                }
                ok = false;
                QString bookAuthor = QInputDialog::getText(
                    nullptr, "Добавить книгу",
                    "Введите автора книги:", QLineEdit::Normal, QString(), &ok,
                    Qt::WindowFlags(), Qt::ImhNone);
                if (!ok) {
                    return;
                }
                if (bookAuthor.isEmpty()) {
                    QMessageBox::warning(
                        nullptr, "Ошибка",
                        "Нельзя добавить книгу с пустым именем автора");
                    return;
                }
                repo.addBook(Book{.name = bookName, .author = bookAuthor});
                table->updateData(repo.getBooks(), repo.getReservations());
            });

        addLayout(table);
    }

   private:
    Repository repo;
};
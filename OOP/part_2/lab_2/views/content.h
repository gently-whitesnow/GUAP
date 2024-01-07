#pragma once

#include <QHBoxLayout>
#include <QLabel>
#include <QPushButton>

#include "../models/book.h"
#include "table.h"

class Content : public QVBoxLayout {
   public:
    Content() : QVBoxLayout() {
        QPushButton* addButton = new QPushButton("добавить книгу");
        addWidget(addButton);

        auto values = new QList<Book>;

        std::vector<Book> data;
        data.emplace_back(Book{1, "1", "1"});
        data.emplace_back(Book{2, "2", "2"});
        data.emplace_back(Book{3, "3", "3"});
        data.emplace_back(Book{4, "4", "4"});
        Table* table = new Table(data);
        addLayout(table);
    }
};
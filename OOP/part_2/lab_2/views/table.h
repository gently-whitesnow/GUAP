#pragma

#include <QHBoxLayout>
#include <QVBoxLayout>

#include "../models/book.h"

class Table : public QHBoxLayout {
   public:
    Table(std::vector<Book>& data) {
        _data = data;

        QVBoxLayout* idLayout = new QVBoxLayout();
        QLabel* idLabel = new QLabel("Ид");
        idLayout->addWidget(idLabel);
        QVBoxLayout* nameLayout = new QVBoxLayout();
        QLabel* nameLabel = new QLabel("Название");
        nameLayout->addWidget(nameLabel);
        QVBoxLayout* authorLayout = new QVBoxLayout();
        QLabel* authorLabel = new QLabel("Автор");
        authorLayout->addWidget(authorLabel);
        QVBoxLayout* deleteLayout = new QVBoxLayout();
        QLabel* deleteLabel = new QLabel("Удалить");
        deleteLayout->addWidget(deleteLabel);

        for (const auto book : _data) {
            QLabel* id = new QLabel(QString::number(book.id));
            idLayout->addWidget(id);
            QLabel* name = new QLabel(book.name);
            nameLayout->addWidget(name);
            QLabel* author = new QLabel(book.author);
            authorLayout->addWidget(author);
            QPushButton* deleteButton = new QPushButton("X");
            deleteLayout->addWidget(deleteButton);
        }

        addLayout(idLayout);
        addLayout(nameLayout);
        addLayout(authorLayout);
        addLayout(deleteLayout);
    }

   private:
    std::vector<Book> _data;
};
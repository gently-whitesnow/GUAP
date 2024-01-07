#pragma

#include <QHBoxLayout>
#include <QVBoxLayout>
#include <functional>

#include "../models/book.h"

class Table : public QHBoxLayout {
   public:
    Table(std::vector<Book>&& data) : data_(std::move(data)) {
        initColumns();

        fillColumns();

        addLayout(idLayout_);
        addLayout(nameLayout_);
        addLayout(authorLayout_);
        addLayout(deleteLayout_);
    }

    void setRemoveCallback(std::function<void(Book)> removeRowCallback) {
        removeRowCallback_ = removeRowCallback;
    }

    void updateData(std::vector<Book>&& data) {
        data_ = std::move(data);
        clearColumns();
        initColumns();
        fillColumns();
        update();
    }

   private:
    void clearLayout(QLayout* layout) {
        if (layout == NULL) return;
        QLayoutItem* item;
        while ((item = layout->takeAt(0))) {
            if (item->layout()) {
                clearLayout(item->layout());
                delete item->layout();
            }
            if (item->widget()) {
                delete item->widget();
            }
            delete item;
        }
    }

    void clearColumns() {
        clearLayout(idLayout_);
        clearLayout(nameLayout_);
        clearLayout(authorLayout_);
        clearLayout(deleteLayout_);
    }

    void initColumns() {
        QLabel* idLabel = new QLabel("Ид");
        idLayout_->addWidget(idLabel);
        QLabel* nameLabel = new QLabel("Название");
        nameLayout_->addWidget(nameLabel);
        QLabel* authorLabel = new QLabel("Автор");
        authorLayout_->addWidget(authorLabel);
        QLabel* deleteLabel = new QLabel("Удалить");
        deleteLayout_->addWidget(deleteLabel);
    }

    void fillColumns() {
        for (const auto book : data_) {
            QLabel* id = new QLabel(QString::number(book.id));
            idLayout_->addWidget(id);
            QLabel* name = new QLabel(book.name);
            nameLayout_->addWidget(name);
            QLabel* author = new QLabel(book.author);
            authorLayout_->addWidget(author);
            QPushButton* deleteButton = new QPushButton("X");
            deleteLayout_->addWidget(deleteButton);
            QObject::connect(deleteButton,
                             QOverload<bool>::of(&QPushButton::clicked),
                             [book, this](bool) {
                                 if (removeRowCallback_) {
                                     removeRowCallback_(book);
                                 }
                             });
        }
    }

    std::vector<Book> data_;
    std::function<void(Book)> removeRowCallback_;
    QVBoxLayout* idLayout_ = new QVBoxLayout();
    QVBoxLayout* nameLayout_ = new QVBoxLayout();
    QVBoxLayout* authorLayout_ = new QVBoxLayout();
    QVBoxLayout* deleteLayout_ = new QVBoxLayout();
};
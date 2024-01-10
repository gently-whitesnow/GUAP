#include "table.h"

#include <QLabel>
#include <QPushButton>

Table::Table(std::vector<Book>&& data, std::vector<Reservation>&& reservations,
             uint32_t userId)
    : data_(std::move(data)),
      reservations_(std::move(reservations)),
      userId_(userId) {
    initColumns();

    fillColumns();

    addLayout(idLayout_);
    addLayout(nameLayout_);
    addLayout(authorLayout_);
    addLayout(reservationLayout_);
    addLayout(deleteLayout_);
}

Table& Table::setRemoveCallback(std::function<void(Book)> removeRowCallback) {
    removeRowCallback_ = removeRowCallback;
    return *this;
}

Table& Table::setRemoveReservationCallback(
    std::function<void(Reservation)> removeReservationCallback) {
    removeReservationCallback_ = removeReservationCallback;
    return *this;
}

Table& Table::setAddReservationCallback(
    std::function<void(User, Book)> addReservationCallback) {
    addReservationCallback_ = addReservationCallback;
    return *this;
}

void Table::updateData(std::vector<Book>&& data,
                       std::vector<Reservation>&& reservations) {
    data_ = std::move(data);
    reservations_ = std::move(reservations);
    clearColumns();
    initColumns();
    fillColumns();
    update();
}

void Table::clearLayout(QLayout* layout) {
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

void Table::clearColumns() {
    clearLayout(idLayout_);
    clearLayout(nameLayout_);
    clearLayout(authorLayout_);
    clearLayout(deleteLayout_);
    clearLayout(reservationLayout_);
}

void Table::initColumns() {
    QLabel* idLabel = new QLabel("Ид");
    idLayout_->addWidget(idLabel);
    QLabel* nameLabel = new QLabel("Название");
    nameLayout_->addWidget(nameLabel);
    QLabel* authorLabel = new QLabel("Автор");
    authorLayout_->addWidget(authorLabel);
    QLabel* reservationLabel = new QLabel("Резервация");
    reservationLayout_->addWidget(reservationLabel);
    QLabel* deleteLabel = new QLabel("Удалить");
    deleteLayout_->addWidget(deleteLabel);
}

void Table::fillColumns() {
    std::map<decltype(Book::id), Reservation> reservationMap;
    for (const auto& reservation : reservations_) {
        reservationMap[reservation.book.id] = reservation;
    }
    for (const auto book : data_) {
        QLabel* id = new QLabel(QString::number(book.id));
        idLayout_->addWidget(id);
        QLabel* name = new QLabel(book.name);
        nameLayout_->addWidget(name);
        QLabel* author = new QLabel(book.author);
        authorLayout_->addWidget(author);
        if (reservationMap.contains(book.id)) {
            Reservation reservationItem = reservationMap.at(book.id);
            if (reservationItem.user.id == userId_) {
                QPushButton* deleteReservationButton = new QPushButton("-");
                reservationLayout_->addWidget(deleteReservationButton);
                QObject::connect(
                    deleteReservationButton,
                    QOverload<bool>::of(&QPushButton::clicked),
                    [reservationItem, this](bool) {
                        if (removeReservationCallback_) {
                            removeReservationCallback_(reservationItem);
                        }
                    });
            } else {
                QLabel* reservationLabel = new QLabel(QString("Резервировано: ") + 
                    QString::number(reservationMap.at(book.id).user.id));
                reservationLayout_->addWidget(reservationLabel);
            }
        } else {
            QPushButton* addReservationButton = new QPushButton("+");
            QObject::connect(
                addReservationButton,
                QOverload<bool>::of(&QPushButton::clicked), [book, this](bool) {
                    if (addReservationCallback_) {
                        addReservationCallback_(User{.id = userId_}, book);
                    }
                });
            reservationLayout_->addWidget(addReservationButton);
        }
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
#pragma

#include <QHBoxLayout>
#include <QVBoxLayout>
#include <functional>
#include <map>

#include <models/book.h>
#include <models/reservation.h>

class Table : public QHBoxLayout {
   public:
    // конструктор таблицы
    Table(std::vector<Book>&& data, std::vector<Reservation>&& reservations,
          uint32_t userId);

    // Добавить обратный вызов для удаления книг
    Table& setRemoveCallback(std::function<void(Book)> removeRowCallback);

    // Добавить обратный вызов для удаления резервации
    Table& setRemoveReservationCallback(
        std::function<void(Reservation)> removeReservationCallback);

    // Добавить обратный вызов для добавления резервации
    Table& setAddReservationCallback(
        std::function<void(User, Book)> addReservationCallback);

    // обновить данные в таблицу
    void updateData(std::vector<Book>&& data,
                    std::vector<Reservation>&& reservations);

   private:
    // очистить данные в строках
    void clearLayout(QLayout* layout);

    // очистить данные в колонках
    void clearColumns();

    // инициализировать название колонок
    void initColumns();

    // заполнить колонки
    void fillColumns();

    std::vector<Book> data_;
    std::vector<Reservation> reservations_;
    uint32_t userId_;
    std::function<void(Book)> removeRowCallback_;
    std::function<void(Reservation)> removeReservationCallback_;
    std::function<void(User, Book)> addReservationCallback_;
    QVBoxLayout* idLayout_ = new QVBoxLayout();
    QVBoxLayout* nameLayout_ = new QVBoxLayout();
    QVBoxLayout* authorLayout_ = new QVBoxLayout();
    QVBoxLayout* deleteLayout_ = new QVBoxLayout();
    QVBoxLayout* reservationLayout_ = new QVBoxLayout();
};
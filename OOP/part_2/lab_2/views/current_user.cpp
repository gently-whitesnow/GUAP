#include "current_user.h"

#include <QLabel>

CurrentUser::CurrentUser(const uint userId) : QHBoxLayout() {
    QString title = QString::fromStdString("Текущий пользователь: ");
    title.append(QString::number(userId));
    QLabel* label = new QLabel(title);
    addWidget(label);
}
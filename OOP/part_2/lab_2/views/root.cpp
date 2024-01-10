#include "root.h"

#include <QInputDialog>

Root::Root() : QVBoxLayout() {
    bool ok = false;
    int id = 0;
    while (!ok) {
        id = QInputDialog::getInt(nullptr, "Авторизация",
                                  "Введите номер пользователя:", 0, 0, 1024, 1,
                                  &ok, Qt::WindowFlags());
    }
    CurrentUser* currentUser = new CurrentUser(id);
    addLayout(currentUser);
    Content* content = new Content(id);
    addLayout(content);
}
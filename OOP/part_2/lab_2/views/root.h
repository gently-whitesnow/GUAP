#pragma once

#include <QVBoxLayout>
#include "current_user.h"
#include "content.h"

class Root : public QVBoxLayout {
   public:
    Root() : QVBoxLayout() {
        CurrentUser* currentUser = new CurrentUser(15);
        addLayout(currentUser);
        Content* content = new Content();
        addLayout(content);
    }
};
#pragma once
#include <models/serialisable.h>
#include <lib/nlohmann/json.hpp>

#include <ctime>
#include <memory>
#include <sstream>
#include <string>

class BookDbModel : public ISerialisable {
   public:
    int Id;
    std::string Title;
    std::string Description;
    std::string Author;
    tm AddDate;
    int Count;

    static std::unique_ptr<ISerialisable> from_json(const nlohmann::json& j) {
        if (!j.is_object()) {
            return nullptr;
        }

        auto bookModel = new BookDbModel();

        bookModel->Id = j.value("Id", 0);  // Provide default value if key is not present
        bookModel->Title = j.value("Title", "");
        bookModel->Description = j.value("Description", "");
        bookModel->Author = j.value("Author", "");

        std::string addDateStr = j.value("AddDate", "");
        std::istringstream addDateStream(addDateStr);
        addDateStream >> std::get_time(&bookModel->AddDate, "%Y-%m-%d %H:%M:%S");

        bookModel->Count = j.value("Count", 0);

        return std::unique_ptr<ISerialisable>(bookModel);
    }
};

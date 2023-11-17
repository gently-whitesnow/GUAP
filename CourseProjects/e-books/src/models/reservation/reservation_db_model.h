#pragma once
#include <models/serialisable.h>

#include <ctime>
#include <memory>
#include <sstream>
#include <string>

class ReservationDbModel : public ISerialisable
{
public:
    int Id;
    int BookId;
    int UserId;
    tm ReserveDate;

    static std::unique_ptr<ISerialisable> from_json(const nlohmann::json& j) {
        if (!j.is_object()) {
            return nullptr;
        }

        auto reserveModel = new ReservationDbModel();

        reserveModel->Id = j.value("Id", 0);  // Provide default value if key is not present
        reserveModel->BookId = j.value("BookId", 0);
        reserveModel->UserId = j.value("UserId", 0);
        
        std::string reserveDateStr = j.value("ReserveDate", "");
        std::istringstream reserveDateStream(reserveDateStr);
        reserveDateStream >> std::get_time(&reserveModel->ReserveDate, "%Y-%m-%d %H:%M:%S");

        return std::unique_ptr<ISerialisable>(reserveModel);
    }
};

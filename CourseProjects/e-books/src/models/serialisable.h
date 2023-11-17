#pragma once
#include <lib/nlohmann/json.hpp>
#include <memory>

class ISerialisable {
   public:
    static std::unique_ptr<ISerialisable> from_json(const nlohmann::json& j) { return nullptr; };
};
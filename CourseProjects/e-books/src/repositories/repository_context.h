#pragma once
#include <concepts>
#include <lib/nlohmann/json.hpp>
#include <unordered_map>
#include <models/serialisable.h>

template <typename TDto>
    requires std::derived_from<TDto, ISerialisable>
class RepositoryContext {
   public:
    virtual ~RepositoryContext() {}
    std::unordered_map<int, TDto> Data;

    int GetId() {
        return _idCounter++;
    };

    static RepositoryContext<TDto> from_json(const nlohmann::json& j) {
        RepositoryContext<TDto> context;

        if (j.contains("Data")) {
            for (const auto& entry : j["Data"].items()) {
                int id = std::stoi(entry.key());
                auto dto = TDto::from_json(entry.value());
                context.Data[id] = *reinterpret_cast<TDto*>(dto.get());
            }
        }
        if (j.contains("IdCounter")) {
            j["IdCounter"].get_to(context._idCounter);
        }

        return context;
    }

    nlohmann::json to_json(nlohmann::json& j, const RepositoryContext<TDto>& context) {
        return nlohmann::json{{"Data", context.Data}, {"IdCounter", context._idCounter}};
    }

   private:
    int _idCounter = 0;
};
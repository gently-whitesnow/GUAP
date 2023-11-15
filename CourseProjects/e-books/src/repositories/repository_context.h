#pragma once
#include <lib/nlohmann/json.hpp>
#include <map>

template <typename TDto>
class RepositoryContext {
   public:
    virtual ~RepositoryContext() {}
    std::map<int, TDto> Data;

    int GetId(){
        return _idCounter++;
    };
    

    private:
    int _idCounter = 0; 

    friend void to_json(nlohmann::json& j, const RepositoryContext<TDto>& context) {
        j = nlohmann::json{{"Data", context.Data}, {"IdCounter", context._idCounter}};
    }

    // TODO как решить это для темплейтов TDto?
    // Определять сериализацию в них? например определить сериазизацию в BookDbModel ??
    friend void from_json(const nlohmann::json& j, RepositoryContext<TDto>& context) {
        if (j.contains("Data")) {
            for (const auto& entry : j["Data"].items()) {
                int id = std::stoi(entry.key());
                TDto dto = entry.value().get<TDto>();
                context.Data[id] = dto;
            }
        }
        if (j.contains("IdCounter")) {
            j["IdCounter"].get_to(context._idCounter);
        }
    }

};
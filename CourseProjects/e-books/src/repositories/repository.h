#pragma once
#include <fstream>
#include <iostream>
#include <lib/nlohmann/json.hpp>
#include <map>
#include <string>
#include <typeinfo>

#include "repository_context.h"
using json = nlohmann::json;

template <typename TDto>
class FileRepository {
   public:
    virtual ~FileRepository() {}
    FileRepository() {
        _fileName = typeid(TDto).name();
        Read();
    }

    const std::map<int, TDto> GetAll() {
        return _context.Data;
    };

    const std::map<int, TDto> Add(TDto& dto) {
        auto id = _context.GetId();
        dto.id = id;
        _context.Data[id] = dto;
        Save();
        return _context.Data;
    }

    const std::map<int, TDto> Update(int id, void(modificationAction)(TDto)) {
        if (!_context.Data.contains(id)) {
            throw "not found exception";
        }
        modificationAction(_context.Data[id]);
        Save();
        return _context.Data;
    }

    const std::map<int, TDto> Delete(int id) {
        if (!_context.Data.contains(id)) {
            throw "not found exception";
        }
        _context.Data.erase(id);
        Save();
        return _context.Data;
    }

   private:
    RepositoryContext<TDto> _context;
    std::string _fileName;

    void Save() {
        json jsonContext(_context);
        std::ofstream myfile;
        myfile.open(_fileName);
        myfile << jsonContext;
        myfile.close();
    }

    void Read() {
        std::ifstream sourceFile(_fileName);
        if (sourceFile.good()) {
            json data;
            sourceFile >> data;
            _context = data;  // Implicit conversion from json
        }
    }
};
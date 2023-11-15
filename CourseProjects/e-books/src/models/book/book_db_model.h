#pragma once
#include <string>
#include <ctime>

class BookDbModel
{
public:
    int Id;
    std::string Title;
    std::string Description;
    std::string Author;
    tm AddDate;
    int Count;

public:
    BookDbModel();
    ~BookDbModel();
};

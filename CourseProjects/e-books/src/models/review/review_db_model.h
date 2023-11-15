#pragma once
#include <string>
#include <ctime>

class ReviewDbModel
{
public:
    int Id;
    int BookId;
    int UserId;
    std::string Content;
    tm AddDate;

public:
    ReviewDbModel();
    ~ReviewDbModel();
};

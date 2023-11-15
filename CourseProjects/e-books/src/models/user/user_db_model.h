#pragma once
#include <string>

class UserDbModel
{
public:
    int Id;
    std::string Email;
    std::string FullName;

public:
    UserDbModel();
    ~UserDbModel();
};

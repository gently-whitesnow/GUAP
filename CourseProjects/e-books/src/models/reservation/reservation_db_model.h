#pragma once
#include <string>
#include <ctime>

class ReservationDbModel
{
public:
    int Id;
    int BookId;
    int UserId;
    tm ReserveDate;

public:
    ReservationDbModel();
    ~ReservationDbModel();
};

#pragma once
#include <string>

class SummaryBooksRequest{
    public:
        std::string SearchQuery;
        bool HasOnlyAvailable;
        int Skip = 0;
        int Take = 10;
};
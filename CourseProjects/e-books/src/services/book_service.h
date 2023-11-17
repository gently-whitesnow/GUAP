#pragma once
#include <models/book/book_db_model.h>
#include <models/book/summary_books_request.h>
#include <models/reservation/reservation_db_model.h>
#include <repositories/book_repository.h>
#include <repositories/reservation_repository.h>

#include <algorithm>
#include <map>
#include <vector>

class BookService {
   public:
    BookService(BookRepository bookRepository, ReservationRepository reservationRepository)
        : _bookRepository(bookRepository), _reservationRepository(reservationRepository){};

    std::vector<BookDbModel> GetBooksSummary(SummaryBooksRequest request) {
        auto rawBooks = _bookRepository.GetAll();

        if (request.SearchQuery != "") {
            rawBooks.erase(std::remove_if(rawBooks.begin(), rawBooks.end(), [&](const auto& bookPair) {
                               const BookDbModel& book = bookPair.second;
                               return book.Title.find(request.SearchQuery) == std::string::npos &&
                                      book.Description.find(request.SearchQuery) == std::string::npos;
                           }),
                           rawBooks.end());
        }

        auto reservations = _reservationRepository.GetAll();

        if (request.HasOnlyAvailable) {
            rawBooks.erase(std::remove_if(rawBooks.begin(), rawBooks.end(), [&](const auto& bookPair) {
                               const BookDbModel& book = bookPair.second;
                               return std::any_of(reservations.begin(), reservations.end(),
                                                  [&](const auto& reservationPair) {
                                                      const ReservationDbModel& reservation = reservationPair.second;
                                                      return reservation.BookId == book.Id;
                                                  });
                           }),
                           rawBooks.end());
        }

        if (request.Skip >= rawBooks.size()) {
            return {};
        }

        std::vector<BookDbModel> ret;
        
        // auto subsetBegin = rawBooks.begin() + request.Skip;
        // auto subsetEnd = rawBooks.begin() + std::min(request.Skip + request.Take, (int)rawBooks.size());

        // std::vector<BookDbModel> resultBooks(subsetBegin, subsetEnd);
        return ret;
    }
    BookDbModel GetBook(int bookId);
    void UpsertBook(BookDbModel book);
    void DeleteBook(int bookId);

   private:
    BookRepository _bookRepository;
    ReservationRepository _reservationRepository;
};
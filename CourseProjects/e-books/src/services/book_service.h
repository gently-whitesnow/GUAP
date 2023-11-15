#pragma once
#include <models/book/book_db_model.h>
#include <models/book/summary_books_request.h>
#include <models/reservation/reservation_db_model.h>
#include <repositories/book_repository.h>

#include <algorithm>
#include <map>
#include <vector>

class BookService {
   public:
    BookService(BookRepository bookRepository) : _bookRepository(bookRepository){};

    // TODO хотелось бы чтобы весь метод компилировался
    std::vector<BookDbModel> GetBooksSummary(SummaryBooksRequest request) {
        auto rawBooks = _bookRepository.GetAll();
        std::vector<BookDbModel> ret;

        if (request.SearchQuery != "") {
            books.erase(std::remove_if(books.begin(), books.end(), [&](const auto& bookPair) {
                            const BookDbModel& book = bookPair.second;
                            return book.Title.find(request.SearchQuery) == std::string::npos &&
                                   book.Description.find(request.SearchQuery) == std::string::npos;
                        }),
                        books.end());
        }

        auto reservations = _bookRepository.GetAll();

        if (request.HasOnlyAvailable) {
            books.erase(std::remove_if(books.begin(), books.end(), [&](const auto& bookPair) {
                            const BookDbModel& book = bookPair.second;
                            return std::any_of(reservations.begin(), reservations.end(),
                                               [&](const auto& reservationPair) {
                                                   const ReservationDbModel& reservation = reservationPair.second;
                                                   return reservation.BookId == book.Id;
                                               });
                        }),
                        books.end());
        }
        if (request.Skip >= books.size()) {
            return {};
        }

        auto subsetBegin = books.begin() + request.Skip;
        auto subsetEnd = books.begin() + std::min(request.Skip + request.Take, (int)books.size());

        std::vector<BookDbModel> resultBooks(subsetBegin, subsetEnd);
        return resultBooks;
    }
    BookDbModel GetBook(int bookId);
    void UpsertBook(BookDbModel book);
    void DeleteBook(int bookId);

   private:
    BookRepository _bookRepository;
};
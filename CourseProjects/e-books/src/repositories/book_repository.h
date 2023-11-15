#pragma once
#include <models/book/book_db_model.h>
#include <repositories/repository.h>

class BookRepository : public FileRepository<BookDbModel> {

};
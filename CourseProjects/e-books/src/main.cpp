#include <iostream>
#include <vector>
#include <services/book_service.h>
#include <repositories/book_repository.h>

using namespace std;

int main() {
    auto b = BookRepository();
    auto service = BookService(b);
    
    cout << "Starting program" << endl;
}
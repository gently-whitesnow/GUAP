#include <cstdio>
#include <iostream>

int main() {
  // Открытие файлов для чтения и записи
  FILE* input_file = fopen("input.txt", "r");
  FILE* output_file = fopen("output.txt", "w");

  if (input_file == NULL || output_file == NULL) {
    std::cerr << "Error: Unable to open one of the files!" << std::endl;
    return 1;
  }
}

 

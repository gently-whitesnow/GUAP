// fstream
#include <fstream>
#include <string>
#include <iostream>

int main() {
  std::ifstream input_file("input.txt");
  std::ofstream output_file("output.txt");

  if (!input_file.is_open() || !output_file.is_open()) {
    std::cerr << "Error: Unable to open one of the files!" << std::endl;
    return 1;
  }

  std::string line;
  while (getline(input_file, line)) {
    // Поиск цитаты в строке
    std::size_t quote_start = line.find('\"');
    if (quote_start != std::string::npos) {
      std::size_t quote_end = line.find('\"', quote_start + 1);
      if (quote_end != std::string::npos) {
        // Сохранение цитаты в выходной файл
        output_file << line.substr(quote_start, quote_end - quote_start + 1) << std::endl;
      }
    }
  }

  input_file.close();
  output_file.close();

  return 0;
}

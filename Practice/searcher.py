from collections import defaultdict
import subprocess
import re
import pickle
import time


class TrigramSearcher:
    def __init__(self):
        self.index = self.load_index("index.bin")
        self.path_index = self.load_index("path_index.bin")
        self.results_count = 10
        self.context_size = 0

    def load_index(self, fileName):
        """Загружает индекс из бинарного файла"""
        try:
            with open(fileName, "rb") as file:
                return pickle.load(file)
        except FileNotFoundError:
            print(f"Файл индекса {fileName} не найден.")
            return defaultdict(set)
        except Exception as e:
            print(f"Ошибка загрузки индекса: {e}")
            return defaultdict(set)

    def generate_trigrams(self, text):
        """Генерация триграмм из текста"""
        trigrams = []
        normalized_text = text.lower()
        for i in range(len(normalized_text) - 2):
            trigrams.append(normalized_text[i : i + 3])
        return trigrams

    def search(self, query):
        query_trigrams = self.generate_trigrams(query)
        results = defaultdict(set)
        count = 0  # Счётчик совпадений

        for trigram in query_trigrams:
            if trigram in self.index:
                for file_link in self.index[trigram]:
                    if count >= self.results_count:
                        return results
                    file_path = self.path_index[file_link]
                    print(file_path)
                    if trigram not in results[file_path]:
                        results[file_path].add(trigram)
                        count += 1
        return results

    def run(self, query):
        """Выполняет поиск всех триграмм с использованием одного вызова grep"""
        results = self.search(query)
        if results:
            print("Результаты поиска:")

            for file_path, trigrams in results.items():
                try:
                    # Формируем регулярное выражение из триграмм
                    regex = "|".join(map(re.escape, trigrams))

                    # Формируем команду grep
                    command = ["grep", "-nE"]

                    # Добавляем флаг контекста, если он больше 0
                    if self.context_size > 0:
                        command.extend(["-C", str(self.context_size)])

                    # Добавляем регулярное выражение и файл
                    command.extend([regex, file_path])

                    # Выполняем команду grep
                    result = subprocess.run(
                        command,
                        stdout=subprocess.PIPE,
                        stderr=subprocess.PIPE,
                        text=True,
                    )

                    # Проверяем вывод команды
                    if result.stdout:
                        print(f"\033[94m\nФайл: {file_path}\033[0m")
                        print(result.stdout.strip())  # Выводим результаты grep
                    elif result.stderr:
                        print(
                            f"Ошибка при обработке файла {file_path}: {result.stderr.strip()}"
                        )
                except Exception as e:
                    print(f"Ошибка при обработке файла {file_path}: {e}")

                print()
        else:
            print("Ничего не найдено.")


if __name__ == "__main__":
    import sys

    if len(sys.argv) != 2:
        print("Использование: python searcher.py <поисковый_запрос>")
    else:
        query = sys.argv[1]
        searcher = TrigramSearcher()
        searcher.run(query)

        # while True:
        #     q = input("Введите запрос: ")

        #     # Замер времени
        #     start_time = time.time()
        #     searcher.run(q)
        #     end_time = time.time()

        #     # Вывод времени выполнения
        #     print(f"Время выполнения: {end_time - start_time:.6f} секунд\n")

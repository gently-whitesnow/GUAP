import os
import json
from collections import defaultdict
import pickle


class TrigramIndexer:
    def __init__(self, index_file="index.json", ignore_file="index.ignore"):
        self.index = defaultdict(set)  # Используем set вместо list
        self.path_index = defaultdict(str)
        self.path_index_lookup = defaultdict(set)
        self.index_file = index_file
        self.ignore = self.load_ignore(ignore_file)
        self.path_counter = 0

    def load_ignore(self, ignore_file):
        """Загружает список папок, которые нужно игнорировать"""
        if os.path.exists(ignore_file):
            with open(ignore_file, "r", encoding="utf-8") as file:
                return [line.strip() for line in file if line.strip()]
        return []

    def generate_trigrams(self, text):
        """Генерация триграмм из текста"""
        trigrams = []
        normalized_text = text.lower()
        for i in range(len(normalized_text) - 2):
            trigrams.append(normalized_text[i : i + 3])
        return trigrams

    def try_index(self, file_path):
        """Индексирует содержимое файла"""
        try:
            with open(file_path, "r", encoding="utf-8") as file:
                lines = file.readlines()
                for _, line in enumerate(lines, start=1):
                    trigrams = self.generate_trigrams(line)
                    for trigram in trigrams:
                        if file_path not in self.path_index_lookup:
                            self.path_index_lookup[file_path] = self.path_counter
                            self.path_index[self.path_counter] = file_path
                            self.path_counter += 1
                        p_index = self.path_index_lookup[file_path]
                        self.index[trigram].add(p_index)
        except Exception as e:
            print(f"Ошибка при индексации файла {file_path}: {e}")

    def index_folder(self, folder_path):
        """Индексирует все текстовые файлы в указанной папке"""
        for root, dirs, files in os.walk(folder_path):
            # Фильтруем список директорий, чтобы исключить игнорируемые папки
            dirs[:] = [
                d
                for d in dirs
                if not any(ignored in os.path.join(root, d) for ignored in self.ignore)
            ]

            if any(ignored in root for ignored in self.ignore):
                print(f"⏭️📂 Не индексируем папку: {root}")
                continue

            for file in files:
                file_path = os.path.join(root, file)
                if any(ignored in file for ignored in self.ignore):
                    print(f"⏭️📝 Не индексируем файл: {file_path}")
                    continue
                print(f"✅📝 Индексация файла: {file_path}")
                self.try_index(file_path)

    def save_index(self, serializable_index, name):
        """Сохраняет индекс в JSON и бинарный файлы"""

        # Сохраняем в JSON
        with open(name, "w", encoding="utf-8") as file:
            json.dump(serializable_index, file)

        # Сохраняем в бинарный файл
        binary_file = name.replace(".json", ".bin")
        with open(binary_file, "wb") as file:
            pickle.dump(serializable_index, file)
        print(f"Индекс сохранён в {name} и {binary_file}")

    def run(self, folder_path):
        """Индексирует папку и сохраняет результат"""
        print(f"Корени индексации: {folder_path}")
        self.index_folder(folder_path)
        self.save_index(
            {key: list(value) for key, value in self.index.items()}, self.index_file
        )
        self.save_index(
            {key: value for key, value in self.path_index.items()}, "path_index.json"
        )


if __name__ == "__main__":
    import sys

    if len(sys.argv) != 2:
        print("Использование: python indexer.py <путь_к_папке>")
    else:
        print("start")
        folder = sys.argv[1]
        indexer = TrigramIndexer()
        indexer.run(folder)

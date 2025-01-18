from collections import defaultdict

class TrigramSearchEngine:
    def __init__(self):
        self.index = defaultdict(list)  # Хранилище триграмм с указанием строк
    
    def generate_trigrams(self, text):
        """Генерация триграмм из текста"""
        trigrams = []
        normalized_text = text.lower()
        for i in range(len(normalized_text) - 2):
            trigrams.append(normalized_text[i:i + 3])
        print(trigrams)
        return trigrams

    def index_text(self, text, doc_id):
        """Индексирует текст, добавляя его триграммы в индекс"""
        trigrams = self.generate_trigrams(text)
        for trigram in trigrams:
            if doc_id not in self.index[trigram]:
                self.index[trigram].append(doc_id)
    
    def search(self, query):
        """Поиск по запросу, используя триграммы"""
        query_trigrams = self.generate_trigrams(query)
        matching_docs = defaultdict(int)

        for trigram in query_trigrams:
            for doc_id in self.index.get(trigram, []):
                matching_docs[doc_id] += 1

        # Сортируем документы по количеству совпадений триграмм
        sorted_results = sorted(matching_docs.items(), key=lambda x: x[1], reverse=True)
        return sorted_results


# Пример использования
engine = TrigramSearchEngine()

# Индексация текстов
documents = {
    1: "This is a simple example of a trigram-based search engine.",
    2: "The trigram model is often used in natural language processing tasks.",
    3: "Searching using trigram indices can be very fast and efficient.",
}

for doc_id, text in documents.items():
    engine.index_text(text, doc_id)

# Поиск
query = "trigram search engine"
results = engine.search(query)

print("Search Results:")
for doc_id, score in results:
    print(f"Document {doc_id}: Score {score}")

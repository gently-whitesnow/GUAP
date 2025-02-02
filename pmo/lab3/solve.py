import numpy as np

# Матрица смежности для графа
# Значение np.inf означает, что прямой связи между вершинами нет
graph = np.array(
    [
        [0, 230, 270, np.inf, np.inf],
        [np.inf, 0, 160, 180, np.inf],
        [np.inf, np.inf, 0, 120, 200],
        [np.inf, np.inf, np.inf, 0, 170],
        [np.inf, np.inf, np.inf, np.inf, 0],
    ]
)


# Метод Дейкстры для нахождения кратчайших путей от заданной вершины
def dijkstra(graph, start):
    n = len(graph)
    visited = [False] * n
    distance = [np.inf] * n
    distance[start] = 0

    for _ in range(n):
        # Выбираем вершину с минимальной меткой
        min_dist = np.inf
        min_vertex = -1
        for i in range(n):
            if not visited[i] and distance[i] < min_dist:
                min_dist = distance[i]
                min_vertex = i

        # Помечаем вершину как посещённую
        visited[min_vertex] = True

        # Обновляем расстояния до соседей
        for neighbor in range(n):
            if graph[min_vertex][neighbor] != np.inf and not visited[neighbor]:
                new_dist = distance[min_vertex] + graph[min_vertex][neighbor]
                if new_dist < distance[neighbor]:
                    distance[neighbor] = new_dist

    return distance


# Метод Флойда-Уоршелла для нахождения кратчайших путей между всеми парами вершин
def floyd_warshall(graph):
    n = len(graph)
    dist = graph.copy()

    for k in range(n):
        for i in range(n):
            for j in range(n):
                dist[i][j] = min(dist[i][j], dist[i][k] + dist[k][j])

    return dist


# Решение методом Дейкстры от вершины 1 (индекс 0)
dijkstra_result = dijkstra(graph, 0)
print("алгоритм Дейкстры")
print(dijkstra_result)

# Решение методом Флойда-Уоршелла
floyd_warshall_result = floyd_warshall(graph)
print("алгоритм Флойда-Уоршелла")
print(floyd_warshall_result)

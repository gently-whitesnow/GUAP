#!/usr/bin/env python3
# Генетический алгоритм для задачи коммивояжёра (вариант 7 — Eil51)
# Используется представление соседей (neighbor representation), меметический подход и 2 вида кроссовера

import random, time
import numpy as np
import matplotlib.pyplot as plt

# --- Координаты 51 города из TSPLIB (вариант Eil51) ---
COORDS = np.array([
    (37, 52), (49, 49), (52, 64), (20, 26), (40, 30), (21, 47),
    (17, 63), (31, 62), (52, 33), (51, 21), (42, 41), (31, 32),
    (5, 25), (12, 42), (36, 16), (52, 41), (27, 23), (17, 33),
    (13, 13), (57, 58), (62, 42), (42, 57), (16, 57), (8, 52),
    (7, 38), (27, 68), (30, 48), (43, 67), (58, 48), (58, 27),
    (37, 69), (38, 46), (46, 10), (61, 33), (62, 63), (63, 69),
    (32, 22), (45, 35), (59, 15), (5, 6),  (10, 17), (21, 10),
    (5, 64), (30, 15), (39, 10), (32, 39), (25, 32), (25, 55),
    (48, 28), (56, 37), (30, 40)
], dtype=np.float64)

N = len(COORDS)

# строим матрицу расстояний между всеми парами городов
DIST = np.linalg.norm(COORDS[:, None] - COORDS[None, :], axis=2)
DIST = np.rint(DIST).astype(int)  # округление — как в TSPLIB, чтобы длина совпадала с эталоном

# Эталонный (оптимальный) маршрут из методички, 1-based → 0-based
OPT_TOUR = np.array([
     1,22, 8,26,31,28, 3,36,35,20, 2,29,21,16,50,34,
    30, 9,49,10,39,33,45,15,44,42,40,19,41,13,25,14,
    24,43, 7,23,48, 6,27,51,46,12,47,18, 4,17,37, 5,
    38,11,32
]) - 1

# --- Параметры ГА ---
POP_SIZE = 200
GENERATIONS = 250
P_CROSSOVER = 0.9
P_MUTATION = 0.3
CROSSOVER_TYPE = "hc"  # "ae" — alternating-edges, "hc" — heuristic crossover
SEED = 42

# --- Представления маршрута --- индекс - из города, значение в город
def perm_to_neighbour(perm):
    succ = np.empty_like(perm)                     # создаём пустой массив такого же размера
    succ[perm[:-1]] = perm[1:]                     # каждому городу ставим следующего по маршруту
    succ[perm[-1]] = perm[0]                       # замыкаем цикл: последний идёт к первому
    return succ

def neighbour_to_perm(succ, start=0):
    # Преобразует представление соседей обратно в упорядоченный список городов
    path = [start]
    while succ[path[-1]] != start:
        path.append(succ[path[-1]])
    return np.array(path)

def tour_length(succ):
    perm = neighbour_to_perm(succ)
    return DIST[perm, np.roll(perm, -1)].sum()

# --- Локальная оптимизация: 2-opt ---
def improve_local_2opt(succ, max_swaps=50):
    # Находит и применяет до max_swaps улучшений с помощью 2-opt (инверсия сегмента)

    swaps = 0
    improved = True
    while improved and swaps < max_swaps:
        improved = False
        perm = neighbour_to_perm(succ)
        for i in range(1, N - 2):
            for j in range(i + 1, N):
                if j - i == 1:
                    continue
                a, b, c, d = perm[i - 1], perm[i], perm[j], perm[(j + 1) % N]
                # если перестановка улучшает маршрут, то применяем ее
                if DIST[a, b] + DIST[c, d] > DIST[a, c] + DIST[b, d]:
                    perm[i:j+1] = perm[i:j+1][::-1]
                    succ[:] = perm_to_neighbour(perm)
                    swaps += 1
                    improved = True
                    if swaps >= max_swaps:
                        return

# Alternating Edges Crossover
# поочередно берем ребра из p1 и p2, если ребенок уже посетил город, то берем случайный город
def crossover_alternating(p1, p2):
    # берем случайный город как стартовый
    start = random.randrange(N)
    # создаем пустой массив для ребенка
    child = -np.ones(N, dtype=int)
    visited = {start} # посещенные города
    curr = start # текущий город
    use_p1 = True # используем ли p1
    while len(visited) < N:
        nxt = (p1 if use_p1 else p2)[curr]
        use_p1 = not use_p1
        if nxt in visited or nxt == start:
            nxt = random.choice([c for c in range(N) if c not in visited and c != start])
        child[curr] = nxt
        visited.add(nxt)
        curr = nxt
    child[curr] = start
    # по итогу получаем маршрут в виде соседей
    return child

# Heuristic Crossover
# из двух возможных следующих городов выбирает тот, что ближе
def crossover_heuristic(p1, p2):
    start = random.randrange(N) # берем случайный город как стартовый
    child = -np.ones(N, dtype=int)
    visited = {start}
    curr = start
    while len(visited) < N:
        c1, c2 = p1[curr], p2[curr]
        candidates = [c for c in (c1, c2) if c not in visited]
        if not candidates:
            nxt = random.choice([c for c in range(N) if c not in visited])
        elif len(candidates) == 1:
            nxt = candidates[0]
        else:
            nxt = min(candidates, key=lambda c: DIST[curr, c])
        child[curr] = nxt
        visited.add(nxt)
        curr = nxt
    child[curr] = start
    # по итогу получаем маршрут в виде соседей
    return child

# --- Генетический алгоритм ---
def genetic_algorithm():
    random.seed(SEED)
    np.random.seed(SEED)
    cross = crossover_alternating if CROSSOVER_TYPE == "ae" else crossover_heuristic

    # Инициализация случайной популяции (случайных маршрутов)
    # превращаем случайный маршруты в представление соседей
    population = [perm_to_neighbour(np.random.permutation(N)) for _ in range(POP_SIZE)]
    # fitness - оцениваем длину случайных маршрутов
    fitness = np.array([tour_length(ind) for ind in population])
    # best_curve - длина лучшего маршрута для каждой генерации
    best_curve = []

    for gen in range(GENERATIONS):
        # Элитизм — копируем лучшего в новое поколение
        best_idx = fitness.argmin()
        # копируем лучшего в новое поколение
        new_pop = [population[best_idx].copy()]

        while len(new_pop) < POP_SIZE:
            # Турнирная селекция размером 2 и выбором лучшего из пары
            a, b = random.sample(range(POP_SIZE), 2)
            p1 = population[a] if fitness[a] < fitness[b] else population[b]
            a, b = random.sample(range(POP_SIZE), 2)
            p2 = population[a] if fitness[a] < fitness[b] else population[b]

            # Кроссовер и мутация
            child = cross(p1, p2) if random.random() < P_CROSSOVER else p1.copy()
            if random.random() < P_MUTATION:
                improve_local_2opt(child, max_swaps=50)

            new_pop.append(child)

        population = new_pop
        fitness = np.array([tour_length(ind) for ind in population])
        best_curve.append(fitness.min())

    best_idx = fitness.argmin()
    return population[best_idx], best_curve

# --- Визуализация маршрута ---
def plot_tour(succ, title):
    perm = neighbour_to_perm(succ)
    xs, ys = COORDS[perm, 0], COORDS[perm, 1]
    plt.figure(figsize=(6, 5))
    plt.scatter(xs, ys)
    plt.plot(xs, ys, linewidth=1)
    plt.plot([xs[-1], xs[0]], [ys[-1], ys[0]], linewidth=1)
    for i, (x, y) in enumerate(zip(xs, ys)):
        plt.text(x, y, str(perm[i]+1), fontsize=8)
    plt.title(title)
    plt.axis("equal")
    plt.tight_layout()
    plt.show()

# --- Главная функция ---
def main():
    t0 = time.time()
    best, curve = genetic_algorithm()
    elapsed = time.time() - t0

    best_len = tour_length(best)
    opt_len = tour_length(perm_to_neighbour(OPT_TOUR))
    gap = (best_len - opt_len) / opt_len * 100

    print(f"Best length: {best_len:.2f}\nOptimal length: {opt_len:.2f}\nGap: {gap:.2f}%\nTime: {elapsed:.1f} s")
    plot_tour(best, title=f"Best tour – {best_len:.1f} (gap {gap:.2f}%)")

    # Кривая сходимости
    plt.figure()
    plt.plot(curve)
    plt.xlabel("Generation")
    plt.ylabel("Best length")
    plt.title("Convergence curve")
    plt.grid(True)
    plt.tight_layout()
    plt.show()

if __name__ == "__main__":
    main()
# Простой генетический алгоритм для варианта 7
#
# f(x) = ln(x) * cos(3x – 15), x ∈ [1, 10] — ищем максимум.
#
# В программе:
# 1. GA с двоичным кодированием (15 бит → ≈ 3·10⁻⁴ точность).
# 2. Визуализация функции и найденного максимума — function_plot.png.
# 3. График сходимости алгоритма — convergence_plot.png.
# 4. Исследование влияния N, Pc, Pm; результаты → интерактивная таблица.


import math, random, time, os, imageio, numpy as np, pandas as pd
import matplotlib.pyplot as plt

def f(x: float) -> float:
    """Целевая функция для оптимизации.
    
    Args:
        x: Аргумент функции, x ∈ [1, 10]
    
    Returns:
        Значение функции f(x) = ln(x) * cos(3x - 15)
    """
    return math.log(x) * math.cos(3 * x - 15)

# 15 бит это от 0 до 32767
# что при 1 до 10 дает точность 0.0003
BITS = 15
MAX_INT = (1 << BITS) - 1
X_MIN, X_MAX = 1.0, 10.0

# нормализация 0-10 в 0-32767
def decode(chrom: int) -> float:
    """Преобразует бинарную хромосому в вещественное число.
    
    Args:
        chrom: Целое число от 0 до MAX_INT (бинарная хромосома)
    
    Returns:
        Вещественное число из интервала [X_MIN, X_MAX]
    """
    return X_MIN + (chrom / MAX_INT) * (X_MAX - X_MIN)

# нормализация 0-32767 в 0-10
def encode(x: float) -> int:
    """Преобразует вещественное число в бинарную хромосому.
    
    Args:
        x: Вещественное число из интервала [X_MIN, X_MAX]
    
    Returns:
        Целое число от 0 до MAX_INT (бинарная хромосома)
    """
    return int((x - X_MIN) / (X_MAX - X_MIN) * MAX_INT)

# мутация, где chrom - хромосома, pm - вероятность мутации
# проходим по всем битам и если случайное число меньше вероятности мутации, то меняем бит
def mutate(chrom: int, pm: float) -> int:
    """Применяет мутацию к хромосоме.
    
    Args:
        chrom: Исходная хромосома
        pm: Вероятность мутации каждого бита
    
    Returns:
        Мутированная хромосома
    """
    for bit in range(BITS):
        if random.random() < pm:
            # ^ - XOR, меняет бит на противоположный
            chrom ^= (1 << bit)
    return chrom

def crossover(p1: int, p2: int, Pc: float) -> tuple[int, int]:
    """Выполняет операцию кроссовера между двумя родителями.
    
    Args:
        p1: Первый родитель (хромосома)
        p2: Второй родитель (хромосома)
        Pc: Вероятность кроссовера (если меньше, то дети копия родителей)
    
    Returns:
        tuple[int, int]: Два потомка (хромосомы)
    """
    if random.random() < Pc:
        # выбираем точку скрещивания
        point = random.randint(1, BITS - 1)
        # создаем маску для скрещивания
        mask = (1 << point) - 1
        # создаем потомков
        c1 = (p1 & ~mask) | (p2 & mask)
        c2 = (p2 & ~mask) | (p1 & mask)
        return c1, c2
    return p1, p2

def genetic_algorithm(pop_size=50, Pc=0.8, Pm=0.01, max_gen=50, return_history=False, seed=0):
    """Основная функция генетического алгоритма.
    
    Args:
        pop_size: Размер популяции
        Pc: Вероятность кроссовера
        Pm: Вероятность мутации
        max_gen: Максимальное число поколений
        return_history: Возвращать ли историю эволюции
        seed: Seed для генератора случайных чисел
    
    Returns:
        Если return_history=False:
            (best_x, best_f) - лучшая точка и её значение
        Иначе:
            (best_x, best_f, history) - лучшая точка, значение и история эволюции
    """
    random.seed(seed)
    pop = [random.randint(0, MAX_INT) for _ in range(pop_size)]
    history = []

    for gen in range(max_gen):
        # декодируем популяцию
        xs = [decode(c) for c in pop]
        # считаем fitness
        fs = [f(x) for x in xs]
        # находим лучшего
        best_i = int(np.argmax(fs))
        # добавляем в историю
        history.append((gen, xs[best_i], fs[best_i]))

        # нормализация fitness
        f_min = min(fs)
        fitness = [val - f_min + 1e-12 for val in fs]
        # выбираем родителей
        probs = [val / sum(fitness) for val in fitness]
        mating_pool = random.choices(pop, probs, k=pop_size)
        # кроссовер
        next_pop = []
        for i in range(0, pop_size, 2):
            p1, p2 = mating_pool[i], mating_pool[(i + 1) % pop_size]
            c1, c2 = crossover(p1, p2, Pc)
            next_pop.extend([c1, c2])
        next_pop = next_pop[:pop_size]
        # мутация
        pop = [mutate(c, Pm) for c in next_pop]

    xs = [decode(c) for c in pop]
    fs = [f(x) for x in xs]
    best_i = int(np.argmax(fs))
    best_x, best_f = xs[best_i], fs[best_i]
    return (best_x, best_f, history) if return_history else (best_x, best_f)

# исследование влияния параметров
def result(param_name, values, base, true_best_f):
    """Исследует влияние параметра на работу алгоритма.
    
    Args:
        param_name: Имя исследуемого параметра ('pop_size', 'Pc' или 'Pm')
        values: Список значений параметра для исследования
        base: Базовые значения остальных параметров
        true_best_f: Истинное максимальное значение функции
    
    Returns:
        Список словарей с результатами для каждого значения параметра
    """
    rows = []
    for val in values:
        kwargs = base.copy(); kwargs[param_name] = val
        start = time.perf_counter()
        bx, bf = genetic_algorithm(**kwargs, seed=val)
        t = time.perf_counter() - start
        rows.append({
            "Параметр": param_name,
            "Значение": val,
            "Популяция": kwargs["pop_size"],
            "Pc": kwargs["Pc"],
            "Pm": kwargs["Pm"],
            "Лучшее x": round(bx, 4),
            "Лучшее f(x)": round(bf, 6),
            "Δf": round(true_best_f - bf, 6),
            "Время, с": round(t, 4)
        })
    return rows

if __name__ == "__main__":
    # Базовый запуск
    POP, PC, PM, GENS = 50, 0.8, 0.01, 30
    best_x, best_f, trace = genetic_algorithm(POP, PC, PM, GENS, return_history=True, seed=42)
    print(f"Best x: {best_x:.5f}, Best f(x): {best_f:.6f}")

    # График функции и лучшей точки
    x_grid = np.linspace(X_MIN, X_MAX, 1500)
    y_grid = np.log(x_grid) * np.cos(3 * x_grid - 15)
    
    plt.figure(figsize=(10, 6))
    plt.plot(x_grid, y_grid, label='f(x)')
    plt.scatter([best_x], [best_f], color='red', label='Лучшая точка')
    plt.title("Функция и найденный максимум")
    plt.xlabel("x")
    plt.ylabel("f(x)")
    plt.legend()
    plt.grid(True)
    plt.savefig("function_plot.png", bbox_inches="tight")
    plt.close()

    # График сходимости
    gen_nums = [g for g, _, _ in trace]
    best_vals = [fit for _, _, fit in trace]
    plt.figure(figsize=(10, 6))
    plt.plot(gen_nums, best_vals)
    plt.xlabel("Поколение")
    plt.ylabel("Лучшее f(x)")
    plt.title("Сходимость ГА")
    plt.grid(True)
    plt.savefig("convergence_plot.png", bbox_inches="tight")
    plt.close()

    # График функции с экстремумами всех поколений
    plt.figure(figsize=(12, 6))
    plt.plot(x_grid, y_grid, label='f(x)', color='gray')
    plt.title("Экстремумы каждого поколения на функции f(x)")
    plt.xlabel("x")
    plt.ylabel("f(x)")

    # Рисуем экстремумы (лучшие точки) из каждого поколения
    gen_xs = [x for _, x, _ in trace]
    gen_fs = [fx for _, _, fx in trace]
    plt.scatter(gen_xs, gen_fs, c=range(len(gen_xs)), cmap='viridis', label='Лучшие особи', s=50)

    # Подпись к последней точке
    plt.scatter([gen_xs[-1]], [gen_fs[-1]], color='red', label='Финальный максимум', zorder=10)

    plt.legend()
    plt.grid(True)
    plt.savefig("generation_extrema.png", bbox_inches="tight")
    plt.close()
    # Истинное максимальное значение функции
    TRUE_BEST_F = max(np.log(x_grid) * np.cos(3 * x_grid - 15))

    # Исследование влияния параметров
    base_cfg = dict(pop_size=50, Pc=0.8, Pm=0.01, max_gen=50)
    results = (result("pop_size", [20, 50, 100], base_cfg, TRUE_BEST_F) +
               result("Pc", [0.6, 0.8, 0.9], base_cfg, TRUE_BEST_F) +
               result("Pm", [0.001, 0.01, 0.05], base_cfg, TRUE_BEST_F))

    df = pd.DataFrame(results)
    print("\nИсследование параметров ГА:")
    print(df.to_string(index=False))

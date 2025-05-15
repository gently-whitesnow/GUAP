"""
ЛР-4, вариант 7
────────────────────────────────────────────
• Эволюционный алгоритм: генетическое программирование (GP)
• Кодирование решения: деревья арифм. выражений
• Целевая функция (fitness): RMS-ошибка
• Операторы:   – кроссовер поддеревьев
               – мутация «растущая/усекающая» + гаусс-сдвиг констант
               – ранговая селекция + 1 элитная особь
• Модель, которую хотим найти:  Ef = a · L^b   (ядро COCOMO)
────────────────────────────────────────────
"""

import random, math, numpy as np
from copy import deepcopy
import matplotlib.pyplot as plt

random.seed(42)                   # фиксируем сид → одинаковый результат

# ────────────────────────────── данные ──────────────────────────────
# 13 проектов идут в обучение, 5 ‒ в “тест”.
projects = [
    (90.2,115.8),(46.2,96.0),(46.5,79.0),(54.5,909.8),(31.1,39.6),
    (67.5,98.4),(12.8,18.9),(10.5,10.3),(21.5,28.5),(3.1,7.0),
    (4.2,9.0),(7.8,7.3),(2.1,5.0),                  # train
    (5.0,8.4),(78.6,98.7),(9.7,15.6),(12.5,23.9),(100.8,138.3)  # test
]
train, test = projects[:13], projects[13:]

# ────────────────────────── “безопасная арифметика” ──────────────────────────
MAX_ABS = 1e6        # любое промежуточное |значение| режем этим потолком

def safe_val(v: float) -> float:
    """На всякий случай убираем nan/inf и слишком большие числа."""
    return v if np.isfinite(v) and abs(v) < MAX_ABS else MAX_ABS

def safe_pow(a: float, b: float) -> float:
    """Безопасное возведение в степень (не уходим в комплекс или overflow)."""
    if a < 0 and not float(b).is_integer():                # (-5)**1.3 = complex
        return 1.0
    if abs(a) > 1e3 or abs(b) > 10:                       # слишком крутые числа
        return 1.0
    try:
        return safe_val(a ** b)
    except Exception:
        return 1.0

# ─────────────────────── генерация / вычисление деревьев ──────────────────────
OPS = ['*', '+']             # разрешённые бинарные операции в “верхушке”

def rnd_const(lo: float = 0.1, hi: float = 10.0) -> float:
    """Случайная вещественная константа ~ U(lo, hi)."""
    return round(random.uniform(lo, hi), 4)

def make_cocomo_node(a=None, b=None):
    """
    Узел-ядро:  a * x ** b
    (если a, b не заданы, берутся случайные).
    """
    return ['*',
            a if a is not None else rnd_const(),
            ['**', 'x', b if b is not None else rnd_const(0, 3)]]

def random_expr(depth: int = 4):
    """Случайное дерево (depth≈4 достаточно для варианта)."""
    if depth == 0 or random.random() < 0.3:
        return make_cocomo_node()
    op = random.choice(OPS)                     # '+' или '*'
    return [op, random_expr(depth - 1), random_expr(depth - 1)]

def evaluate(expr, x: float) -> float:
    """Вычисляем значение дерева для L = x."""
    try:
        # 1. листья
        if isinstance(expr, (int, float)):
            return expr
        if expr == 'x':
            return x
        if isinstance(expr, str):               # строка-константа
            return float(expr)

        # 2. внутренний узел
        op, left, right = expr
        a, b = evaluate(left, x), evaluate(right, x)

        if op == '*':
            res = a * b
        elif op == '+':
            res = a + b
        elif op == '**':
            res = safe_pow(a, b)
        else:
            res = 1.0
        return safe_val(res)
    except Exception:
        return 1.0

# ────────────────────────────── фитнесс ─────────────────────────────
def fitness_rms(expr, data) -> float:
    """Среднеквадратичная ошибка (RMS) на data."""
    return math.sqrt(
        sum((evaluate(expr, L) - Ef) ** 2 for L, Ef in data) / len(data)
    )

# ────────────────────────────── GP-операторы ─────────────────────────────
def cx_subtree(p1, p2):
    """
    Кроссовер поддеревьев:
    выбираем случайный узел и меняем поддеревья между двумя родителями.
    """
    if not isinstance(p1, list) or not isinstance(p2, list):
        return deepcopy(p2), deepcopy(p1)       # один из родителей – лист
    if random.random() < 0.3:                   # точка среза
        return deepcopy(p2), deepcopy(p1)
    op1, l1, r1 = p1
    op2, l2, r2 = p2
    return [op1, cx_subtree(l1, l2)[0], cx_subtree(r1, r2)[0]], \
           [op2, cx_subtree(l2, l1)[1], cx_subtree(r2, r1)[1]]

def mut_grow_shrink(expr, depth=2):
    """Растим/усекаем случайное поддерево."""
    if isinstance(expr, list) and random.random() < 0.3:
        return random_expr(depth)
    if isinstance(expr, list):
        return [expr[0],
                mut_grow_shrink(expr[1], depth-1),
                mut_grow_shrink(expr[2], depth-1)]
    return expr

def mut_consts(expr, sigma=0.8):
    """Гаусс-мутация числовых констант (× случайный коэффициент)."""
    if isinstance(expr, (int, float)):
        return round(expr + random.gauss(0, sigma), 4)
    if isinstance(expr, list):
        return [expr[0], mut_consts(expr[1], sigma), mut_consts(expr[2], sigma)]
    return expr

def rank_selection(pop, scores, k):
    """Берём k лучших по возрастанию фитнеса (ранговый отбор)."""
    ranked = [ind for _, ind in sorted(zip(scores, pop), key=lambda t: t[0])]
    return ranked[:k]

# ──────────────────────── стартовая популяция ────────────────────────
# Сделаем «умный» seed: найдём a0, b0 через лог-регрессию.
logs = [(math.log(L), math.log(Ef)) for L, Ef in train]
b0 = sum(y*x for x, y in logs) / sum(x*x for x, _ in logs)
a0 = math.exp(sum(y - b0*x for x, y in logs) / len(logs))

def seeded_cocomo():
    """Мутированный клон формулы a0*L**b0 (даёт адекватную отправную точку)."""
    return ['*',
            round(max(0.1, random.gauss(a0, 3)), 4),
            ['**', 'x', round(random.gauss(b0, 1), 4)]]

def init_population(pop_size):
    """60 % — seed-формы, 40 % — случайные деревья."""
    return [seeded_cocomo() if i < 0.6 * pop_size else random_expr()
            for i in range(pop_size)]

# ─────────────────────────── GP-цикл ────────────────────────────
def gp_run(pop_size=300, generations=150, cx_pb=0.8, mut_pb=0.75):
    population = init_population(pop_size)
    best_hist, best_ind = [], None

    for g in range(generations):
        # 1) оценка популяции
        scores = [fitness_rms(ind, train) for ind in population]
        elite_idx = int(np.argmin(scores))
        best_hist.append(scores[elite_idx])     # лог для графика
        best_ind = population[elite_idx]

        # 2) отбор & элитизм
        selected = rank_selection(population, scores, k=int(pop_size * 0.4))
        new_pop = [deepcopy(best_ind)]          # оставляем лучшего

        # 3) вариации → заполняем новую популяцию
        while len(new_pop) < pop_size:
            p1, p2 = random.sample(selected, 2)
            c1, c2 = deepcopy(p1), deepcopy(p2)

            if random.random() < cx_pb:
                c1, c2 = cx_subtree(p1, p2)

            if random.random() < mut_pb:
                c1 = mut_grow_shrink(c1)
                if random.random() < 0.7:       # шанс “покрутить” числа
                    c1 = mut_consts(c1)

            new_pop.extend([c1, c2])

        population = new_pop[:pop_size]

    return best_ind, best_hist

# ───────────────────────── запуск ─────────────────────────
best_expr, hist = gp_run()

def predict(expr, data):
    """Считаем прогнозы для набора точек."""
    return [evaluate(expr, x) for x, _ in data]

train_pred, test_pred = predict(best_expr, train), predict(best_expr, test)
train_true, test_true = [y for _, y in train], [y for _, y in test]

def rms(y, yh):
    return math.sqrt(sum((a - b) ** 2 for a, b in zip(y, yh)) / len(y))

print("Best tree:", best_expr)
print(f"Train RMS: {rms(train_true, train_pred):.4f}")
print(f"Test  RMS: {rms(test_true,  test_pred ): .4f}")

# ───────────────────────── графики ─────────────────────────
plt.figure(figsize=(5, 3))
plt.plot(hist)
plt.title("RMS per Generation"); plt.xlabel("Generation"); plt.ylabel("RMS")
plt.grid(); plt.tight_layout()

plt.figure(figsize=(6, 3))
idx_t = range(len(train_true))
idx_v = range(len(train_true), len(train_true) + len(test_true))
plt.plot(idx_t, train_true, 'bo-', label='Train true')
plt.plot(idx_t, train_pred, 'rx--', label='Train pred')
plt.plot(idx_v, test_true,  'go-', label='Test true')
plt.plot(idx_v, test_pred,  'kx--', label='Test pred')
plt.title("Ef: predicted vs true"); plt.legend(); plt.grid(); plt.tight_layout()
plt.show()
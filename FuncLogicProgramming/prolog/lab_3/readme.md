# лаба 3 пролог

## установка

brew install swi-prolog

## использование

swipl --help

## запуск с предзагруженным файлом

swipl -f solve.pl

4. Разработать функцию, находящую теоретико-множественное пересечение
   двух списков.
   Например:
   Вход: (1 2 3 4 5), (4 5 6 7).
   Выход: (4 5).

```
?- intersection([1, 2, 3, 4, 5, 6, 7, 8], [4, 5, 6, 7], Result).
```

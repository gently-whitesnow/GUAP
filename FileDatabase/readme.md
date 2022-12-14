https://pro.guap.ru/get-task/b0b6466e0d432d37933ecaa5733c4af0
## Задача

Задачей курсового проекта является разработка программы для заданной
предметной области, которая позволяет вводить информацию, хранить еѐ в
файле, осуществлять поиск, модификацию, сортировку и удаление данных

### 8 Вариант

Предметная область – «Расписание поездов». Данные о маршруте
поезда хранятся в структуре TRAIN, содержащей следующие поля:
 название пункта назначения;
 номер поезда;
 время отправления.
Задание на поиск: найти поезд, номер которого введѐн с клавиатуры.

#### Обратить внимание

При реализации программы необходимо сделать отдельные функции добавления,
редактирования, удаления, поиска, сортировки, сохранения, считывания из
файла и т.д. 

Вводимые данные должны храниться в списке.

При сортировке элементов в списке необходимо менять не содержимое элементов (оно остается по
прежнему адресу в памяти), а указатель на голову списка и указатели на
следующий элемент. Если в программе использовались операторы
динамического выделения памяти, то следует предпринять меры по
обнаружению возможных утечек памяти.

Для сохранения базы данных в самом простом случае можно
использовать бинарный или текстовый файл. По желанию студентов могут
использоваться и другие форматы хранения данных.

При вводе данных в консольном приложении (в форме) необходимо
выполнять проверки вводимых значений:
+ проверка числового значения;
+ проверка ввода строки в соответствии с заданным форматом (например,
  формат № группы был определен: вначале 4 цифры, 1 символ в конце - группа
  4445К)
+ корректный формат даты;
+ вводимые строки не должны превышать заданную длину;
+ другие проверки в соответствии с выбранными типами данных.

## Реализация
```
| Id    | TrainNumber     | PointName            | DepartureTime        | 🔼 - Up, 🔽 - Down
| 11112 | N321            | Pereburg             | 14.12.2000 00:00     |
| 11114 | N324            | Tula                 | 14.11.1999 00:00     |
| 11115 | N325            | Koselsk              | 14.12.1998 00:00     |
| 11116 | N326            | Krim                 | 21.12.2000 00:00     |
| 11117 | N322            | Moscow               | 14.12.1999 00:00     |
| 11118 | N321            | Peter                | 14.12.2000 00:00     |
| 11119 | N323            | Kaluga               | 15.12.1999 00:00     |
| 11120 | N324            | Tula                 | 14.11.1999 00:00     |
.........................................................................

For SORT press: 'O'
For DELETE press: 'D'
For FILTER press: 'F'
For CHANGE press: 'C'
For ADD press: 'A'
For SAVE press: 'S'

Press 'R' to reset
Press 'Q' to leave
```



# download app
https://www.dosbox.com/

# configure
https://www.youtube.com/watch?v=9EMs3x-LJvs

# developing

1) launch dosbox
2) commands:
- dir

1) трансляция (компилирование)
- tasm any.asm 
Или одной строкой с доп инфой
tasm /z/zi/n hello.asm hello.obj hello.lst
tasm /z/zi/n hello hello hello

/z - ключ, разрешающий вывод на экран строк исходного текста программы, в которых Ассемблер обнаружил ошибки;
/zi - ключ, управляющий включением в результирующий файл полных сведений о номерах строк и именах исходного модуля;
/n - ключ, который исключает из листинга информацию о симво- лических обозначениях впрограмме.

2) компановка (флаг /v передает в загрузочный файл информацию, используе- мую при отладке программ)
- tlink/v ANY.OBJ
Получаем загрузочный файл exe
файл карт сборки map

3) запуск
- ANY.EXE

### Непечатные символы
пробел (20h), пере- вод строки (0Ah), возврат каретки (0Dh) и табуляция (09h).

### Командные операторы 
или просто команды имеют следую- щий формат:
[метка:] [префикс] мнемоника [операнд(ы)| [;комментарий],

метка - определяемое пользователем имя команды, заканчиваю- щееся двоеточием. Значением метки является адрес отмеченной ко- манды. Используются для организации команд передачи управления. Метка состоит из последовательности символов или цифр, однако всегда начинается ссимволов английского алфавита или ссимволов
@_?;
префикс - элемент, который служит для изменения стандартного
действия команды;
мнемоника - мнемоническое обозначение команды, которое представляет собой ключевые слова Ассемблера и идентифицирует выполняемую командой операцию. Обычно используются сокращен- ные английские слова, передающие смысл команды;
операнд(ы) - объекты, которые участвуют в указанной операции. Это могут быть адреса данных или непосредственно сами данные, не- обходимые для выполнения команды. Команды могут быть двух, одно и безоперандными. Если операндов два, то они разделяются запятой; комментарий - начинается с точки с запятой и предназначен для пояснения к программе. Может выполняться на русском языке, так как не влияет на выполнение программы.
Метка, префикс, мнемоника и операнд(ы) разделяются по край- ней мере одним пробелом друг от друга.


; команды
; mov destination source - используется для копирования 
; int - ввод вывод
; add sub (арифметические)
; and or xor  - логические 
; управление потоком - jmp call ret

ADD dst, srс; - сложение

SUB dst, srс; - вычитание целых чисел

MUL srс - умножение чисел без знака
Для слов второй операнд должен находится в регистре AX
Для байт в регистре AL
После выполнения операции с однобайтовыми числами результат записывается в 16битовый AX
Для двухбайтовых чисел результат попадает в (в DX - старшая часть, в АХ - младшая).

IMUL src. - умножение чисел со знаком

DIV src
IDIV - команды деления (знаковый и беззнаковый)
AL := quot ((AX)/(src)); частное при делении на байт.
АН := rem ((AX)/(src)); остаток при делении на байт. 
AX := quot ((DX:AX)/(src)); частное при делении на слово
DX := rem ((DX:AX)/(src)); остаток при делении на слово.

CBW- преобразовать байт в AL в слово в АХ.
CWD - преобразовать слово в АХ в двойное слово в DX:AX. При этом регистр DX заполняется значением старшего разряда регист- pa AX:

### Регистры

AX - акумулятор (регистр общего назначения) -часто для временного хранения данных и выполнения арифм операций (16 бит)
CX - счетный регистр
BX - базовый регистр
DX - регистр данных
Выбор зависит от конкретного контекста программы
AH - старший байт регистра AX (8 бит) (использовался для указания какую функцию вызвать)
AL - младший байт регистра AX (8 бит) (использовался для передачи дополнительных параметров)

### Типы данных
DB (Define Byte) (определить байт),
 DW (Define Word) (определить слово),
  D (Define Doubleword) (определить двойное слово),
   реже DQ (Define Quadword) (определить четыре слова) и
    DT (Define Tenbyte) 

### Функции

09h - вывод строки на экран
01h - вывод символа с клавиатуры без эха
02h - вывод символа на экран

4Ch - завершение программы

### прерывание
int 21h - передача процессором управления операционной системе 
Обработчик анализирует что лежит в AH и выполняет это


### системы счисления
(Binary - b),
 шестнадцатеричной (Hexadecimal - h), 
 восьме- ричной (Octal - q) или десятичной (Decimal - d) системах счисления.

### Debug -r -u -q

-p -шаг
-d ds:0
-r

### lea
Load Effective Address загружает в регистр эффективный адрес операнда
Можно сказать загрузка ссылки на ресурс, а не копирование самого ресурса
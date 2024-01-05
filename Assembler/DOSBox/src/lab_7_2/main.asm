; CHANGE_1
; Вариант 8 , сделать заглавные буквы строчными
; заменить Y на y и Z на z 
; Так как это противоречит логике, заменим y на Y и z на Z
.MODEL SMALL
.STACK 256
.DATA
MYTEXT DB 'Ourz Native z Town with Happy young People', 13, 10, '$' ;объявляем текстовую переменную
; 13 - возврат каретки(\r), 10 - перевод строки(\n), $ - конец строки
strlen = ($ - MYTEXT) - 3 ; длина строки MYTEXT, где - 3 вычитание символов (13, 10, '$')

.CODE
;основная программа
Start:
    MOV AX, @DATA
    MOV DS, AX ; Инициализация Data

    LEA DX, MYTEXT ; вывод исходной строки в консоль
    MOV AH, 09h ; 
    INT 21h ;

    XOR AX, AX ; очищаем регистр AX
    LEA BX, MYTEXT ; загружаем адрес строки в регистр BX
    MOV CX, strlen ; инициализируем счетчик CorrectHandler

CorrectHandler: ; функция вызывающая процедуру коррекции, если символ находится в промещутке между (A-Z)
    MOV AH, [BX] ; загрузка значения находящегося по адресу в регистр AH
        
    ; логика замены y на Y и z на Z
    CMP AH, 79h ; сравнение значения с 'y' из ASCII таблицы
    JE ToUpperHandler ; Если значение равно 'y', то преобразовываем к заглавному аналогу
    CMP AH, 7Ah ; сравнение значения с 'z' из ASCII таблицы
    JE ToUpperHandler ; Если значение равно 'z', то преобразовываем к заглавному аналогу

    ; логика перевода в строчные буквы
    CMP AH, 41h ; сравнение значения с 'A' из ASCII таблицы
    JB MoveCaretCounter ; Если значение меньше 'A', то увеличиваем счетчик каретки
    CMP AH, 5Ah ; сравнение значения с 'Z' из ASCII таблицы
    JA MoveCaretCounter ; Если значение больше 'Z', то увеличиваем счетчик каретки
    CALL ToLowerCase ; Попали в диапазон A-Z поэтому преобразовываем букву
    JMP MoveCaretCounter

ToUpperHandler:
    CALL ToUpperCase
    JMP MoveCaretCounter

MoveCaretCounter: ; функция инкремента каретки данных
    INC BX
    
    LOOP CorrectHandler ; уменьшает счетчик цикла CX и выполняет операцию, если CX = 0, то выходит из цикла
    
    LEA DX, MYTEXT ; вывод результирующей строки в консоль
    MOV AH, 09h ; 
    INT 21h ;

    MOV AX, 4C00h ; завершение программы
    INT 21h ;

; выставляет в единицу 6 бит регистра AH, что превращает 
; заглавную букву в строчную
ToLowerCase PROC NEAR 
        OR AH, 20h ; 20h - 0 0 1 0 0 0 0 0
        MOV [BX], AH ; записываем полученное значение 
    RET
ToLowerCase ENDP

; выставляет в ноль 6 бит регистра AH, что превращает 
; строчную букву в заглавную
ToUpperCase PROC NEAR 
        AND AH, 0DFh ; 0DFh - 1 1 0 1 1 1 1 1
        MOV [BX], AH ; записываем полученное значение 
    RET
ToUpperCase ENDP

END Start

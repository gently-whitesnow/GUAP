; Лабораторная работа 4 Вариант - 8
; Найти и вывести X
; X = (A * B - C * D)^2
; Когда
; A = 0Fh = 15d
; B = 14d
; C = 10h = 16d
; D = 4d

Data SEGMENT
    A DB 0Fh 
    B DB 14 
    C DB 10h
    D DB 4
Data Ends

Ourstack SEGMENT Stack
    DB 100h DUP (?)
Ourstack Ends

ASSUME CS:Code, DS:Data, SS:Ourstack

Code SEGMENT
Start: 
    mov AX, Data
    mov DS, AX ; загрузили сегмент с данными

   ;Вычисление 'E' = A * B
    xor AX, AX ; очищяем регистр AX
    mov AL, A ; записываем в младший байт AL регистра AX значение A
    mul B       ; AX = AL * B = 'E'
    push AX ; запоминаем результат 'E'
    
    ;Вычисление 'F' = С * D
    xor AX, AX ; очищяем регистр AX
    mov AL, C ; записываем в младший байт AL регистра AX значение C
    mul D       ; AX = AL * D = 'F'

    ;Вычисление 'G' = 'E' - 'F'
    pop BX ; достаем результат первого вычисления
    SUB BX, AX; ; BX = BX - AX = 'G'

    ;Вычисление G^2
    xor AX, AX ; очищяем регистр AX
    mov AX, BX ; записываем в регистр как операнд тоже самое значение, 
               ;но с предыдущей операции (так как работаем со словом, то записываем в AX)
    MUL BX ; AX = AX * BX (так как значения одинаковы, то получаем квадрат числа) 
           ; На выходе имеем запись в DX(cтарший байт) и в AX(младший байт) 

    mov Ah, 4Ch
    int 21h ; прерывание

Code Ends 
END Start

; Ответ 21316d или 5344h
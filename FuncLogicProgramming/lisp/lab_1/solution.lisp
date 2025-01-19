; Вариант 4
(defun intersect (list1 list2)
  (cond
    ((or (null list1) (null list2)) nil)
    ((is-in-list (car list1) list2)
     (cons (car list1) (intersect (cdr list1) list2)))
    (t (intersect (cdr list1) list2))))

(defun is-in-list (element list)
  (cond
    ((null list) nil) 
    ((equal element (car list)) t)
    (t (is-in-list element (cdr list)))))

; Запуск функции с примером
(intersect '(1 2 3 4 5) '(4 5 6 7))

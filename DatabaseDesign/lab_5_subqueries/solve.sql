-- Процедуры с наименьшей стоимостью среди всех процедур, которые проводил врач Иванов Иван Иванович
SELECT p.id, p.name, p.price
FROM procedures p
         JOIN procedure_history ph ON p.id = ph.procedure_id
         JOIN visits v ON ph.visit_id = v.id
         JOIN doctors d ON v.doctor_id = d.id
WHERE d.name = 'Иван' AND d.surname = 'Иванов' AND d.patronymic = 'Иванович'
  AND p.price <= ALL (
    SELECT p2.price
    FROM procedures p2
             JOIN procedure_history ph2 ON p2.id = ph2.procedure_id
             JOIN visits v2 ON ph2.visit_id = v2.id
             JOIN doctors d2 ON v2.doctor_id = d2.id
    WHERE d2.name = 'Иван' AND d2.surname = 'Иванов' AND d2.patronymic = 'Иванович'
);

-- MIN
SELECT p.id, p.name, p.price
FROM procedures p
         JOIN procedure_history ph ON p.id = ph.procedure_id
         JOIN visits v ON ph.visit_id = v.id
         JOIN doctors d ON v.doctor_id = d.id
WHERE d.name = 'Иван' AND d.surname = 'Иванов' AND d.patronymic = 'Иванович'
  AND p.price = (
    SELECT MIN(p2.price)
    FROM procedures p2
             JOIN procedure_history ph2 ON p2.id = ph2.procedure_id
             JOIN visits v2 ON ph2.visit_id = v2.id
             JOIN doctors d2 ON v2.doctor_id = d2.id
    WHERE d2.name = 'Иван' AND d2.surname = 'Иванов' AND d2.patronymic = 'Иванович'
);


-- Кабинет, в котором проводилось меньше среднего приемов
SELECT c.room
FROM cabinets c
         JOIN visits v ON c.id = v.cabinet_id
GROUP BY c.room
HAVING COUNT(v.id) < (
    SELECT AVG(sub.visit_count)
    FROM (SELECT COUNT(v2.id) AS visit_count FROM visits v2 GROUP BY v2.cabinet_id) AS sub
);

SELECT c.room
FROM cabinets as c
         LEFT JOIN visits v ON c.id = v.cabinet_id
GROUP BY c.room
HAVING COUNT(v.id) < (
    SELECT AVG(sub.visit_count)
    FROM (
             SELECT COUNT(v2.id) AS visit_count
             FROM cabinets c2
                      LEFT JOIN visits v2 ON c2.id = v2.cabinet_id
             GROUP BY c2.id
         ) AS sub
);



-- Врач, проводивший все процедуры
SELECT d.name, d.surname, d.patronymic
FROM doctors d
WHERE NOT EXISTS (
    SELECT 1
    FROM procedures p
    WHERE NOT EXISTS (
        SELECT 1
        FROM procedure_history ph
                 JOIN visits v ON ph.visit_id = v.id
        WHERE ph.procedure_id = p.id AND v.doctor_id = d.id
    )
);
-- С использованием агрегатной функции
SELECT d.name, d.surname, d.patronymic
FROM doctors d
         JOIN visits v ON d.id = v.doctor_id
         JOIN procedure_history ph ON v.id = ph.visit_id
GROUP BY d.id, d.name, d.surname, d.patronymic
HAVING COUNT(DISTINCT ph.procedure_id) = (SELECT COUNT(*) FROM procedures);


-- Врач, принимавший Иванова Петра, но не проводивший процедур
SELECT d.id, d.name, d.surname, d.patronymic
FROM visits v
JOIN patients p ON v.patient_id = p.id
JOIN doctors d ON v.doctor_id = d.id
WHERE p.name = 'Петр' AND p.surname = 'Иванов'
AND v.doctor_id NOT IN (
SELECT v.doctor_id FROM visits v
                                 JOIN procedure_history ph ON v.id = ph.visit_id);

-- except

SELECT d.id, d.name, d.surname, d.patronymic
FROM visits v
         JOIN patients p ON v.patient_id = p.id
         JOIN doctors d ON v.doctor_id = d.id
WHERE p.name = 'Петр' AND p.surname = 'Иванов'

EXCEPT

SELECT d2.id, d2.name, d2.surname, d2.patronymic FROM visits v2
                                                          JOIN procedure_history ph2 ON v2.id = ph2.visit_id
                                                          JOIN doctors d2 ON v2.doctor_id = d2.id;


-- leftjoin
SELECT d.id, d.name, d.surname, d.patronymic
FROM visits v
         JOIN patients p ON v.patient_id = p.id
         JOIN doctors d ON v.doctor_id = d.id
LEFT JOIN visits v2 on v2.doctor_id = d.id
                                JOIN procedure_history ph ON v.id = ph.visit_id
WHERE p.name = 'Петр' AND p.surname = 'Иванов' AND v2.doctor_id IS NULL;

WITH doctors_who_treated_petrov AS (
    SELECT d.id, d.name, d.surname, d.patronymic
    FROM doctors d
             JOIN visits v ON d.id = v.doctor_id
    WHERE v.patient_id = (SELECT id FROM patients WHERE name = 'Петр' AND surname = 'Иванов')
)

   , doctors_who_conducted_procedures AS (
    SELECT v.doctor_id
    FROM visits v 
             JOIN procedure_history ph ON v.id = ph.visit_id
)

SELECT t.id, t.name, t.surname, t.patronymic
FROM doctors_who_treated_petrov as t
LEFT JOIN doctors_who_conducted_procedures as c
ON t.id = c.doctor_id
WHERE c.doctor_id IS NULL;


SELECT d.id, d.name, d.surname, d.patronymic
FROM doctors d
         JOIN visits v ON v.doctor_id = d.id
         JOIN patients p ON v.patient_id = p.id
    
LEFT JOIN 
    
    (SELECT v2.doctor_id
           FROM visits v2
                    JOIN procedure_history ph ON v2.id = ph.visit_id) AS sub 
    ON d.id = sub.doctor_id
WHERE p.name = 'Петр' AND p.surname = 'Иванов' AND sub.doctor_id IS NULL;

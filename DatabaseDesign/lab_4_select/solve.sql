SELECT DISTINCT d.id, d.name, d.surname, d.patronymic

FROM doctors d
         JOIN visits v ON d.id = v.doctor_id
         JOIN procedure_history ph ON v.id = ph.visit_id
         JOIN procedures p ON ph.procedure_id = p.id
WHERE p.name ILIKE '%лазер%';


SELECT p.id, p.name
FROM procedures p
         LEFT JOIN procedure_history ph ON p.id = ph.procedure_id
WHERE ph.procedure_id IS NULL;

-- Доктор, проводивший одному пациенту и прием, и процедуру в одном кабинете
SELECT d.id, d.name, d.surname, d.patronymic
FROM doctors d
         JOIN visits v ON d.id = v.doctor_id
         JOIN procedure_history ph ON v.id = ph.visit_id
WHERE v.id = ph.visit_id
  AND v.cabinet_id = ph.cabinet_id;


SELECT d.id, d.name, d.surname, d.patronymic
FROM visits v1
         LEFT JOIN procedure_history ph
                   ON v1.id = ph.visit_id
         LEFT JOIN visits v2 ON
            v2.doctor_id = v1.doctor_id AND
            v2.patient_id = v1.patient_id
         JOIN doctors d ON d.id = v1.doctor_id
WHERE v1.cabinet_id = ph.cabinet_id
   OR (v2.cabinet_id = ph.cabinet_id AND v2.id != v1.id);

insert into visits (cabinet_id, doctor_id, patient_id, visit_date_time)
values (2, 2, 3, now());
-- Задача в задании непонятна, решаем задачу - вычислить докторов
-- после консультации которых не было выполнено других процедур


WITH procedure_ids_of_concrete_patient AS (select h.id from procedures_history as h
                                           where h.patient_id = (select max(p.id) from patients as p
                                                                 where name = 'Петр' and surname = 'Иванов')),
    
     doctors_with_one_of_his_procedure AS (select h.doctor_id from procedures_history as h
                                           where h.id in (select * from procedure_ids_of_concrete_patient)
                                           group by h.doctor_id
                                           having count(h.id) = 1 and 
                                                  max(h.procedure_id) in (select max(id) from procedures as p where p.name = 'Consultation')),

     this_doctor as (select * from doctors as d
                     where d.id in (select * from doctors_with_one_of_his_procedure))

select * from this_doctor
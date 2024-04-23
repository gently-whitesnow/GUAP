select * from doctors as docs
where docs.id in (select h.doctor_id from procedures_history as h
                  where h.patient_id in (select h.patient_id from procedures_history as h
                                         where h.procedure_id in (select p.id from procedures as p
                                                                  where p.name = 'Consultation'))
                  group by h.cabinet , h.doctor_id
                  having count(h.procedure_id) > 1)
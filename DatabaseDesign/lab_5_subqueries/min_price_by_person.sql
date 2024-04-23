SELECT * from procedures as p
where p.id in (select h.procedure_id from procedures_history as h
               where doctor_id = (select id from doctors as d
                                  where d.name = 'Иван'
                                    and d.surname = 'Иванов'
                                    and d.patronymic = 'Иванович'))
order by p.price
    limit 1



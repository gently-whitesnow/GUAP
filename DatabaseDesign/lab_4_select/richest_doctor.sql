select h.doctor_id,
       sum(h.price) as total_price,
       max(name) as name,
       max(surname) as surname,
       max(patronymic) as patronymic
from procedures_history as h
         inner join doctors as d
                    on h.doctor_id = d.id
group by h.doctor_id
order by total_price desc
    limit 1
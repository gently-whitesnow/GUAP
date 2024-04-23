select distinct p.id, p.name, d.name, d.surname, d.patronymic
from procedures as p
         join procedures_history as h
              on h.procedure_id = p.id
         join doctors as d
              on d.id = h.doctor_id

where p.name ilike '%laser%'; -- ilike - case-insensitive
select p.id, p.name
from procedures as p
         left join procedures_history as h
                   on h.procedure_id = p.id
where h.procedure_id is null

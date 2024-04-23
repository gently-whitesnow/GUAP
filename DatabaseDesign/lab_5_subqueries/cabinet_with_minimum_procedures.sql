select h.cabinet, count(*) as count from procedures_history as h
group by h.cabinet
order by count

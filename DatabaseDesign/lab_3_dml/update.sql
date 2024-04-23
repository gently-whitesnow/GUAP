-- индексируем цены для истории процедур за последние 12 месяцев

update procedures_history
set price = (random() * 2000)::numeric(10, 2)
where procedure_date_time > now() - interval '12 month';
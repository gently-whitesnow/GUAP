-- изменяем атрибуты столбца name таблицы procedures
alter table procedures
    alter column name set not null;

-- добавляем колонку полиса
alter table patients
    add column policy varchar(10) null;

update patients
set policy = substr(md5(random()::text), 1, 10)
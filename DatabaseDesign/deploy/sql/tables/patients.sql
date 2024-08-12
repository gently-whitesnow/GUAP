create table patients
(
    id         serial primary key,
    name       varchar(100) not null,
    surname    varchar(100) not null,
    patronymic varchar(100)
);
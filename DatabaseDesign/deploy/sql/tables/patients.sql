create table patients
(
    id         serial
        primary key,
    name       varchar not null,
    surname    varchar not null,
    patronymic varchar
);
create table doctors
(
    id         serial
        primary key,
    name       varchar not null,
    surname    varchar not null,
    patronymic varchar
);
create table procedures
(
    id    serial primary key,
    name  varchar(300) not null,
    price numeric(10, 2) not null
);
create table cabinets
(
    id    serial primary key,
    room  varchar(10) not null
);

create table doctors
(
    id         serial primary key,
    name       varchar(100) not null,
    surname    varchar(100) not null,
    patronymic varchar(100)
);

create table patients
(
    id         serial primary key,
    name       varchar(100) not null,
    surname    varchar(100) not null,
    patronymic varchar(100)
);

CREATE TABLE visits
(
    id              SERIAL PRIMARY KEY,
    cabinet_id      INTEGER,
    doctor_id       INTEGER,
    patient_id      INTEGER   NOT NULL,
    visit_date_time TIMESTAMP NOT NULL,

    FOREIGN KEY (cabinet_id)
        REFERENCES cabinets (id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    FOREIGN KEY (doctor_id)
        REFERENCES doctors (id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    FOREIGN KEY (patient_id)
        REFERENCES patients (id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

create table procedure_history
(
    id           serial primary key,
    visit_id     INTEGER NOT NULL,
    procedure_id integer,
    cabinet_id   integer,
    price        numeric(10, 2) not null,

    foreign key (procedure_id)
        references procedures (id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    foreign key (cabinet_id)
        REFERENCES cabinets (id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    foreign key (visit_id)
        REFERENCES visits (id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
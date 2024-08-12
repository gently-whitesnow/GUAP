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
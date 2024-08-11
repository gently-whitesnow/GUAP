create table procedure_history
(
    id                  serial primary key,
    visit_id        INTEGER NOT NULL CONSTRAINT procedure_history_visit_id_fk
                    REFERENCES visits (id)
                    ON DELETE CASCADE
                    ON UPDATE CASCADE ,
    procedure_id           integer constraint procedure_history_procedure_id_fk
            REFERENCES procedures (id)
                    ON DELETE SET NULL
                    ON UPDATE CASCADE,
    cabinet_id           integer constraint procedure_history_cabinet_id_fk
        REFERENCES cabinets (id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    price               numeric          not null
);
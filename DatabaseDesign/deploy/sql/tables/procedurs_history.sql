create table procedures_history
(
    id                  serial primary key,
    procedure_id        integer constraint procedures_history_procedure_id_fk
            references procedures,
    doctor_id           integer constraint procedures_history_doctor_id_fk
            references doctors,
    patient_id          integer constraint procedures_history_patient_id_fk
            references patients,
    procedure_date_time timestamp        not null,
    price               numeric          not null,
    cabinet             double precision not null
);
CREATE TABLE visits
(
    id              SERIAL PRIMARY KEY,
    cabinet_id      INTEGER CONSTRAINT visit_cabinet_id_fk
                    REFERENCES cabinets (id)
                    ON DELETE SET NULL
                    ON UPDATE CASCADE,
    doctor_id       INTEGER CONSTRAINT visit_doctor_id_fk
                    REFERENCES doctors (id)
                    ON DELETE SET NULL
                    ON UPDATE CASCADE,
    patient_id      INTEGER NOT NULL CONSTRAINT visit_patient_id_fk
                    REFERENCES patients (id)
                    ON DELETE CASCADE
                    ON UPDATE CASCADE,
    visit_date_time TIMESTAMP NOT NULL
);

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

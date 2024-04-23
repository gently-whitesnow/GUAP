SELECT
    d.id,
    d.name,
    d.surname,
    d.patronymic
FROM
    doctors d
WHERE
    NOT EXISTS (
        SELECT
            p.id
        FROM
            procedures p
        WHERE
            NOT EXISTS (
                SELECT
                    ph.id
                FROM
                    procedures_history ph
                WHERE
                        ph.doctor_id = d.id
                  AND ph.procedure_id = p.id
            )
    );

-- Not exist
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


-- ALL
SELECT
    d.id,
    d.name,
    d.surname,
    d.patronymic
FROM
    doctors d
WHERE
        (
            SELECT COUNT(DISTINCT id)
            FROM procedures
        ) = ALL (
            SELECT COUNT(DISTINCT ph.procedure_id)
            FROM procedures_history ph
            WHERE ph.doctor_id = d.id
        );

-- ANY

SELECT
    d.id,
    d.name,
    d.surname,
    d.patronymic
FROM
    doctors d
WHERE
        (
            SELECT COUNT(DISTINCT id)
            FROM procedures
        ) <= ANY (
            SELECT COUNT(DISTINCT ph.procedure_id)
            FROM procedures_history ph
            WHERE ph.doctor_id = d.id
            GROUP BY ph.doctor_id
        );

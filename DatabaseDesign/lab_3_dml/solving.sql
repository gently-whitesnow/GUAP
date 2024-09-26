INSERT INTO patients (name, surname, patronymic)
VALUES ('Анна', 'Морозова', 'Ивановна');

UPDATE doctors
SET surname = 'Смирнов'
WHERE name = 'Иван' AND surname = 'Иванов';


DELETE FROM procedures
WHERE price < 2000.00;


MERGE INTO patients AS target
USING (SELECT 'Петр' AS name, 'Иванов2' AS surname, 'Васильевич' AS patronymic) AS source
ON (target.name = source.name AND target.surname = source.surname)
WHEN MATCHED THEN
    UPDATE SET patronymic = source.patronymic
WHEN NOT MATCHED THEN
    INSERT (name, surname, patronymic)
    VALUES (source.name, source.surname, source.patronymic);
 
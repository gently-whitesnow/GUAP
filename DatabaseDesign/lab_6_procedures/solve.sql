-- Каскадное удаление
CREATE
    OR REPLACE PROCEDURE cascade_delete_patient(
    in_patient_id int
)
AS
$$

BEGIN
    -- Сначала удаляем историю процедур данного пациента
    DELETE
    FROM procedure_history
    WHERE visit_id IN (SELECT id FROM visits WHERE patient_id = in_patient_id);

-- Теперь удаляем визиты пациента
    DELETE FROM visits WHERE patient_id = in_patient_id;

-- И, наконец, удаляем самого пациента
    DELETE FROM patients WHERE id = in_patient_id;

END;
$$
    LANGUAGE plpgsql;

call cascade_delete_patient(in_patient_id := 20);

-- Вставка с пополнение справочников
CREATE
    OR REPLACE PROCEDURE p_create_visit(
    in_patient_name varchar,
    in_patient_surname varchar,
    in_patient_patronymic varchar,
    in_doctor_id integer,
    in_cabinet_id integer
)
AS
$$
DECLARE
    local_patient_id integer;
BEGIN
    IF NOT EXISTS(SELECT 1
                  FROM patients
                  WHERE name = in_patient_name
                    AND surname = in_patient_surname
                    AND patronymic = in_patient_patronymic)
    THEN
        INSERT INTO patients(name, surname, patronymic)
        VALUES (in_patient_name, in_patient_surname, in_patient_patronymic)
        RETURNING id INTO local_patient_id;
    ELSE
        SELECT id
        INTO local_patient_id
        FROM patients
        WHERE name = in_patient_name
          AND surname = in_patient_surname
          AND patronymic = in_patient_patronymic;
    END IF;

    INSERT INTO visits (patient_id, doctor_id, cabinet_id, visit_date_time)
    VALUES (local_patient_id, in_doctor_id, in_cabinet_id, now());

END;
$$
    LANGUAGE plpgsql;

call p_create_visit(
        in_patient_name := 'Иван',
        in_patient_surname := 'Иванов',
        in_patient_patronymic := 'Иванович',
        in_doctor_id := 1,
        in_cabinet_id := 2
    );

-- любая функция
CREATE
    OR REPLACE FUNCTION f_get_patient_visits(
    in_patient_id integer)
    RETURNS TABLE
            (
                id                integer,
                visit_date_time   timestamp,
                doctor_name       varchar,
                doctor_surname    varchar,
                doctor_patronymic varchar,
                procedure_name    varchar
            )
AS
$$
BEGIN
    RETURN QUERY
        SELECT v.id,
               v.visit_date_time,
               d.name       AS doctor_name,
               d.surname    AS doctor_surname,
               d.patronymic AS doctor_patronymic,
               pr.name      AS procedure_name
        FROM visits v
                 JOIN patients p ON v.patient_id = p.id
                 JOIN doctors d ON v.doctor_id = d.id
                 JOIN cabinets c ON v.cabinet_id = c.id
                 JOIN procedure_history ph ON v.id = ph.visit_id
                 JOIN procedures pr ON ph.procedure_id = pr.id
        WHERE v.patient_id = in_patient_id;
END;
$$
    LANGUAGE plpgsql;

select * from public.f_get_patient_visits(in_patient_id := 1)

-- удаление доктора, если визитов у него больше не остается
CREATE
    OR REPLACE PROCEDURE remove_doctor_with_empty_visits(
    in_visit_id int
)
AS
$$
DECLARE
    local_doctor_id integer;
BEGIN

    DELETE FROM visits WHERE id = in_visit_id RETURNING doctor_id INTO local_doctor_id;

    DELETE FROM doctors
    WHERE id = local_doctor_id
      AND NOT EXISTS (SELECT 1 FROM visits WHERE doctor_id = local_doctor_id);

END;
$$
    LANGUAGE plpgsql;

call remove_doctor_with_empty_visits(in_visit_id := 1);

-- Роль для пользователей с правами на запись
CREATE ROLE writer_role;

-- Роль для пользователей с правами на чтение
CREATE ROLE reader_role;

-- Создание пользователя для записи и назначение ему роли
CREATE USER writer_user WITH PASSWORD 'password';
GRANT writer_role TO writer_user;

-- Создание пользователя для чтения и назначение ему роли
CREATE USER reader_user WITH PASSWORD 'password';
GRANT reader_role TO reader_user;

-- Назначение прав на выполнение процедур пользователям с ролью writer_role
GRANT EXECUTE ON PROCEDURE cascade_delete_patient(int) TO writer_role;
GRANT EXECUTE ON PROCEDURE p_create_visit(varchar, varchar, varchar, integer, integer) TO writer_role;
GRANT EXECUTE ON PROCEDURE remove_doctor_visits(int) TO writer_role;

-- Назначение прав на выполнение функции пользователям с ролью reader_role
GRANT EXECUTE ON FUNCTION f_get_patient_visits(integer) TO reader_role;

-- Вход в систему от имени пользователя с правами на запись
SET ROLE writer_user;

-- Выполнение процедуры
CALL p_create_visit('Иван', 'Иванов', 'Иванович', 1, 2);

-- Вход в систему от имени пользователя с правами на чтение
SET ROLE reader_user;

-- Выполнение функции для получения данных
SELECT * FROM f_get_patient_visits(1);
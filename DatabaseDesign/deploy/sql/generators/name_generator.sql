CREATE
OR REPLACE FUNCTION generate_random_name()
    RETURNS VARCHAR AS $$
DECLARE
names   VARCHAR[] := ARRAY[
        'James', 'Mary', 'John', 'Patricia', 'Robert', 'Jennifer', 'Michael', 'Linda', 'William', 'Elizabeth',
        'David', 'Barbara', 'Richard', 'Susan', 'Joseph', 'Jessica', 'Thomas', 'Sarah', 'Charles', 'Karen',
        'Christopher', 'Nancy', 'Daniel', 'Lisa', 'Matthew', 'Margaret', 'Anthony', 'Betty', 'Donald', 'Sandra',
        'Mark', 'Ashley', 'Paul', 'Dorothy', 'Steven', 'Kimberly', 'Andrew', 'Emily', 'Kenneth', 'Donna',
        'Joshua', 'Michelle', 'Kevin', 'Carol', 'Brian', 'Amanda', 'George', 'Melissa', 'Edward', 'Deborah',
        'Ronald', 'Stephanie', 'Timothy', 'Rebecca', 'Jason', 'Laura', 'Jeffrey', 'Sharon', 'Ryan', 'Cynthia',
        'Jacob', 'Kathleen', 'Gary', 'Helen', 'Nicholas', 'Amy', 'Eric', 'Anna', 'Jonathan', 'Brenda',
        'Stephen', 'Pamela', 'Larry', 'Nicole', 'Justin', 'Samantha', 'Scott', 'Christine', 'Brandon', 'Catherine',
        'Benjamin', 'Virginia', 'Samuel', 'Debra', 'Gregory', 'Rachel', 'Frank', 'Janet', 'Raymond', 'Emma',
        'Alexander', 'Carolyn', 'Patrick', 'Maria', 'Jack', 'Heather', 'Dennis', 'Diane', 'Jerry', 'Julie',
        'Tyler', 'Joyce', 'Aaron', 'Victoria', 'Henry', 'Kelly', 'Douglas', 'Christina', 'Peter', 'Lauren',
        'Jose', 'Frances', 'Adam', 'Martha', 'Zachary', 'Judith', 'Nathan', 'Cheryl', 'Walter', 'Megan',
        'Harold', 'Andrea', 'Kyle', 'Ann', 'Carl', 'Alice', 'Arthur', 'Jean', 'Gerald', 'Doris',
        'Roger', 'Jacqueline', 'Keith', 'Kathryn', 'Jeremy', 'Hannah', 'Terry', 'Olivia', 'Lawrence', 'Gloria',
        'Sean', 'Marie', 'Christian', 'Teresa', 'Albert', 'Sara', 'Jesse', 'Janice', 'Ethan', 'Julia',
        'Willie', 'Grace', 'Billy', 'Judy', 'Bryan', 'Theresa', 'Bruce', 'Rose', 'Jordan', 'Beverly',
        'Ralph', 'Denise', 'Roy', 'Marilyn', 'Eugene', 'Amber', 'Wayne', 'Madison', 'Louis', 'Danielle',
        'Adam', 'Brittany', 'Russell', 'Diana', 'Alan', 'Abigail', 'Philip', 'Jane', 'Bobby', 'Natalie',
        'Johnny', 'Lori', 'Antonio', 'Tiffany', 'Phillip', 'Alexis', 'Ronnie', 'Kayla', 'Jimmy', 'Jacqueline',
        'Earl', 'Cassandra', 'Eric', 'Robin', 'Sam', 'Holly', 'Curtis', 'Monica', 'Jon', 'Christine','Иван'
        ];
BEGIN
    RETURN
names[floor(random() * array_length(names, 1)) + 1];
END;
$$
LANGUAGE plpgsql;


CREATE
OR REPLACE FUNCTION generate_random_surname()
    RETURNS VARCHAR AS $$
DECLARE
surnames   VARCHAR[] := ARRAY[
        'Smith', 'Johnson', 'Williams', 'Jones', 'Brown', 'Davis', 'Miller', 'Wilson', 'Moore', 'Taylor',
        'Anderson', 'Thomas', 'Jackson', 'White', 'Harris', 'Martin', 'Thompson', 'Garcia', 'Martinez', 'Robinson',
        'Clark', 'Rodriguez', 'Lewis', 'Lee', 'Walker', 'Hall', 'Allen', 'Young', 'Hernandez', 'King',
        'Wright', 'Lopez', 'Hill', 'Scott', 'Green', 'Adams', 'Baker', 'Gonzalez', 'Nelson', 'Carter',
        'Mitchell', 'Perez', 'Roberts', 'Turner', 'Phillips', 'Campbell', 'Parker', 'Evans', 'Edwards', 'Collins',
        'Stewart', 'Sanchez', 'Morris', 'Rogers', 'Reed', 'Cook', 'Morgan', 'Bell', 'Murphy', 'Bailey',
        'Rivera', 'Cooper', 'Richardson', 'Cox', 'Howard', 'Ward', 'Torres', 'Peterson', 'Gray', 'Ramirez',
        'James', 'Watson', 'Brooks', 'Kelly', 'Sanders', 'Price', 'Bennett', 'Wood', 'Barnes', 'Ross',
        'Henderson', 'Coleman', 'Jenkins', 'Perry', 'Powell', 'Long', 'Patterson', 'Hughes', 'Flores', 'Washington',
        'Butler', 'Simmons', 'Foster', 'Gonzales', 'Bryant', 'Alexander', 'Russell', 'Griffin', 'Diaz', 'Hayes',
        'Myers', 'Ford', 'Hamilton', 'Graham', 'Sullivan', 'Wallace', 'Woods', 'Cole', 'West', 'Jordan',
        'Owens', 'Reynolds', 'Fisher', 'Ellis', 'Harrison', 'Gibson', 'Mcdonald', 'Cruz', 'Marshall', 'Ortiz',
        'Gomez', 'Murray', 'Freeman', 'Wells', 'Webb', 'Simpson', 'Stevens', 'Tucker', 'Porter', 'Hunter',
        'Hicks', 'Crawford', 'Henry', 'Boyd', 'Mason', 'Morales', 'Kennedy', 'Warren', 'Dixon', 'Ramos',
        'Reyes', 'Burns', 'Gordon', 'Shaw', 'Holmes', 'Rice', 'Robertson', 'Hunt', 'Black', 'Daniels',
        'Palmer', 'Mills', 'Nichols', 'Grant', 'Knight', 'Ferguson', 'Rose', 'Stone', 'Hawkins', 'Dunn',
        'Perkins', 'Hudson', 'Spencer', 'Gardner', 'Stephens', 'Payne', 'Pierce', 'Berry', 'Matthews', 'Arnold',
        'Wagner', 'Willis', 'Ray', 'Watkins', 'Olson', 'Carroll', 'Duncan', 'Snyder', 'Hart', 'Cunningham',
        'Bradley', 'Lane', 'Andrews', 'Ruiz', 'Harper', 'Fox', 'Riley', 'Armstrong', 'Carpenter', 'Weaver',
        'Greene', 'Lawrence', 'Elliott', 'Chavez', 'Sims', 'Austin', 'Peters', 'Kelley', 'Franklin', 'Lawson', 'Иванов'
        ];
BEGIN
RETURN surnames[floor(random() * array_length(surnames, 1)) + 1];
END;
$$
LANGUAGE plpgsql;

CREATE
OR REPLACE FUNCTION generate_random_patronymic()
    RETURNS VARCHAR AS $$
DECLARE
patronymics   VARCHAR[] := ARRAY[
        'Jameson', 'Johnson', 'Robinson', 'Anderson', 'Peterson', 'Richardson', 'Henderson', 'Thompson', 'Williams', 'Jackson',
        'Johnson', 'Wilson', 'Roberts', 'Harrison', 'Peters', 'Davis', 'Taylor', 'Harris', 'Mitchell', 'Mitchelson',
        'Tomson', 'Anders', 'Richards', 'Andrews', 'Williams', 'Samson', 'Jonson', 'Tomson', 'Peters', 'Simmons',
        'Hendricks', 'Thomas', 'Dickens', 'Adamson', 'Harrison', 'James', 'Thomson', 'Adams', 'Tomlinson', 'Jameson',
        'Thomson', 'Williamson', 'Robertson', 'Anderson', 'Dickinson', 'Henderson', 'Tomlinson', 'Thomson', 'Anderson', 'Harrison',
        'Johnson', 'Tomson', 'Richards', 'Anderson', 'Harris', 'Tomson', 'Henderson', 'Roberts', 'Harrison', 'Johnson',
        'Peters', 'Samson', 'Adams', 'Richards', 'Roberts', 'Adamson', 'Richards', 'Williams', 'Anderson', 'Johnson',
        'Peters', 'Andrews', 'Samson', 'Johnson', 'Dickens', 'Thompson', 'Richardson', 'Williams', 'Harrison', 'Tomlinson',
        'Peters', 'Richards', 'Dickens', 'Anderson', 'Thomson', 'Robinson', 'Taylor', 'Jameson', 'Henderson', 'Thompson',
        'Roberts', 'Anderson', 'Samson', 'Johnson', 'Richards', 'Adamson', 'Robinson', 'Harris', 'Thompson', 'Richards',
        'Jameson', 'Hendricks', 'Henderson', 'Richardson', 'Johnson', 'Williams', 'Dickens', 'Thomson', 'James', 'Taylor',
        'Tomlinson', 'Richardson', 'Adamson', 'Jameson', 'Harris', 'Dickens', 'Roberts', 'Thompson', 'Adamson', 'Richards',
        'Hendricks', 'James', 'Taylor', 'Johnson', 'Peters', 'Anderson', 'Taylor', 'Harris', 'Samson', 'Harrison',
        'Robinson', 'Peters', 'Taylor', 'Richardson', 'Anderson', 'Roberts', 'Roberts', 'Johnson', 'Tomlinson', 'Dickens',
        'Hendricks', 'Henderson', 'Samson', 'Dickens', 'Roberts', 'Adamson', 'Harrison', 'Thompson', 'Williams', 'Johnson',
        'Richardson', 'Peters', 'Taylor', 'Jameson', 'Hendricks', 'Johnson', 'Thompson', 'Richards', 'Harris', 'Robinson',
        'Tomlinson', 'James', 'Samson', 'Richardson', 'Roberts', 'Hendricks', 'Anderson', 'Thompson', 'Adamson', 'Williams',
        'Peters', 'Anderson', 'Robinson', 'Johnson', 'Richards', 'Adamson', 'Henderson', 'Hendricks', 'Roberts', 'Harris',
        'Thompson', 'Jameson', 'Taylor', 'Richards', 'Roberts', 'Dickens', 'Tomlinson', 'Hendricks', 'Anderson', 'Roberts', 'Иванович'
        ];
BEGIN
RETURN patronymics[floor(random() * array_length(patronymics, 1)) + 1];
END;
$$
LANGUAGE plpgsql;



CREATE
    OR REPLACE FUNCTION generate_procedure_name_by_index(index_param INTEGER)
    RETURNS VARCHAR AS
$$
DECLARE
    procedures VARCHAR[] := ARRAY [
        'Consultation',
        'Routine check-up', 'Dental cleaning', 'Eye examination', 'Blood test', 'X-ray laser', 'MRI scan', 'Ultrasound', 'Colonoscopy', 'Endoscopy', 'Electrocardiogram',
        'Mammogram', 'Pap smear', 'Bone density test', 'Prostate exam', 'CT scan', 'Biopsy', 'Physical therapy', 'Occupational therapy', 'Speech therapy', 'Chiropractic adjustment',
        'Chemotherapy', 'Radiation therapy', 'Dialysis', 'Surgery', 'Appendectomy', 'Gallbladder removal', 'Hernia repair', 'Cataract surgery', 'LASIK eye surgery', 'Hip replacement',
        'Knee replacement', 'Shoulder arthroscopy', 'ACL reconstruction', 'Tonsillectomy', 'Vasectomy', 'Tubal ligation', 'Breast augmentation', 'Rhinoplasty', 'Liposuction', 'Botox injections',
        'Dermal fillers', 'Laser hair removal', 'Teeth whitening', 'Dental implants', 'Root canal', 'Dental crown', 'Braces', 'Wisdom teeth removal', 'Dentures', 'Orthodontic retainer',
        'Invisalign', 'Dental bridge', 'Periodontal surgery', 'Gastric bypass surgery', 'Laparoscopic surgery', 'Laminectomy', 'Spinal fusion', 'Gastric sleeve surgery', 'Thyroidectomy', 'Mastectomy'];
BEGIN
    RETURN procedures[index_param % array_length(procedures, 1)];
END
$$
    LANGUAGE plpgsql;

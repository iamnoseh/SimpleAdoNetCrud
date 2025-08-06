-- Эҷоди пойгоҳи Dapper-Practice-Db
-- CREATE DATABASE "Dapper-Practice-Db";

-- Истифода аз пойгоҳ
-- \c "Dapper-Practice-Db";

-- Эҷоди ҷадвали Students
CREATE TABLE IF NOT EXISTS Students (
    Id SERIAL PRIMARY KEY,
    firstname VARCHAR(100) NOT NULL,
    lastname VARCHAR(100) NOT NULL,
    birthday DATE NOT NULL,
    address VARCHAR(500),
    level INTEGER NOT NULL
);

-- Эҷоди ҷадвали Teachers
CREATE TABLE IF NOT EXISTS Teachers (
    Id SERIAL PRIMARY KEY,
    firstname VARCHAR(100) NOT NULL,
    lastname VARCHAR(100) NOT NULL,
    birthday DATE NOT NULL,
    address VARCHAR(500),
    salary DECIMAL(10,2) NOT NULL
);

-- Иловаи маълумоти мисолӣ барои санҷиш
INSERT INTO Students (firstname, lastname, birthday, address, level) VALUES
('Алӣ', 'Юсуфов', '2000-01-15', 'Душанбе, кӯчаи Рӯдакӣ 25', 1),
('Мариям', 'Ҳасанова', '1999-05-20', 'Душанбе, кӯчаи Айнӣ 10', 2),
('Саид', 'Раҳимов', '2001-03-10', 'Душанбе, кӯчаи Фирдавсӣ 5', 1);

INSERT INTO Teachers (firstname, lastname, birthday, address, salary) VALUES
('Фарида', 'Каримова', '1980-08-12', 'Душанбе, кӯчаи Исмоили Сомонӣ 15', 2500.00),
('Ҷамшед', 'Муҳаммадов', '1975-12-05', 'Душанбе, кӯчаи Шоҳ Мансур 8', 3000.00),
('Нигора', 'Одинаева', '1985-04-18', 'Душанбе, кӯчаи Борбад 12', 2800.00);

-- Намоиши маълумот
SELECT 'Шогирдон:' as info;
SELECT * FROM Students;

SELECT 'Муаллимон:' as info;
SELECT * FROM Teachers;
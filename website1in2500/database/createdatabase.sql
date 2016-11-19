DROP TABLE IF EXISTS players CASCADE;
DROP TABLE IF EXISTS scores;

CREATE TABLE players (
    id SERIAL PRIMARY KEY NOT NULL,
    name VARCHAR NOT NULL,
    age VARCHAR,
    sex VARCHAR
);

CREATE TABLE scores (
    id INT REFERENCES players(id),
    points INT NOT NULL,
    timeused REAL NOT NULL
);

-- INSERT players
INSERT INTO players (name, age, sex) VALUES ('menino1',13,'M');
INSERT INTO players (name, age, sex) VALUES ('menina1',10,'F');
INSERT INTO players (name, age, sex) VALUES ('menino2',5,'M');
INSERT INTO players (name, age, sex) VALUES ('menino3',6,'M');
INSERT INTO players (name, age, sex) VALUES ('menino4',8,'M');
INSERT INTO players (name, age, sex) VALUES ('menina2',11,'F');
INSERT INTO players (name, age, sex) VALUES ('menina3',6,'F');
INSERT INTO players (name, age, sex) VALUES ('menina4',4,'F');
INSERT INTO players (name, age, sex) VALUES ('menina5',11,'F');
INSERT INTO players (name, age, sex) VALUES ('menina6','','F');
INSERT INTO players (name, age, sex) VALUES ('menino4','','M');
INSERT INTO players (name, age, sex) VALUES ('menina7','','F');
INSERT INTO players (name, age, sex) VALUES ('menina8',17,'');
INSERT INTO players (name, age, sex) VALUES ('menino5',36,'');
INSERT INTO players (name, age, sex) VALUES ('menino6',51,'');
INSERT INTO players (name, age, sex) VALUES ('menino7',1,'M');

-- INSERT scores

INSERT INTO scores (id, points, timeused ) VALUES (1,50000, 234.24);
INSERT INTO scores (id, points , timeused ) VALUES (4,3000, 724);
INSERT INTO scores (id, points, timeused ) VALUES (5,5500,2762.237);
INSERT INTO scores (id, points, timeused ) VALUES (2,5700,2530);
INSERT INTO scores (id, points, timeused ) VALUES (6,54,235);
INSERT INTO scores (id, points, timeused ) VALUES (1,7145,423);
INSERT INTO scores (id, points, timeused ) VALUES (1,34411,62);
INSERT INTO scores (id, points, timeused ) VALUES (3,7614, 27);
INSERT INTO scores (id, points, timeused ) VALUES (7,81723, 279);
INSERT INTO scores (id, points, timeused ) VALUES (10,12371, 985);






CREATE DATABASE scheme_db;
USE scheme_db;
DROP TABLE IF EXISTS schemes;

CREATE TABLE schemes (
    scheme_id INT AUTO_INCREMENT,
    name CHAR(32),
    version INT,
    dt DATETIME,
    swarm_1 CHAR(24),
    swarm_2 CHAR(24),
    swarm_3 CHAR(24),
    swarm_4 CHAR(24),
    swarm_5 CHAR(24),
    swarm_6 CHAR(24),   
    PRIMARY KEY (scheme_id),
    UNIQUE (name, version)
);

INSERT INTO schemes (name, version, dt, swarm_1, swarm_2, swarm_3, swarm_4, swarm_5, swarm_6) VALUES ("Perseida Max", 1, now(), "", "", "", "", "", "");
INSERT INTO schemes (name, version, dt, swarm_1, swarm_2, swarm_3, swarm_4, swarm_5, swarm_6) VALUES ("Perseida Max", 2, now(), "", "", "", "", "", "");
INSERT INTO schemes (name, version, dt, swarm_1, swarm_2, swarm_3, swarm_4, swarm_5, swarm_6) VALUES ("Perseida Max", 3, now(), "", "", "", "", "", "");
INSERT INTO schemes (name, version, dt, swarm_1, swarm_2, swarm_3, swarm_4, swarm_5, swarm_6) VALUES ("Perseida Max", 4, now(), "", "", "", "", "", "");
INSERT INTO schemes (name, version, dt, swarm_1, swarm_2, swarm_3, swarm_4, swarm_5, swarm_6) VALUES ("Perseida Max", 5, now(), "", "", "", "", "", "");
INSERT INTO schemes (name, version, dt, swarm_1, swarm_2, swarm_3, swarm_4, swarm_5, swarm_6) VALUES ("Perseida Max", 6, now(), "", "", "", "", "", "");
INSERT INTO schemes (name, version, dt, swarm_1, swarm_2, swarm_3, swarm_4, swarm_5, swarm_6) VALUES ("Perseida Max", 7, now(), "", "", "", "", "", "");
INSERT INTO schemes (name, version, dt, swarm_1, swarm_2, swarm_3, swarm_4, swarm_5, swarm_6) VALUES ("Perseida Max", 8, now(), "", "", "", "", "", "");
INSERT INTO schemes (name, version, dt, swarm_1, swarm_2, swarm_3, swarm_4, swarm_5, swarm_6) VALUES ("Perseida Max", 9, now(), "", "", "", "", "", "");
INSERT INTO schemes (name, version, dt, swarm_1, swarm_2, swarm_3, swarm_4, swarm_5, swarm_6) VALUES ("Perseida Max", 10, now(), "", "", "", "", "", "");

-- docker exec -it <container_id> mysql -u root -p
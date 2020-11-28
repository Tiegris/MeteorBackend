CREATE DATABASE scheme_db;
USE scheme_db;
DROP TABLE IF EXISTS schemes;

CREATE TABLE schemes (
    scheme_id INT AUTO_INCREMENT,
    name CHAR(32),
    version INT,
    date DATE,
    swarm_1 CHAR(24),
    swarm_2 CHAR(24),
    swarm_3 CHAR(24),
    swarm_4 CHAR(24),
    swarm_5 CHAR(24),
    swarm_6 CHAR(24),   
    PRIMARY KEY (scheme_id),
    UNIQUE (name, version)
);

INSERT INTO schemes (name, version, date, swarm_1, swarm_2, swarm_3, swarm_4, swarm_5, swarm_6) VALUES ("Perseida Max", 1, curdate(), "", "", "", "", "", "");

-- docker exec -it <container_id> mysql -u root -p
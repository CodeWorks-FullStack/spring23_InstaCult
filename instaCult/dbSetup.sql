CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS cults(
  id INT AUTO_INCREMENT PRIMARY KEY NOT NULL,
  name VARCHAR(25) NOT NULL,
  description VARCHAR(500),
  tags VARCHAR(255),
  leaderId VARCHAR(255) NOT NULL,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',

  FOREIGN KEY (leaderId) REFERENCES accounts(id) ON DELETE CASCADE
)default charset utf8mb4 COMMENT '';


INSERT INTO cults
(name, description, tags, leaderId)
VALUES
-- ('NASCULT', "GO FAST, TURN LEFT, DON'T WRECK. IF YOU AIN'T FIRST YOU'RE LAST", 'fast, cars, engines, racing, sunburn, left', '634844a08c9d1ba02348913d');
-- ('Pong Heros', "Who's got time to do the lab? we have ping pong paddles to smash and riff on guitar hero to also smash. Sponsored by Rocket Mortgage.", 'guitar, ping pong, senioritis, 24/7/365-hackathon', '631b5b5fa7f0b66bb817725a');
;
SELECT
c.name, leader.name
FROM cults c
JOIN accounts leader ON c.leaderId = leader.id
WHERE c.tags LIKE '%coffee%';

SELECT LAST_INSERT_ID();
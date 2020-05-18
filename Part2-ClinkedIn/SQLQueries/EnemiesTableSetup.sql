create table Enemies(
	EnemyId int identity(1,1),
	EnemyClinkerId int NOT NULL,
	ClinkerId int NOT NULL,
);

INSERT Enemies
(EnemyClinkerId, ClinkerId)
VALUES
(1, 1),
(1, 2),
(2, 3)
;

select *
from Enemies;
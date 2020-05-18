create table Friends(
	FriendId int identity(1,1),
	FriendClinkerId int NOT NULL,
	ClinkerId int NOT NULL,
);

INSERT Friends
(FriendClinkerId, ClinkerId)
VALUES
(1, 3)
;

select *
from Friends;

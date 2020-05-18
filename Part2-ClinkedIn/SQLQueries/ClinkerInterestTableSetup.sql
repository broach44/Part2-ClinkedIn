create table ClinkerInterest(
	ClinkerInterestId int identity(1,1),
	InterestId int NOT NULL,
	ClinkerId int NOT NULL,
);

INSERT ClinkerInterest
(InterestId, ClinkerId)
VALUES
(1, 1),
(1, 2),
(2, 3)
;

select *
from ClinkerInterest;

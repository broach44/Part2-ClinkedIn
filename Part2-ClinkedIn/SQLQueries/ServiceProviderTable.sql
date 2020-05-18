create table ServiceProvider(
	ServiceProviderId int identity(1,1),
	ServiceId int NOT NULL,
	ClinkerId int NOT NULL,
);

INSERT ServiceProvider
(ServiceId, ClinkerId)
VALUES
(1, 1),
(2, 2),
(3, 3)
;

select *
from ServiceProvider;
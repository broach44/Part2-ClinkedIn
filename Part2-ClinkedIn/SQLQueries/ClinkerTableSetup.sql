create table Clinker(
	ClinkerId int identity(1,1),
	FirstName varchar(50) NOT NULL,
	LastName varchar(50) NOT NULL,
	PrisonTermEndDate datetime NOT NULL
);

INSERT Clinker
(FirstName, LastName, PrisonTermEndDate)
VALUES
('Sam', 'Smith', '2021-03-31'),
('John', 'Wayne', '2045-02-20'),
('John', 'Smith', '2030-05-15')
;

select *
from clinker;
create table Interest(
	InterestId int identity(1,1),
	InterestName varchar(100) NOT NULL,
	InterestDescription varchar(255) NOT NULL,
);

INSERT Interest
(InterestName, InterestDescription)
VALUES
('Kickball', 'A sport played similar to baseball.  Instead of a baseball you kick a ball and must throw ball at the person to get them out.'),
('Tattoos', 'Interested in tattoo design and/or collection'),
('Yoga', 'Form of exercise that offers peach of mind')
;

select *
from Interest;
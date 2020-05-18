create table Service(
	ServiceId int identity(1,1),
	ServiceName varchar(100) NOT NULL,
	ServicePrice decimal(18, 2) NOT NULL,
);

INSERT Service
(ServiceName, ServicePrice)
VALUES
('Tattooing', 100.00),
('Protection', 250.00),
('Backrub', 50.50)
;

select *
from Service;
CREATE TABLE employee
(
	id SERIAL PRIMARY KEY,
	employee_name varchar(100) NOT NULL,
	email varchar(100) NOT NULL,
	_password varchar(16) NOT NULL,
	_role varchar(50) NOT NULL,
)
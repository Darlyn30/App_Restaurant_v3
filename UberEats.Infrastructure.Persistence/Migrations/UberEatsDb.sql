CREATE DATABASE UberEatsDb

USE UberEatsDb

CREATE TABLE Users
(
	Id INT IDENTITY(1,1),
	Name VARCHAR(50) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	IsActive BIT,
	PasswordHash NVARCHAR(MAX) NOT NULL,
	Pin VARCHAR(6) NOT NULL,
	Role VARCHAR(20),
	PRIMARY KEY(Email)
)

SELECT * FROM Users


CREATE TABLE UnverifiedAccounts
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Pin VARCHAR(6) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	Name VARCHAR(50) NOT NULL,
	--NO NECESITO EL HASH AQUI
)


CREATE TRIGGER GetPIN
ON Users
AFTER INSERT
AS
BEGIN
	INSERT INTO UnverifiedAccounts(Email, Pin, Name)
	SELECT Email, Pin, Name
	FROM inserted
END


CREATE TRIGGER changeStatus
ON Users
INSTEAD OF DELETE
AS
BEGIN
	    -- Actualizamos el campo Estatus en la tabla cuentas
    UPDATE u
    SET u.IsActive = 1  -- Cambia Estatus a 1 (activo)
    FROM Users AS u
    INNER JOIN deleted AS d ON u.Email = d.Email;

    -- Ahora procedemos con la eliminaci?n del registro en cuenta_creadas
    DELETE FROM UnverifiedAccounts
END
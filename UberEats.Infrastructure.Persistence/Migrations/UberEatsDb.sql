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

SELECT * FROM UnverifiedAccounts

CREATE TABLE Categories
(
	Id  INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(50),
	ImgUrl VARCHAR(MAX)
)

/*
INSERT INTO Categories(Name, ImgUrl) VALUES ('Comida Japonesa', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS3jj8mQv9ibd_Ta7HrhrhTHRA1HDW7jQXZmQ&s'),
('Comida Dominicana', 'https://tuguiadominicana.com/wp-content/uploads/2023/02/plato-tipico-domonicano-@yolandazarzuela-edited.jpg'),
('Bebidas', 'https://megaadventuresdr.com/wp-content/uploads/2019/03/Best-Drinks-1.jpg')
*/


CREATE TABLE Restaurants
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(100),
	Description VARCHAR(MAX),
	CategoryId INT NOT NULL,
	OpeningTime TIME,
	ClosingTime TIME,
	DeliveryAvailable BIT,
	Active BIT,
	ImgUrl VARCHAR(MAX),
	FOREIGN KEY(CategoryId) REFERENCES Categories(Id)
)

SELECT * FROM Restaurants


CREATE TABLE Foods
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(100),
	Description VARCHAR(MAX),
	Price DECIMAL(10,2),
	RestaurantId INT,
	Active BIT,
	ImgUrl VARCHAR(MAX),
	FOREIGN KEY (RestaurantId) REFERENCES Restaurants(Id)
)

SELECT * FROM Foods

/*
INSERT INTO Foods (Name, Description, Price, RestaurantId, Active)
VALUES
('Salchipapa', 'Salchicha con papa al vapor', 9.99, 3, 1),
('Burrito', 'Rollo de tortilla con carne, arroz, frijoles y guacamole', 15.50, 3, 1),
('Pasta Carbonara', 'Pasta con salsa cremosa de huevo, queso parmesano y panceta', 12.00, 3, 1),
('Ramen', 'Sopa japonesa con fideos, caldo y vegetales', 14.00, 3, 1),
('Burger Classic', 'Hamburguesa con carne, queso, lechuga y tomate', 8.50, 3, 1)
*/





CREATE TABLE Carts
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	UserEmail  VARCHAR(100) NOT NULL,
	CreationAt DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (UserEmail) REFERENCES Users(Email)
)

CREATE TABLE CartItems
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	CartId INT NOT NULL,
	FoodId INT NOT NULL,
	Quantity INT NOT NULL,
	Price DECIMAL(10,2) NOT NULL
	FOREIGN KEY(CartId) REFERENCES Carts(Id),
	FOREIGN KEY(FoodId) REFERENCES Foods(Id)
)



--para el usuario, uno obtiene el pin, y lo mete en la tabla de cuentas no verificadas
CREATE TRIGGER GetPIN
ON Users
AFTER INSERT
AS
BEGIN
	INSERT INTO UnverifiedAccounts(Email, Pin, Name)
	SELECT Email, Pin, Name
	FROM inserted
END

--despues de verificar la cuenta, borra de unverifiedAccounts --PENDIENTE DE ARREGLO
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
    DELETE FROM UnverifiedAccounts --AQUI TENGO QUE HACER QUE EVALUE EL ID DE LA CUENTA QUE SE VERIFICA, YA QUE SI HAY VARIAS CUENTAS SIN VERIFICAR, ENTONCES, SE BORRARAN TODAS
END

--SP PARA CERRAR O ABRIR EL RESTAURANTE

CREATE PROCEDURE SP_CloseRestaurants
AS
BEGIN
	UPDATE Restaurants
	SET Active = 
	CASE
		WHEN OpeningTime < ClosingTime AND CAST(GETDATE() AS TIME) BETWEEN OpeningTime AND ClosingTime THEN 1
		WHEN OpeningTime > ClosingTime AND (
			CAST(GETDATE() AS TIME) >= OpeningTime OR CAST(GETDATE() AS TIME) <= ClosingTime
		) THEN 1
		ELSE 0
	END
END

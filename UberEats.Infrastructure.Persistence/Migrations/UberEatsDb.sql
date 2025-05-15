CREATE DATABASE UberEatsDb

USE UberEatsDb

CREATE TABLE Users
(
	Id INT IDENTITY(1,1),
	Name VARCHAR(50) NOT NULL,
	Email VARCHAR(100) UNIQUE NOT NULL,
	IsActive BIT,
	PasswordHash NVARCHAR(MAX) NOT NULL,
	Pin VARCHAR(6) NOT NULL,
	Role VARCHAR(20),
	PRIMARY KEY(Id)
)

SELECT * FROM Users

DELETE FROM Users




CREATE TABLE UnverifiedAccounts
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Pin VARCHAR(6) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	Name VARCHAR(50) NOT NULL,
	--NO NECESITO EL HASH AQUI
)

SELECT * FROM UnverifiedAccounts

DELETE FROM UnverifiedAccounts
WHERE Email = 'zarcort242@gmail.com'


CREATE TABLE Categories
(
	Id  INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(50),
	ImgUrl VARCHAR(MAX)
)

/*
INSERT INTO Categories(Name, ImgUrl) VALUES ('Comida Japonesa', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS3jj8mQv9ibd_Ta7HrhrhTHRA1HDW7jQXZmQ&s'),
('Comida Dominicana', 'https://tuguiadominicana.com/wp-content/uploads/2023/02/plato-tipico-domonicano-@yolandazarzuela-edited.jpg'),
('Bebidas', 'https://megaadventuresdr.com/wp-content/uploads/2019/03/Best-Drinks-1.jpg'),
('Comida Rapida', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR9-YpXE9XEjQNzTd5r12q0j-YEgKzRfN2Q_w&s')
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
	CONSTRAINT FK_Restaurants_Categories FOREIGN KEY (CategoryId)
        REFERENCES Categories(Id) ON DELETE CASCADE

)

SELECT * FROM Restaurants

INSERT INTO Restaurants(Name, Description, CategoryId, OpeningTime, ClosingTime, DeliveryAvailable, Active, ImgUrl)
VALUES
('Burger King', 'Restaurante de comida rápida especializado en hamburguesas, papas fritas y batidos.', 4, '10:00', '23:00', 1, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT7-bvtsGpW8lJ8BY0T2320dA9uiXs6P-p7Lg&s'),
('McDonalds', 'Cadena global de comida rápida conocida por sus hamburguesas, papas fritas y batidos.', 4, '09:00', '22:00', 1, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTb80gfq6gYMsparrGJ5Nxfuz9dDm-fD-D1XA&s'),
('Taco Bell', 'Comida rápida especializada en tacos, burritos y otros platos mexicanos.', 4, '11:00', '23:00', 1, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRUVW9S30tBmfikQiY3tKn4Qf3692-PzE04kA&s'),
('KFC', 'Restaurante de pollo frito con acompañamientos como puré de papas, ensalada y pan.', 4, '10:30', '23:30', 1, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQhJsEkx_boO6xhK6Sz2bbtLFcMUKaQDOg5LA&s'),
('Subway', 'Comida rápida especializada en sandwiches con una variedad de ingredientes frescos.', 4, '09:00', '22:00', 1, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQteUIpsN4-OytUhZNrH1Xei_Q4E1u1Z-RcLQ&s')


INSERT INTO Restaurants(Name, Description, CategoryId, OpeningTime, ClosingTime, DeliveryAvailable, Active, ImgUrl)
VALUES
('Sakura Sushi', 'Restaurante japonés especializado en sushi, sashimi y tempura.', 1, '12:00', '22:00', 1, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR1SpCk5-NYZliqe_2bv4zxzXJJfanZzEQiuw&s'),
('Tokyo Express', 'Comida rápida japonesa con opciones como ramen y donburi.', 1, '11:30', '21:30', 1, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRavQbBjxi-rlvmamuFOO3vdkxPPRVdVFjSYw&s'),
('Nippon House', 'Restaurante elegante con platos tradicionales japoneses.', 1, '13:00', '23:00', 1, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR_d0m-WfaaiOr5p4bVi6Ubajd2jEV17Odwhw&s'),
('Samurai Grill', 'Fusión japonesa con teppanyaki y yakitori.', 1, '12:00', '22:30', 1, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTriOyUp8gQMS4ER3uLLUbpnSq7I3EDUE0pxw&s'),
('Wasabi Corner', 'Lugar acogedor con menú japonés tradicional y moderno.', 1, '11:00', '21:45', 1, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSTg4N9elkgfjOr6OCr79bNtxThfI6HcrOW_A&s');

CREATE TABLE Foods
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(100),
	Description VARCHAR(MAX),
	Price DECIMAL(10,2),
	RestaurantId INT,
	Active BIT,
	ImgUrl VARCHAR(MAX),
	CONSTRAINT FK_Foods_Restaurants FOREIGN KEY (RestaurantId)
        REFERENCES Restaurants(Id) ON DELETE CASCADE
)

SELECT * FROM Foods

/*
INSERT INTO Foods (Name, Description, Price, RestaurantId, Active, ImgUrl)
VALUES
('Hamburguesa Clásica', 'Hamburguesa con carne de res, queso, lechuga, tomate y mayonesa.', 8.50, 4, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSXuYk7rahziwq_gxtaQuTOMrAISJr3gFcHCg&s'),
('Papas Fritas', 'Papas fritas crujientes servidas con ketchup y mayonesa.', 3.00, 6, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSv_T8aU39CEZuT0wkdzrsp7rJPz2d2r8Vw3Q&s'),
('Hot Dog', 'Pan de hot dog con salchicha, mostaza, ketchup y cebolla.', 5.00, 8, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSxpflsD0jMI0GAphrYu-UM5N5JTwwatec-RQ&s'),
('Tacos', 'Tortillas de maíz rellenas de carne de res, pollo o cerdo, cebolla, cilantro y salsa.', 7.00, 6, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQppb6fWNQFq8_FtUkllEyATzWrq6l9ooJJrA&s'),
('Pizza Margarita', 'Pizza con salsa de tomate, queso mozzarella y albahaca fresca.', 12.00, 8, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQTUVlOnkavE0RG6TiM9LEyDZ7nzZ9kNA8jOQ&s')
*/


CREATE TABLE Carts
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL,
	CreationAt DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (UserId) REFERENCES Users(Id)
)
SELECT * FROM Carts

INSERT INTO Carts (UserId)
VALUES (1)

INSERT INTO CartItems (CartId, FoodId, Quantity, Price)
VALUES (2, 8, 2, 10.50)

CREATE TABLE CartItems
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	CartId INT NOT NULL,
	FoodId INT NOT NULL,
	Quantity INT NOT NULL CHECK (Quantity > 0),
	Price DECIMAL(10,2) NOT NULL,
	ImgUrl VARCHAR(MAX)
	FOREIGN KEY(CartId) REFERENCES Carts(Id) ON DELETE CASCADE,
	FOREIGN KEY(FoodId) REFERENCES Foods(Id)
)

SELECT * FROM CartItems

UPDATE CartItems
SET ImgUrl  = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQppb6fWNQFq8_FtUkllEyATzWrq6l9ooJJrA&s'
WHERE Id = 2

CREATE TABLE Payments
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	PaymentName VARCHAR(50) --Efectivo -Tarjeta -Paypal
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

/*
SELECT name
FROM sys.triggers
WHERE parent_id = OBJECT_ID('dbo.Users') -- esto busca los triggers que hay en X tabla
*/
--DROP TRIGGER dbo.changeStatus --con esto podemos borrar un trigger

--despues de verificar la cuenta, borra de unverifiedAccounts --PENDIENTE DE ARREGLO
CREATE TRIGGER changeStatus
ON UnverifiedAccounts
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

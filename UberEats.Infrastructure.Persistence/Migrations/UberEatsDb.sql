CREATE DATABASE UberEatsDb

USE UberEatsDb

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
	IsActive BIT,
    Role NVARCHAR(50) NOT NULL -- Ej: "Cliente", "Repartidor", "Admin"
)

select * from Users

delete from Users



CREATE TABLE Restaurants (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Adress NVARCHAR(200) NOT NULL
)

CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
)

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    CategoryId INT NOT NULL,
    RestaurantId INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    FOREIGN KEY (RestaurantId) REFERENCES Restaurants(Id)
)

CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    RestaurantId INT NOT NULL,
    DateOrder DATETIME NOT NULL DEFAULT GETDATE(),
    Status NVARCHAR(50) NOT NULL, -- Ej: "Pendiente", "Preparando", "En camino", "Entregado"
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (RestaurantId) REFERENCES Restaurants(Id)
)

CREATE TABLE OrderDetails (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
)

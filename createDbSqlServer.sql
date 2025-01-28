CREATE DATABASE RestauranteDB;

SELECT name, database_id, create_date 
FROM sys.databases 
WHERE name = 'RestauranteDB';

USE RestauranteDB;

-- Plato principal
CREATE TABLE PlatoPrincipal (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0),
    Ingredientes NVARCHAR(MAX) NOT NULL
);

INSERT INTO PlatoPrincipal (Nombre, Precio, Ingredientes)
VALUES 
('Plato combinado', 12.50, 'Pollo, patatas, tomate'),
('Plato vegetariano', 10.00, 'Tofu, verduras, arroz');

SELECT * FROM PlatoPrincipal;

SELECT * 
FROM PlatoPrincipal
WHERE Ingredientes LIKE '%Tofu%';

SELECT * 
FROM PlatoPrincipal
ORDER BY Precio ASC;

SELECT DISTINCT Nombre, Precio FROM PlatoPrincipal;






-- Postre
CREATE TABLE Postre (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0),
    Calorias INT NOT NULL CHECK (Calorias >= 0)
);


INSERT INTO Postre (Nombre, Precio, Calorias)
VALUES 
('Tarta de manzana', 4.50, 350),
('Helado de chocolate', 3.00, 250);


SELECT * FROM Postre;


SELECT * 
FROM Postre
WHERE Calorias > 300;


SELECT * 
FROM Postre
ORDER BY Precio ASC;


SELECT DISTINCT Nombre, Precio FROM Postre;





-- Bebida
CREATE TABLE Bebida (
    Id INT IDENTITY(1,1) PRIMARY KEY,  
    Nombre NVARCHAR(100) NOT NULL,  
    Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0), 
    EsAlcoholica BIT NOT NULL         
);


INSERT INTO Bebida (Nombre, Precio, EsAlcoholica)
VALUES 
('Cerveza', 3.50, 1),         
('Zumo de naranja', 2.00, 0); 


SELECT * FROM Bebida;


SELECT * 
FROM Bebida
WHERE EsAlcoholica = 1;


SELECT * 
FROM Bebida
ORDER BY Precio ASC;


SELECT DISTINCT Nombre, Precio 
FROM Bebida;

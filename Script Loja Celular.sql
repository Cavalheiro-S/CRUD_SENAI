CREATE DATABASE LojaCelular

go
USE LojaCelular

go
CREATE TABLE Logon
(
	Id INT PRIMARY KEY IDENTITY,
	NomeUsuario VARCHAR(100) NOT NULL,
	Usuario VARCHAR(20) NOT NULL,
	Senha VARCHAR(20) NOT NULL
)

go
CREATE TABLE Marca
(
	Codigo INT PRIMARY KEY ,
	Nome VARCHAR(50) NOT NULL
)

go
CREATE TABLE Produto
(
	Codigo INT NOT NULL,
	CodMarca INT NOT NULL,
	Modelo VARCHAR(50),
	SistOperacional VARCHAR(20),
	Armazenamento VARCHAR(10),
	Cor VARCHAR(20),
	Importacao BIT,
	Preco DECIMAL(10,2)
)
go
ALTER TABLE Produto ADD FOREIGN KEY (CodMarca) REFERENCES Marca(Codigo)
go
INSERT INTO Logon VALUES
('Brenda Borba', 'Brenda', '00000'),
('Ana Gusmão', 'Ana', '11111'),
('Beatryz Fagundes', 'Beatryz', '22222')
go
INSERT INTO Marca VALUES
(1,'Samsung'),
(2,'Motorola'),
(3,'LG'),
(4,'Nokia'),
(5,'Sony'),
(6,'Apple'),
(7,'Xiaomi'),
(8,'Asus'),
(9,'Redmi'),
(10,'Multilaser')
go
INSERT INTO Produto VALUES
('1010',6,'iPhone 11','iOS','64GB','Preto','0', 4399.00),
('1011',1,'Samsung Galaxi S10','Android','128GB','Branco','0',2789.07),
('1012',7,'Xiaomi Redmi Note 8','Android','64GB','Cinza','1',1388.00),
('1013',7,'Xiaomi Mi 8 Lite','Android','64GB','Preto','1',1296.20),
('1014',3,'LG K41S','Android','32GB','Titanium','0',939.00),
('1015',8,'ASUS Zenfone Max Pro M2','Android','64GB','Black Saphire','1',1499.00),
('1016',1,'Samsung Galaxy A31 Dual Chip','Android','128GB','Preto','0',1836.00),
('1017',1,'Samsung A21s','Android','64GB','Preto','0',1389.99),
('1018',6,'iPhone 8 Plus','iOS','128GB','Cinza Espacial','0',3599.00),
('1019',6,'iPhone XR','iOS','64GB','Branco','0',4499.00)


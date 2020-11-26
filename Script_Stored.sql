USE LojaCelular

IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME = 'sp_inserirProduto' AND xtype = 'p')
	DROP PROCEDURE sp_inserirProduto

GO
CREATE OR ALTER PROCEDURE sp_inserirProduto
(
	@Codigo INT,
	@Modelo VARCHAR(50),
	@SistOpera VARCHAR(20),
	@Armazenamento VARCHAR(10),
	@Preco DECIMAL(6,2),
	@Cor VARCHAR(10),
	@CodigoMarca INT,
	@Importacao BIT
	
)
AS

IF(@Codigo IS NULL OR EXISTS(SELECT Produto.Codigo FROM Produto WHERE Codigo = @Codigo))
BEGIN
	RAISERROR('Codigo não disponivel',16,1)
	
	RETURN - 1
END
IF(@CodigoMarca IS NULL)
BEGIN
	RAISERROR('Codigo da marca não pode ser  nulo',16,1)
	
	RETURN - 1
END

BEGIN TRAN

INSERT INTO Produto(Codigo, Modelo,SistOperacional,Armazenamento,Preco,Cor,CodMarca,Importacao)
VALUES(@Codigo, @Modelo, @SistOpera, @Armazenamento,@Preco, @Cor, @CodigoMarca, @Importacao)

IF(@@ERROR <> '')
	ROLLBACK TRAN
ELSE
	COMMIT TRAN

--PROCEDURE PARA ALTERAR DADOS
	IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME = 'sp_alterarProduto' AND xtype = 'p')
	DROP PROCEDURE sp_alterarProduto

GO
CREATE OR ALTER PROCEDURE sp_alterarProduto
(
	@Codigo INT,
	@Modelo VARCHAR(50),
	@SistOpera VARCHAR(20),
	@Armazenamento VARCHAR(10),
	@Preco DECIMAL(6,2),
	@Cor VARCHAR(10),
	@CodigoMarca INT,
	@Importacao BIT
	
)
AS

IF(@Codigo IS NULL )
BEGIN
	RAISERROR('Codigo não disponivel',16,1)
	
	RETURN - 1
END
IF(@CodigoMarca IS NULL)
BEGIN
	RAISERROR('Codigo da marca não pode ser  nulo',16,1)
	
	RETURN - 1
END

BEGIN TRAN

UPDATE Produto 
SET 
Modelo = @Modelo,
SistOperacional = @SistOpera,
Armazenamento = @Armazenamento,
Preco = @Preco,
Cor = @Cor,
CodMarca = @CodigoMarca,
Importacao = @Importacao
WHERE Codigo = @Codigo


IF(@@ERROR <> '')
	ROLLBACK TRAN
ELSE
	COMMIT TRAN

--------------------PROCEDURE PARA EXCLUIR DADOS
	IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME = 'sp_deletarProduto' AND xtype = 'p')
	DROP PROCEDURE sp_deletarProduto

GO
CREATE OR ALTER PROCEDURE sp_deletarProduto
(
	@Codigo INT
)
AS

IF(@Codigo IS NULL )
BEGIN
	RAISERROR('Codigo não disponivel',16,1)
	
	RETURN - 1
END


BEGIN TRAN

DELETE FROM Produto WHERE Codigo = @Codigo


IF(@@ERROR <> '')
	ROLLBACK TRAN
ELSE
	COMMIT TRAN



USE LojaCelular
	go
	CREATE VIEW vw_Produto

	as

	SELECT P.Codigo,P.Modelo,M.Nome[Marca] ,P.SistOperacional[Sistema Operacional], 
	P.Armazenamento, P.Cor, P.Preco[Pre�o], P.Importacao[Importa��o] 
	FROM Produto P
	INNER JOIN Marca M
	on P.CodMarca = M.Codigo
	





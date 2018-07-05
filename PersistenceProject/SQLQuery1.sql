create table Fornecedores
(
	ID int IDENTITY NOT NULL,
	Nome varchar(100) NOT NULL,
	CNPJ varchar(50) NOT NULL,

	primary key (ID)
);

create table Produtos
(
	ID int IDENTITY NOT NULL,
	Nome varchar(100) NOT NULL,
	Descricao varchar(100),
	PrecoDeCusto decimal(10,2),
	PrecoDeVenda decimal(10,2),
	Estoque int,

	primary key (ID)
);

create table Notas
(
	ID int IDENTITY NOT NULL,
	Numero varchar(100) NOT NULL,
	Fornecedor int,
	DataEmissao datetime,
	DataEntrada datetime

	primary key (ID),
	FOREIGN KEY (Fornecedor) REFERENCES Fornecedores(ID)
);

select * from Fornecedores
select * from Produtos

truncate table Fornecedores

insert into Fornecedores values ('Fernando', '123'); SELECT SCOPE_IDENTITY();
insert into Fornecedores values ('Osni', '567');

INSERT INTO Fornecedores VALUES (@Nome, @CNPJ); SELECT SCOPE_IDENTITY();

update Fornecedores set Nome = 'Fernando123' where ID = 1;
update Fornecedores set Nome = 'Osni123', CNPJ = '567123' where ID = 3;
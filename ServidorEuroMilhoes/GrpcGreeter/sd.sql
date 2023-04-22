use EuroMilhoes

create table ApostasAtuais(
	nif int NOT NULL,
	numeros  varchar(20) NOT NULL,
	estrelas varchar(10)NOT NULL,
	DataAposta DATETIME DEFAULT GETDATE() NOT NULL,
	SorteioAtual BIT DEFAULT(1) NOT NULL,
	PRIMARY KEY(nif,DataAposta),
);

create table ApostasArquivadas(
	nif int NOT NULL,
	numeros  varchar(20) NOT NULL,
	estrelas varchar(10) NOT NULL,
	DataAposta DATETIME NOT NULL,
	PRIMARY KEY(nif,DataAposta),
);

-- Scaffold-DbContext -Connection name="EuroMilhoesContext" Microsoft.EntityFrameworkCore.SqlServer –Context EuroMilhoesContext -ContextDir Data -OutputDir Models -DataAnnotations
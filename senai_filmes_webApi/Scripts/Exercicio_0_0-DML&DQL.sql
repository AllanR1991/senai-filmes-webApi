use Filme;

insert into Genero(Genero) values
	('A��o'),
	('Terror'),
	('Com�dia'),
	('Suspense')

insert into Filme(IdGenero,Filme) values
	(2,'O Exorcista do Papa'),
	(1,'John Wick 4: Baba Yaga'),
	(4,'Alerta M�ximo'),
	(3,'Mist�rio em Paris')

insert into Acesso values
	('Administrador'),
	('Comun')

insert into Usuario values
	(1,'admin@admin.com.br','admin'),
	(2,'allan@allan.com.br','allan')

select * from Filme;
select * from Genero;

SELECT Usuario.IdUsuario, Usuario.Email, Usuario.Senha, Acesso.Acesso  FROM Usuario
   LEFT JOIN Acesso on Acesso.IdAcesso = Usuario.IdAcesso
    WHERE Email = 'allan@allan.com.br' AND Senha = 'allan';
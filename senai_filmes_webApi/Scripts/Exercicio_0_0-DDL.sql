--Comandos DDL -> Data Definition Language
create database Filme;

use Filme;

create table Genero(
	IdGenero int primary key identity,
	Genero varchar(50) not null,
)

create table Filme(
	IdFilme int primary Key identity,
	IdGenero int foreign key references Genero(IdGenero),
	Filme varchar(100) not null,
)

Create table Acesso (
	IdAcesso int primary key identity,
	Acesso varchar(100)
)

create table Usuario(
	IdUsuario int primary Key identity,
	IdAcesso int foreign key references Acesso(IdAcesso),
	Email varchar(250) not null unique,
	Senha varchar(250) not null
)


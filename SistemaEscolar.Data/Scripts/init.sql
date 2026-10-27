--CREATE DATABASE SistemaEscolar;
--USE SistemaEscolar;

CREATE TABLE Periodos (
	id int primary key identity(1,1),
	fechaInicio date not null,
	fechaFin date not null,
	nombre varchar(50) not null,
	estatus bit default 1,
	createdAt datetime default sysdatetime()
);

CREATE TABLE Carreras (
	id int primary key identity(1,1),
	clave varchar(10) not null,
	nombre varchar(100) not null,
	duracion int not null,
	estatus bit default 1,
	createdAt datetime default sysdatetime()
);

CREATE TABLE Materias (
	id int primary key identity (1,1),
	nombre varchar(100) not null,
	creditos int,
	semestre int,
	horas int,
	estatus bit default 1,
	createdAt datetime default sysdatetime()
);

CREATE TABLE Usuarios (
	id int primary key identity(1,1),
	username varchar(50) not null unique,
	password varchar(255) not null,
	estatus bit default 1,
	createdAt datetime default sysdatetime()
);

CREATE TABLE Docentes (
	id int primary key identity(1,1),
	nombre varchar(100) not null,
	apellidoPaterno varchar(50) not null,
	apellidoMaterno varchar(50) not null,
	fechaNacimiento date,
	sexo char(1),
	especialidad varchar(100),
	fechaIngreso date,
	sueldo decimal(10,2),
	unidadAcademica varchar(100),
	estatus bit default 1,
	createdAt datetime default sysdatetime()
);

CREATE TABLE Alumnos (
	id int primary key identity(1,1),
	matricula varchar(10),
	nombre varchar(100) not null,
	apellidoPaterno varchar(50) not null,
	apellidoMaterno varchar(50) not null,
	fechaNacimiento date,
	fechaIngreso date,
	sexo char(1),
	estatus bit default 1,
	createdAt datetime default sysdatetime()
);
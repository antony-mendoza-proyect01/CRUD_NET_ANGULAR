
create database CRUD_PELICULA

use CRUD_PELICULA

create table Genero(
IdGenero int primary key identity, 
Nombre varchar(50),
FechaCreacion datetime default getdate()
)



create table Pelicula (
IdPelicula int primary key identity,
NombrePelicula varchar(50),
IdGenero int references Genero(IdGenero),
FechaCreacion datetime default getdate()
)


insert into Genero(Nombre) values
('Terror'),
('Accion'),
('Aventura'),
('Drama'),
('Comedia')

insert into Pelicula (NombrePelicula,IdGenero,FechaCreacion) values
('Terror en la jungla',2, GETDATE())


select * from Pelicula

select * from Genero

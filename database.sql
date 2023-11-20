CREATE DATABASE Examen;
USE Examen
GO

CREATE TABLE Usuarios(
UsuarioID int identity(1,1) PRIMARY KEY,
Nombre varchar(50) NOT NULL,
CorreoElectronico varchar(50),
Telefono varchar(15) unique
);

CREATE TABLE Equipos(
EquipoID int identity(1,1) PRIMARY KEY,
TipoEquipo varchar(50) NOT NULL,
Modelo varchar(50),
UsuarioID INT,
CONSTRAINT fk_usuario FOREIGN KEY(UsuarioID) REFERENCES Usuarios(UsuarioID),
);

CREATE TABLE Tecnicos(
TecnicoID int identity(1,1) PRIMARY KEY,
Nombre varchar(50) NOT NULL,
Especialidad  varchar(50)
);

CREATE TABLE Reparaciones(
ReparacionID int identity(1,1) PRIMARY KEY,
EquipoID INT,
FechaSolicitud DATETIME,
Estado CHAR,
CONSTRAINT fk_equipo FOREIGN KEY(EquipoID) REFERENCES Equipos(EquipoID)
);

CREATE TABLE DetallesReparacion(
DetalleID int identity(1,1) PRIMARY KEY,
ReparacionID INT,
Descripcion VARCHAR(50),
FechaInicio DATETIME,
FechaFin DATETIME,
CONSTRAINT fk_reparacion FOREIGN KEY(ReparacionID) REFERENCES Reparaciones(ReparacionID)
);

CREATE TABLE Asignaciones(
AsignacionID int identity(1,1) PRIMARY KEY,
ReparacionID INT,
TecnicoID INT,
FechaAsignacion DATETIME,
CONSTRAINT fk_reparacion_asignaciones FOREIGN KEY(ReparacionID) REFERENCES Reparaciones(ReparacionID),
CONSTRAINT fk_tecnico FOREIGN KEY(TecnicoID) REFERENCES Tecnicos(TecnicoID)
);
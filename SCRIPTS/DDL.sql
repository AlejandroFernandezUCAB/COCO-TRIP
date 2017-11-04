CREATE ROLE admin_cocotrip WITH LOGIN CREATEDB PASSWORD 'ds1718a';
CREATE DATABASE cocotrip WITH OWNER = admin_cocotrip ENCODING = UTF8;

--Creates Tables
--Modulo 1
--Fin de modulo 
--Modulo 2
CREATE TABLE Preferencia(
   pr_usuario   int not null,
    pr_categoria int not null,
    CONSTRAINT pk_usuario PRIMARY KEY (pr_usuario, pr_categoria) 
);
--Fin de modulo 
--Modulo 3
--Fin de modulo 
--Modulo 4
--Fin de modulo 
--Modulo 5
--Fin de modulo 
--Modulo 6
--Fin de modulo 
--Modulo 7
/**
Tablas del Modulo (7) de Gestion de Lugares Turisticos y
 Actividades en Lugares Turiticos

Autores:
  Camacho Joaquin
  Herrera Jose
  Quiroga Sabina
**/

CREATE TABLE Lugar_Turistico
(
  lu_id integer CONSTRAINT pk_lugar_turistico PRIMARY KEY,
  lu_nombre varchar(400) CONSTRAINT nn_lu_nombre NOT NULL,
  lu_costo decimal CONSTRAINT nn_lu_costo NOT NULL,
  lu_descripcion varchar(2000) CONSTRAINT nn_lu_descripcion NOT NULL,
  lu_direccion varchar(2000) CONSTRAINT nn_lu_descripcion NOT NULL,
  lu_correo varchar(320) CONSTRAINT nn_lu_correo NOT NULL,
  lu_telefono bigint CONSTRAINT nn_lu_telefono NOT NULL,
   -- Coordenadas GPS (Google Maps)
  lu_latitud decimal CONSTRAINT nn_lu_latitud NOT NULL,
  lu_longitud decimal CONSTRAINT nn_lu_longitud NOT NULL,
  -- Coordenadas GPS (Google Maps)
  lu_activar boolean DEFAULT true CONSTRAINT nn_lu_activar NOT NULL
  -- Faltan los FOREIGN KEY de Categorias y Sub_Categorias
);

CREATE TABLE Actividad
(
  ac_id integer,
  ac_foto bytea CONSTRAINT nn_ac_foto NOT NULL,
  ac_nombre varchar(400) CONSTRAINT nn_ac_nombre NOT NULL,
  ac_duracion time CONSTRAINT nn_ac_duracion NOT NULL,
  ac_descripcion varchar(2000) CONSTRAINT nn_ac_descripcion NOT NULL,
  ac_activar boolean DEFAULT true CONSTRAINT nn_ac_activar NOT NULL,
  fk_ac_lugar_turistico integer CONSTRAINT fk_ac_lugar_turistico REFERENCES Lugar_Turistico(lu_id),
  CONSTRAINT pk_actividad PRIMARY KEY(fk_ac_lugar_turistico,ac_id)
);

CREATE TABLE LT_Horario
(
  ho_id integer,
  ho_dia_semana integer CONSTRAINT nn_ho_dia_semana NOT NULL,
  ho_hora_apertura time CONSTRAINT nn_ho_hora_apertura NOT NULL,
  ho_hora_cierre time CONSTRAINT nn_ho_hora_cierre NOT NULL,
  fk_ho_lugar_turistico integer CONSTRAINT fk_ho_lugar_turistico REFERENCES Lugar_Turistico(lu_id),
  CONSTRAINT pk_horario PRIMARY KEY(fk_ho_lugar_turistico, ho_id)
);

CREATE TABLE LT_Foto
(
  fo_id integer,
  fo_byte bytea CONSTRAINT nn_fo_byte NOT NULL,
  fk_fo_lugar_turistico integer CONSTRAINT fk_fo_lugar_turistico REFERENCES Lugar_Turistico(lu_id),
  CONSTRAINT pk_foto PRIMARY KEY(fk_fo_lugar_turistico, fo_id)
);
--Fin de modulo 
--Modulo 8
--Fin de modulo 
--Modulo 9
--Fin de modulo  

--ALTERS
--Modulo 1
--Fin de modulo
--Modulo 2
--Fin de modulo 
--Modulo 3
--Fin de modulo 
--Modulo 4
--Fin de modulo 
--Modulo 5
--Fin de modulo 
--Modulo 6
--Fin de modulo 
--Modulo 7
--Fin de modulo 
--Modulo 8
--Fin de modulo 
--Modulo 9
--Fin de modulo

--SEQUENCES
--Modulo 1
--Fin de modulo
--Modulo 2
--Fin de modulo 
--Modulo 3
--Fin de modulo 
--Modulo 4
--Fin de modulo 
--Modulo 5
--Fin de modulo 
--Modulo 6
--Fin de modulo 
--Modulo 7
/**
Secuencias de las Tablas
**/

CREATE SEQUENCE SEQ_Lugar_Turistico;
CREATE SEQUENCE SEQ_Actividad;
CREATE SEQUENCE SEQ_LT_Horario;
CREATE SEQUENCE SEQ_LT_Foto;
--Fin de modulo 
--Modulo 8
--Fin de modulo 
--Modulo 9
--Fin de modulo 

--INDEX
--Modulo 1
--Fin de modulo
--Modulo 2
--Fin de modulo 
--Modulo 3
--Fin de modulo 
--Modulo 4
--Fin de modulo 
--Modulo 5
--Fin de modulo 
--Modulo 6
--Fin de modulo 
--Modulo 7
/**
Index de las tablas
**/

CREATE INDEX IX_LUGAR_TURISTICO ON lugar_turistico (lu_id);
CREATE INDEX IX_ACTIVIDAD ON actividad (fk_ac_lugar_turistico, ac_id);
CREATE INDEX IX_LT_HORARIO ON lt_horario (fk_ho_lugar_turistico, ho_id);
CREATE INDEX IX_LT_FOTO ON lt_foto (fk_fo_lugar_turistico, fo_id);
--Fin de modulo 
--Modulo 8
--Fin de modulo 
--Modulo 9
--Fin de modulo 

--Fin Creates tables
=======
CREATE ROLE admin_cocotrip WITH LOGIN CREATEDB PASSWORD 'ds1718a';
CREATE DATABASE cocotrip WITH OWNER = admin_cocotrip ENCODING = UTF8;
--DROPS
--Modulo 3
Drop table Miembro;
Drop table Amigo;
Drop table Grupo;
Drop table Usuario;
Drop SEQUENCE SEQ_Grupo;
Drop SEQUENCE SEQ_Miembro;
Drop SEQUENCE SEQ_Amigo;
drop SEQUENCE SEQ_Usuario;
--Fin de modulo
--Creates Tables

--Modulo 1
CREATE TABLE USUARIO (
    us_id		integer,
    us_nombreUsuario 	varchar(20) DEFAULT '' UNIQUE,
    us_nombre         	varchar(30) NOT NULL,
    us_apellido        	varchar(30) NOT NULL,
    us_fechaNacimiento  date NOT NULL,
    us_genero		varchar(1) CHECK (us_genero ='M' OR us_genero='F'),
    us_email	        varchar(30) NOT NULL UNIQUE,
    us_password         varchar(20)DEFAULT '',
    us_foto		bytea,
    us_validacion	boolean NOT NULL,
    CONSTRAINT primaria_usuario PRIMARY KEY(us_id)
    
);
--Fin de modulo 
--Modulo 2
CREATE TABLE Preferencia(
    pr_usuario   int NOT NULL,
    pr_categoria int NOT NULL,
    CONSTRAINT pk_usuario PRIMARY KEY (pr_usuario, pr_categoria) ,
    CONSTRAINT fk_usuario FOREIGN KEY (pr_usuario) REFERENCES USUARIO (us_id)
);
--Fin de modulo 
--Modulo 3
Create Table Grupo
(
gr_id int NOT NULL,
gr_nombre varchar(100) NOT NULL,
gr_foto bytea,
fk_usuario int NOT NULL,

CONSTRAINT pk_grupo PRIMARY KEY (gr_id),
CONSTRAINT fk_grupo_usuario FOREIGN KEY (fk_usuario) References Usuario(us_id)
);

Create Table Miembro
(
mi_id int NOT NULL,
fk_grupo int NOT NULL,
fk_usuario int NOT NULL,

CONSTRAINT pk_miembro PRIMARY KEY (mi_id),
CONSTRAINT fk_miembro_grupo FOREIGN KEY (fk_grupo) References Grupo(gr_id),
CONSTRAINT fk_miembro_usuario FOREIGN KEY (fk_usuario) References Usuario(us_id)
);

Create Table Amigo
(
am_id int NOT NULL,
fk_usuario_conoce int NOT NULL,
fk_usuario_posee int NOT NULL,

CONSTRAINT pk_amigo PRIMARY KEY (am_id),
CONSTRAINT fk_amigo_usuario_conoce FOREIGN KEY (fk_usuario_conoce) References Usuario(us_id),
CONSTRAINT fk_amigo_usuario_posee FOREIGN KEY (fk_usuario_posee) References Usuario(us_id)
);
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
create table evento
(
	ev_id int primary key,
	ev_nombre varchar(100) not null,
	ev_descripcion varchar(500),
	ev_precio int,
	ev_fecha_inicio timestamp,
	ev_fecha_fin timestamp,
	ev_hora_inicio time,
	ev_hora_fin time,
	ev_foto bytea,
	ev_localidad int,
	ev_categoria int
);


create table localidad
(
	lo_id int primary key,
	lo_nombre varchar(200),
	lo_descripcion varchar(500),
	lo_lugar varchar (500)
);
--Fin de modulo 
--Modulo 9
CREATE TABLE categoria 
(
  ca_id integer NOT NULL,
  ca_nombre character varying(20) not null,
  ca_descripcion character varying(100) not null,
  ca_status boolean not null,
  ca_fkcategoriasuperior integer,
  ca_nivel integer
);
--Fin de modulo 9

--ALTERS
--Modulo 1
--Fin de modulo
--Modulo 2
alter table preferencia add constraint fk_categoria foreign key (pr_categoria) references categoria (ca_id);
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
alter table evento add constraint fk_categoria foreign key (ev_categoria) references categoria (ca_id);
alter table evento add constraint fk_localidad foreign key (ev_localidad) references localidad (lo_id);
--Fin de modulo 
--Modulo 9

ALTER TABLE ONLY categoria ADD CONSTRAINT categoria_pkey PRIMARY KEY (ca_id);
ALTER TABLE ONLY categoria ADD CONSTRAINT pk_categoriapadre FOREIGN KEY (ca_fkcategoriasuperior) REFERENCES categoria(ca_id);

--Fin de modulo

--SEQUENCES
--Modulo 1
CREATE SEQUENCE SEQ_Usuario;
--Fin de modulo
--Modulo 2
--Fin de modulo 
--Modulo 3
CREATE SEQUENCE SEQ_Grupo;
CREATE SEQUENCE SEQ_Miembro;
CREATE SEQUENCE SEQ_Amigo;
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
CREATE SEQUENCE SEQ_Categoria
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

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
CREATE SEQUENCE SEQ_Evento

	START WITH 1

	INCREMENT BY 1

	NO MINVALUE

	NO MAXVALUE

	CACHE 1;


CREATE SEQUENCE SEQ_Localidad

	START WITH 1

	INCREMENT BY 1

	NO MINVALUE
	NO MAXVALUE
	CACHE 1;
--Fin de modulo 
--Modulo 9
--Fin de modulo 

--Fin Creates tables
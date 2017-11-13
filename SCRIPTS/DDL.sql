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
    us_nombreUsuario 	varchar(20)  UNIQUE,
    us_nombre         	varchar(30) NOT NULL,
    us_apellido        	varchar(30) NOT NULL,
    us_fechaNacimiento  date,
    us_genero		varchar(1) CHECK (us_genero ='M' OR us_genero='F'),
    us_email	        varchar(30) NOT NULL UNIQUE,
    us_password         varchar(20),
    us_foto		varchar(100),
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
CONSTRAINT fk_grupo_usuario FOREIGN KEY (fk_usuario) References Usuario(us_id) on delete cascade
);

Create Table Miembro
(
mi_id int NOT NULL,
fk_grupo int NOT NULL,
fk_usuario int NOT NULL,

CONSTRAINT pk_miembro PRIMARY KEY (mi_id),
CONSTRAINT fk_miembro_grupo FOREIGN KEY (fk_grupo) References Grupo(gr_id) on delete cascade,
CONSTRAINT fk_miembro_usuario FOREIGN KEY (fk_usuario) References Usuario(us_id) on delete cascade
);

Create Table Amigo
(
am_id int NOT NULL,
fk_usuario_conoce int NOT NULL,
fk_usuario_posee int NOT NULL,

CONSTRAINT pk_amigo PRIMARY KEY (am_id),
CONSTRAINT fk_amigo_usuario_conoce FOREIGN KEY (fk_usuario_conoce) References Usuario(us_id) on delete cascade,
CONSTRAINT fk_amigo_usuario_posee FOREIGN KEY (fk_usuario_posee) References Usuario(us_id) on delete cascade
);

--Fin de modulo
--Modulo 4
--Fin de modulo
--Modulo 5
CREATE TABLE Itinerario
(
    it_id integer NOT NULL,
    it_nombre character varying(80) NOT NULL,
    it_fechaInicio date,
    it_fechaFin date,
    it_visible boolean DEFAULT true NOT NULL,
    it_idUsuario integer NOT NULL,
    CONSTRAINT pk_Itinerario PRIMARY KEY (it_id),
    CONSTRAINT fk_idUsuario FOREIGN KEY (it_idUsuario)
        REFERENCES Usuario (us_id) MATCH SIMPLE
        ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE Agenda
(
    ag_id integer NOT NULL,
    ag_idItinerario integer NOT NULL,
    ag_fechaInicio date NOT NULL,
    ag_fechaFin date NOT NULL,
    ag_idLugarTuristico integer,
    ag_idActividad integer,
    ag_idEvento integer,
    ag_fk_lugar_turistico integer,
    CONSTRAINT pk_Agenda PRIMARY KEY (ag_id),
    CONSTRAINT fk_idItinerario FOREIGN KEY (ag_idItinerario)
        REFERENCES Itinerario (it_id) MATCH SIMPLE
        ON UPDATE NO ACTION ON DELETE CASCADE
);
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
  -- Faltan las Categorias y Sub_Categorias
);

CREATE TABLE Actividad
(
  ac_id integer CONSTRAINT pk_actividad PRIMARY KEY,
  ac_foto varchar (320) CONSTRAINT nn_ac_foto NOT NULL,
  ac_nombre varchar(400) CONSTRAINT nn_ac_nombre NOT NULL,
  ac_duracion time CONSTRAINT nn_ac_duracion NOT NULL,
  ac_descripcion varchar(2000) CONSTRAINT nn_ac_descripcion NOT NULL,
  ac_activar boolean DEFAULT true CONSTRAINT nn_ac_activar NOT NULL,
  fk_ac_lugar_turistico integer CONSTRAINT fk_ac_lugar_turistico REFERENCES Lugar_Turistico(lu_id)
);

CREATE TABLE LT_Horario
(
  ho_id integer CONSTRAINT pk_horario PRIMARY KEY,
  ho_dia_semana integer CONSTRAINT nn_ho_dia_semana NOT NULL,
  ho_hora_apertura time CONSTRAINT nn_ho_hora_apertura NOT NULL,
  ho_hora_cierre time CONSTRAINT nn_ho_hora_cierre NOT NULL,
  fk_ho_lugar_turistico integer CONSTRAINT fk_ho_lugar_turistico REFERENCES Lugar_Turistico(lu_id)
);

CREATE TABLE LT_Foto
(
  fo_id integer CONSTRAINT pk_foto PRIMARY KEY,
  fo_ruta varchar (320) CONSTRAINT nn_fo_ruta NOT NULL,
  fk_fo_lugar_turistico integer CONSTRAINT fk_fo_lugar_turistico REFERENCES Lugar_Turistico(lu_id)
);

--Tabla interseccion entre Lugar_Turistico y Categoria--
--Nota: Antes de crear la tabla es necesario tener creada la tabla Categoria y Lugar_Turistico
CREATE TABLE LT_C
(
  id_lugar_turistico integer CONSTRAINT fk_lt_c_lugar_turistico REFERENCES Lugar_Turistico(lu_id),
  id_categoria integer CONSTRAINT fk_lt_c_categoria REFERENCES categoria(ca_id),
  id_categoria_superior integer DEFAULT 0 CONSTRAINT nn_lt_c_categoria_superior NOT NULL,
  categoria_nombre VARCHAR(500) CONSTRAINT nn_lt_c_categoria_nombre NOT NULL,
  CONSTRAINT pk_lt_c PRIMARY KEY (id_lugar_turistico, id_categoria)
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
	ev_foto varchar,
	ev_localidad int,
	ev_categoria int
);
create table localidad(
	lo_id int primary key,
	lo_nombre varchar(200),
	lo_descripcion varchar(500),
	lo_coord_x int,
  lo_coord_y int
);
--Fin de modulo
--Modulo 9
CREATE TABLE categoria
(
  ca_id integer UNIQUE NOT NULL,
  ca_nombre character varying(500) not null,
  ca_descripcion character varying(2000) not null,
  ca_status boolean default true not null,
  ca_fkcategoriasuperior integer,
  ca_nivel integer
);
--Fin de modulo 9

--ALTERS
--Modulo 1
--Fin de modulo
--Modulo 2
--alter table preferencia add constraint fk_categoria foreign key (pr_categoria) references categoria (ca_id);
--Fin de modulo
--Modulo 3
--Fin de modulo
--Modulo 4
--Fin de modulo
--Modulo 5
ALTER TABLE Agenda add CONSTRAINT fk_idLugarTuristico FOREIGN KEY (ag_idLugarTuristico) REFERENCES lugar_turistico (lu_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE CASCADE;
ALTER TABLE Agenda add CONSTRAINT fk_idActividad FOREIGN KEY (ag_fk_lugar_turistico, ag_idActividad) REFERENCES Actividad (fk_ac_lugar_turistico,ac_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE CASCADE;
ALTER TABLE Agenda add CONSTRAINT fk_idEvento FOREIGN KEY (ag_idEvento) REFERENCES Evento (ev_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE CASCADE;

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
CREATE SEQUENCE SEQ_Itinerario;
CREATE SEQUENCE SEQ_Agenda;
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
CREATE SEQUENCE SEQ_Evento;
CREATE SEQUENCE SEQ_Localidad;
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
CREATE INDEX IX_Itinerario ON itinerario (it_id);
CREATE INDEX IX_Agenda ON Agenda (ag_id);
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
GRANT ALL PRIVILEGES ON TABLE categoria TO admin_cocotrip;
--Fin de modulo
--Fin Creates tables

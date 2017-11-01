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
)
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
--Fin Creates tables
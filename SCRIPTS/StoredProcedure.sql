/**
Procedimientos del Modulo (1) de Login de Usuario, Registro de Usuario y Home
Autores:
  Carlos Valero
  Pedro Garcia
  Homero Manrique
**/

/*INSERT*/
-- Inserta el Usuario
-- devuelve el id del usuario
CREATE OR REPLACE FUNCTION InsertarUsuario
(_nombreUsuario VARCHAR(20), _nombre VARCHAR(30),
 _apellido VARCHAR(30), _fechaNacimiento date,
 _genero VARCHAR(1), _correo VARCHAR(30),
 _clave VARCHAR(20), _foto VARCHAR(100))
RETURNS integer AS
$$
BEGIN

   INSERT INTO usuario VALUES
    (nextval('seq_Usuario'), _nombreUsuario, _nombre, _apellido, _fechaNacimiento, _genero, _correo, _clave, _foto, false);

   RETURN currval('seq_Usuario');

END;
$$ LANGUAGE plpgsql;

-- Inserta el usuario con los datos de Facebook
-- Devuelve el id del usuario
CREATE OR REPLACE FUNCTION InsertarUsuarioFacebook
(_nombre VARCHAR(30), _apellido VARCHAR(30), _correo VARCHAR(30))
RETURNS integer AS
$$
BEGIN

   INSERT INTO usuario (us_id,us_nombre, us_apellido, us_email, us_validacion) VALUES
    (nextval('seq_Usuario'), _nombre, _apellido, _correo, false);

   RETURN currval('seq_Usuario');

END;
$$ LANGUAGE plpgsql;
/*SELECT*/

-- Consulta un usuario por su nombre de usuario y clave
-- devuelve los datos del usuario
CREATE OR REPLACE FUNCTION ConsultarUsuarioNombre(_nombreUsuario varchar, _clave varchar)
RETURNS TABLE
  (id integer,
   nombreUsuario varchar,
   email varchar,
   nombre varchar,
   apellido varchar,
   fechNacimiento date,
   genero varchar,
   foto varchar)
AS
$$
BEGIN
	RETURN QUERY SELECT
	us_id, us_nombreUsuario, us_email, us_nombre, us_apellido, us_fechanacimiento,us_genero,us_foto
	FROM usuario
	WHERE us_nombreusuario=_nombreUsuario AND _clave = us_password AND us_validacion=true;
END;
$$ LANGUAGE plpgsql;

-- Consulta un usuario por su correo y clave
-- devuelve los datos del usuario
CREATE OR REPLACE FUNCTION ConsultarUsuarioCorreo(_correo varchar, _clave varchar)
RETURNS TABLE
  (id integer,
   nombreUsuario varchar,
   email varchar,
   nombre varchar,
   apellido varchar,
   fechNacimiento date,
   genero varchar,
   foto varchar)
AS
$$
BEGIN
	RETURN QUERY SELECT
	us_id, us_nombreUsuario, us_email, us_nombre, us_apellido, us_fechanacimiento,us_genero,us_foto
	FROM usuario
	WHERE us_email=_correo AND  us_password=_clave  AND us_validacion= true;
END;
$$ LANGUAGE plpgsql;

--Consulta un usuario por su correo, esta funcion es para iniciar sesion con la red social
-- devuelve los datos del usuario
CREATE OR REPLACE FUNCTION ConsultarUsuarioSocial(_correo varchar)
RETURNS TABLE
  (id integer,
   nombreUsuario varchar,
   email varchar,
   nombre varchar,
   apellido varchar,
   fechNacimiento date,
   genero varchar,
   validacion boolean,
   foto varchar,
   password varchar)
AS
$$
BEGIN
	RETURN QUERY SELECT
	us_id, us_nombreUsuario, us_email, us_nombre, us_apellido, us_fechanacimiento,us_genero, us_validacion,us_foto, us_password
	FROM usuario
	WHERE us_email=_correo;
END;
$$ LANGUAGE plpgsql;
--Consulta el usuario por su nombre de usuario sin clave
CREATE OR REPLACE FUNCTION ConsultarUsuarioSoloNombre(_nombreUsuario varchar)
RETURNS TABLE
  (id integer,
   nombreUsuario varchar,
   email varchar,
   nombre varchar,
   apellido varchar,
   fechNacimiento date,
   genero varchar,
   validacion boolean,
   foto varchar)
AS
$$
BEGIN
	RETURN QUERY SELECT
	us_id, us_nombreUsuario, us_email, us_nombre, us_apellido, us_fechanacimiento,us_genero, us_validacion,us_foto
	FROM usuario
	WHERE us_nombreUsuario=_nombreUsuario;
END;
$$ LANGUAGE plpgsql;

--Consulta el usuario por su id de usuario sin clave
CREATE OR REPLACE FUNCTION ConsultarUsuarioSoloId(_id integer)
RETURNS TABLE
  (nombreUsuario varchar,
   email varchar,
   nombre varchar,
   apellido varchar,
   fechNacimiento date,
   genero varchar,
   foto varchar)
AS
$$
BEGIN
	RETURN QUERY SELECT
	us_nombreUsuario, us_email, us_nombre, us_apellido, us_fechanacimiento,us_genero, us_foto
	FROM usuario
	WHERE us_id=_id;
END;
$$ LANGUAGE plpgsql;

--Recupera la contrasena de un usuario con su correo
-- devuelve la clave del usuario
CREATE OR REPLACE FUNCTION RecuperarContrasena(_correo varchar)
RETURNS TABLE(clave varchar)
AS $$
DECLARE clave VARCHAR(20);
BEGIN

	RETURN QUERY SELECT
  us_password
	FROM usuario WHERE us_email = _correo AND us_validacion=true;

END;
$$ LANGUAGE plpgsql;
--Consulta los lugares turisticos segun las preferencias del usuario
--se le da un id y retorna lista con lugares turisticos
CREATE OR REPLACE FUNCTION BuscarLugarTuristicoSegunPreferencias ( _idUsuario int)
RETURNS TABLE(
  nombre VARCHAR,
  costo  NUMERIC,
  descripcion VARCHAR,
  direccion VARCHAR,
  categoria_nombre VARCHAR
) AS $$
BEGIN
  RETURN QUERY
	SELECT lu_nombre, lu_costo, lu_descripcion, lu_direccion, ca_nombre
  FROM usuario, preferencia, categoria, lugar_turistico, lt_c
  WHERE
	 (pr_usuario =_idUsuario) and (us_id=_idUsuario) and (pr_categoria = ca_id) and (id_categoria = pr_categoria) and (lu_id = id_lugar_turistico)
   limit 20;
END;
$$ LANGUAGE plpgsql;

--Consulta los eventos que van a ocurrir segun las preferencias del usuario
--se le da un id y la fecha actual (del sistema por ejemplo) retorna lista con los eventos
CREATE OR REPLACE FUNCTION BuscarEventoSegunPreferencias( _idUsuario int, _fechaActual date)
RETURNS TABLE(
  nombre VARCHAR,
  fecha_ini TIMESTAMP,
  fecha_fin TIMESTAMP,
  hora_inicio TIME,
  hora_fin TIME,
  precio  INTEGER,
  descripcion VARCHAR,
  foto_evento  VARCHAR,
  nombre_local VARCHAR,
  categoria_nombre VARCHAR
) AS $$
BEGIN
  RETURN QUERY
	 SELECT ev_nombre, ev_fecha_inicio, ev_fecha_fin, ev_hora_inicio, ev_hora_fin, ev_precio, ev_descripcion,ev_foto, lo_nombre, ca_nombre
	 FROM usuario, preferencia, categoria,evento,localidad
	 WHERE
	  (pr_usuario = _idUsuario) and (us_id=_idUsuario) and (pr_categoria = ca_id) and (ev_categoria= ca_id)and (ev_localidad = lo_id) and (ev_fecha_inicio >= _fechaActual)
    limit 20;
END;
$$ LANGUAGE plpgsql;

/*UPDATES*/
--Valida un usuario que no este validado
CREATE OR REPLACE FUNCTION ValidarUsuario(_correo varchar, _id integer)
RETURNS void AS
$$
BEGIN
	UPDATE usuario SET us_validacion=true
	WHERE us_email=_correo AND us_id = _id;
END;
$$ LANGUAGE plpgsql;
-- Actualiza un usuario registrado completando todos sus datos
CREATE OR REPLACE FUNCTION ActualizarUsuario(_nombreUsuario VARCHAR(20), _nombre VARCHAR(30),
 _apellido VARCHAR(30), _fechaNacimiento date,
 _genero VARCHAR(1), _correo VARCHAR(30),
 _clave VARCHAR(20), _foto VARCHAR(100))
RETURNS void AS
$$
BEGIN
	UPDATE usuario SET us_nombreusuario=_nombreUsuario, us_nombre =_nombre, us_apellido= _apellido, us_fechanacimiento =_fechaNacimiento, us_genero = _genero , us_email=_correo, us_password=_clave, us_foto =_foto
	WHERE us_email=_correo;
END;
$$ LANGUAGE plpgsql;
/*DELETES*/
-- Elimina un usuario
CREATE OR REPLACE FUNCTION EliminarUsuario(_id integer)
RETURNS void AS
$$
BEGIN
	DELETE FROM USUARIO
	WHERE us_id = _id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION InsertarCategoria
(_nombreCategoria VARCHAR(20), _descripcionCategoria VARCHAR(200),
 _status BOOLEAN, _idCategoriaSup integer,
 _nivel integer)
RETURNS integer AS
$$
BEGIN

INSERT INTO categoria VALUES
 (nextval('SEQ_Categoria'), _nombreCategoria,_descripcionCategoria,_status,_idCategoriaSup,_nivel);

   RETURN currval('SEQ_Categoria');

END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION EliminarCategoria(_id integer)
RETURNS void AS
$$
BEGIN
	DELETE FROM categoria
	WHERE ca_id = _id;
END;
$$ LANGUAGE plpgsql;
/**
Procedimientos del Modulo (2) de Gestion de Perfil, configuración de sistema y preferencias
Autores:
  Fernández Pedro
  Navas Ronald
  Verrocchi Gianfranco
**/
--Agregar Preferencia (Insert)
CREATE OR REPLACE FUNCTION InsertarPreferencia
( _idUsuario int , _idCategoria int )
RETURNS integer AS $$
DECLARE idUsuario int;
DECLARE idCategoria int;
BEGIN

	INSERT INTO preferencia VALUES
	( _idUsuario, _idCategoria);

   return 1;

END;
$$ LANGUAGE plpgsql;

--Agregar Preferencia (Insert)
CREATE OR REPLACE FUNCTION EliminarPreferencia
( _idUsuario int , _idCategoria int )
RETURNS integer AS $$
DECLARE idUsuario int;
DECLARE idCategoria int;
BEGIN

	DELETE FROM PREFERENCIA WHERE _idUsuario = pr_usuario AND _idCategoria = pr_categoria;

   return 1;

END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION BuscarPreferencias
( _idUsuario int)
RETURNS TABLE(
  id int,
  nombre VARCHAR,
  descripcion VARCHAR,
  status boolean,
  nivel int
) AS $$
BEGIN
  RETURN QUERY SELECT
	ca_id,ca_nombre, ca_descripcion, ca_status, ca_nivel
	FROM preferencia, categoria
	WHERE pr_usuario = _idUsuario and pr_categoria = ca_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION BuscarListaPreferenciasPorCategoria
( _idUsuario int, _nombrePreferencia varchar)
RETURNS TABLE(
  id int,
  nombre VARCHAR
) AS $$
BEGIN
  RETURN QUERY SELECT
	c.ca_id, c.ca_nombre
	FROM categoria c
	WHERE ca_id NOT IN (Select c.ca_id from preferencia p where p.pr_usuario = _idUsuario and p.pr_categoria = c.ca_id )
	AND LOWER(c.ca_nombre) LIKE CONCAT(LOWER(_nombrePreferencia),'%');
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION ModificarDatosUsuario
( _idUsuario int , _nombre varchar , _apellido varchar , _fechaNacimiento date , _genero varchar )
RETURNS integer AS $$
BEGIN
   UPDATE usuario
   SET   us_nombre = _nombre  ,  us_apellido = _apellido ,   us_fechaNacimiento = _fechaNacimiento ,   us_genero = _genero
   WHERE _idUsuario = us_id;     return 1;

END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION ModificarPass
( _idUsuario int , _password varchar)
RETURNS integer AS $$
BEGIN
   UPDATE usuario
   SET   us_password = _password
   WHERE _idUsuario = us_id;     return 1;

END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION ModificarFoto
( _idUsuario int , _foto bytea)
RETURNS integer AS $$
BEGIN
   UPDATE usuario
   SET   us_foto = _foto
   WHERE _idUsuario = us_id;     return 1;

END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION BorrarUsuario
(  _idUsuario int, _password varchar)
RETURNS integer AS $$
BEGIN
   DELETE FROM usuario
   WHERE _password = us_password and _idUsuario = us_id;     return 1;

END;
$$ LANGUAGE plpgsql;

-- Consulta la contraseña del usuario
-- devuelve los datos del usuario
CREATE OR REPLACE FUNCTION ConsultarContrasena(_username varchar)
RETURNS TABLE(clave varchar)
AS $$
DECLARE clave VARCHAR(20);
BEGIN

	RETURN QUERY SELECT
  us_password
	FROM usuario WHERE us_nombreUsuario = _username AND us_validacion=true;

END;
$$ LANGUAGE plpgsql;

/**
Procedimientos del Modulo (3) Amistades y Grupos

Autores antiguos:
  Mariangel Perez
  Oswaldo Lopez
  Aquiles Pulido

Autores actuales:
  Joaquin Camacho
  Jose I. Herrera
  Sabina Quiroga
**/

-------------------------------------------------------------------------------------
-----------------------------PROCEDIMIENTOS MARIANGEL--------------------------------
-------------------------------------------------------------------------------------
----------------------------PROCEDIMIENTO ELIMINAR AMIGO-----------------------------
CREATE OR REPLACE FUNCTION EliminarAmigo
(id integer, id_amigo integer) RETURNS integer
AS $$
DECLARE
  resultado integer;
BEGIN
  DELETE FROM Amigo
  WHERE
  (fk_usuario_conoce = id_amigo AND  fk_usuario_posee = id)
  OR
  (fk_usuario_conoce = id AND  fk_usuario_posee = id_amigo);

  IF found THEN
  resultado := 1;
  ELSE resultado := 0;
  END IF;

  RETURN resultado;
END;
$$ LANGUAGE plpgsql;

----------------------------PROCEDIMIENTO ELIMINAR GRUPO-----------------------------
CREATE OR REPLACE FUNCTION EliminarGrupo
(id_grupo integer) RETURNS integer
AS $$
DECLARE
 resultado integer;
BEGIN

  DELETE FROM Miembro
  WHERE fk_grupo = id_grupo;

  DELETE FROM Grupo
  WHERE gr_id = id_grupo;

  IF found THEN
  resultado := 1;
  ELSE resultado := 0;
  END IF;

  RETURN resultado;
END;
$$ LANGUAGE plpgsql;

---------------------------PROCEDIMIENTO LISTA DE AMIGOS------------------------------
CREATE OR REPLACE FUNCTION ObtenerListaDeAmigos
(id integer)
RETURNS TABLE
(us_nombre varchar,
 us_apellido varchar,
 us_nombreusuario varchar,
 us_foto varchar)
AS $$
BEGIN
  RETURN QUERY
  SELECT u.us_nombre, u.us_apellido, u.us_nombreusuario, u.us_foto
  FROM Amigo a, Usuario u
  WHERE (a.fk_usuario_conoce = id AND  a.fk_usuario_posee = u.us_id AND a.am_aceptado = true)
  OR
  (a.fk_usuario_posee = id AND  a.fk_usuario_conoce = u.us_id AND a.am_aceptado = true)
  ORDER BY us_nombre, us_apellido ASC;
END;
$$ LANGUAGE plpgsql;

----------------------------PROCEDIMIENTO MODIFICAR GRUPO----------------------------------
CREATE OR REPLACE FUNCTION ModificarNombreGrupo
(id_grupo integer, nombre varchar)
RETURNS integer
AS $$
DECLARE
resultado integer;
BEGIN

  UPDATE Grupo SET
  gr_nombre = nombre
  WHERE gr_id = id_grupo;

  IF found THEN
  resultado := 1;
  ELSE resultado := 0;
  END IF;

  RETURN resultado;
END;
$$ LANGUAGE plpgsql;

/*CREATE OR REPLACE FUNCTION ModificarFotoGrupo
(id_grupo integer, foto varchar)
RETURNS integer
AS $$
DECLARE
resultado integer;
BEGIN

  UPDATE Grupo SET
  gr_foto = foto
  WHERE gr_id = id_grupo;

  IF found THEN
  resultado := 1;
  ELSE resultado := 0;
  END IF;

  RETURN resultado;
END;
$$ LANGUAGE plpgsql;*/

-----------------------------PROCEDIMIENTO ELIMINAR INTEGRANTE--------------------------------
CREATE OR REPLACE FUNCTION EliminarIntegrante
(id_grupo integer, id_usuario integer)
RETURNS integer
AS $$
DECLARE
 resultado integer;
BEGIN

  DELETE FROM Miembro
  WHERE fk_grupo = id_grupo AND  fk_usuario = id_usuario;

  IF found THEN
  resultado := 1;
  ELSE resultado := 0;
  END IF;

  RETURN resultado;
END;
$$ LANGUAGE plpgsql;
-------------------------PROCEDIMIENTO AGREGAR INTEGRANTE----------------------------
CREATE OR REPLACE FUNCTION AgregarIntegrante
(id_grupo integer,
 id_usuario integer)
RETURNS integer
AS $$
DECLARE
resultado integer;
BEGIN
  INSERT INTO Miembro
  (mi_id, fk_grupo, fk_usuario)
  VALUES
  (nextval('SEQ_Miembro'), id_grupo, id_usuario);

  IF found THEN
  resultado := 1;
  ELSE resultado := 0;
  END IF;

  RETURN resultado;
END;
$$ LANGUAGE plpgsql;


-------------------------PROCEDIMIENTO CONOCER ID DE USUARIO----------------------------
/*CREATE OR REPLACE FUNCTION ConseguirIdUsuario
(nombre_usuario varchar)
RETURNS TABLE
(id integer)
AS $$
BEGIN
  RETURN QUERY
  SELECT us_id
  FROM Usuario
  WHERE us_nombreusuario = nombre_usuario;
END;
$$ LANGUAGE plpgsql;*/

-------------------------PROCEDIMIENTO OBTENER LIDER----------------------------
CREATE OR REPLACE FUNCTION ObtenerLider
(id_grupo integer)
RETURNS TABLE
(id integer,
nombre varchar,
apellido varchar,
usuario varchar,
foto varchar)
AS $$
BEGIN
    RETURN QUERY
    SELECT us_id, us_nombre, us_apellido, us_nombreusuario, us_foto
    FROM Grupo g, Usuario u
    WHERE
    g.gr_id = id_grupo
    AND
    g.fk_usuario = u.us_id;
END;
$$ LANGUAGE plpgsql;

----------------PROCEDIMIENTO OBTENER MIEMBROS SIN LIDER-----------------
CREATE OR REPLACE FUNCTION VisualizarMiembroSinLider
(id_grupo integer)
RETURNS TABLE
(id integer,
 nombre varchar,
 apellido varchar,
 nombreusuario varchar,
 foto varchar)
AS
$$
BEGIN
  RETURN QUERY
  SELECT us_id, us_nombre, us_apellido, us_nombreusuario, us_foto
  FROM Miembro m, Usuario u
  WHERE m.fk_grupo = id_grupo
  AND m.fk_usuario = u.us_id;
END;
$$ LANGUAGE plpgsql;

-------------PROCEDIMIENTO OBTENER AMIGOS QUE NO ESTAN EN EL GRUPO-------------
CREATE OR REPLACE FUNCTION ListaAmigosSinGrupo
(id_grupo integer,
id_usuario integer)
RETURNS TABLE
(us_id integer,
us_nombre varchar,
us_apellido varchar,
us_nombreusuario varchar,
us_foto varchar)
AS $$
BEGIN
  RETURN QUERY
  SELECT u.us_id, u.us_nombre, u.us_apellido, u.us_nombreusuario, u.us_foto
  FROM Amigo a, Usuario u
  WHERE a.fk_usuario_conoce = id_usuario AND  a.fk_usuario_posee = u.us_id and a.am_aceptado = true
  UNION
  SELECT u.us_id, u.us_nombre, u.us_apellido,u.us_nombreusuario, u.us_foto
  FROM Amigo a, Usuario u
  WHERE a.fk_usuario_posee = id_usuario AND  a.fk_usuario_conoce = u.us_id and a.am_aceptado = true
  EXCEPT
  SELECT u.us_id, u.us_nombre, u.us_apellido,u.us_nombreusuario, u.us_foto
  FROM Usuario u
  WHERE u.us_id = id_usuario
  EXCEPT
  SELECT u.us_id, u.us_nombre, u.us_apellido, u.us_nombreusuario, u.us_foto
  FROM Usuario u, Miembro m, Grupo g
  WHERE m.fk_grupo = id_grupo
  AND m.fk_usuario = u.us_id
  ORDER BY us_nombre, us_apellido ASC;
END;
$$ LANGUAGE plpgsql;

-------------------------------------------------------------------------------------
------------------------------PROCEDIMIENTOS OSWALDO---------------------------------
-------------------------------------------------------------------------------------

-------------PROCEDIMIENTO ENVIAR SOLICITUD DE AMIGO-------------
CREATE OR REPLACE FUNCTION AgregarAmigo
(usuario_1 integer, usuario_2 integer)
RETURNS integer AS $$
DECLARE
 resultado integer;
BEGIN

  INSERT INTO Amigo
  VALUES
  (nextval('SEQ_Amigo'), false, usuario_1, usuario_2);

  IF found THEN
  resultado := 1;
  ELSE resultado := 0;
  END IF;

  RETURN resultado;
END;
$$ LANGUAGE plpgsql;


-------------VISUALIZAR EL PERFIL DE LOS USUARIOS-------------
CREATE OR REPLACE FUNCTION VisualizarPerfilPublico
(nombre_usuario varchar)
RETURNS TABLE
(nombre varchar,
apellido varchar,
correo varchar,
foto varchar,
usuario varchar)
AS
$$
BEGIN
    RETURN QUERY SELECT
    us_nombre, us_apellido, us_email, us_foto, us_nombreUsuario
    FROM usuario
    WHERE us_nombreUsuario = nombre_usuario;
    END;
$$ LANGUAGE plpgsql;

-------------PROCEDIMIENTO PARA SALIR DEL GRUPO------------
CREATE OR REPLACE FUNCTION SalirDeGrupo
(id_grupo integer, id_usuario integer)
RETURNS integer
AS
$$
DECLARE
resultado integer;
BEGIN
    DELETE FROM Miembro m
    WHERE fk_grupo = id_grupo AND fk_usuario = id_usuario;

    IF found THEN
    resultado := 1;
    ELSE resultado := 0;
    END IF;

    RETURN resultado;
END;
$$ LANGUAGE plpgsql;

-------------VERIFICAR SI EL USUARIO ES EL LIDER-------------
/*CREATE OR REPLACE FUNCTION VerificarLider
(id_grupo integer, id_usuario integer)
RETURNS TABLE
(nombre_grupo integer,
lider integer)
AS $$
BEGIN
  RETURN QUERY
  SELECT gr_id, fk_usuario
  FROM grupo
  WHERE gr_id = id_grupo
  AND fk_usuario = id_usuario;
  END;
$$ LANGUAGE plpgsql;*/

-------------LISTA DE NOTIFICACIONES DE AMIGO DEL USUARIO-------------
CREATE OR REPLACE FUNCTION ObtenerListaDeNotificaciones
(id_usuario integer)
RETURNS TABLE
(us_nombre varchar,
us_apellido varchar,
us_nombreusuario varchar,
us_foto varchar)
AS $$
BEGIN
RETURN QUERY
SELECT u2.us_nombre, u2.us_apellido,u2.us_nombreusuario, u2.us_foto
FROM Amigo a, Usuario u, Usuario u2
WHERE a.fk_usuario_posee = u.us_id
AND a.am_aceptado = false
AND u.us_id = id_usuario
AND a.fk_usuario_conoce = u2.us_id;
END;
$$ LANGUAGE plpgsql;


-------------RECHAZAR NOTIFICACION DE SOLICITUD DE AMISTAD DEL USUARIO-------------
CREATE OR REPLACE FUNCTION RechazarNotificacion
(id  integer, id_usuario_rechazado integer)
RETURNS integer
AS $$
DECLARE
 resultado integer;
BEGIN
  DELETE FROM Amigo
  WHERE fk_usuario_conoce = id_usuario_rechazado
  AND  fk_usuario_posee = id
  AND am_aceptado = false;

  IF found THEN
  resultado := 1;
  ELSE resultado := 0;
  END IF;

  RETURN resultado;
END;

$$ LANGUAGE plpgsql;

-------------ACEPTAR NOTIFICACION DE SOLICITUD DE AMISTAD DEL USUARIO-------------
CREATE OR REPLACE FUNCTION AceptarNotificacion
(id  integer, id_usuario_aceptado integer)
RETURNS integer
AS $$
DECLARE
 resultado integer;
BEGIN

  UPDATE Amigo SET am_aceptado = true
  WHERE fk_usuario_conoce = id_usuario_aceptado
  AND fk_usuario_posee = id
  AND am_aceptado = false;

  IF found THEN
  resultado := 1;
  ELSE resultado := 0;
  END IF;

  RETURN resultado;
END;
$$ LANGUAGE plpgsql;

------COMPRUEBA SI EXISTE UNA SOLICITUD DE AMISTAD O SI EL USUARI YA ES TU AMIGO--------
CREATE OR REPLACE FUNCTION ExisteSolicitud
(usuario_1 integer, usuario_2 integer)
RETURNS TABLE
(id integer)
AS $$
BEGIN
  RETURN QUERY
  SELECT am_id
  FROM Amigo
  WHERE (fk_usuario_conoce = usuario_1
  AND fk_usuario_posee = usuario_2)
  OR
  (fk_usuario_conoce = usuario_2
  AND fk_usuario_posee = usuario_1);
END;
$$ LANGUAGE plpgsql;

-------------------------------------------------------------------------------------
------------------------------PROCEDIMIENTOS AQUILES---------------------------------
-------------------------------------------------------------------------------------
-- Consulta la tabla usuario y devolver la lista que coincida con el nombre ingresado
CREATE OR REPLACE FUNCTION BuscarAmigos (id_usuario integer, _nombre varchar)
RETURNS TABLE
  (nombre varchar,
   nombreusu varchar,
   foto varchar)
AS
$$
BEGIN
  RETURN QUERY
  SELECT DISTINCT
  us_nombre, us_nombreusuario, us_foto
  FROM Usuario
  WHERE  LOWER(us_nombreusuario) = LOWER(_nombre)
  OR LOWER(us_nombre) = LOWER(_nombre)
  OR LOWER(us_nombre) LIKE LOWER(_nombre) || '%'
  OR LOWER(us_apellido) = LOWER(_nombre)
  OR LOWER(us_apellido) LIKE LOWER(_nombre) || '%'
  OR LOWER(us_nombreusuario) LIKE LOWER(_nombre) || '%'
  EXCEPT
  SELECT DISTINCT
  us_nombre, us_nombreusuario, us_foto
  FROM Usuario
  WHERE us_id = id_usuario
  EXCEPT
  SELECT DISTINCT
  us_nombre, us_nombreusuario, us_foto
  FROM Usuario, Amigo
  WHERE (us_id <> id_usuario)
  AND
  ((us_id = fk_usuario_conoce AND fk_usuario_conoce <> id_usuario)
  OR (us_id <> fk_usuario_conoce AND fk_usuario_conoce = id_usuario))
  AND ((us_id = fk_usuario_posee AND fk_usuario_posee <> id_usuario)
  OR (us_id <> fk_usuario_posee AND fk_usuario_posee = id_usuario))
  AND (LOWER(us_nombreusuario) = LOWER(_nombre)
  OR LOWER(us_nombre) = LOWER(_nombre)
  OR LOWER(us_nombre) LIKE LOWER(_nombre) || '%'
  OR LOWER(us_nombreusuario) LIKE LOWER(_nombre) || '%');
END;
$$ LANGUAGE plpgsql;

-- Crear Grupo de amigos

CREATE OR REPLACE FUNCTION AgregarGrupo
(nombre varchar, _fk_usuario integer)
RETURNS integer AS
$$
BEGIN

  INSERT INTO Grupo
  (gr_id, gr_nombre, fk_usuario)
  VALUES
  (nextval('SEQ_Grupo'), nombre, _fk_usuario);

  RETURN currval('SEQ_Grupo');

  --INSERT INTO Miembro (mi_id,fk_grupo,fk_usuario)
  --  VALUES
  --(nextval('SEQ_Miembro'),currval('SEQ_Grupo'),_fk_usuario);

  --if found then
  --result := result + 1;
  --else result := 0;
  --end if;
END;
$$ LANGUAGE plpgsql;

------
/*CREATE OR REPLACE FUNCTION AgregarGrupoSinFoto
(nombre varchar, _fk_usuario integer)
RETURNS integer AS $$
DECLARE
resultado integer;
BEGIN

  INSERT INTO Grupo
  (gr_id, gr_nombre, fk_usuario)
  VALUES
  (nextval('SEQ_Grupo'), nombre, _fk_usuario);

  IF found THEN
  resultado := 1;
  ELSE resultado :=0;
  END IF;

  --INSERT INTO Miembro (mi_id, fk_grupo, fk_usuario)
  --  VALUES
  --(nextval('SEQ_Miembro'), currval('SEQ_Grupo'), _fk_usuario);

  --IF found THEN
  --resultado := resultado + 1;
  --ELSE resultado := 0;
  --END IF;

  RETURN resultado;
END;
$$ LANGUAGE plpgsql;*/


-- Consultar la tabla de grupos y devolver la lista que coincida con el id de el usuario
CREATE OR REPLACE FUNCTION ConsultarListaGrupos (id_usuario integer)
RETURNS TABLE
(id integer,
nombre varchar,
foto varchar)
AS
$$
BEGIN
  RETURN QUERY
  SELECT g.gr_id, g.gr_nombre, g.gr_foto
  FROM Grupo g
  WHERE g.fk_usuario = id_usuario
  UNION
  SELECT g.gr_id, g.gr_nombre, g.gr_foto
  FROM Grupo g, Miembro m
  WHERE m.fk_usuario = id_usuario
  AND m.fk_grupo = g.gr_id;
END;
$$ LANGUAGE plpgsql;

-- Consultar perfil del grupo que coincida con el id del grupo
CREATE OR REPLACE FUNCTION ConsultarPerfilGrupo
(id_grupo integer) RETURNS TABLE
(nombre varchar, foto varchar, cantidad_integrantes integer)
AS
$$
BEGIN
  RETURN QUERY
  SELECT g.gr_nombre, g.gr_foto,
  CAST
  ((SELECT COUNT(*)
  FROM Grupo g, Miembro m
  WHERE g.gr_id = id_grupo
  AND g.gr_id = m.fk_grupo) + 1
  AS integer)
  FROM Grupo g
  WHERE g.gr_id = id_grupo;
END;
$$ LANGUAGE plpgsql;

---------------------------------
/*CREATE OR REPLACE FUNCTION ConseguirIdUsuario(
  nombreUsuario character varying)
    RETURNS TABLE
    (id integer)

AS $$
BEGIN
RETURN QUERY
SELECT us_id
FROM Usuario
WHERE us_nombreusuario = nombreUsuario;
END;
$$ LANGUAGE plpgsql;*/

--Consultar a todos los miembros de un grupo incluyendo al lider
CREATE OR REPLACE FUNCTION VisualizarMiembroGrupo
(id_grupo integer)
RETURNS TABLE
(id integer,
nombre varchar,
apellido varchar,
nombreusuario varchar,
foto varchar)
AS
$$
BEGIN
  RETURN QUERY
  SELECT us_id, us_nombre, us_apellido, us_nombreusuario, us_foto
  FROM Grupo g, Usuario u
  WHERE g.gr_id = id_grupo
  AND g.fk_usuario = u.us_id
  UNION
  SELECT us_id, us_nombre, us_apellido, us_nombreusuario, us_foto
  FROM Grupo g, Usuario u, Miembro m
  WHERE g.gr_id = id_grupo
  AND g.gr_id = m.fk_grupo
  AND m.fk_usuario = u.us_id;
END;
$$ LANGUAGE plpgsql;

--Consultar ultimo grupo
CREATE OR REPLACE FUNCTION ConsultarUltimo
(id_usuario integer)
RETURNS TABLE
(id integer,
nombre varchar,
foto varchar)
AS
$$
BEGIN
  RETURN QUERY
  SELECT gr_id, gr_nombre, gr_foto
  FROM Grupo
  WHERE  fk_usuario = id_usuario
  ORDER BY Grupo DESC
  LIMIT 1;
END;
$$ LANGUAGE plpgsql;

/**
Procedimientos del Modulo 5, Gestion de Itinerarios
Autores:
  Arguelles, Marialette
  Jraiche, Michel
  Orrillo, ev_hora_inicio
**/

-- FUNCTION: public.consultar_itinerarios(integer)

-- DROP FUNCTION public.consultar_itinerarios(integer);

CREATE OR REPLACE FUNCTION consultar_itinerarios(
	idusuario integer)
    RETURNS TABLE(id integer, id_usuario integer, nombre character varying, fechainicio date, fechafin date, a_fechainicio date, a_fechafin date, visible boolean,
                  lu_id integer, lu_nombre character varying, lu_descripcion character varying, lu_costo numeric,
                  ac_id integer, ac_nombre character varying, ac_descripcion character varying, ac_duracion time without time zone, ac_foto character varying,
                  ev_id integer, ev_nombre character varying, ev_descripcion character varying, ev_precio integer, ev_fechaini timestamp without time zone, ev_fechafin timestamp without time zone, ev_horainicio time without time zone, ev_horafin time without time zone,ev_foto character varying)
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
    ROWS 1000
AS $BODY$

    BEGIN
      RETURN QUERY

	SELECT  i.it_id as "ID", i.it_idusuario as "ID_usuario", i.it_nombre as "Nombre", i.it_fechainicio as "FechaInicio", i.it_fechafin as "FechaFin", a.ag_fechainicio as "A.FechaInicio", a.ag_fechafin as "A.FechaFin", i.it_visible as "visible",
		  a.ag_idlugarturistico as "lu_id", lt.lu_nombre as "lu_nombre", lt.lu_descripcion as "lu_descripcion", lt.lu_costo as "lu_costo",
          a.ag_idactividad as "ac_id", ac.ac_nombre as "ac_nombre", ac.ac_descripcion as "ac_descripcion", ac.ac_duracion as "ac_duracion", ac.ac_foto as "ac_foto",
          a.ag_idevento as "ev_id", e.ev_nombre as "ev_nombre", e.ev_descripcion as "ev_descripcion", e.ev_precio as "ev_precio", e.ev_fecha_inicio as "ev_fechaini", e.ev_fecha_fin as "ev_fechafin", e.ev_hora_inicio as "ev_horainicio", e.ev_hora_fin as "ev_horafin", e.ev_foto as "ev_foto"
      	FROM agenda a
      	FULL OUTER JOIN itinerario as i ON a.ag_iditinerario = i.it_id
      	LEFT OUTER JOIN evento e ON a.ag_idevento = e.ev_id
      	LEFT OUTER JOIN actividad ac ON a.ag_idactividad = ac.ac_id
      	LEFT OUTER JOIN lugar_turistico lt ON a.ag_idlugarturistico = lt.lu_id
      	WHERE (i.it_idusuario=idusuario)
 		ORDER BY i.it_id, a.ag_fechainicio;
    END;

$BODY$;

ALTER FUNCTION consultar_itinerarios(integer)
    OWNER TO admin_cocotrip;


-----------------Consultar Eventos---------------------------
CREATE OR REPLACE FUNCTION consultar_eventos( busqueda varchar, fechainicio date, fechafin date )
RETURNS TABLE (id_evento integer, nombre_evento varchar, foto varchar) AS $$

DECLARE
    s varchar;

BEGIN
  s := '%' || busqueda || '%';

    RETURN QUERY SELECT
  ev_id, ev_nombre, ev_foto
  FROM evento
  WHERE (LOWER(ev_nombre) like LOWER(s)) and (ev_fecha_inicio BETWEEN fechainicio and fechafin);
END;
$$ LANGUAGE plpgsql;


-----------------Consultar Lugares Turisticos---------------------------
CREATE OR REPLACE FUNCTION consultar_lugarturistico( busqueda varchar )
RETURNS TABLE (id_lugarturistico integer, nombre_lugarturistico varchar) AS $$

DECLARE
    s varchar;

BEGIN
  s := '%' || busqueda || '%';

    RETURN QUERY SELECT
  lu_id, lu_nombre
  FROM lugar_turistico
  WHERE lower(lu_nombre) like lower(s);
END;
$$ LANGUAGE plpgsql;


-----------------Consultar Actividades---------------------------
CREATE OR REPLACE FUNCTION consultar_actividades( busqueda varchar )
RETURNS TABLE (id_actividad integer, nombre_actividad varchar, foto varchar) AS $$

DECLARE
    s varchar;

BEGIN
  s := '%' || busqueda || '%';

    RETURN QUERY SELECT
  ac_id, ac_nombre, ac_foto
  FROM actividad
  WHERE lower(ac_nombre) like lower(s);
END;
$$ LANGUAGE plpgsql;



------------------- Consultar Itinerarios por correo --------------------
CREATE OR REPLACE FUNCTION consultar_itinerarios_correo(idusuario integer)
    RETURNS TABLE(
    id integer,
    nombre character varying,
    a_fechainicio date,
    a_fechafin date,
    lu_nombre character varying,
    lu_descripcion character varying,
    ac_nombre character varying,
    ac_descripcion character varying,
    ev_nombre character varying,
    ev_descripcion character varying)
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
    ROWS 1000
AS $BODY$

    BEGIN
      RETURN QUERY
    SELECT i.it_id as "ID", i.it_nombre as "Nombre",
        a.ag_fechainicio as "A.FechaInicio", a.ag_fechafin as "A.FechaFin",
        lt.lu_nombre as "lu_nombre", lt.lu_descripcion as "lu_descripcion",
        ac.ac_nombre as "ac_nombre", ac.ac_descripcion as "ac_descripcion",
        e.ev_nombre as "ev_nombre", e.ev_descripcion as "ev_descripcion"
        FROM agenda a
        FULL OUTER JOIN itinerario as i ON a.ag_iditinerario = i.it_id
        LEFT OUTER JOIN evento e ON a.ag_idevento = e.ev_id
        LEFT OUTER JOIN actividad ac ON a.ag_idactividad = ac.ac_id
        LEFT OUTER JOIN lugar_turistico lt ON a.ag_idlugarturistico = lt.lu_id
        WHERE (i.it_idusuario=idusuario) and (a.ag_fechainicio BETWEEN date(now()) and (date(now()) + 7))
    ORDER BY i.it_id, a.ag_fechainicio;
    END;
$BODY$;


ALTER FUNCTION public.consultar_itinerarios(integer)
    OWNER TO admin_cocotrip;


--Cambiar visibilidad de un itinerario
-- FUNCTION: public.setvisible(integer, boolean, integer)

-- DROP FUNCTION public.setvisible(integer, boolean, integer);

CREATE OR REPLACE FUNCTION setvisible(
	idusuario integer,
	visible boolean,
	iditinerario integer)
    RETURNS boolean
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
AS $BODY$

    BEGIN

    UPDATE Itinerario
      SET it_visible=visible
      WHERE
          it_id=iditinerario and it_idusuario = idusuario;
      return true;
    END;

$BODY$;

ALTER FUNCTION setvisible(integer, boolean, integer)
    OWNER TO admin_cocotrip;



  --Insertar evento en itineratio
  CREATE OR REPLACE FUNCTION add_evento_it(idevento integer, iditinerario integer, fechaini date, fechafin date)
    RETURNS boolean AS
    $BODY$
    DECLARE
    i integer;
    BEGIN
    SELECT ag_idEvento FROM Agenda WHERE (idevento=ag_idEvento) AND (iditinerario=ag_idItinerario) into i;
    IF i is null THEN
      INSERT INTO Agenda (ag_id,ag_idItinerario,ag_fechainicio,ag_fechafin, ag_idEvento) VALUES (nextval('seq_Agenda'),iditinerario,fechaini,fechafin,idevento);
      return true;
    ELSE
    return false;
    END IF;
    END;
    $BODY$
    LANGUAGE plpgsql VOLATILE
    COST 100;

     --Insertar actividad en itineratio
    CREATE OR REPLACE FUNCTION add_actividad_it(idactividad integer, iditinerario integer,fechaini date, fechafin date)
    RETURNS boolean AS
	$BODY$
    DECLARE
    i integer;
    BEGIN
    SELECT ag_idActividad FROM Agenda WHERE (idactividad=ag_idActividad) AND (iditinerario=ag_idItinerario) into i;
    IF i is null THEN
      INSERT INTO Agenda (ag_id,ag_idItinerario,ag_fechainicio,ag_fechafin, ag_idActividad) VALUES (nextval('seq_Agenda'),iditinerario,fechaini,fechafin,idactividad);
      return true;
    ELSE
    return false;
    END IF;
    END;
	$BODY$
    LANGUAGE plpgsql  VOLATILE
    COST 100;

    --Insertar lugar turistico en itineratio
    CREATE OR REPLACE FUNCTION add_lugar_it(idlugar integer, iditinerario integer, fechaini date, fechafin date)
    RETURNS boolean AS
	$BODY$
    DECLARE
    i integer;
    BEGIN
    SELECT ag_idLugarTuristico FROM Agenda WHERE (idlugar=ag_idLugarTuristico) AND (iditinerario=ag_idItinerario) into i;
    IF i is null THEN
      INSERT INTO Agenda (ag_id,ag_idItinerario,ag_fechainicio,ag_fechafin,ag_idLugarTuristico) VALUES (nextval('seq_Agenda'),iditinerario,fechaini,fechafin,idlugar);
      return true;
    ELSE
    return false;
    END IF;
    END;
	$BODY$
    LANGUAGE plpgsql  VOLATILE
    COST 100;

    --Eliminar item del itineratio
   CREATE OR REPLACE FUNCTION del_item_it(tipo varchar, iditem integer, iditinerario integer)
    RETURNS boolean AS
	$BODY$
    DECLARE
    i integer;
    BEGIN
    SELECT it_id FROM Itinerario where (iditinerario=it_id) into i;
    IF i is null THEN
    return false;
    else
    If (tipo='Lugar Turistico' OR tipo='Actividad' OR tipo='Evento' ) THEN
      IF tipo='Lugar Turistico' THEN
      DELETE FROM Agenda WHERE (iditem=ag_idlugarturistico) AND (iditinerario=ag_idItinerario);
      return true;
      END IF;
      IF tipo='Actividad' THEN
      DELETE FROM Agenda WHERE (iditem=ag_idactividad) AND (iditinerario=ag_idItinerario);
      return true;
      else
      return false;
      END IF;
      IF tipo='Evento' THEN
      DELETE FROM Agenda WHERE (iditem=ag_idevento) AND (iditinerario=ag_idItinerario);
      return true;
      else
      return false;
      END IF;
	ELSE
    return false;
    END IF;
	END IF;
    END
	$BODY$
    LANGUAGE plpgsql  VOLATILE
    COST 100;


    --Agregar itineratio
    CREATE OR REPLACE FUNCTION add_itinerario(nombre character varying(80),idusuario integer)
    RETURNS TABLE (itid integer, itnombre character varying(80),itidusuario integer) AS
	$BODY$
    DECLARE
    i integer;
    BEGIN
 	 INSERT INTO Itinerario (it_id,it_nombre,it_idUsuario) VALUES (nextval('seq_Itinerario'),nombre,idusuario);
     SELECT FIRST_VALUE(it_id)OVER (order by it_id DESC) into i from itinerario;
     RETURN QUERY
     SELECT it_id,it_nombre,it_idUsuario from itinerario
     WHERE it_id=i;
    END;
	$BODY$
    LANGUAGE plpgsql  VOLATILE
    COST 100;

    --Eliminar itineratio
    CREATE OR REPLACE FUNCTION del_itinerario(iditinerario integer)
    RETURNS boolean AS
	$BODY$
    DECLARE
    i integer;
    BEGIN
    SELECT it_id FROM Itinerario where (iditinerario=it_id) into i;
      IF i is null THEN
      return false;
      else
      DELETE from Itinerario where (iditinerario=it_id);
      return true;
      END IF;
    END;
	$BODY$
    LANGUAGE plpgsql  VOLATILE
    COST 100;

    --Modificar itineratio
    CREATE OR REPLACE FUNCTION mod_itinerario(
	iditinerario integer,
	nombre character varying,
	fechaini date,
	fechafin date,
	idusuario integer)
    RETURNS TABLE(itid integer, itnombre character varying, itfechaini date, itfechafin date, itidusuario integer)
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
    ROWS 1000
AS $BODY$

    DECLARE
    i integer;
    BEGIN
      UPDATE Itinerario
      SET it_nombre=nombre,
          it_fechainicio=fechaini,
          it_fechafin=fechafin
      WHERE
          it_id=iditinerario;
      RETURN QUERY
      SELECT it_id, it_nombre, it_fechainicio, it_fechafin, it_idusuario from Itinerario
     WHERE
      it_id=iditinerario;
    END;

$BODY$;


----------------------Insertar notificacion
    CREATE OR REPLACE FUNCTION agregar_notificacion(id_usuario integer)
    RETURNS boolean AS
  $BODY$

    DECLARE
    i integer;

    BEGIN
    SELECT no_id FROM Notificacion WHERE (id_usuario = no_idUsuario) into i;
    IF i is null THEN
      INSERT INTO Notificacion (no_id, no_correo, no_push, no_idUsuario)
      VALUES (nextval('SEQ_Notificacion'), true, true, id_usuario);
      return true;
    ELSE
    return true;
    END IF;
    END;
  $BODY$
    LANGUAGE plpgsql  VOLATILE
    COST 100;

----------------------Eliminar notificacion
    CREATE OR REPLACE FUNCTION eliminar_notificacion(id_usuario integer)
    RETURNS boolean AS
  $BODY$

    DECLARE
    i integer;

    BEGIN
    SELECT no_id FROM Notificacion where (id_usuario = no_id) into i;
    IF i is null THEN
    return true;
    else
      DELETE FROM Notificacion WHERE (id_usuario = no_idUsuario);
        return true;
  END IF;
    END
  $BODY$
    LANGUAGE plpgsql  VOLATILE
    COST 100;

----------------------------- Modificar Notificacion ------------------------
    CREATE OR REPLACE FUNCTION modificar_notificacion(
  id_usuario integer,
  correo boolean,
  push boolean)
    RETURNS void AS $$

  BEGIN
    UPDATE Notificacion
      SET no_correo = correo,
          no_push = push
      WHERE
          id_usuario = no_idUsuario;
  END;
  $$ LANGUAGE plpgsql;


------------------------------- Consultar Notificaciones ---------------------------------
CREATE OR REPLACE FUNCTION consultar_notificaciones( id_usuario integer )
RETURNS TABLE (correo boolean) AS $$

BEGIN
    RETURN QUERY SELECT no_correo
        FROM Notificacion
        WHERE no_idUsuario = id_usuario;
END;
$$ LANGUAGE plpgsql;


    /* fin de procedimientos de Modulo_5 */

/**
Procedimientos del Modulo (7) de Gestion de Lugares Turisticos y
 Actividades en Lugares Turisticos
Autores:
  Camacho Joaquin
  Herrera Jose
  Quiroga Sabina
**/

/*INSERT*/
-- Insertar datos en la tabla lugar_turistico
-- Retorna el ID de la tupla insertada
CREATE OR REPLACE FUNCTION InsertarLugarTuristico
(_nombre VARCHAR(400), _costo decimal,
 _descripcion VARCHAR(2000), _direccion VARCHAR(2000),
 _correo VARCHAR(320), _telefono BIGINT,
 _latitud decimal, _longitud decimal, _activar boolean)
RETURNS integer AS
$$
BEGIN

   INSERT INTO lugar_turistico
   (lu_id, lu_nombre, lu_costo, lu_descripcion,
    lu_direccion, lu_correo, lu_telefono, lu_latitud,
    lu_longitud, lu_activar)
	VALUES
    (nextval('seq_lugar_turistico'), _nombre, _costo,
     _descripcion, _direccion, _correo, _telefono,
      _latitud, _longitud, _activar);

   RETURN currval('seq_lugar_turistico');

END;
$$ LANGUAGE plpgsql;

-- Insertar datos en la tabla actividad
-- Retorna el ID de la tupla insertada
CREATE OR REPLACE FUNCTION InsertarActividad
(_nombre VARCHAR(400), _foto VARCHAR(320), _duracion time,
_descripcion VARCHAR(2000), _activar boolean, _fk integer)
RETURNS integer AS
$$
BEGIN

   INSERT INTO actividad
   (ac_id, ac_foto, ac_nombre, ac_duracion,
    ac_descripcion, ac_activar, fk_ac_lugar_turistico)
	VALUES
    (nextval('seq_actividad'), _foto || currval('seq_actividad') || '.jpg', _nombre,
    _duracion, _descripcion, _activar, _fk);

   RETURN currval('seq_actividad');

END;
$$ LANGUAGE plpgsql;

--Consultar los Lugares turisticos
CREATE OR REPLACE FUNCTION ConsultarLugaresTuristico ()
RETURNS TABLE
	  (
    id integer,
    nombre varchar,
    costo decimal,
    descripcion varchar,
    direccion varchar,
    correo varchar,
    telefono bigint,
    latitud decimal,
    longitud decimal,
    activar boolean)
AS
$$
BEGIN
	RETURN QUERY SELECT lu_id,lu_nombre, lu_costo,
	lu_descripcion, lu_direccion,
	lu_correo, lu_telefono, lu_latitud, lu_longitud, lu_activar
  FROM lugar_turistico;
END;
$$ LANGUAGE plpgsql;

-- Insertar datos en la tabla lt_horario
-- Retorna el ID de la tupla insertada
CREATE OR REPLACE FUNCTION InsertarHorario
(_dia integer, _apertura time, _cierre time, _fk integer)
RETURNS integer AS
$$
BEGIN

	INSERT INTO lt_horario
	(ho_id, ho_dia_semana, ho_hora_apertura, ho_hora_cierre,
	 fk_ho_lugar_turistico)
		VALUES
	(nextval('seq_lt_horario'), _dia, _apertura, _cierre, _fk);

	RETURN currval ('seq_lt_horario');

END;
$$ LANGUAGE plpgsql;

-- Insertar datos en la tabla lt_foto
-- Retorna el ID de la tupla insertada
CREATE OR REPLACE FUNCTION InsertarFoto
(ruta VARCHAR(320), _fk integer)
RETURNS integer AS
$$
DECLARE
_id integer;
BEGIN

	INSERT INTO lt_foto
	(fo_id, fo_ruta, fk_fo_lugar_turistico)
	VALUES
	(nextval('seq_lt_foto'), ruta || currval('seq_lt_foto') || '.jpg', _fk);

	RETURN currval ('seq_lt_foto');

END;
$$ LANGUAGE plpgsql;

-- Inserta categorias en la tabla LT_C
CREATE OR REPLACE FUNCTION InsertarCategoriaLugarTuristico
(_id_lu integer, _id_ca integer)
RETURNS void AS
$$
BEGIN

  INSERT INTO LT_C
  (id_lugar_turistico, id_categoria, id_categoria_superior, categoria_nombre)
  VALUES
  (_id_lu, _id_ca,
    (select COALESCE(ca_fkcategoriasuperior,0)
    from categoria
    where ca_id = _id_ca),
    (select ca_nombre
    from categoria
    where ca_id = _id_ca)
  );

END;
$$ LANGUAGE plpgsql;

/*SELECT*/

-- Consultar la tabla lugar turistico con la informacion minima por rango
CREATE OR REPLACE FUNCTION ConsultarListaLugarTuristico (_desde integer, _hasta integer)
RETURNS TABLE
  (id integer,
   nombre varchar,
   costo decimal,
   descripcion varchar,
   activar boolean)
AS
$$
BEGIN
	RETURN QUERY SELECT
	lu_id, lu_nombre, lu_costo, lu_descripcion, lu_activar
	FROM lugar_turistico
	WHERE lu_id between _desde AND _hasta;
END;
$$ LANGUAGE plpgsql;

-- Consultar la tabla lugar turistico por ID
CREATE OR REPLACE FUNCTION ConsultarLugarTuristico (_id integer)
RETURNS TABLE
	  (nombre varchar,
    costo decimal,
    descripcion varchar,
    direccion varchar,
    correo varchar,
    telefono bigint,
    latitud decimal,
    longitud decimal,
    activar boolean)
AS
$$
BEGIN
	RETURN QUERY SELECT lu_nombre, lu_costo,
	lu_descripcion, lu_direccion,
	lu_correo, lu_telefono, lu_latitud, lu_longitud, lu_activar
  FROM lugar_turistico WHERE lu_id = _id;
END;
$$ LANGUAGE plpgsql;

-- Consultar en la tabla actividad las actividades de
-- un lugar turistico por ID del lugar turistico
CREATE OR REPLACE FUNCTION ConsultarActividades (_fk integer)
RETURNS TABLE
  (id integer,
   foto varchar,
   nombre varchar,
   duracion time,
   descripcion varchar,
   activar boolean)
AS
$$
BEGIN
	RETURN QUERY SELECT ac_id, ac_foto,
			    ac_nombre, ac_duracion,
			    ac_descripcion, ac_activar
	FROM actividad WHERE fk_ac_lugar_turistico = _fk;
END;
$$ LANGUAGE plpgsql;

-- Consultar la tabla actividad por ID
CREATE OR REPLACE FUNCTION ConsultarActividad (_id integer)
RETURNS TABLE
  (foto varchar,
   nombre varchar,
   duracion time,
   descripcion varchar,
   activar boolean)
AS
$$
BEGIN
	RETURN QUERY SELECT ac_foto,
			    ac_nombre, ac_duracion,
			    ac_descripcion, ac_activar
	FROM actividad WHERE ac_id = _id;
END;
$$ LANGUAGE plpgsql;

-- Consultar en la tabla actividad los id y nombres de las actividades
-- asociadas a un lugar turistico por ID del lugar turistico
CREATE OR REPLACE FUNCTION ConsultarNombreActividades (_fk integer)
RETURNS TABLE (nombre varchar)
AS
$$
BEGIN
	RETURN QUERY SELECT ac_nombre
	FROM actividad WHERE fk_ac_lugar_turistico = _fk;
END;
$$ LANGUAGE plpgsql;

-- Consultar horarios de un lugar turistico por ID del lugar turistico
CREATE OR REPLACE FUNCTION ConsultarHorarios
(_fk integer) RETURNS TABLE (id integer, dia integer, apertura time, cierre time)
AS
$$
BEGIN

  RETURN QUERY SELECT ho_id, ho_dia_semana,
          ho_hora_apertura, ho_hora_cierre
  FROM lt_horario WHERE fk_ho_lugar_turistico = _fk;

END;
$$ LANGUAGE plpgsql;

-- Consultar horario especifico de un lugar turistico por ID
-- del lugar turistico
CREATE OR REPLACE FUNCTION ConsultarDiaHorario
(_fk integer, _dia integer) RETURNS TABLE
  (apertura time, cierre time)
AS
$$
BEGIN

  RETURN QUERY SELECT ho_hora_apertura, ho_hora_cierre
  FROM lt_horario WHERE fk_ho_lugar_turistico = _fk
                        AND
                        ho_dia_semana = _dia;
END;
$$ LANGUAGE plpgsql;

-- Consultar fotos de un lugar turistico por ID
-- del lugar turistico
CREATE OR REPLACE FUNCTION ConsultarFotos
(_fk integer) RETURNS TABLE (id integer, ruta varchar)
AS
$$
BEGIN

  RETURN QUERY SELECT fo_id, fo_ruta FROM lt_foto
  WHERE fk_fo_lugar_turistico = _fk;

END;
$$ LANGUAGE plpgsql;

-- Consultar categorias de un lugar turistico por ID
-- del lugar Turisticos
CREATE OR REPLACE FUNCTION ConsultarCategoriaLugarTuristico (_id_lu integer)
RETURNS TABLE (id_ca integer, id_ca_su integer, nombre varchar)
AS
$$
BEGIN

  RETURN QUERY SELECT id_categoria, id_categoria_superior, categoria_nombre
  FROM lt_c WHERE id_lugar_turistico = _id_lu;

END;
$$ LANGUAGE plpgsql;

-- Consultar lista de categorias
CREATE OR REPLACE FUNCTION ConsultarCategoria ()
RETURNS TABLE (id integer, nombre VARCHAR)
AS
$$
BEGIN

  RETURN QUERY SELECT ca_id, ca_nombre FROM categoria
  WHERE ca_fkcategoriasuperior IS NULL
  AND ca_status = true;

END;
$$ LANGUAGE plpgsql;

-- Consultar lista de subcategorias de una categoria
CREATE OR REPLACE FUNCTION ConsultarSubCategoria (_id integer)
RETURNS TABLE (id integer, nombre VARCHAR)
AS
$$
BEGIN

  RETURN QUERY SELECT ca_id, ca_nombre FROM categoria WHERE
  ca_fkcategoriasuperior = _id and ca_status = true;

END;
$$ LANGUAGE plpgsql;

/*UPDATE*/

-- Actualizar estado del lugar turistico por ID del lugar turistico
CREATE OR REPLACE FUNCTION ActivarLugarTuristico (_id integer, _activar boolean)
RETURNS void AS
$$
BEGIN
	UPDATE lugar_turistico SET lu_activar = _activar WHERE lu_id = _id;
END;
$$ LANGUAGE plpgsql;

-- Actualizar estado de la actividad por ID de la actividad
CREATE OR REPLACE FUNCTION ActivarActividad (_id integer, _activar boolean)
RETURNS void AS
$$
BEGIN
  UPDATE actividad SET ac_activar = _activar WHERE ac_id = _id;
END;
$$ LANGUAGE plpgsql;

-- Actualizar datos del lugar turistico por ID del lugar turistico
CREATE OR REPLACE FUNCTION ActualizarLugarTuristico
(_id integer, _nombre VARCHAR(400), _costo decimal,
 _descripcion VARCHAR(2000), _direccion VARCHAR(2000),
 _correo VARCHAR(320), _telefono BIGINT,
 _latitud decimal, _longitud decimal, _activar boolean)
 RETURNS void AS
 $$
BEGIN
  UPDATE lugar_turistico SET
    lu_nombre = _nombre,
    lu_costo = _costo,
    lu_descripcion = _descripcion,
    lu_direccion = _direccion,
    lu_correo = _correo,
    lu_telefono = _telefono,
    lu_latitud = _latitud,
    lu_longitud = _longitud,
    lu_activar = _activar
    WHERE lu_id = _id;
END;
$$ LANGUAGE plpgsql;

-- Actualizar datos de la actividad por ID
CREATE OR REPLACE FUNCTION ActualizarActividad
(_id integer, _foto varchar(320),
 _nombre VARCHAR(400), _duracion time,
 _descripcion VARCHAR(2000), _activar boolean)
 RETURNS void AS
 $$
 BEGIN
  UPDATE actividad SET
    ac_foto = _foto,
    ac_nombre = _nombre,
    ac_duracion = _duracion,
    ac_descripcion = _descripcion,
    ac_activar = _activar
  WHERE ac_id = _id;
 END;
 $$ LANGUAGE plpgsql;

-- Actualizar horario de un lugar turistico por ID del
-- horario
CREATE OR REPLACE FUNCTION ActualizarHorario
(_id integer, _dia integer, _apertura time, _cierre time)
RETURNS void AS
$$
BEGIN
  UPDATE lt_horario SET
    ho_dia_semana = _dia,
    ho_hora_apertura = _apertura,
    ho_hora_cierre = _cierre
  WHERE ho_id = _id;
END;
$$ LANGUAGE plpgsql;

-- Actualizar foto de un lugar turistico por ID de la foto
CREATE OR REPLACE FUNCTION ActualizarFoto
(_id integer, _foto varchar(320)) RETURNS void AS
$$
BEGIN
  UPDATE lt_foto SET
    fo_ruta = _foto
  WHERE fo_id = _id;
END;
$$ LANGUAGE plpgsql;

/*DELETE*/

-- Eliminar actividad de un lugar turistico
CREATE OR REPLACE FUNCTION EliminarActividad (_id integer)
RETURNS void AS
$$
BEGIN
	DELETE FROM actividad WHERE ac_id = _id;
END;
$$ LANGUAGE plpgsql;

-- Eliminar foto de un lugar turistico
CREATE OR REPLACE FUNCTION EliminarFoto
(_id integer) RETURNS void AS
$$
BEGIN
  DELETE FROM lt_foto WHERE fo_id = _id;
END;
$$ LANGUAGE plpgsql;

-- Eliminar horario de un lugar turistico
CREATE OR REPLACE FUNCTION EliminarHorario
(_id integer) RETURNS void AS
$$
BEGIN
  DELETE FROM lt_horario WHERE ho_id = _id;
END;
$$ LANGUAGE plpgsql;

-- Eliminar categoria de un lugar Turisticos
CREATE OR REPLACE FUNCTION EliminarCategoriaLugarTuristico
(_id_lu integer, _id_ca integer) RETURNS void AS
$$
BEGIN
  DELETE FROM lt_c WHERE id_lugar_turistico = _id_lu
  AND id_categoria = _id_ca;
END;
$$ LANGUAGE plpgsql;

-------------------------------------------

CREATE OR REPLACE FUNCTION m9_agregarcategoria(nombrecategoria character varying, descripcioncategoria character varying, nivel integer, status boolean) RETURNS void
    LANGUAGE plpgsql
    AS $$
    BEGIN
      INSERT INTO CATEGORIA (CA_ID, CA_NOMBRE, CA_DESCRIPCION, CA_NIVEL, CA_STATUS)
          VALUES (nextval('seq_categoria'), nombrecategoria, descripcioncategoria, nivel, status);
    END; $$;

CREATE OR REPLACE FUNCTION m9_agregarsubcategoria(nombresubcategoria character varying, descripcionsubcat character varying, nivel integer, status boolean, categoriapadre integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
    BEGIN
        INSERT INTO CATEGORIA (CA_ID, CA_NOMBRE, CA_DESCRIPCION, CA_NIVEL, CA_STATUS, CA_FKCATEGORIASUPERIOR)
              VALUES (nextval('seq_categoria'), nombresubcategoria, descripcionsubcat, nivel, status, categoriapadre);
    END; $$;

CREATE OR REPLACE function m9_devolverid(nombrecategoria VARCHAR(50)) RETURNS TEXT AS
  $BODY$
  DECLARE
    CATEGORIA TEXT;
  BEGIN
      SELECT CA_ID INTO CATEGORIA FROM CATEGORIA WHERE (CA_NOMBRE = nombrecategoria);
      RETURN CATEGORIA;
  END;
  $BODY$
LANGUAGE plpgsql;


CREATE OR REPLACE function m9_devolverTodasCategorias() RETURNS TABLE (idcat INT, nombrecategoria VARCHAR(50), descripcion VARCHAR(100), ca_estatus BOOLEAN, nivel INT, fk INT ) AS $$
BEGIN
			RETURN 	QUERY
					SELECT ca_id, ca_nombre, ca_descripcion, ca_status, ca_nivel, ca_fkcategoriasuperior FROM CATEGORIA;
END;
$$ LANGUAGE plpgsql;

-------------------------------PROCEDIMIENTO MODIFICAR CATEGORIA DEVUELVE 1 SI ES EXICTOSO -------------

CREATE OR REPLACE FUNCTION m9_modificarcategoria
(_id integer,_nombre VARCHAR, _descripcion  VARCHAR, _nivel integer, _categoriapadre integer)
RETURNS integer
    AS $$
    BEGIN
        UPDATE categoria
        SET
        ca_nombre=_nombre, ca_descripcion=_descripcion, ca_fkcategoriasuperior=_categoriapadre,ca_nivel=_nivel
        WHERE ca_id=_id;
        return 1;

    END;
    $$
    LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION m9_actualizarEstatusCategoria(estatus Boolean, id_categoria INT)
  RETURNS void
   AS $$
BEGIN
    UPDATE categoria
   SET ca_status=estatus
   from (select c.ca_id as id from categoria c left join categoria ca on c.ca_id = ca.ca_fkcategoriasuperior left join categoria cat on cat.ca_fkcategoriasuperior = ca.ca_id where c.ca_id = id_categoria
	union
select ca.ca_id as id from categoria c left join categoria ca on c.ca_id = ca.ca_fkcategoriasuperior left join categoria cat on cat.ca_fkcategoriasuperior = ca.ca_id where c.ca_id = id_categoria
	union
select cat.ca_id as id from categoria c left join categoria ca on c.ca_id = ca.ca_fkcategoriasuperior left join categoria cat on cat.ca_fkcategoriasuperior = ca.ca_id where c.ca_id = id_categoria
) as ca

 WHERE ca.id = ca_id;
END; $$
  LANGUAGE plpgsql;

 CREATE OR REPLACE FUNCTION m9_obtenercategoriatop()
  RETURNS TABLE(categoria_id INT, categoria_nombre VARCHAR, categoria_descripcion VARCHAR, categoria_estatus BOOLEAN, categoria_nivel INT, categoria_catsup INT)
   AS $$
DECLARE
   var_r  record;
BEGIN
   FOR var_r IN(SELECT 	ca.ca_id ID, ca.ca_nombre nombre, ca.ca_descripcion descripcion, ca.ca_status estatus, ca.ca_nivel nivel, ca.ca_fkcategoriasuperior sup
		FROM categoria ca where  ca.ca_fkcategoriasuperior is null)
   LOOP
  categoria_id := var_r.ID;
  categoria_nombre := var_r.nombre;
  categoria_descripcion := var_r.descripcion;
  categoria_estatus := var_r.estatus;
  categoria_nivel := var_r.nivel;
  categoria_catsup := var_r.sup;
  RETURN NEXT;
   END LOOP;
END; $$
  LANGUAGE plpgsql;

 CREATE OR REPLACE FUNCTION m9_obtenercategorianotop(sup INT)
  RETURNS TABLE(categoria_id INT, categoria_nombre VARCHAR, categoria_descripcion VARCHAR, categoria_estatus BOOLEAN, categoria_nivel INT, categoria_catsup INT)
   AS $$
DECLARE
   var_r  record;
BEGIN
   FOR var_r IN(SELECT 	ca.ca_id ID, ca.ca_nombre nombre, ca.ca_descripcion descripcion, ca.ca_status estatus, ca.ca_nivel nivel, ca.ca_fkcategoriasuperior sup
		FROM categoria ca where  ca.ca_fkcategoriasuperior = sup)
   LOOP
  categoria_id := var_r.ID;
  categoria_nombre := var_r.nombre;
  categoria_descripcion := var_r.descripcion;
  categoria_estatus := var_r.estatus;
  categoria_nivel := var_r.nivel;
  categoria_catsup := var_r.sup;
  RETURN NEXT;
   END LOOP;
END; $$
  LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION m9_ObtenerCategoriaPorId(idCategoria INT)
  RETURNS TABLE
  (

      categoria_id integer ,
      categoria_nombre character varying(20) ,
      categoria_descripcion character varying(100),
      categoria_status boolean ,
      categoria_nivel integer,
      categoria_fkcategoriasuperior integer

  )
  AS
  $$
  BEGIN

    RETURN QUERY
    SELECT ca_id,ca_nombre,ca_descripcion,ca_status,ca_nivel, ca_fkcategoriasuperior
     FROM categoria
    WHERE ca_id=idCategoria;
  END;
  $$
  LANGUAGE plpgsql;


  -------------------------PROCEDIMIENTO BUSCAR CATEGORIA POR STATUS HABILITADO-------------

  CREATE OR REPLACE FUNCTION m9_ConsultarCategoriaHabilitada()
 /** (_status boolean)**/
  RETURNS TABLE
  (

      categoria_id integer ,
      categoria_nombre character varying(20) ,
      categoria_descripcion character varying(100),
      categoria_status boolean ,
      categoria_nivel integer,
      categoria_fkcategoriasuperior integer

  )
  AS
  $$
  BEGIN

    RETURN QUERY
    SELECT ca_id,ca_nombre,ca_descripcion,ca_status,ca_nivel, ca_fkcategoriasuperior
     FROM categoria
    WHERE ca_status=true;
  END;
  $$
  LANGUAGE plpgsql;


/**
Procedimientos del Modulo (8) de gestion de eventos y localidades de eventos
Autores:
  Noe Herrera
  Jorge Marin
  Miguelangel Medina
**/

/*INSERT*/

--inserta un evento en una localidad



CREATE OR REPLACE FUNCTION InsertarEvento
(
  _nombreEvento VARCHAR(100),
  _descripcionEvento VARCHAR(500),
  _precioEvento double precision,
  _fechaInicioEvento timestamp,
  _fechaFinEvento timestamp,
  _horaInicioEvento time,
  _horaFinEvento time,
  _fotoEvento varchar,
  _localidadEvento integer,
  _categoriaEvento integer
)
RETURNS void AS
$$
BEGIN

   INSERT INTO evento VALUES
    (nextval('SEQ_Evento'), _nombreEvento, _descripcionEvento,
      _precioEvento, _fechaInicioEvento, _fechaFinEvento,
      _horaInicioEvento, _horaFinEvento, _fotoEvento, _localidadEvento,
      _categoriaEvento);
   END;
$$ LANGUAGE plpgsql;

--inserta una localidad
CREATE OR REPLACE FUNCTION InsertarLocalidad
(
  _nombreLocalidad varchar(20),
  _descripcionLocalidad varchar(500),
  _coordenada varchar(50)
)
RETURNS void AS
$$
BEGIN

    INSERT INTO localidad VALUES
      (nextval('SEQ_Localidad'), _nombreLocalidad, _descripcionLocalidad, _coordenada);
    END;
$$ LANGUAGE plpgsql;


/*DELETE*/

--elimina evento por su id
CREATE OR REPLACE FUNCTION EliminarEventoporId
(_id_eve integer) RETURNS void AS
$$
BEGIN
  DELETE FROM evento WHERE ev_id = _id_eve;
END;
$$ LANGUAGE plpgsql;

--elimina evento por su nombre
CREATE OR REPLACE FUNCTION EliminarEventoNombre
(_nom_eve varchar) RETURNS void AS
$$
BEGIN
  DELETE FROM evento WHERE ev_nombre = _nom_eve;
END;
$$ LANGUAGE plpgsql;

--elimina localidad por su id
CREATE OR REPLACE FUNCTION EliminarLocalidadporId
(_id_lo integer) RETURNS void AS
$$
BEGIN
  DELETE FROM evento WHERE ev_localidad = _id_lo;
  DELETE FROM localidad WHERE lo_id = _id_lo;
END;
$$ LANGUAGE plpgsql;

--elimina localidad por su nombre
CREATE OR REPLACE FUNCTION EliminarLocalidadNombre
(_nom_localidad varchar, _id_localidad integer) RETURNS void AS
$$
BEGIN
  DELETE FROM evento WHERE ev_localidad = _id_localidad;
  DELETE FROM localidad WHERE lo_nombre = _nom_localidad;
END;
$$ LANGUAGE plpgsql;

-- Consulta las localidades que tienen eventos asignados
-- devuelve la informacion de las localidades
CREATE OR REPLACE FUNCTION ConsultarLocalidadesConEventosAsignados()
RETURNS TABLE
  (
    locaidad_id integer,
     nombreLocalidad varchar,
     descripcionLocalidad varchar,
     coordenada varchar
  )
AS
$$
BEGIN
  RETURN QUERY
    select lo_id, lo_nombre, lo_descripcion, lo_coordenada
    from localidad, evento
    where lo_id = ev_localidad
    group by lo_id, lo_nombre,lo_descripcion, lo_coordenada;
END;
$$ LANGUAGE plpgsql;

 --Lista de todas las localidades
CREATE OR REPLACE FUNCTION consultarlocalidades()
  RETURNS TABLE(locaidad_id integer, nombrelocalidad character varying, descripcionlocalidad character varying, coordenada character varying) AS
$BODY$
BEGIN
  RETURN QUERY
    select  lo_id,lo_nombre, lo_descripcion, lo_coordenada
    from localidad;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;


/*SELECT*/


-- Consulta Localidad por nombre
-- devuelve el id de la localidad
CREATE OR REPLACE FUNCTION LocalidadIdNombre
(
  _nombre varchar(50)
)
RETURNS TABLE
  (
  id_localidad integer
  )
AS
$$
BEGIN
  RETURN QUERY
    SELECT lo_id
    from localidad
    where lo_nombre = _nombre;
END;
$$ LANGUAGE plpgsql;


-- Consulta eventos por id de categoria
-- devuelve la informacion de los eventos en esa categoria

CREATE OR REPLACE FUNCTION ConsultarEventoPorIdCategoria
(
  _id integer
)
RETURNS TABLE
  (
  id_evento integer,
	nombreEvento varchar,
	descripcionEvento varchar,
	precioEvento double precision,
	fecha_inicioEvento timestamp,
	fecha_finEvento timestamp,
	hora_inicioEvento time,
	hora_finEvento time,
	fotoEvento varchar,
	localidadEvento int
  )
AS
$$
BEGIN
  RETURN QUERY
    SELECT ev_id, ev_nombre, ev_descripcion, ev_precio, ev_fecha_inicio, ev_fecha_fin, ev_hora_inicio, ev_hora_fin, ev_foto, ev_localidad
    from evento
    where ev_categoria = _id;
END;
$$ LANGUAGE plpgsql;

-- Consulta eventos por nombre de categoria
-- devuelve la informacion de los eventos en esa categoria
CREATE OR REPLACE FUNCTION ConsultarEventoPorNombreCategoria
(
  _nombreCategoria varchar(50)
)
RETURNS TABLE
  (
     id_evento integer,
     nombreEvento varchar,
     descripcionEvento varchar,
     precioEvento integer,
     fechaInicioEvento timestamp,
     fechaFinEvento timestamp,
     horaInicioEvento time,
     horaFinEvento time,
     fotoEvento varchar,
     categoriaEvento integer,
     localidadEvento integer
  )
AS
$$
BEGIN
  RETURN QUERY
    SELECT  ev_id, ev_nombre, ev_descripcion, ev_precio, ev_fecha_inicio, ev_fecha_fin, ev_hora_inicio, ev_hora_fin, ev_foto, ca_id, lo_id
    from evento, categoria, localidad
    where ev_categoria = ca_id and ev_localidad = lo_id and ca_nombre = _nombreCategoria;
END;
$$ LANGUAGE plpgsql;

-- Consulta todos los eventos
-- devuelve la informacion todos los eventos
CREATE OR REPLACE FUNCTION ConsultarEventos
(
)
RETURNS TABLE
  (
     id_evento integer,
     nombreEvento varchar,
     descripcionEvento varchar,
     precioEvento integer,
     fechaInicioEvento timestamp,
     fechaFinEvento timestamp,
     horaInicioEvento time,
     horaFinEvento time,
     fotoEvento varchar,
     categoriaEvento integer,
     localidadEvento integer
  )
AS
$$
BEGIN
  RETURN QUERY
    SELECT ev_id, ev_nombre, ev_descripcion, ev_precio, ev_fecha_inicio, ev_fecha_fin, ev_hora_inicio, ev_hora_fin, ev_foto, ca_id, lo_id
    from evento, categoria, localidad
    where ev_localidad = lo_id and ev_categoria = ca_id;
END;
$$ LANGUAGE plpgsql;


-- Consulta evento por su id
-- devuelve la informacion del evento
CREATE OR REPLACE FUNCTION ConsultarEventoPorIdEvento
(
  _id integer
)
RETURNS TABLE
  (
     id_evento integer,
     nombreEvento varchar,
     descripcionEvento varchar,
     precioEvento double precision,
     fechaInicioEvento timestamp,
     fechaFinEvento timestamp,
     horaInicioEvento time,
     horaFinEvento time,
     fotoEvento varchar,
     localidadEvento integer,
     categoriaEvento integer
  )
AS
$$
BEGIN
  RETURN QUERY
    SELECT ev_id, ev_nombre, ev_descripcion, ev_precio, ev_fecha_inicio, ev_fecha_fin, ev_hora_inicio, ev_hora_fin, ev_foto, ev_localidad, ev_categoria
    from evento
    where  ev_id = _id;
END;
$$ LANGUAGE plpgsql;

-- Consulta evento por su nombre
-- devuelve la informacion del evento
CREATE OR REPLACE FUNCTION ConsultarEventoPorIdNombre
(
  _nombreEvento varchar
)
RETURNS TABLE
  (
     id_evento integer,
     nombreEvento varchar,
     descripcionEvento varchar,
     precioEvento integer,
     fechaInicioEvento timestamp,
     fechaFinEvento timestamp,
     horaInicioEvento time,
     horaFinEvento time,
     fotoEvento varchar,
     categoriaEvento varchar,
     localidadEvento varchar
  )
AS
$$
BEGIN
  RETURN QUERY
    SELECT ev_id, ev_nombre, ev_descripcion, ev_precio, ev_fecha_inicio, ev_fecha_fin, ev_hora_inicio, ev_hora_fin, ev_foto, ca_nombre, lo_nombre
    from evento, categoria, localidad
    where ev_categoria = ca_id and ev_localidad = lo_id and ev_nombre like _nombreEvento;
END;
$$ LANGUAGE plpgsql;


-- Consulta eventos a partir de una fecha dada
-- devuelve la informacion de los eventos que su fecha sea mayor a la dada
CREATE OR REPLACE FUNCTION ConsultarEventosPorFecha
(
  _fecha timestamp
)
RETURNS TABLE
  (
     id_evento integer,
     nombreEvento varchar,
     descripcionEvento varchar,
     precioEvento integer,
     fechaInicioEvento timestamp,
     fechaFinEvento timestamp,
     horaInicioEvento time,
     horaFinEvento time,
     fotoEvento varchar,
     categoriaEvento varchar,
     localidadEvento varchar
  )
AS
$$
BEGIN
  RETURN QUERY
    SELECT ev_id,ev_nombre, ev_descripcion, ev_precio, ev_fecha_inicio, ev_fecha_fin, ev_hora_inicio, ev_hora_fin, ev_foto, ca_nombre, lo_nombre
    from evento, categoria, localidad
    where ev_categoria = ca_id and ev_localidad = lo_id and ev_fecha_inicio >= _fecha;
END;
$$ LANGUAGE plpgsql;



-- Consulta una localidad por su id
-- devuelve la informacion de la localidad
CREATE OR REPLACE FUNCTION ConsultarLocalidadPorId
(
  _id integer
)
RETURNS TABLE
  (
     id_localidad integer,
     nombreLocalidad varchar,
     descripcionLocalidad varchar,
     coordenada varchar
  )
AS
$$
BEGIN
  RETURN QUERY
    select lo_id,lo_nombre, lo_descripcion, lo_coordenada
    from localidad
    where lo_id=_id;
END;
$$ LANGUAGE plpgsql;

-- Consulta una localidad por su nombre
-- devuelve la informacion de la localidad
CREATE OR REPLACE FUNCTION ConsultarLocalidadPorNombre
(
  _nombreLocalidad varchar(50)
)
RETURNS TABLE
  (
     id_localidad integer,
     nombreLocalidad varchar,
     descripcionLocalidad varchar,
     coordenada varchar
  )
AS
$$
BEGIN
  RETURN QUERY
    select lo_id,lo_nombre, lo_descripcion, lo_coordenada
    from localidad
    where lo_nombre=_nombreLocalidad;
END;
$$ LANGUAGE plpgsql;

--Consulta y te devuelve true si existe el evento por id

/*UPDATE*/

--actualizar evento por id
CREATE OR REPLACE FUNCTION actualizarEventoPorId
(
  _id integer,
  _nombreEvento VARCHAR(100),
  _descripcionEvento VARCHAR(500),
  _precioEvento double precision,
  _fechaInicioEvento timestamp,
  _fechaFinEvento timestamp,
  _horaInicioEvento time,
  _horaFinEvento time,
  _fotoEvento varchar,
  _localidadEvento integer,
  _categoriaEvento integer

)
RETURNS void AS
$$
BEGIN
  UPDATE evento SET
    ev_nombre=_nombreEvento,
    ev_descripcion=_descripcionEvento,
    ev_precio=_precioEvento,
    ev_fecha_inicio=_fechaInicioEvento,
    ev_fecha_fin=_fechaFinEvento,
    ev_hora_inicio=_horaInicioEvento,
    ev_hora_fin=_horaFinEvento,
    ev_foto=_fotoEvento,
    ev_localidad=_localidadEvento,
    ev_categoria=_categoriaEvento
  WHERE ev_id=_id;
END;
$$ LANGUAGE plpgsql;

--actualizar localidad por id
CREATE OR REPLACE FUNCTION actualizarLocalidadPorId
(
  _id integer,
  _nombreLocalidad varchar(20),
  _descripcionLocalidad varchar(500),
  _coordenada varchar(50)
)
RETURNS void AS
$$
BEGIN
  UPDATE localidad SET
    lo_nombre=_nombreLocalidad,
    lo_descripcion=_descripcionLocalidad,
    lo_coordenada=_coordenada
  WHERE lo_id=_id;
END;
$$ LANGUAGE plpgsql;

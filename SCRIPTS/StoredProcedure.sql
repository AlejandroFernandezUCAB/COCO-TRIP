

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
 _clave VARCHAR(20), _foto bytea)
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
(_nombre VARCHAR(30), _apellido VARCHAR(30), _fechaNacimiento date, _correo VARCHAR(30),
 _foto bytea)
RETURNS integer AS
$$
BEGIN

   INSERT INTO usuario (us_id,us_nombre, us_apellido, us_fechanacimiento, us_email, us_foto, us_validacion) VALUES
    (nextval('seq_Usuario'), _nombre, _apellido, _fechaNacimiento, _correo, _foto, false);

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
   foto bytea)
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
   foto bytea)
AS
$$
BEGIN
	RETURN QUERY SELECT
	us_id, us_nombreUsuario, us_email, us_nombre, us_apellido, us_fechanacimiento,us_genero,us_foto
	FROM usuario
	WHERE us_email=_correo AND _clave = us_password AND us_validacion= true;
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
   foto bytea)
AS
$$
BEGIN
	RETURN QUERY SELECT
	us_id, us_nombreUsuario, us_email, us_nombre, us_apellido, us_fechanacimiento,us_genero,us_foto
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
   foto bytea)
AS
$$
BEGIN
	RETURN QUERY SELECT
	us_id, us_nombreUsuario, us_email, us_nombre, us_apellido, us_fechanacimiento,us_genero,us_foto
	FROM usuario
	WHERE us_nombreUsuario=_nombreUsuario;
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

/*UPDATES*/
CREATE OR REPLACE FUNCTION ValidarUsuario(_correo varchar, _id integer)
RETURNS void AS
$$
BEGIN
	UPDATE usuario SET us_validacion=true
	WHERE us_email=_correo AND us_id = _id;
END;
$$ LANGUAGE plpgsql;

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
(_foto bytea, _nombre VARCHAR(400), _duracion time,
_descripcion VARCHAR(2000), _activar boolean, _fk integer)
RETURNS integer AS
$$
BEGIN

   INSERT INTO actividad
   (ac_id, ac_foto, ac_nombre, ac_duracion,
    ac_descripcion, ac_activar, fk_ac_lugar_turistico)
	VALUES
    (nextval('seq_actividad'), _foto, _nombre,
    _duracion, _descripcion, _activar, _fk);

   RETURN currval('seq_actividad');

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
(_foto bytea, _fk integer)
RETURNS integer AS
$$
DECLARE
_id integer;
BEGIN

	INSERT INTO lt_foto
	(fo_id, fo_byte, fk_fo_lugar_turistico)
	VALUES
	(nextval('seq_lt_foto'), _foto, _fk);

	RETURN currval ('seq_lt_foto');

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
   foto bytea,
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
  (foto bytea,
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
(_fk integer) RETURNS TABLE (id integer, byte bytea)
AS
$$
BEGIN

  RETURN QUERY SELECT fo_id, fo_byte FROM lt_foto
  WHERE fk_fo_lugar_turistico = _fk;

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
(_id integer, _foto bytea,
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
(_id integer, _foto bytea) RETURNS void AS
$$
BEGIN
  UPDATE lt_foto SET
    fo_byte = _foto
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



CREATE FUNCTION m9_agregarcategoria(nombrecategoria character varying, descripcioncategoria character varying, nivel integer, status boolean) RETURNS void
    LANGUAGE plpgsql
    AS $$
    BEGIN
      INSERT INTO CATEGORIA (CA_IDCATEGORIA, CA_NOMBRE, CA_DESCRIPCION, CA_NIVEL, CA_STATUS) 
          VALUES (nextval('secuencia_categoria'), nombrecategoria, descripcioncategoria, nivel, status);
    END; $$;

CREATE FUNCTION m9_agregarsubcategoria(nombresubcategoria character varying, descripcionsubcat character varying, nivel integer, status boolean, categoriapadre integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
    BEGIN
        INSERT INTO CATEGORIA (CA_IDCATEGORIA, CA_NOMBRE, CA_DESCRIPCION, CA_NIVEL, CA_STATUS, CA_FKCATEGORIASUPERIOR) 
              VALUES (nextval('secuencia_categoria'), nombresubcategoria, descripcionsubcat, nivel, status, categoriapadre);
    END; $$;



CREATE FUNCTION m9_modificarcategoria(nuevonombre character varying, nuevadescripcion character varying, categoriapadre integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
    BEGIN
        /*UPDATE TABLE CATEGORIA  */
    END; $$;


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
  _precioEvento integer,
  _fechaInicioEvento timestamp,
  _fechaFinEvento timestamp,
  _horaInicioEvento time,
  _horaFinEvento time,
  _fotoEvento bytea,
  _localidadEvento integer,
  _categoriaEvento integer
)
RETURNS integer AS
$$
BEGIN

   INSERT INTO evento VALUES
    (nextval('SEQ_Evento'), _nombreEvento, _descripcionEvento, 
      _precioEvento, _fechaInicioEvento, _fechaFinEvento, 
      _horaInicioEvento, _horaFinEvento, _fotoEvento, _localidadEvento, 
      _categoriaEvento);
   
    RETURN currval('SEQ_Evento');
   END;
$$ LANGUAGE plpgsql;

--inserta una localidad
CREATE OR REPLACE FUNCTION InsertarLocalidad
(
  _nombreLocalidad varchar(20),
  _descripcionLocalidad varchar(500),
  _coordenadaXLocalidad integer,
  _coordenadaYLocalidad integer
)
RETURNS integer AS
$$
BEGIN

    INSERT INTO localidad VALUES
      (nextval('SEQ_Localidad'), _nombreLocalidad, _descripcionLocalidad, _coordenadaXLocalidad, _coordenadaYLocalidad);
    
      RETURN currval('SEQ_Localidad');
    END;
$$ LANGUAGE plpgsql;


/*DELETE*/

--elimina evento por su id
CREATE OR REPLACE FUNCTION EliminarEventoPorId
(
  _id integer
)
RETURNS true AS
$$
BEGIN

    DELETE from evento where ev_id = _id;

END;
$$ LANGUAGE plpgsql;

--elimina evento por su nombre
CREATE OR REPLACE FUNCTION EliminarEventoPorNombre
(
  _nombreEvento integer
)
RETURNS true AS
$$
BEGIN

    DELETE from evento where ev_nombre = _nombreEvento;

END;
$$ LANGUAGE plpgsql;

--elimina localidad por su id
CREATE OR REPLACE FUNCTION EliminarLocalidadPorId
(
  _id integer
)
RETURNS true AS
$$
BEGIN

    DELETE from localidad where lo_id = _id;

END;
$$ LANGUAGE plpgsql;

--elimina localidad por su nombre
CREATE OR REPLACE FUNCTION EliminarLocalidadPorNombre
(
  _nombreLocalidad integer
)
RETURNS true AS
$$
BEGIN

    DELETE from localidad where lo_nombre = _nombreLocalidad;

END;
$$ LANGUAGE plpgsql;

/*SELECT*/

-- Consulta eventos por id de categoria
-- devuelve la informacion de los eventos en esa categoria
CREATE OR REPLACE FUNCTION ConsultarEventoPorIdCategoria
(
  _id integer
)
RETURNS TABLE
  (
     id integer,
     nombreEvento varchar,
     descripcionEvento varchar,
     precioEvento integer,
     fechaInicioEvento timestamp,
     fechaFinEvento timestamp,
     horaInicioEvento time,
     horaFinEvento time,
     fotoEvento bytea,
     categoriaEvento varchar,
     localidadEvento varchar
  )
AS
$$
BEGIN
  RETURN QUERY 
    SELECT ev_id, ev_nombre, ev_descripcion, ev_precio, ev_fecha_inicio, ev_fecha_fin, ev_hora_inicio, ev_hora_fin, ev_foto, ca_nombre, lo_nombre
    from evento, categoria, localidad
    where ev_categoria = ca_id and ev_localidad = lo_id and ca_id = _id;
END;
$$ LANGUAGE plpgsql;

-- Consulta eventos por nombre de categoria
-- devuelve la informacion de los eventos en esa categoria
CREATE OR REPLACE FUNCTION ConsultarEventoPorNombreCategoria
(
  _nombreCategoria integer
)
RETURNS TABLE
  (
     id integer,
     nombreEvento varchar,
     descripcionEvento varchar,
     precioEvento integer,
     fechaInicioEvento timestamp,
     fechaFinEvento timestamp,
     horaInicioEvento time,
     horaFinEvento time,
     fotoEvento bytea,
     categoriaEvento varchar,
     localidadEvento varchar
  )
AS
$$
BEGIN
  RETURN QUERY 
    SELECT ev_id, ev_nombre, ev_descripcion, ev_precio, ev_fecha_inicio, ev_fecha_fin, ev_hora_inicio, ev_hora_fin, ev_foto, ca_nombre, lo_nombre
    from evento, categoria, localidad
    where ev_categoria = ca_id and ev_localidad = lo_id and ca_nombre = _nombreCategoria;
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
     id integer,
     nombreEvento varchar,
     descripcionEvento varchar,
     precioEvento integer,
     fechaInicioEvento timestamp,
     fechaFinEvento timestamp,
     horaInicioEvento time,
     horaFinEvento time,
     fotoEvento bytea,
     categoriaEvento varchar,
     localidadEvento varchar
  )
AS
$$
BEGIN
  RETURN QUERY 
    SELECT ev_id, ev_nombre, ev_descripcion, ev_precio, ev_fecha_inicio, ev_fecha_fin, ev_hora_inicio, ev_hora_fin, ev_foto, ca_nombre, lo_nombre
    from evento, categoria, localidad
    where ev_categoria = ca_id and ev_localidad = lo_id and ev_id = _id;
END;
$$ LANGUAGE plpgsql;

-- Consulta las localidades que tienen eventos asignados 
-- devuelve la informacion de las localidades
CREATE OR REPLACE FUNCTION ConsultarLocalidadesConEventosAsignados()
RETURNS TABLE
  (
     id integer,
     nombreLocalidad varchar,
     descripcionLocalidad varchar,
     coordenadaXLocalidad integer,
     coordenadaYLocalidad integer
  )
AS
$$
BEGIN
  RETURN QUERY 
    select lo_id, lo_nombre, lo_descripcion, lo_coord_x, lo_coord_y
    from localidad, evento
    where lo_id = ev_localidad
    group by lo_id;
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
     id integer,
     nombreLocalidad varchar,
     descripcionLocalidad varchar,
     coordenadaXLocalidad integer,
     coordenadaYLocalidad integer
  )
AS
$$
BEGIN
  RETURN QUERY 
    select lo_id, lo_nombre, lo_descripcion, lo_coord_x, lo_coord_y
    from localidad
    where lo_id=_id;
END;
$$ LANGUAGE plpgsql;

-- Consulta una localidad por su nombre 
-- devuelve la informacion de la localidad
CREATE OR REPLACE FUNCTION ConsultarLocalidadPorNombre
(
  _nombreLocalidad integer
)
RETURNS TABLE
  (
     id integer,
     nombreLocalidad varchar,
     descripcionLocalidad varchar,
     coordenadaXLocalidad integer,
     coordenadaYLocalidad integer
  )
AS
$$
BEGIN
  RETURN QUERY 
    select lo_id, lo_nombre, lo_descripcion, lo_coord_x, lo_coord_y
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
  _precioEvento integer,
  _fechaInicioEvento timestamp,
  _fechaFinEvento timestamp,
  _horaInicioEvento time,
  _horaFinEvento time,
  _fotoEvento bytea,
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
  _coordenadaXLocalidad integer,
  _coordenadaYLocalidad integer
)
RETURNS void AS
$$
BEGIN
  UPDATE localidad SET 
    lo_nombre=_nombreLocalidad,
    lo_descripcion=_descripcionLocalidad,
    lo_coord_x=_coordenadaXLocalidad,
    lo_coord_y=_coordenadaYLocalidad
  WHERE lo_id=_id;
END;
$$ LANGUAGE plpgsql;















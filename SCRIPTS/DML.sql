/*

delete from lt_horario;
delete from lt_foto;
delete from actividad;
delete from lt_c;
delete from lugar_turistico;

drop sequence seq_lugar_turistico;
drop sequence seq_lt_foto;
drop sequence seq_lt_horario;
drop sequence seq_actividad;

create sequence seq_lugar_turistico;
create sequence seq_lt_foto;
create sequence seq_lt_horario;
create sequence seq_actividad;

*/

--Datos de prueba del Modulo 7
/*
INSERT INTO public.categoria(
	ca_id, ca_nombre, ca_descripcion, ca_status, ca_fkcategoriasuperior, ca_nivel)
	VALUES (1, 'Sitios Naturales', 'Naturaleza', true, null, 0);

INSERT INTO public.categoria(
  ca_id, ca_nombre, ca_descripcion, ca_status, ca_fkcategoriasuperior, ca_nivel)
  VALUES (2, 'Montañas', 'Naturaleza', true, 1, 0);

INSERT INTO public.categoria(
  ca_id, ca_nombre, ca_descripcion, ca_status, ca_fkcategoriasuperior, ca_nivel)
  VALUES (3, 'Costas', 'Playa', true, 1, 0);

INSERT INTO public.categoria(
  ca_id, ca_nombre, ca_descripcion, ca_status, ca_fkcategoriasuperior, ca_nivel)
  VALUES (4, 'Bosques', 'Todo tipo de ecosistema', true, 1, 0);
*/
SELECT InsertarLugarTuristico
(
  'Parque Generalisimo de Miranda',
  0,
  'Lugar al aire libre',
  'Frente a la estacion Miranda, Caracas',
  'parquemiranda@gob.ve',
  02122732867,
  10.4932131,
  -66.83946550000002,
  true
);

SELECT InsertarActividad
(
  'Caminata por el parque',
  'Ruta A',
  cast('02:00' as time),
  'Lugar al aire libre',
  true,
  1
);

SELECT InsertarFoto
(
  'Ruta LT',
  1
);

SELECT InsertarHorario
(
  0,
  '08:00 AM',
  '12:00 PM',
  1
);

SELECT InsertarHorario
(
  1,
  '08:00 AM',
  '06:00 PM',
  1
);

SELECT InsertarHorario
(
  2,
  '08:00 AM',
  '06:00 PM',
  1
);

SELECT InsertarHorario
(
  3,
  '08:00 AM',
  '06:00 PM',
  1
);

SELECT InsertarHorario
(
  4,
  '08:00 AM',
  '06:00 PM',
  1
);

SELECT InsertarHorario
(
  5,
  '08:00 AM',
  '06:00 PM',
  1
);

SELECT InsertarHorario
(
  6,
  '08:00 AM',
  '08:00 PM',
  1
);

SELECT InsertarCategoriaLugarTuristico
(
  1,
  1
);

SELECT InsertarCategoriaLugarTuristico
(
  1,
  4
);

SELECT InsertarLugarTuristico
(
  'Waraira Repano',
  0,
  'Parque Nacional',
  'Norte de Caracas, Distrito Capital',
  'eventoswaraira@gmail.com',
  02123394192,
  10.5165724,
  -66.85787040000002,
  true
);

SELECT InsertarActividad
(
  'Subir el avila (waraira repano)',
  'Ruta A',
  cast('05:00' as time),
  'Lugar al aire libre',
  true,
  2
);

SELECT InsertarFoto
(
  'Ruta LT',
  2
);

SELECT InsertarHorario
(
  0,
  '08:00 AM',
  '12:00 PM',
  2
);

SELECT InsertarHorario
(
  1,
  '08:00 AM',
  '12:00 PM',
  2
);

SELECT InsertarHorario
(
  2,
  '08:00 AM',
  '12:00 PM',
  2
);

SELECT InsertarHorario
(
  3,
  '08:00 AM',
  '12:00 PM',
  2
);

SELECT InsertarHorario
(
  4,
  '08:00 AM',
  '12:00 PM',
  2
);

SELECT InsertarHorario
(
  5,
  '08:00 AM',
  '12:00 PM',
  2
);

SELECT InsertarHorario
(
  6,
  '08:00 AM',
  '12:00 PM',
  2
);

SELECT InsertarCategoriaLugarTuristico
(
  2,
  1
);

SELECT InsertarCategoriaLugarTuristico
(
  2,
  2
);


--INSERTS MODULO 9

INSERT INTO categoria (
  ca_id, ca_nombre, ca_descripcion, ca_status, ca_fkcategoriasuperior, ca_nivel)
  VALUES (1, 'Lugares', 'Categoria asociada a los lugares', true, null, 1);

INSERT INTO categoria(
  ca_id, ca_nombre, ca_descripcion, ca_status, ca_fkcategoriasuperior, ca_nivel)
  VALUES (2, 'Eventos', 'Categoria asociada a los eventos', true, null, 1);

INSERT INTO categoria(
  ca_id, ca_nombre, ca_descripcion, ca_status, ca_fkcategoriasuperior, ca_nivel)
  VALUES (3, 'Turismo', 'Categoria asociada al turismo', true, null, 1);
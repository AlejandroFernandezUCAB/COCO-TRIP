--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.5
-- Dumped by pg_dump version 9.6.5

-- Started on 2017-11-05 17:17:09

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 1 (class 3079 OID 12387)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2130 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

--
-- TOC entry 199 (class 1255 OID 16407)
-- Name: m9_agregarcategoria(character varying, character varying, integer, boolean); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION m9_agregarcategoria(nombrecategoria character varying, descripcioncategoria character varying, nivel integer, status boolean) RETURNS void
    LANGUAGE plpgsql
    AS $$
    BEGIN
    	INSERT INTO CATEGORIA (CA_IDCATEGORIA, CA_NOMBRE, CA_DESCRIPCION, CA_NIVEL, CA_STATUS) 
        	VALUES (nextval('secuencia_categoria'), nombrecategoria, descripcioncategoria, nivel, status);
    END; $$;


ALTER FUNCTION public.m9_agregarcategoria(nombrecategoria character varying, descripcioncategoria character varying, nivel integer, status boolean) OWNER TO postgres;

--
-- TOC entry 200 (class 1255 OID 16410)
-- Name: m9_agregarsubcategoria(character varying, character varying, integer, boolean, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION m9_agregarsubcategoria(nombresubcategoria character varying, descripcionsubcat character varying, nivel integer, status boolean, categoriapadre integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
    BEGIN
    		INSERT INTO CATEGORIA (CA_IDCATEGORIA, CA_NOMBRE, CA_DESCRIPCION, CA_NIVEL, CA_STATUS, CA_FKCATEGORIASUPERIOR) 
            	VALUES (nextval('secuencia_categoria'), nombresubcategoria, descripcionsubcat, nivel, status, categoriapadre);
    END; $$;


ALTER FUNCTION public.m9_agregarsubcategoria(nombresubcategoria character varying, descripcionsubcat character varying, nivel integer, status boolean, categoriapadre integer) OWNER TO postgres;

--
-- TOC entry 201 (class 1255 OID 16412)
-- Name: m9_modificarcategoria(character varying, character varying, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION m9_modificarcategoria(nuevonombre character varying, nuevadescripcion character varying, categoriapadre integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
    BEGIN
    		/*UPDATE TABLE CATEGORIA  */
    END; $$;


ALTER FUNCTION public.m9_modificarcategoria(nuevonombre character varying, nuevadescripcion character varying, categoriapadre integer) OWNER TO postgres;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 185 (class 1259 OID 16395)
-- Name: categoria; Type: TABLE; Schema: public; Owner: Abel Marquez
--

CREATE TABLE categoria (
    ca_idcategoria integer NOT NULL,
    ca_nombre character varying(20),
    ca_descripcion character varying(100),
    ca_status boolean,
    ca_fkcategoriasuperior integer,
    ca_nivel integer
);


ALTER TABLE categoria OWNER TO "Abel Marquez";

--
-- TOC entry 186 (class 1259 OID 16405)
-- Name: secuencia_categoria; Type: SEQUENCE; Schema: public; Owner: Abel Marquez
--

CREATE SEQUENCE secuencia_categoria
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE secuencia_categoria OWNER TO "Abel Marquez";

--
-- TOC entry 2005 (class 2606 OID 16399)
-- Name: categoria categoria_pkey; Type: CONSTRAINT; Schema: public; Owner: Abel Marquez
--

ALTER TABLE ONLY categoria
    ADD CONSTRAINT categoria_pkey PRIMARY KEY (ca_idcategoria);


--
-- TOC entry 2006 (class 2606 OID 16400)
-- Name: categoria pk_categoriapadre; Type: FK CONSTRAINT; Schema: public; Owner: Abel Marquez
--

ALTER TABLE ONLY categoria
    ADD CONSTRAINT pk_categoriapadre FOREIGN KEY (ca_fkcategoriasuperior) REFERENCES categoria(ca_idcategoria);


-- Completed on 2017-11-05 17:17:09

--
-- PostgreSQL database dump complete
--


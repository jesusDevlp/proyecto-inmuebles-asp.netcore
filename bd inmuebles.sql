drop database  INMUEBLES
go

create database INMUEBLES
go

use INMUEBLES
go

use master
go

drop table if exists distrito
create table distrito (
id_Distrito			int IDENTITY(1,1) NOT NULL primary key,
nombre_distrito     varchar(100),
lugar_distrito      varchar (50),
eliminado			char(2) default 'No'
)
go

insert into distrito (nombre_distrito, lugar_distrito) values 
 ('Jesus Maria','Lima'), 
('Breña','Lima'),
 ('San Isidro','Lima'),
 ('Miraflores','Lima'),
 ('Surco','Lima'),
 ('Ancón','Lima'),
 ('Ate','Lima'),
('Barranco','Lima'),
 ('Carabayllo','Lima'),
 ('Cercado de Lima','Lima'),
('Chaclacayo','Lima'),
 ('Chorrillos','Lima'),
 ('Cieneguilla','Lima'),
 ('Comas','Lima'),
 ('El agustino','Lima'),
('Independencia','Lima'),
('La molina','Lima'),
('La victoria','Lima'),
('Lince','Lima'),
('Los olivos','Lima'),
('Lurigancho','Lima'),
 ('Lurín','Lima'),
 ('Magdalena del mar','Lima'),
 ('Pachacámac','Lima'),
 ('Pucusana','Lima'),
('Pueblo libre','Lima'),
 ('Puente piedra','Lima'),
 ('Punta hermosa','Lima'),
 ('Punta negra','Lima'),
 ('Rímac','Lima'),
 ('San bartolo','Lima'),
 ('San borja','Lima'),
 ('San Juan de Lurigancho','Lima'),
 ('San Juan de Miraflores','Lima'),
 ('San Luis','Lima'),
('San Martin de Porres','Lima'),
 ('San Miguel','Lima'),
('Santa Anita','Lima'),
 ('Santa María del Mar','Lima'),
('Santa Rosa','Lima'),
 ('Surquillo','Lima'),
 ('Villa el Salvador','Lima'),
 ('Villa Maria del Triunfo','Lima')

drop table if exists tipoInmueble
create table tipoInmueble(
id_Tipo_Inmueble     int IDENTITY(1,1) NOT NULL primary key ,
desc_Inmueble        varchar(100)  NOT NULL ,
eliminado			 char(2) default 'No'
)
go

insert into tipoInmueble (desc_Inmueble) values 
 ('Departamento'),
 ('Casa'),
('Terreno'),
('Edificio'),
('Local')


drop table if exists inmueble
create table inmueble(
id_Inmueble          int IDENTITY(1,1) NOT NULL primary key ,
id_Tipo_Inmueble     int NOT NULL,
desc_Inmueble        varchar(100)  NOT NULL ,
ubic_Inmueble        varchar(100)  NOT NULL ,
costo_Inmueble       decimal  NOT NULL,
id_Distrito			 int NOT NULL,
url_imagen			 varchar(MAX) NOT NULL,
eliminado	         char(2) default 'No'
)
go

insert into inmueble (id_Tipo_Inmueble,desc_Inmueble,ubic_Inmueble,costo_Inmueble,id_Distrito,url_imagen) values
(1,'Departamento Duplex','Chacarilla del Estanque',500.000,5,'asdasdasdasdasdasdas')

ALTER TABLE  inmueble
	ADD  FOREIGN KEY (id_Tipo_Inmueble) REFERENCES tipoInmueble(id_Tipo_Inmueble)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go

ALTER TABLE  inmueble
	ADD  FOREIGN KEY (id_Distrito) REFERENCES distrito(id_Distrito)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go

drop table if exists empleado
create table empleado(
id_Empleado			int IDENTITY(1,1) NOT NULL primary key,
nombre_Empleado		varchar (50) NOT NULL,
correo_Cliente		varchar(20) NOT NULL,
tlf_Empleado		varchar (20) NOT NULL,
eliminado	        char(2) default 'No'
)
go

insert into empleado(nombre_Empleado, correo_Cliente, tlf_Empleado) values
('Carlos','carlos@gmail.com','987654321')


drop table if exists condicion
create table condicion(
id_Condicion			int IDENTITY(1,1) NOT NULL primary key,
des_Condicion			varchar(255),
eliminado	            char(2) default 'No'
)
go

insert into condicion (des_condicion) values 
('Disponible'),
 ('Vendido'),
 ('Hipotecado'),
 ('Alquilado')

drop table if exists formaPago
create table formaPago(
id_Forma_Pago		int IDENTITY(1,1) NOT NULL primary key,
des_Forma_Pago		varchar(50),
eliminado	        char(2) default 'No')
go

insert into formaPago (des_forma_pago) values 
('Transferencia Bancaria'),
 ('Efectivo'),
 ('Cheque nominativo bancario')


drop table if exists venta
CREATE TABLE venta(
id_Venta             int IDENTITY(1,1)   NOT NULL ,
id_Inmueble          int  NOT NULL,
id_Empleado			 int NOT NULL,
nombre_Cliente		 varchar (30) NOT NULL,
nro_Documento		 varchar (20) NOT NULL,
id_Condicion		 int NOT NULL,
id_Forma_Pago	     int NOT NULL,
total_Venta          decimal  NOT NULL,
id_Tipo_Inmueble     int not null,
total_General        decimal  NOT NULL ,
fecha_Venta          datetime  NOT NULL,
eliminado	        char(2) default 'No'
	 
)
go

insert into venta (id_Inmueble,id_Empleado,nombre_Cliente,nro_Documento,id_Condicion,id_Forma_Pago,total_Venta,id_Tipo_Inmueble,total_General,fecha_Venta) values
(1,1,'Pedro','75577456',1,1,500.00,1,900.00,'2020-02-02')

select * from venta

ALTER TABLE venta
	ADD  FOREIGN KEY (id_Inmueble) REFERENCES inmueble(id_Inmueble)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go

ALTER TABLE venta
	ADD  FOREIGN KEY (id_Condicion) REFERENCES condicion(id_Condicion)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go
ALTER TABLE venta
	ADD  FOREIGN KEY (id_Empleado) REFERENCES empleado(id_Empleado)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go


ALTER TABLE venta
	ADD  FOREIGN KEY (id_Forma_Pago) REFERENCES formaPago(id_Forma_Pago)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go

ALTER TABLE venta
	ADD  FOREIGN KEY (id_Tipo_Inmueble) REFERENCES tipoInmueble(id_Tipo_Inmueble)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go

-- PROCEDURES
-- PARA LISTAR DISTRITO
CREATE OR ALTER PROCEDURE sp_GetListDistrito
AS
BEGIN
	SELECT id_Distrito, nombre_distrito
	FROM distrito;
END
GO

exec  sp_GetListDistrito
go

-- PARA LISTAR CONDICION
CREATE OR ALTER PROCEDURE sp_GetCondicion
AS
BEGIN
	SELECT c.id_Condicion, c.des_Condicion
	FROM condicion c
END
GO

exec  sp_GetCondicion
go

-- PARA LISTAR FORMAPAGO
CREATE OR ALTER PROCEDURE sp_GetFormaPago
AS
BEGIN
	SELECT f.id_Forma_Pago,f.des_Forma_Pago
	FROM formaPago f
END
GO

exec  sp_GetFormaPago
go

-- PARA LISTAR TIPOINMUEBLE
CREATE OR ALTER PROCEDURE sp_GetTipoInmueble
AS
BEGIN
	SELECT t.id_Tipo_Inmueble,t.desc_Inmueble
	FROM tipoInmueble t
END
GO

exec  sp_GetTipoInmueble
go



-- PARA LISTAR EMPLEADO
CREATE OR ALTER PROCEDURE sp_GetEmpleado
AS
BEGIN
	SELECT e.id_Empleado,e.nombre_Empleado,e.correo_Cliente,e.tlf_Empleado
	FROM empleado e
END
GO

exec  sp_GetEmpleado
go



-- PARA GRABAR EMPLEADO
CREATE OR ALTER PROCEDURE PA_GRABAR_EMPLEADO
@id_Empleado int,@nombre_Empleado VARCHAR(50),@correo_Cliente varchar(20),@tlf_Empleado VARCHAR(20)
AS
	MERGE empleado AS TARGET
	USING (SELECT @id_Empleado,@nombre_Empleado,@correo_Cliente,@tlf_Empleado,'No') AS SOURCE(id_Empleado,nombre_Empleado,correo_Cliente,tlf_Empleado,eliminado)
	ON TARGET.id_Empleado = SOURCE.id_Empleado
	WHEN MATCHED THEN
		UPDATE SET TARGET.nombre_Empleado=SOURCE.nombre_Empleado,
		           TARGET.correo_Cliente=SOURCE.correo_Cliente,
		           TARGET.tlf_Empleado=SOURCE.tlf_Empleado,
				   TARGET.eliminado=SOURCE.eliminado
	WHEN NOT MATCHED THEN
		INSERT VALUES(SOURCE.nombre_Empleado,SOURCE.correo_Cliente,SOURCE.tlf_Empleado,SOURCE.eliminado);
GO

-- PARA ELIMINAR DE FORMA LOGICA EMPLEADO
CREATE OR ALTER PROCEDURE PA_ELIMINAR_EMPLEADO
@id_Empleado int
AS
	UPDATE empleado
		SET eliminado='Si'
	WHERE id_Empleado = @id_Empleado
GO

-- PARA LISTAR LOS INMUEBLES
CREATE OR ALTER PROCEDURE sp_GetInmueble
AS
BEGIN
	SELECT i.id_inmueble, i.desc_inmueble,i.ubic_inmueble,i.costo_inmueble,i.id_distrito, d.nombre_distrito
	FROM inmueble i inner join distrito d on i.id_distrito = d.id_distrito;
END
GO

exec  sp_GetInmueble
go

-- PARA GRABAR INMUEBLE
CREATE OR ALTER PROCEDURE PA_GRABAR_INMUEBLE
@id_Inmueble int,@id_Tipo_Inmueble int,@desc_Inmueble VARCHAR(100),
@ubic_Inmueble VARCHAR(100),@costo_Inmueble int,@id_Distrito int,
@url_imagen varchar(max)
AS
	MERGE inmueble AS TARGET
	USING (SELECT @id_Inmueble,@id_Tipo_Inmueble,@desc_Inmueble,@ubic_Inmueble,@costo_Inmueble,@id_Distrito,@url_imagen,'No')
	AS SOURCE(id_Inmueble,id_Tipo_Inmueble,desc_Inmueble,ubic_Inmueble,costo_Inmueble,id_Distrito,url_imagen,eliminado)
	ON TARGET.id_Inmueble = SOURCE.id_Inmueble
	WHEN MATCHED THEN
		UPDATE SET TARGET.id_Tipo_Inmueble=SOURCE.id_Tipo_Inmueble,	
		           TARGET.desc_Inmueble=SOURCE.desc_Inmueble,
		           TARGET.ubic_Inmueble=SOURCE.ubic_Inmueble,
		           TARGET.costo_Inmueble=SOURCE.costo_Inmueble,
		           TARGET.id_Distrito=SOURCE.id_Distrito,
		           TARGET.url_imagen=SOURCE.url_imagen,
				   TARGET.eliminado=SOURCE.eliminado
	WHEN NOT MATCHED THEN
		INSERT VALUES(SOURCE.id_Tipo_Inmueble,SOURCE.desc_Inmueble,SOURCE.ubic_Inmueble,SOURCE.costo_Inmueble,
		              SOURCE.id_Distrito,SOURCE.url_imagen,SOURCE.eliminado);
GO

-- PARA ELIMINAR DE FORMA LOGICA INMUEBLE
CREATE OR ALTER PROCEDURE PA_ELIMINAR_INMUEBLE
@id_Inmueble int
AS
	UPDATE inmueble
		SET eliminado='Si'
	WHERE id_Inmueble = @id_Inmueble
GO

-- PARA LISTAR VENTA
CREATE OR ALTER PROCEDURE sp_GetVenta
AS
BEGIN
	SELECT v.id_Venta,v.id_Inmueble,i.desc_Inmueble,v.id_Empleado,e.nombre_Empleado,v.nombre_Cliente,v.nro_Documento,v.id_Condicion,c.des_Condicion,
	v.id_Forma_Pago,f.des_Forma_Pago,v.total_Venta,v.id_Tipo_Inmueble,t.desc_Inmueble,v.total_General,v.fecha_Venta
	FROM venta v inner join inmueble i on v.id_Inmueble=i.id_Inmueble
	             inner join empleado e on v.id_Empleado=e.id_Empleado
				 inner join condicion c on v.id_Condicion=c.id_Condicion
				 inner join formaPago f on v.id_Forma_Pago=f.id_Forma_Pago
				 inner join tipoInmueble t on v.id_Tipo_Inmueble=t.id_Tipo_Inmueble
END
GO

exec sp_GetVenta 
go

-- PARA GRABAR VENTA
CREATE OR ALTER PROCEDURE PA_GRABAR_VENTA
@id_Venta int,@id_Inmueble int,@id_Empleado int,@nombre_Cliente VARCHAR(30),
@nro_Documento VARCHAR(20),@id_Condicion int,@id_Forma_Pago int,@total_Venta int,@id_Tipo_Inmueble int,
@total_General int,@fecha_Venta datetime

AS
	MERGE venta AS TARGET
	USING (SELECT @id_Venta,@id_Inmueble,@id_Empleado,@nombre_Cliente,@nro_Documento,@id_Condicion,
	@id_Forma_Pago,@total_Venta,@id_Tipo_Inmueble,@total_General,@fecha_Venta,'No')
	AS SOURCE(id_Venta,id_Inmueble,id_Empleado,nombre_Cliente,nro_Documento,id_Condicion,
	id_Forma_Pago,total_Venta,id_Tipo_Inmueble,total_General,fecha_Venta,eliminado)
	ON TARGET.id_Venta = SOURCE.id_Venta
	WHEN MATCHED THEN
		UPDATE SET TARGET.id_Inmueble=SOURCE.id_Inmueble,	
		           TARGET.id_Empleado=SOURCE.id_Empleado,
		           TARGET.nombre_Cliente=SOURCE.nombre_Cliente,
		           TARGET.nro_Documento=SOURCE.nro_Documento,
		           TARGET.id_Condicion=SOURCE.id_Condicion,
		           TARGET.id_Forma_Pago=SOURCE.id_Forma_Pago,
				   TARGET.total_Venta=SOURCE.total_Venta,
		           TARGET.id_Tipo_Inmueble=SOURCE.id_Tipo_Inmueble,
		           TARGET.total_General=SOURCE.total_General,
		           TARGET.fecha_Venta=SOURCE.fecha_Venta,
				   TARGET.eliminado=SOURCE.eliminado
	WHEN NOT MATCHED THEN
		INSERT VALUES(SOURCE.id_Inmueble,SOURCE.id_Empleado,SOURCE.nombre_Cliente,SOURCE.nro_Documento,
		              SOURCE.id_Condicion,SOURCE.id_Forma_Pago,SOURCE.total_Venta,SOURCE.id_Tipo_Inmueble,
					  SOURCE.total_General,SOURCE.fecha_Venta,SOURCE.eliminado);
GO

exec PA_GRABAR_VENTA 1,1,1,'Alejandro','11112222',1,1,500,1,450,'1996-07-19'


-- PARA ELIMINAR DE FORMA LOGICA VENTA
CREATE OR ALTER PROCEDURE PA_ELIMINAR_VENTA
@id_Venta int
AS
	UPDATE venta  SET eliminado='Si'  WHERE id_Venta = @id_Venta
GO

--PROCEDURES FOR INMUEBLES HOME :v
CREATE OR ALTER PROCEDURE sp_GetInmuebleHome
AS
	SELECT I.id_Inmueble, tpI.desc_Inmueble, I.desc_Inmueble, I.ubic_Inmueble, 'Distrito'=(D.nombre_distrito+' - '+D.lugar_distrito),url_imagen,costo_Inmueble  FROM inmueble I 
	inner join tipoInmueble tpI on I.id_Tipo_Inmueble = tpI.id_Tipo_Inmueble
	inner join distrito D on D.id_Distrito = I.id_Distrito
	where I.eliminado = 'No'
GO

exec sp_GetInmuebleHome
go

CREATE OR ALTER PROCEDURE sp_GetInmueble
AS
BEGIN
	SELECT i.id_inmueble, i.desc_inmueble,i.ubic_inmueble,i.costo_inmueble, d.nombre_distrito,i.url_imagen,i.id_distrito,i.id_Tipo_Inmueble
	FROM inmueble i inner join distrito d on i.id_distrito = d.id_distrito where i.eliminado in ('No')
END
GO
exec  sp_GetInmueble
go

-- PARA GRABAR INMUEBLE
CREATE OR ALTER PROCEDURE PA_GRABAR_INMUEBLE
@id_Inmueble int,@id_Tipo_Inmueble int,@desc_Inmueble VARCHAR(100),
@ubic_Inmueble VARCHAR(100),@costo_Inmueble decimal,@id_Distrito int,
@url_imagen varchar(max)
AS
	MERGE inmueble AS TARGET
	USING (SELECT @id_Inmueble,@id_Tipo_Inmueble,@desc_Inmueble,@ubic_Inmueble,@costo_Inmueble,@id_Distrito,@url_imagen,'No')
	AS SOURCE(id_Inmueble,id_Tipo_Inmueble,desc_Inmueble,ubic_Inmueble,costo_Inmueble,id_Distrito,url_imagen,eliminado)
	ON TARGET.id_Inmueble = SOURCE.id_Inmueble
	WHEN MATCHED THEN
		UPDATE SET TARGET.id_Tipo_Inmueble=SOURCE.id_Tipo_Inmueble,	
		           TARGET.desc_Inmueble=SOURCE.desc_Inmueble,
		           TARGET.ubic_Inmueble=SOURCE.ubic_Inmueble,
		           TARGET.costo_Inmueble=SOURCE.costo_Inmueble,
		           TARGET.id_Distrito=SOURCE.id_Distrito,
		           TARGET.url_imagen=SOURCE.url_imagen,
				   TARGET.eliminado=SOURCE.eliminado
	WHEN NOT MATCHED THEN
		INSERT VALUES(SOURCE.id_Tipo_Inmueble,SOURCE.desc_Inmueble,SOURCE.ubic_Inmueble,SOURCE.costo_Inmueble,
		              SOURCE.id_Distrito,SOURCE.url_imagen,SOURCE.eliminado);
GO
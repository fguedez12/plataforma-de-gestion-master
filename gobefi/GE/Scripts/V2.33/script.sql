update TipoDocumentos set Etapa1=0 where Nombre='Listado de Jefaturas'
update TipoDocumentos set Etapa1=0 where Nombre='Residuos – Practicas reutilización de papel'
update TipoDocumentos set Etapa1=0 where Nombre='Procedimiento para Compras Sustentables
'
update TipoDocumentos set Nombre='Colaboradores concientizados' where Nombre='Listado colaboradores concientizados SEV-CC'
update TipoDocumentos set Nombre='Procedimiento formal destinado a disminuir el consumo de papel​' where Nombre='Procedimiento formal de papel'
update TipoDocumentos set Nombre='Actividad interna de concientización​' where Nombre='Actividad interna de concientización SEV-CC'

insert into TipoDocumentos values ('Informe de análisis diagnóstico ambiental',1,16,null,1,0)

update EncuestaColaboradores set Year = 2024

update Viajes set Year = 2024

delete Permisos
where MenuId = 
(
select id from Menu where Nombre='Estado reporte de consumos unidades'
)


update TipoDocumentos set Etapa2=0
where Id=1010

update TipoDocumentos set Etapa2=0
where Id=1009

update TipoDocumentos set NombreE2='Plan anual de capacitación a implementar año t+1'
where Id=1015

insert into TipoDocumentos values
('Resolución aprueba plan de gestión ambiental',2,17,'Resolución aprueba plan de gestión ambiental',0,1)

update Menu set Title='Diseño Plan de gestión Ambiental – Etapa 2' where Nombre='Plan de Gestión Ambiental'

update Permisos set Escritura=1 where MenuId=53 and RolId='a4d32084-8d5b-423f-bb83-9327b3f689bc'

update Servicios set RevisionRed=0, ComentarioRed='Aún sin revisión por parte de la Red.'

delete Permisos where RolId='a4d32084-8d5b-423f-bb83-9327b3f689bc' and MenuId=47

-- Script de verificación para las nuevas columnas añadidas en V2.35
-- Tarea #2: Database Schema Updates

-- Verificar estructura de la tabla Acciones
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Acciones' 
AND COLUMN_NAME IN ('CostoAsociado', 'EstadoAvance', 'Observaciones')
ORDER BY COLUMN_NAME;

-- Verificar estructura de la tabla PlanGestion_Tareas
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'PlanGestion_Tareas' 
AND COLUMN_NAME = 'DescripcionTareaEjecutada';

-- Verificar que la migración se aplicó correctamente
SELECT MigrationId, ProductVersion
FROM __EFMigrationsHistory
WHERE MigrationId = '20250609151215_UpdateAccionTareaForSeguimiento';

-- Test básico de operaciones CRUD
-- Verificar que las columnas aceptan valores NULL/NOT NULL según diseño
SELECT 
    COUNT(*) as TotalAcciones,
    COUNT(CostoAsociado) as AccionesConCosto,
    COUNT(EstadoAvance) as AccionesConEstado,
    COUNT(Observaciones) as AccionesConObservaciones
FROM Acciones;

SELECT 
    COUNT(*) as TotalTareas,
    COUNT(DescripcionTareaEjecutada) as TareasConDescripcion
FROM PlanGestion_Tareas;
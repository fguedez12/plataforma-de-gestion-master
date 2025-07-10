-- Script de pruebas CRUD para las nuevas columnas V2.35
-- Tarea #2: Database Schema Updates

-- ===== PRUEBAS DE INSERCIÓN =====

-- Test 1: Insertar una nueva acción con los nuevos campos
-- NOTA: Esto es solo un ejemplo, NO ejecutar en producción sin datos válidos
/*
INSERT INTO Acciones (
    DimensionBrechaId, ObjetivoId, MedidaDescripcion, Cobertura, NivelMedida, OtroServicio,
    CostoAsociado, EstadoAvance, Observaciones,
    CreatedAt, UpdatedAt, Version, Active, CreatedBy
) VALUES (
    1, 1, 'Medida de prueba', 'Cobertura test', 'Nivel test', 'Servicio test',
    '$100.000', 'No completada', 'Observaciones de prueba',
    GETDATE(), GETDATE(), 1, 1, 'system'
);
*/

-- Test 2: Insertar una nueva tarea con el nuevo campo
-- NOTA: Esto es solo un ejemplo, NO ejecutar en producción sin datos válidos
/*
INSERT INTO PlanGestion_Tareas (
    DimensionBrechaId, AccionId, Nombre, FechaInicio, FechaFin, Responsable, EstadoAvance,
    DescripcionTareaEjecutada, Observaciones,
    CreatedAt, UpdatedAt, Version, Active, CreatedBy
) VALUES (
    1, 1, 'Tarea de prueba', GETDATE(), DATEADD(month, 1, GETDATE()), 'Responsable Test', 'En progreso',
    'Descripción de la tarea ejecutada para prueba', 'Observaciones de la tarea',
    GETDATE(), GETDATE(), 1, 1, 'system'
);
*/

-- ===== PRUEBAS DE ACTUALIZACIÓN =====

-- Test 3: Actualizar una acción existente con los nuevos campos
-- NOTA: Ajustar el ID según datos reales
/*
UPDATE Acciones 
SET 
    CostoAsociado = '$150.000',
    EstadoAvance = 'Completada',
    Observaciones = 'Acción completada exitosamente',
    UpdatedAt = GETDATE(),
    Version = Version + 1
WHERE Id = 1; -- Cambiar por un ID válido
*/

-- Test 4: Actualizar una tarea existente con el nuevo campo
-- NOTA: Ajustar el ID según datos reales
/*
UPDATE PlanGestion_Tareas 
SET 
    DescripcionTareaEjecutada = 'Tarea ejecutada con éxito, se completaron todos los objetivos',
    UpdatedAt = GETDATE(),
    Version = Version + 1
WHERE Id = 1; -- Cambiar por un ID válido
*/

-- ===== PRUEBAS DE CONSULTA =====

-- Test 5: Consultar acciones con los nuevos campos
SELECT TOP 5
    Id,
    MedidaDescripcion,
    CostoAsociado,
    EstadoAvance,
    Observaciones,
    CreatedAt,
    UpdatedAt
FROM Acciones
WHERE Active = 1
ORDER BY CreatedAt DESC;

-- Test 6: Consultar tareas con el nuevo campo
SELECT TOP 5
    Id,
    Nombre,
    Responsable,
    EstadoAvance,
    DescripcionTareaEjecutada,
    CreatedAt,
    UpdatedAt
FROM PlanGestion_Tareas
WHERE Active = 1
ORDER BY CreatedAt DESC;

-- ===== VALIDACIONES =====

-- Test 7: Verificar que los campos pueden ser NULL/NOT NULL según diseño
SELECT 
    'Campos opcionales en Acciones' as Verificacion,
    COUNT(*) as Total,
    SUM(CASE WHEN CostoAsociado IS NULL THEN 1 ELSE 0 END) as CostoNull,
    SUM(CASE WHEN EstadoAvance IS NULL THEN 1 ELSE 0 END) as EstadoNull,
    SUM(CASE WHEN Observaciones IS NULL THEN 1 ELSE 0 END) as ObservacionesNull
FROM Acciones
WHERE Active = 1

UNION ALL

SELECT 
    'Campo requerido en Tareas' as Verificacion,
    COUNT(*) as Total,
    SUM(CASE WHEN DescripcionTareaEjecutada IS NULL OR DescripcionTareaEjecutada = '' THEN 1 ELSE 0 END) as DescripcionVacia,
    0 as EstadoNull,
    0 as ObservacionesNull
FROM PlanGestion_Tareas
WHERE Active = 1;
-- =============================================
-- Script para actualizar registro ID 87 en tabla programas
-- Fecha: 2025-06-06
-- Propósito: Restaurar valores del registro más antiguo
-- =============================================

USE gestiona_energia;
GO

-- =============================================
-- DIFERENCIAS IDENTIFICADAS ENTRE REGISTROS:
-- =============================================
-- REGISTRO MÁS RECIENTE (Version 7):
-- - Version: 7
-- - UpdatedAt: 2025-03-20 15:01:33.9969899
-- - ModifiedBy: 79ed5057-28db-49f3-b578-20850c528ec6
-- - CreatedBy: 79ed5057-28db-49f3-b578-20850c528ec6
--
-- REGISTRO MÁS ANTIGUO (Version 6) - VALORES A RESTAURAR:
-- - Version: 6
-- - UpdatedAt: 17-12-2024 19:05 (2024-12-17 19:05:00)
-- - ModifiedBy: 79ed5057-28db-49f3-b578-20850c528ec6
-- - CreatedBy: 79ed5057-28db-49f3-b578-20850c528ec6
-- =============================================

-- Verificar si el registro existe antes de la actualización
IF NOT EXISTS (SELECT 1 FROM programas WHERE Id = 87)
BEGIN
    PRINT 'ERROR: No existe el registro con ID 87 en la tabla programas'
    RETURN
END

-- Mostrar estado actual del registro antes de la actualización
PRINT 'Estado actual del registro ID 87:'
SELECT Id, CreatedAt, UpdatedAt, Version, Active, ServicioId, ModifiedBy, CreatedBy,
       Nombre, Descripcion, AdjuntoUrl, AdjuntoNombre
FROM programas 
WHERE Id = 87;

-- Iniciar transacción para manejo de errores
BEGIN TRANSACTION;

BEGIN TRY
    -- Actualizar registro ID 87 con valores del registro más antiguo
    UPDATE programas 
    SET 
        -- CAMPOS QUE CAMBIAN - Restaurar valores del registro más antiguo:
        UpdatedAt = '2024-12-17 19:05:00',      -- CAMBIO: De '2025-03-20 15:01:33.9969899' a '2024-12-17 19:05:00'
        Version = 6,                            -- CAMBIO: De 7 a 6 (versión más antigua)
        
        -- CAMPOS QUE SE MANTIENEN IGUALES:
        Active = 1,                             -- MANTIENE: VERDADERO (1)
        ServicioId = 628,                       -- MANTIENE: Mismo valor
        ModifiedBy = '79ed5057-28db-49f3-b578-20850c528ec6',  -- MANTIENE: Mismo valor
        CreatedBy = '79ed5057-28db-49f3-b578-20850c528ec6',   -- MANTIENE: Mismo valor
        Nombre = 'Programa de Trabajo de Gestión Medioambiental de la Dirección de Aeropuertos',  -- MANTIENE: Mismo valor
        Descripcion = 'Programa de Trabajo de Gestión Medioambiental de la Dirección de Aeropuertos',  -- MANTIENE: Mismo valor
        AdjuntoUrl = '\\fs-apps.minenergia.cl\gestiona_energia\RepGestionaEnergia\estado_verde\documentos\053f6fce-4f49-49f7-ac72-7244a82bc135.xlsx',  -- MANTIENE: Mismo valor
        AdjuntoNombre = 'Programa trabajo 2025 Plan de Gestión ambiental DAP 2024_Cargado red_rev UGEM.xlsx'  -- MANTIENE: Mismo valor
        
    WHERE Id = 87;

    -- Verificar que la actualización fue exitosa
    IF @@ROWCOUNT = 1
    BEGIN
        PRINT 'SUCCESS: Registro ID 87 actualizado correctamente'
        
        -- Mostrar estado después de la actualización
        PRINT 'Estado después de la actualización:'
        SELECT Id, CreatedAt, UpdatedAt, Version, Active, ServicioId, ModifiedBy, CreatedBy,
               Nombre, Descripcion, AdjuntoUrl, AdjuntoNombre
        FROM programas 
        WHERE Id = 87;
        
        -- Confirmar transacción
        COMMIT TRANSACTION;
        PRINT 'Transacción confirmada exitosamente.'
    END
    ELSE
    BEGIN
        PRINT 'ERROR: No se pudo actualizar el registro ID 87'
        ROLLBACK TRANSACTION;
        RAISERROR('Falló la actualización del registro', 16, 1);
    END

END TRY
BEGIN CATCH
    -- Manejo de errores
    PRINT 'ERROR: ' + ERROR_MESSAGE();
    PRINT 'Número de error: ' + CAST(ERROR_NUMBER() AS VARCHAR(10));
    PRINT 'Línea de error: ' + CAST(ERROR_LINE() AS VARCHAR(10));
    PRINT 'Procedimiento: ' + ISNULL(ERROR_PROCEDURE(), 'Script principal');
    
    -- Deshacer transacción en caso de error
    IF @@TRANCOUNT > 0
    BEGIN
        ROLLBACK TRANSACTION;
        PRINT 'Transacción revertida debido al error.'
    END
END CATCH

-- =============================================
-- RESUMEN DE CAMBIOS APLICADOS:
-- =============================================
-- CAMBIOS REALIZADOS (Restaurar registro más antiguo):
-- 1. UpdatedAt: '2025-03-20 15:01:33.9969899' → '2024-12-17 19:05:00'
-- 2. Version: 7 → 6 (versión más antigua)
--
-- CAMPOS SIN CAMBIOS:
-- - Id: 87 (identificador único)
-- - CreatedAt: 10-09-2024 12:16 (2024-09-10 12:16:01.4121978)
-- - Active: 1 (VERDADERO)
-- - ServicioId: 628
-- - ModifiedBy: 79ed5057-28db-49f3-b578-20850c528ec6
-- - CreatedBy: 79ed5057-28db-49f3-b578-20850c528ec6
-- - Nombre: Programa de Trabajo de Gestión Medioambiental de la Dirección de Aeropuertos
-- - Descripcion: Programa de Trabajo de Gestión Medioambiental de la Dirección de Aeropuertos
-- - AdjuntoUrl: \\fs-apps.minenergia.cl\gestiona_energia\RepGestionaEnergia\estado_verde\documentos\053f6fce-4f49-49f7-ac72-7244a82bc135.xlsx
-- - AdjuntoNombre: Programa trabajo 2025 Plan de Gestión ambiental DAP 2024_Cargado red_rev UGEM.xlsx
-- =============================================

PRINT 'Script de actualización para programas ID 87 completado.';
GO
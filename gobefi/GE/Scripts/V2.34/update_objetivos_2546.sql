-- =============================================
-- Script para actualizar registro ID 2546 en tabla objetivos
-- Fecha: 2025-06-06
-- Propósito: Restaurar valores del registro más antiguo
-- =============================================

USE gestiona_energia;
GO

-- Verificar si el registro existe antes de la actualización
IF NOT EXISTS (SELECT 1 FROM objetivos WHERE Id = 2546)
BEGIN
    PRINT 'ERROR: No existe el registro con ID 2546 en la tabla objetivos'
    RETURN
END

-- Mostrar estado actual del registro antes de la actualización
PRINT 'Estado actual del registro ID 2546:'
SELECT Id, CreatedAt, UpdatedAt, Version, Active, ModifiedBy, CreatedBy, 
       DimensionBrechaId, Titulo, Descripcion, ObjetivoId
FROM objetivos 
WHERE Id = 2546;

-- Iniciar transacción para manejo de errores
BEGIN TRANSACTION;

BEGIN TRY
    -- Actualizar registro ID 2546 con valores del registro más antiguo
    UPDATE objetivos 
    SET 
        -- CreatedAt se mantiene igual (no se especifica cambio)
        UpdatedAt = NULL,                    -- CAMBIO: De '2025-05-15 17:32:58.0079596' a NULL (vacío)
        Version = 1,                         -- CAMBIO: De 2 a 1
        Active = 1,                          -- MANTIENE: VERDADERO equivale a 1 en SQL Server
        ModifiedBy = NULL,                   -- CAMBIO: De '4d6eb304-6b27-41e6-8f8a-35fc144ccab3' a NULL (vacío)
        CreatedBy = '4d6eb304-6b27-41e6-8f8a-35fc144ccab3',  -- MANTIENE: Mismo valor
        DimensionBrechaId = 3,               -- MANTIENE: Mismo valor
        Titulo = 'Disminuir el consumo del agua en un 5% en las unidades que es posible obtener la facturación',  -- CAMBIO: Se elimina "a lo más"
        Descripcion = 'Propender a reducir por lo menos en un  5% el  uso del agua, en aquellas unidades donde sea factible',  -- MANTIENE: Mismo valor
        ObjetivoId = NULL                    -- CAMBIO: Se mantiene como NULL (vacío)
    WHERE Id = 2546;

    -- Verificar que la actualización fue exitosa
    IF @@ROWCOUNT = 1
    BEGIN
        PRINT 'SUCCESS: Registro ID 2546 actualizado correctamente'
        
        -- Mostrar estado después de la actualización
        PRINT 'Estado después de la actualización:'
        SELECT Id, CreatedAt, UpdatedAt, Version, Active, ModifiedBy, CreatedBy, 
               DimensionBrechaId, Titulo, Descripcion, ObjetivoId
        FROM objetivos 
        WHERE Id = 2546;
        
        -- Confirmar transacción
        COMMIT TRANSACTION;
    END
    ELSE
    BEGIN
        PRINT 'ERROR: No se pudo actualizar el registro ID 2546'
        ROLLBACK TRANSACTION;
    END

END TRY
BEGIN CATCH
    -- Manejo de errores
    PRINT 'ERROR: ' + ERROR_MESSAGE();
    PRINT 'Número de error: ' + CAST(ERROR_NUMBER() AS VARCHAR(10));
    PRINT 'Línea de error: ' + CAST(ERROR_LINE() AS VARCHAR(10));
    
    -- Deshacer transacción en caso de error
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
END CATCH

-- =============================================
-- RESUMEN DE CAMBIOS APLICADOS:
-- =============================================
-- 1. UpdatedAt: '2025-05-15 17:32:58.0079596' → NULL
-- 2. Version: 2 → 1  
-- 3. ModifiedBy: '4d6eb304-6b27-41e6-8f8a-35fc144ccab3' → NULL
-- 4. Titulo: Se elimina "a lo más" del texto original
-- 5. ObjetivoId: Se mantiene como NULL
--
-- CAMPOS SIN CAMBIOS:
-- - Id: 2546 (identificador único)
-- - CreatedAt: Se mantiene el valor original
-- - Active: 1 (equivale a VERDADERO)
-- - CreatedBy: '4d6eb304-6b27-41e6-8f8a-35fc144ccab3'
-- - DimensionBrechaId: 3
-- - Descripcion: Sin cambios
-- =============================================
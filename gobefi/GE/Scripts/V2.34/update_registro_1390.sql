-- ================================================================
-- Script de actualización para registro ID 1390 en tabla brechas
-- Fecha de creación: 2025-01-06
-- Descripción: Actualiza el registro con los valores del segundo registro
--              (registro más antiguo)
-- ================================================================

-- DIFERENCIAS IDENTIFICADAS:
-- 1. UpdatedAt: 2025-03-13 11:40:14.4766027 → 26-12-2024 19:05 (segundo registro)
-- 2. Version: 14 → 13 (segundo registro tiene versión más baja)
-- 3. CreatedAt: Normalización al formato del segundo registro

USE gestiona_energia;
GO

-- Verificar que el registro existe antes de actualizar
IF EXISTS (SELECT 1 FROM brechas WHERE Id = 1390)
BEGIN
    PRINT 'Iniciando actualización del registro ID 1390 en tabla brechas...';
    
    -- Actualizar el registro con los valores del segundo registro (más antiguo)
    UPDATE brechas 
    SET 
        -- Actualización de fecha del segundo registro
        UpdatedAt = '2024-12-26 19:05:00',
        
        -- Versión del segundo registro (más baja)
        Version = 13,
        
        -- Formato de CreatedAt del segundo registro
        CreatedAt = '2024-10-10 10:59:00'
        
    WHERE Id = 1390;
    
    -- Verificar que la actualización fue exitosa
    IF @@ROWCOUNT = 1
    BEGIN
        PRINT 'Registro ID 1390 actualizado exitosamente.';
        PRINT 'Cambios aplicados desde el segundo registro:';
        PRINT '- UpdatedAt: 2025-03-13 11:40:14.4766027 → 2024-12-26 19:05:00';
        PRINT '- Version: 14 → 13';
        PRINT '- CreatedAt: 2024-10-10 10:59:02.6022552 → 2024-10-10 10:59:00';
    END
    ELSE
    BEGIN
        PRINT 'ERROR: No se pudo actualizar el registro ID 1390.';
        RAISERROR('Falló la actualización del registro', 16, 1);
    END
END
ELSE
BEGIN
    PRINT 'ERROR: No se encontró el registro con ID 1390 en tabla brechas.';
    RAISERROR('Registro no encontrado', 16, 1);
END

-- Mostrar el estado final del registro actualizado
SELECT 
    Id,
    CreatedAt,
    UpdatedAt,
    Version,
    Active,
    ModifiedBy,
    CreatedBy,
    ServicioId,
    Descripcion,
    DimensionBrechaId,
    TipoBrecha,
    Titulo,
    Priorizacion,
    ObjetivoId
FROM brechas 
WHERE Id = 1390;

PRINT 'Script de actualización completado.';
GO
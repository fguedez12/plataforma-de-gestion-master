USE gestiona_energia;

-- =========================================================================
-- Script: update_acciones_multiple.sql
-- Descripción: Actualiza múltiples registros de la tabla acciones
--              manteniendo los valores de los registros más antiguos
-- Fecha: 06/06/2025
-- Versión: 2.34
-- CORRECCIONES: Se reemplazaron valores NULL en UpdatedAt por CreatedAt
--               ya que la columna UpdatedAt no permite valores NULL
-- =========================================================================

BEGIN TRANSACTION;

BEGIN TRY
    PRINT 'Iniciando actualización de registros múltiples en tabla acciones...';
    
    -- =====================================================================
    -- REGISTRO ID 607
    -- Diferencias encontradas:
    -- - Version: 2 -> 1 (mantener versión antigua)
    -- - UpdatedAt: 2025-04-03 11:24:52.8775142 -> CreatedAt (usar fecha de creación - columna no permite NULL)
    -- - ModifiedBy: 7e1951bb-43cb-4be9-8cf1-42671bc2e4d6 -> NULL (mantener vacío)
    -- - Piloto: 0 -> 0 (FALSO convertido a 0)
    -- NOTA: UpdatedAt no permite NULL, se usa CreatedAt como valor por defecto
    -- =====================================================================
    
    PRINT 'Verificando registro ID 607 antes de la actualización...';
    SELECT Id, Version, CreatedAt, UpdatedAt, ModifiedBy, Piloto
    FROM acciones
    WHERE Id = 607;
    
    UPDATE acciones
    SET
        Version = 1,
        UpdatedAt = CreatedAt, -- Usar fecha de creación ya que la columna no permite NULL
        ModifiedBy = NULL,
        Piloto = 0
    WHERE Id = 607;
    
    PRINT 'Registro ID 607 actualizado. Verificando cambios...';
    SELECT Id, Version, CreatedAt, UpdatedAt, ModifiedBy, Piloto
    FROM acciones
    WHERE Id = 607;
    
    -- =====================================================================
    -- REGISTRO ID 1958
    -- Diferencias encontradas:
    -- - Version: 5 -> 4 (mantener versión antigua)
    -- - UpdatedAt: 2025-04-17 12:47:43.8235099 -> 2024-12-23 23:04:00 (mantener fecha antigua)
    -- - ModifiedBy: bef71efd-ea55-4ba7-ab47-995959d50329 -> eb9a228e-5752-412d-a47a-d51d70cbbe9d (mantener usuario antiguo)
    -- - PresupuestoImplementacion: 0 -> NULL (mantener vacío como NULL)
    -- - PresupuestoImplementacionPedido: 0 -> 0 (FALSO convertido a 0)
    -- - Piloto: 0 -> 0 (FALSO convertido a 0)
    -- - ResponsableEmail: NULL -> NULL (mantener vacío)
    -- - ResponsableNombre: NULL -> NULL (mantener vacío)
    -- =====================================================================
    
    PRINT 'Verificando registro ID 1958 antes de la actualización...';
    SELECT Id, Version, UpdatedAt, ModifiedBy, PresupuestoImplementacion, 
           PresupuestoImplementacionPedido, Piloto, ResponsableEmail, ResponsableNombre
    FROM acciones 
    WHERE Id = 1958;
    
    UPDATE acciones 
    SET 
        Version = 4,
        UpdatedAt = '2024-12-23 23:04:00',
        ModifiedBy = 'eb9a228e-5752-412d-a47a-d51d70cbbe9d',
        PresupuestoImplementacion = NULL,
        PresupuestoImplementacionPedido = 0,
        Piloto = 0,
        ResponsableEmail = NULL,
        ResponsableNombre = NULL
    WHERE Id = 1958;
    
    PRINT 'Registro ID 1958 actualizado. Verificando cambios...';
    SELECT Id, Version, UpdatedAt, ModifiedBy, PresupuestoImplementacion, 
           PresupuestoImplementacionPedido, Piloto, ResponsableEmail, ResponsableNombre
    FROM acciones 
    WHERE Id = 1958;
    
    -- =====================================================================
    -- REGISTRO ID 2094
    -- Diferencias encontradas:
    -- - Version: 2 -> 1 (mantener versión antigua)
    -- - UpdatedAt: 2025-03-25 14:41:58.8137938 -> CreatedAt (usar fecha de creación - columna no permite NULL)
    -- - ModifiedBy: 79ed5057-28db-49f3-b578-20850c528ec6 -> NULL (mantener vacío)
    -- - ResponsableEmail: rosa.araya@mop.gov.cl -> jaqueline.morales@mop.gov.cl (mantener email antiguo)
    -- - ResponsableNombre: Rosa Araya Castillo -> Jaqueline Morales (mantener nombre antiguo)
    -- NOTA: UpdatedAt no permite NULL, se usa CreatedAt como valor por defecto
    -- =====================================================================
    
    PRINT 'Verificando registro ID 2094 antes de la actualización...';
    SELECT Id, Version, CreatedAt, UpdatedAt, ModifiedBy, ResponsableEmail, ResponsableNombre
    FROM acciones
    WHERE Id = 2094;
    
    UPDATE acciones
    SET
        Version = 1,
        UpdatedAt = CreatedAt, -- Usar fecha de creación ya que la columna no permite NULL
        ModifiedBy = NULL,
        ResponsableEmail = 'jaqueline.morales@mop.gov.cl',
        ResponsableNombre = 'Jaqueline Morales'
    WHERE Id = 2094;
    
    PRINT 'Registro ID 2094 actualizado. Verificando cambios...';
    SELECT Id, Version, CreatedAt, UpdatedAt, ModifiedBy, ResponsableEmail, ResponsableNombre
    FROM acciones
    WHERE Id = 2094;
    
    -- =====================================================================
    -- REGISTRO ID 2351
    -- Diferencias encontradas:
    -- - Version: 4 -> 2 (mantener versión antigua)
    -- - UpdatedAt: 2025-01-01 00:09:56.3370688 -> 2024-10-11 19:42:00 (mantener fecha antigua)
    -- - AdjuntoNombre: RC, OC FACT COT.pdf -> Cotizacion6906.pdf (mantener nombre adjunto antiguo)
    -- - PresupuestoImplementacion: 178500 -> 107100 (mantener presupuesto antiguo)
    -- - Piloto: 1 -> 1 (VERDADERO convertido a 1)
    -- =====================================================================
    
    PRINT 'Verificando registro ID 2351 antes de la actualización...';
    SELECT Id, Version, UpdatedAt, AdjuntoNombre, PresupuestoImplementacion, Piloto
    FROM acciones 
    WHERE Id = 2351;
    
    UPDATE acciones 
    SET 
        Version = 2,
        UpdatedAt = '2024-10-11 19:42:00',
        AdjuntoNombre = 'Cotizacion6906.pdf',
        PresupuestoImplementacion = 107100,
        Piloto = 1
    WHERE Id = 2351;
    
    PRINT 'Registro ID 2351 actualizado. Verificando cambios...';
    SELECT Id, Version, UpdatedAt, AdjuntoNombre, PresupuestoImplementacion, Piloto
    FROM acciones 
    WHERE Id = 2351;
    
    -- =====================================================================
    -- REGISTRO ID 2361
    -- Diferencias encontradas:
    -- - Version: 2 -> 1 (mantener versión antigua)
    -- - UpdatedAt: 2025-01-01 00:09:15.6753806 -> CreatedAt (usar fecha de creación - columna no permite NULL)
    -- - PresupuestoImplementacion: 749700 -> 428400 (mantener presupuesto antiguo)
    -- - Piloto: 1 -> 1 (VERDADERO convertido a 1)
    -- NOTA: UpdatedAt no permite NULL, se usa CreatedAt como valor por defecto
    -- =====================================================================
    
    PRINT 'Verificando registro ID 2361 antes de la actualización...';
    SELECT Id, Version, CreatedAt, UpdatedAt, PresupuestoImplementacion, Piloto
    FROM acciones
    WHERE Id = 2361;
    
    UPDATE acciones
    SET
        Version = 1,
        UpdatedAt = CreatedAt, -- Usar fecha de creación ya que la columna no permite NULL
        PresupuestoImplementacion = 428400,
        Piloto = 1
    WHERE Id = 2361;
    
    PRINT 'Registro ID 2361 actualizado. Verificando cambios...';
    SELECT Id, Version, CreatedAt, UpdatedAt, PresupuestoImplementacion, Piloto
    FROM acciones
    WHERE Id = 2361;
    
    -- =====================================================================
    -- REGISTRO ID 2521
    -- Diferencias encontradas:
    -- - Version: 8 -> 7 (mantener versión antigua)
    -- - UpdatedAt: 2025-02-18 16:25:44.8779757 -> 2024-12-27 17:49:00 (mantener fecha antigua)
    -- - AdjuntoNombre: NULL -> NULL (mantener vacío como NULL)
    -- =====================================================================
    
    PRINT 'Verificando registro ID 2521 antes de la actualización...';
    SELECT Id, Version, UpdatedAt, AdjuntoNombre
    FROM acciones 
    WHERE Id = 2521;
    
    UPDATE acciones 
    SET 
        Version = 7,
        UpdatedAt = '2024-12-27 17:49:00',
        AdjuntoNombre = NULL
    WHERE Id = 2521;
    
    PRINT 'Registro ID 2521 actualizado. Verificando cambios...';
    SELECT Id, Version, UpdatedAt, AdjuntoNombre
    FROM acciones 
    WHERE Id = 2521;
    
    -- =====================================================================
    -- REGISTRO ID 2714
    -- Diferencias encontradas:
    -- - Version: 4 -> 3 (mantener versión antigua)
    -- - UpdatedAt: 2025-01-13 15:02:09.9017342 -> 2024-12-30 21:04:00 (mantener fecha antigua)
    -- =====================================================================
    
    PRINT 'Verificando registro ID 2714 antes de la actualización...';
    SELECT Id, Version, UpdatedAt
    FROM acciones 
    WHERE Id = 2714;
    
    UPDATE acciones 
    SET 
        Version = 3,
        UpdatedAt = '2024-12-30 21:04:00'
    WHERE Id = 2714;
    
    PRINT 'Registro ID 2714 actualizado. Verificando cambios...';
    SELECT Id, Version, UpdatedAt
    FROM acciones 
    WHERE Id = 2714;
    
    -- =====================================================================
    -- VERIFICACIÓN FINAL
    -- =====================================================================
    
    PRINT 'Verificación final de todos los registros actualizados:';
    SELECT Id, Version, CreatedAt, UpdatedAt, ModifiedBy, AdjuntoNombre,
           PresupuestoImplementacion, PresupuestoImplementacionPedido,
           Piloto, ResponsableEmail, ResponsableNombre
    FROM acciones
    WHERE Id IN (607, 1958, 2094, 2351, 2361, 2521, 2714)
    ORDER BY Id;
    
    PRINT 'Actualización de registros múltiples completada exitosamente.';
    
    COMMIT TRANSACTION;
    PRINT 'Transacción confirmada.';
    
END TRY
BEGIN CATCH
    PRINT 'Error durante la actualización: ' + ERROR_MESSAGE();
    PRINT 'Línea del error: ' + CAST(ERROR_LINE() AS VARCHAR(10));
    PRINT 'Procedimiento: ' + ISNULL(ERROR_PROCEDURE(), 'Script principal');
    
    ROLLBACK TRANSACTION;
    PRINT 'Transacción revertida debido al error.';
    
    -- Re-lanzar el error para que sea visible
    THROW;
END CATCH;

-- =========================================================================
-- FIN DEL SCRIPT
-- =========================================================================
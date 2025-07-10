USE gestiona_energia;

-- =========================================================================
-- Script: update_indicadores_multiple.sql
-- Descripción: Actualiza múltiples registros de la tabla indicadores
--              manteniendo los valores de los registros más antiguos
-- Fecha: 06/06/2025
-- Versión: 2.34
-- CORRECCIONES: Se reemplazaron valores NULL en UpdatedAt por CreatedAt
--               ya que la columna UpdatedAt no permite valores NULL
-- =========================================================================

BEGIN TRANSACTION;

BEGIN TRY
    PRINT 'Iniciando actualización de registros múltiples en tabla indicadores...';
    
    -- =====================================================================
    -- REGISTRO ID 2546
    -- Diferencias encontradas:
    -- - Version: 2 -> 1 (mantener versión antigua)
    -- - UpdatedAt: 2025-05-20 14:42:27.2764736 -> CreatedAt (usar fecha de creación - columna no permite NULL) 
    -- - ModifiedBy: 4d6eb304-6b27-41e6-8f8a-35fc144ccab3 -> NULL (mantener vacío)
    -- - UnidadMedida: porcentaje -> NULL (mantener vacío)
    -- NOTA: UpdatedAt no permite NULL, se usa CreatedAt como valor por defecto
    -- =====================================================================
    
    PRINT 'Verificando registro ID 2546 antes de la actualización...';
    SELECT Id, Version, CreatedAt, UpdatedAt, ModifiedBy, UnidadMedida
    FROM indicadores
    WHERE Id = 2546;
    
    UPDATE indicadores
    SET
        Version = 1,
        UpdatedAt = CreatedAt, -- Usar fecha de creación ya que la columna no permite NULL
        ModifiedBy = NULL,
        UnidadMedida = NULL
    WHERE Id = 2546;
    
    PRINT 'Registro ID 2546 actualizado. Verificando cambios...';
    SELECT Id, Version, CreatedAt, UpdatedAt, ModifiedBy, UnidadMedida
    FROM indicadores
    WHERE Id = 2546;
    
    -- =====================================================================
    -- REGISTRO ID 2549
    -- Diferencias encontradas:
    -- - Version: 3 -> 1 (mantener versión antigua)
    -- - UpdatedAt: 2025-05-20 14:34:56.5621668 -> CreatedAt (usar fecha de creación - columna no permite NULL)
    -- - ModifiedBy: 4d6eb304-6b27-41e6-8f8a-35fc144ccab3 -> NULL (mantener vacío)
    -- - Numerador: Cambio de "N° de adquisiciones a través de licitación pública..." -> "N° de compras a través de licitación pública..." (mantener texto antiguo)
    -- - Denominador: Cambio de "N° de adquisiciones a través de licitación pública..." -> "N° de compras a través de licitación pública..." (mantener texto antiguo)
    -- - UnidadMedida: Porcentaje -> Porcentaje (mantener valor antiguo)
    -- NOTA: UpdatedAt no permite NULL, se usa CreatedAt como valor por defecto
    -- =====================================================================
    
    PRINT 'Verificando registro ID 2549 antes de la actualización...';
    SELECT Id, Version, CreatedAt, UpdatedAt, ModifiedBy, Numerador, Denominador, UnidadMedida
    FROM indicadores
    WHERE Id = 2549;
    
    UPDATE indicadores
    SET
        Version = 1,
        UpdatedAt = CreatedAt, -- Usar fecha de creación ya que la columna no permite NULL
        ModifiedBy = NULL,
        Numerador = 'N° de compras a través de licitación pública adjudicadas y compras ágiles realizadas que utilicen criterios de evaluación y/o requisitos sustentables en el periodo t',
        Denominador = 'N° de compras a través de licitación pública adjudicadas y compras ágiles realizadas en rubros factibles de aplicar criterios de sustentabilidad en el periodo t',
        UnidadMedida = 'Porcentaje'
    WHERE Id = 2549;
    
    PRINT 'Registro ID 2549 actualizado. Verificando cambios...';
    SELECT Id, Version, CreatedAt, UpdatedAt, ModifiedBy, Numerador, Denominador, UnidadMedida
    FROM indicadores
    WHERE Id = 2549;
    
    -- =====================================================================
    -- REGISTRO ID 2757
    -- Diferencias encontradas:
    -- - Version: 4 -> 2 (mantener versión antigua)
    -- - UpdatedAt: 2025-05-07 11:25:40.5385387 -> 2024-12-30 13:57:00 (mantener fecha antigua)
    -- - UnidadMedida: "Informe de diagnóstico de la infraestructura en materia de eficiencia energética." -> "Informe de diagnóstico de la infraestructura en materia de eficiencia energética" (sin punto final - mantener texto antiguo)
    -- - Valor: 1 -> 1 (mantener valor antiguo)
    -- =====================================================================
    
    PRINT 'Verificando registro ID 2757 antes de la actualización...';
    SELECT Id, Version, UpdatedAt, UnidadMedida, Valor
    FROM indicadores
    WHERE Id = 2757;
    
    UPDATE indicadores
    SET
        Version = 2,
        UpdatedAt = '2024-12-30 13:57:00',
        UnidadMedida = 'Informe de diagnóstico de la infraestructura en materia de eficiencia energética',
        Valor = 1
    WHERE Id = 2757;
    
    PRINT 'Registro ID 2757 actualizado. Verificando cambios...';
    SELECT Id, Version, UpdatedAt, UnidadMedida, Valor
    FROM indicadores
    WHERE Id = 2757;
    
    -- =====================================================================
    -- REGISTRO ID 2990
    -- Diferencias encontradas:
    -- - Version: 4 -> 2 (mantener versión antigua)
    -- - UpdatedAt: 2025-03-25 10:32:39.9331779 -> 2024-12-30 21:49:00 (mantener fecha antigua)
    -- - ModifiedBy: 56b777e5-ecc7-4a3d-8cfe-c030897904bc -> 2232bfab-db74-4374-8388-1f116c71fe02 (mantener usuario antiguo)
    -- - Numerador: Cambio de "Kilogramos de CO₂..." -> "Toneladas de CO₂..." (mantener texto antiguo)
    -- - UnidadMedida: "Kg. de CO₂ / Km recorridos" -> "Unidad" (mantener unidad antigua)
    -- - Valor: NULL -> NULL (mantener vacío como NULL)
    -- =====================================================================
    
    PRINT 'Verificando registro ID 2990 antes de la actualización...';
    SELECT Id, Version, UpdatedAt, ModifiedBy, Numerador, UnidadMedida, Valor
    FROM indicadores
    WHERE Id = 2990;
    
    UPDATE indicadores
    SET
        Version = 2,
        UpdatedAt = '2024-12-30 21:49:00',
        ModifiedBy = '2232bfab-db74-4374-8388-1f116c71fe02',
        Numerador = 'Toneladas de CO₂ emitidas durante año t (litros combustibles consumidos por los vehículos de la Dirección Nacional en período t * factor )',
        UnidadMedida = 'Unidad',
        Valor = NULL
    WHERE Id = 2990;
    
    PRINT 'Registro ID 2990 actualizado. Verificando cambios...';
    SELECT Id, Version, UpdatedAt, ModifiedBy, Numerador, UnidadMedida, Valor
    FROM indicadores
    WHERE Id = 2990;
    
    -- =====================================================================
    -- VERIFICACIÓN FINAL
    -- =====================================================================
    
    PRINT 'Verificación final de todos los registros actualizados:';
    SELECT Id, Version, CreatedAt, UpdatedAt, ModifiedBy, Numerador, Denominador,
           UnidadMedida, Valor
    FROM indicadores
    WHERE Id IN (2546, 2549, 2757, 2990)
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
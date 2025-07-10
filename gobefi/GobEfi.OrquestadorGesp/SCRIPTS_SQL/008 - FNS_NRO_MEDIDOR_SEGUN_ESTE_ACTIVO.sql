SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Iván Molina Huerta
-- Create date: 2020-08-13 13:05
-- Description:	Arma un nro de medidor dependiendo del Estado active del medidor (@ActiveFlagMedidor)
-- =============================================
CREATE FUNCTION FNS_NRO_MEDIDOR_SEGUN_ESTE_ACTIVO 
(
@ActiveFlagMedidor bit, 
@NumeroMedidor varchar(1024)
)
RETURNS VARCHAR(1024) 
AS
BEGIN
	declare @Result varchar(1024)
	
	SET @Result =
	CASE @ActiveFlagMedidor
		WHEN 0 THEN '~' + @NumeroMedidor
	else
		' ' + @NumeroMedidor
	end
	RETURN @Result

END
GO


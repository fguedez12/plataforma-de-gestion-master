using System;
namespace OrquestadorGesp.ContextEFNetCore
{
	public class _BasePkEntity : _BaseEntity
	{
		// Clave primaria obtenida para reportes 
		// donde el campo Int64PK se saca de ROW_NUMBER 
		// con ORDER BY de una tupla unica desde el
		// select arrojado por el SP
		public long Int64PK {get; set;}
	}
}

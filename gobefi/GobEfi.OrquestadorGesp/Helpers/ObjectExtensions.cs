using OrquestadorGesp.Helpers.ArrayExtensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace OrquestadorGesp.Helpers
{
	public static class ObjectExtensions
	{
		private static readonly MethodInfo CloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

		public static bool IsPrimitive(this Type type)
		{
			if (type == typeof(String)) return true;
			return (type.IsValueType & type.IsPrimitive);
		}
    #region Para ver si un tipo es ancestro de otro o es del mismo tipo
    public static bool IsAncestorOrSameTipe(Type ancestorType, Type potentialDescendant)
    {
      return potentialDescendant.IsSubclassOf(ancestorType)
             || potentialDescendant == ancestorType;
    }
    #endregion

    #region Codigo para clonar objetos PROFUNDAMENTE
    public static Object Copy(this Object originalObject, NPOI.XSSF.UserModel.XSSFRow filaExcelInicial)
		{
			return InternalCopy(originalObject, new Dictionary<Object, Object>(new ReferenceEqualityComparer()));
		}
		private static Object InternalCopy(Object originalObject, IDictionary<Object, Object> visited)
		{
			if (originalObject == null) return null;
			var typeToReflect = originalObject.GetType();
			if (IsPrimitive(typeToReflect)) return originalObject;
			if (visited.ContainsKey(originalObject)) return visited[originalObject];
			if (typeof(Delegate).IsAssignableFrom(typeToReflect)) return null;
			var cloneObject = CloneMethod.Invoke(originalObject, null);
			if (typeToReflect.IsArray)
			{
				var arrayType = typeToReflect.GetElementType();
				if (IsPrimitive(arrayType) == false)
				{
					Array clonedArray = (Array)cloneObject;
					clonedArray.ForEach((array, indices) => array.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices));
				}

			}
			visited.Add(originalObject, cloneObject);
			CopyFields(originalObject, visited, cloneObject, typeToReflect);
			RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
			return cloneObject;
		}

		private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
		{
			if (typeToReflect.BaseType != null)
			{
				RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
				CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
			}
		}

		private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
		{
			foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
			{
				if (filter != null && filter(fieldInfo) == false) continue;
				if (IsPrimitive(fieldInfo.FieldType)) continue;
				var originalFieldValue = fieldInfo.GetValue(originalObject);
				var clonedFieldValue = InternalCopy(originalFieldValue, visited);
				fieldInfo.SetValue(cloneObject, clonedFieldValue);
			}
		}
		public static T Copy<T>(this T original)
		{
			return (T)Copy((Object)original);
		}
		#endregion

		#region Copiar Propiedades Nivel Superficial
		public static void CopyProperties(this object source, object destination)
		{
			// If any this null throw an exception
			if (source == null || destination == null)
				throw new Exception("Source or/and Destination Objects are null");
			// Getting the Types of the objects
			Type typeDest = destination.GetType();
			Type typeSrc = source.GetType();
			// Collect all the valid properties to map
			var results = from srcProp in typeSrc.GetProperties()
										let targetProperty = typeDest.GetProperty(srcProp.Name)
										where srcProp.CanRead
										&& targetProperty != null
										&& (targetProperty.GetSetMethod(true) != null && !targetProperty.GetSetMethod(true).IsPrivate)
										&& (targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) == 0
										&& targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType)
										select new { sourceProperty = srcProp, targetProperty = targetProperty };
			//map the properties
			foreach (var props in results)
			{
				props.targetProperty.SetValue(destination, props.sourceProperty.GetValue(source, null), null);
			}
		}
		#endregion

		#region para obtener propiedades con formato "GetPropValue<int>(obj, "PropiedadAlgo.SubPropiedadOtraCosa");"
		public static object GetPropObjValueFromObj(this object obj, string name)
		{
			foreach (string part in name.Split('.'))
			{
				if (obj == null) { return null; }

				Type type = obj.GetType();
				PropertyInfo info = type.GetProperty(part);
				if (info == null) { return null; }

				obj = info.GetValue(obj, null);
			}
			return obj;
		}

		public static T GetPropValue<T>(this object obj, string name)
		{
			object retval = GetPropObjValueFromObj(obj, name);
			if (retval == null) { return default(T); }

			// throws InvalidCastException if types are incompatible
			return (T)retval;
		}
		#endregion
	}

	public class ReferenceEqualityComparer : EqualityComparer<Object>
	{
		#region Codigo de extension para clonar objetos PROFUNDAMENTE
		public override bool Equals(object x, object y)
		{
			return ReferenceEquals(x, y);
		}
		public override int GetHashCode(object obj)
		{
			if (obj == null) return 0;
			return obj.GetHashCode();
		}
		#endregion
	}
}

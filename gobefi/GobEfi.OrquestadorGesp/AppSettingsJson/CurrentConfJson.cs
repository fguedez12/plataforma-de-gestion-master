using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.AppSettingsJson
{
	public class CurrentConfJson
	{
		private static ConfJson confJson = null;
		public static ConfJson CurrentConf {
			get { return confJson; }
			set {
				if (confJson == null)
				{
					confJson = value;
				}
			}
		}
	}
}

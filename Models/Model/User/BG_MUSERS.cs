using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model.User
{
	public class BG_MUSERS
	{
		public string? USER_TEXT { get; set; }
		public string? USER_NAME { get; set; }
		public string? USER_PASS { get; set; }
		public string? USER_DEPT { get; set; }
		public string? USER_MAIL { get; set; }
		public string? USER_MOBL { get; set; }
		public string? USER_IMAG { get; set; }
		public decimal USER_ROLE { get; set; }
		public decimal ACT { get; set; }
		public DateTime CDT { get; set; }
		public DateTime UDT { get; set; }
		public string? CDU { get; set; }
		public string? UDU { get; set; }
		public decimal VAR { get; set; }

		public static BG_MUSERS ConvertToModel(DataRow row)
		{
			return new BG_MUSERS
			{
				USER_TEXT = row.Table.Columns.Contains("USER_TEXT") ? Convert.ToString(row["USER_TEXT"]) : string.Empty,
				USER_NAME = row.Table.Columns.Contains("USER_NAME") ? Convert.ToString(row["USER_NAME"]) : string.Empty,
				USER_PASS = row.Table.Columns.Contains("USER_PASS") ? Convert.ToString(row["USER_PASS"]) : string.Empty,
				USER_DEPT = row.Table.Columns.Contains("USER_DEPT") ? Convert.ToString(row["USER_DEPT"]) : string.Empty,
				USER_MAIL = row.Table.Columns.Contains("USER_MAIL") ? Convert.ToString(row["USER_MAIL"]) : string.Empty,
				USER_MOBL = row.Table.Columns.Contains("USER_MOBL") ? Convert.ToString(row["USER_MOBL"]) : string.Empty,
				USER_IMAG = row.Table.Columns.Contains("USER_IMAG") ? Convert.ToString(row["USER_IMAG"]) : string.Empty,
				USER_ROLE = row.Table.Columns.Contains("USER_ROLE") ? Convert.ToInt32(row["USER_ROLE"]) : 0,
				ACT = row.Table.Columns.Contains("ACT") ? Convert.ToInt32(row["ACT"]) : 0,
				CDT = row.Table.Columns.Contains("CDT") ? Convert.ToDateTime(row["CDT"]) : DateTime.Now,
				UDT = row.Table.Columns.Contains("UDT") ? Convert.ToDateTime(row["UDT"]) : DateTime.Now,
				CDU = row.Table.Columns.Contains("CDU") ? Convert.ToString(row["CDU"]) : string.Empty,
				UDU = row.Table.Columns.Contains("UDU") ? Convert.ToString(row["UDU"]) : string.Empty,
				VAR = row.Table.Columns.Contains("VAR") ? Convert.ToInt32(row["VAR"]) : 0,
			};

		}
	}
}
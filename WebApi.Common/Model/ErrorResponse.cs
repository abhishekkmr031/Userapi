using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Common.Model
{
	public class ErrorResponse
	{
		public int StatusCode { get; set; }
		public string Error { get; set; } = string.Empty;
		public List<string> Details { get; set; } = new();
	}
}

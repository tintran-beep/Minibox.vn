using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Shared.Model.BaseModel
{
	public class ResponseModel<TModel>
	{
		public int? StatusCode { get; set; }
		public string? ErrorMessage { get; set; }
		public TModel? Data { get; set; }
	}
}

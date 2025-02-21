using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Shared.Model.BaseModel
{
	public class RequestModel<TModel>
	{
		public TModel? Data { get; set; }
	}
}

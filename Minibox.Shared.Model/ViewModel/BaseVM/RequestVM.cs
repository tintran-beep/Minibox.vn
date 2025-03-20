using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Shared.Model.ViewModel.BaseVM
{
	public class RequestVM<TModel>
	{
		public TModel? Data { get; set; }
	}
}

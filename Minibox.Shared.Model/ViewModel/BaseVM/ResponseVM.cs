using Minibox.Shared.Library.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Shared.Model.ViewModel.BaseVM
{
	public class ResponseVM<T>
	{
		public bool IsSuccess { get; set; } = false;
		public string Message { get; set; } = string.Empty;
		public T? Data { get; set; } = default;

		public ResponseVM() { }

		private ResponseVM(bool isSuccess, string message, T? data)
		{
			IsSuccess = isSuccess;
			Message = message;
			Data = data;
		}

		public static ResponseVM<T> Success(T data, string message = "Operation successful.")
		{
			return new ResponseVM<T>(true, message, data);
		}

		public static ResponseVM<T> Failure(string message)
		{
			return new ResponseVM<T>(false, message, default);
		}

		public static ResponseVM<T> Failure(T data, string message)
		{
			return new ResponseVM<T>(false, message, data);
		}
	}
}

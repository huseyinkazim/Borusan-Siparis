using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borusan.Data
{
	public class ServiceResponse<TDTOEntity>
	{
		public bool IsSuccess { get; set; }
		public string? Error { get; set; }
		public TDTOEntity? Result { get; set; }

		public ServiceResponse()
		{
			IsSuccess = true;
		}
		public ServiceResponse(TDTOEntity? result)
		{
			if (result == null)
			{
				IsSuccess = false;
				Error = "Değer bulunamadı";
			}
			IsSuccess = true;
			Result = result;
		}
		public ServiceResponse(string error)
		{
			IsSuccess = false;
			Error = error;
		}
	}
}

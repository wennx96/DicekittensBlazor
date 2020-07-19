using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DicekittensBlazor.Data
{
	[Table("Users")]
	public class AccountModel
	{
		[Key]
		public string UserID { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }
	}
}

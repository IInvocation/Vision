﻿using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Authentication.Data
{
	[EntityName("UserClaim")]
    public class UserClaimEntity : IEntity<int>
    {
	    /// <summary>	Gets or sets the identifier. </summary>
	    /// <value>	The identifier. </value>
	    public int Id { get; set; }

		/// <summary>	Gets or sets the identifier of the user. </summary>
		/// <value>	The identifier of the user. </value>
		public int UserId { get; set; }

		/// <summary>	Gets or sets the type. </summary>
		/// <value>	The type. </value>
		public string Type { get; set; }

		/// <summary>	Gets or sets the value. </summary>
		/// <value>	The value. </value>
		public string Value { get; set; }
    }
}
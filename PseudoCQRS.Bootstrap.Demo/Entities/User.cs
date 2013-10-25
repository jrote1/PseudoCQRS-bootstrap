using System;
using System.Collections.Generic;
using PseudoCQRS.Bootstrap.DataAccess;

namespace PseudoCQRS.Bootstrap.Demo.Entities
{
	public class User : BaseEntity
	{
		public virtual string Name { get; protected set; }
		public virtual string EmailAddress { get; protected set; }
		public virtual string Password { get; protected set; }

		protected User()
		{

		}

		public User( string name, string emailAddress, string password )
		{
			this.Name = name;
			this.EmailAddress = emailAddress;
			this.Password = password;
		}

	}
}
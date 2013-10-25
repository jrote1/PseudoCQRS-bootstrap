using System;
using System.Collections.Generic;
using System.Linq;
using PseudoCQRS.Bootstrap.DataAccess;
using PseudoCQRS.Bootstrap.Demo.Entities;

namespace PseudoCQRS.Bootstrap.Demo.Modules.Users.Add
{
	public class UserAddCommandHandler : ICommandHandler<UserAddCommand>
	{
		private readonly IRepository _repository;

		public UserAddCommandHandler( IRepository repository )
		{
			_repository = repository;
		}

		public CommandResult Handle( UserAddCommand cmd )
		{
			_repository.Save( new User( cmd.Name, cmd.EmailAddress, cmd.Password ) );
			return new CommandResult();
		}
	}
}
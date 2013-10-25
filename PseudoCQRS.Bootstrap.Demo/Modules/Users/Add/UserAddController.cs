using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PseudoCQRS.Controllers;

namespace PseudoCQRS.Bootstrap.Demo.Modules.Users.Add
{
	public class UserAddController : BaseReadExecuteController<UserAddViewModel, EmptyViewModelProviderArgument, UserAddCommand>
	{
		public UserAddController( ICommandExecutor commandExecutor, IViewModelFactory<UserAddViewModel, EmptyViewModelProviderArgument> viewModelFactory ) : base( commandExecutor, viewModelFactory ) { }
		public override ActionResult OnSuccessfulExecution( UserAddViewModel viewModel, CommandResult cmdResult )
		{
			return RedirectToAction( "Execute", "Home" );
		}

		public override string ViewPath
		{
			get { return "Users/Add"; }
		}
	}
}
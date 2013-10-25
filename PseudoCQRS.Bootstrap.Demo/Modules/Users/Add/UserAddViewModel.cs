using FluentValidation.Attributes;

namespace PseudoCQRS.Bootstrap.Demo.Modules.Users.Add
{
	[Validator( typeof( UserAddViewModelValidator ) )]
	public class UserAddViewModel
	{
		public string Name { get; set; }
		public string EmailAddress { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
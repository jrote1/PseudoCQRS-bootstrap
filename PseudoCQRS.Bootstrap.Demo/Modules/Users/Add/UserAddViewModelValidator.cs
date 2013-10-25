using FluentValidation;

namespace PseudoCQRS.Bootstrap.Demo.Modules.Users.Add
{
	public class UserAddViewModelValidator : AbstractValidator<UserAddViewModel>
	{
		public UserAddViewModelValidator()
		{
			RuleFor( x => x.Name ).NotEmpty();
			RuleFor( x => x.EmailAddress ).NotEmpty();
			RuleFor( x => x.Password ).NotEmpty();
			RuleFor( x => x.ConfirmPassword ).NotEmpty().Equal( x => x.Password );
		}
	}
}
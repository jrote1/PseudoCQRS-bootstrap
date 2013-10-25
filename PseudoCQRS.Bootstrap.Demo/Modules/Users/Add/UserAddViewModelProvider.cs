namespace PseudoCQRS.Bootstrap.Demo.Modules.Users.Add
{
	public class UserAddViewModelProvider : IViewModelProvider<UserAddViewModel, EmptyViewModelProviderArgument>
	{
		public UserAddViewModel GetViewModel( EmptyViewModelProviderArgument args )
		{
			return new UserAddViewModel();
		}
	}
}
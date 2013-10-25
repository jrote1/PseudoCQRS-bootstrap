using PseudoCQRS.Controllers;

namespace PseudoCQRS.Bootstrap
{
	public class ViewModelToCommandMappingEngine: IViewModelToCommandMappingEngine
	{
		public TTo Map<TFrom, TTo>( TFrom viewModel )
		{
			return AutoMapper.Mapper.Map<TFrom, TTo>( viewModel );
		}
	
	
	}
}

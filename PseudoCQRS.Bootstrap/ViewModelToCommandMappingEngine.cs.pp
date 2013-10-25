using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PseudoCQRS.Controllers;

namespace $rootnamespace$
{
	public class ViewModelToCommandMappingEngine: IViewModelToCommandMappingEngine
	{
		public TTo Map<TFrom, TTo>( TFrom viewModel )
		{
			return AutoMapper.Mapper.Map<TFrom, TTo>( viewModel );
		}
	
	
	}
}

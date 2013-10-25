using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace $rootnamespace$
{
	public class EventSubscriberAssembliesProvider : IEventSubscriberAssembliesProvider
	{
		public IEnumerable<Assembly> GetEventSubscriberAssemblies()
		{
			return new List<Assembly>
			{
				this.GetType().Assembly
			};
		}
	}
}

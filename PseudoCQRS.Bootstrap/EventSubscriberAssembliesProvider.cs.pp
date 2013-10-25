using System.Collections.Generic;
using System.Reflection;

namespace PseudoCQRS.Bootstrap
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

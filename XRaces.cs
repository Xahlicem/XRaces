using Terraria.ModLoader;

namespace XRaces
{
	class XRaces : Mod
	{
		public XRaces()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}
	}
}

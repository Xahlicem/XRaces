using Terraria.ModLoader;
using Terraria;

namespace XRaces {
    public class XRaces : Mod {
        public XRaces() {
            Properties = new ModProperties() {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }
    }
}
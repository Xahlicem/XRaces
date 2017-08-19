using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces {
    class XRaces : Mod {
        public XRaces() {
            Properties = new ModProperties() {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }

        public override void AddRecipeGroups() {
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Soul", new int[] {
                this.ItemType<Items.Misc.AntSoul>(),
                this.ItemType<Items.Misc.DemonSoul>(),
                this.ItemType<Items.Misc.GoblinSoul>(),
                this.ItemType<Items.Misc.LizardmanSoul>(),
                this.ItemType<Items.Misc.RobotSoul>(),
                this.ItemType<Items.Misc.ShadeSoul>(),
                this.ItemType<Items.Misc.SkeletonSoul>(),
                this.ItemType<Items.Misc.SlimeSoul>(),
                this.ItemType<Items.Misc.ZombieSoul>()
            });
            RecipeGroup.RegisterGroup("XRSouls", group);
        }
    }
}
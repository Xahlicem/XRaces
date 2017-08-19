using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

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
            RecipeGroup group = new RecipeGroup(() => Language.GetText("any") + " Soul", new int[] {
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
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace XRaces.Items.Misc {
    public class SoulVessel : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Soul Vessel");
            Tooltip.SetDefault("Contains untapped power");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults() {
            item.useTime = 120;
            item.useStyle = 4;
            item.maxStack = 1;
            item.rare = 0;
            item.consumable = false;
        }
    }
}
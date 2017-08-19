using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Items.Misc {
    public class SlimeSoul : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Slime Soul");
            Tooltip.SetDefault("Contains the powers of slippery slimy goo!");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults() {
            item.useTime = 120;
            item.useStyle = 4;
            item.maxStack = 1;
            item.rare = 0;
            item.consumable = true;
        }

        public override bool UseItem(Player player) {
            player.GetModPlayer<XRPlayer>().ChangeRace(XRPlayer.Race.Slime);
            return true;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.requiredTile[0] = TileID.Campfire;
            recipe.AddIngredient(mod.ItemType<Items.Misc.SoulVessel>(), 1);
            recipe.AddIngredient(ItemID.SlimeBanner);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
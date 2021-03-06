using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Items.Misc {
    public class ZombieSoul : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Zombie Soul");
            Tooltip.SetDefault("Contains the powers of decompisition!");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults() {
            //item.CloneDefaults(ItemID.RecallPotion);
            item.useTime = 120;
            item.useStyle = 4;
            item.maxStack = 1;
            item.rare = 0;
            item.consumable = true;
        }

        public override bool UseItem(Player player) {
            player.GetModPlayer<XRPlayer>().ChangeRace(Race.Zombie);
            return true;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.requiredTile[0] = TileID.Campfire;
            recipe.AddIngredient(mod.ItemType<Items.Misc.SoulVessel>(), 1);
            recipe.AddIngredient(ItemID.ZombieBanner);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips) {
            tooltips.Add(new XToolTipLine(mod, "Zombie", "No need to breathe or swim", false));
            tooltips.Add(new XToolTipLine(mod, "Zombie", "Walk and attack slower", true));
            tooltips.Add(new XToolTipLine(mod, "Zombie", "You are a heavy hitter", false));
            tooltips.Add(new XToolTipLine(mod, "Zombie", "Lower life", true));
            tooltips.Add(new XToolTipLine(mod, "Zombie", "Regenerate life quickly", false));
            tooltips.Add(new XToolTipLine(mod, "Zombie", "Reduced ranged and thrown damage", true));
        }
    }
}
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Items.Misc {
    public class LizardmanSoul : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Lizardman Soul");
            Tooltip.SetDefault("Contains the powers of The Black Marsh!");
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
            player.GetModPlayer<XRPlayer>().ChangeRace(Race.Lizardman);
            return true;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.requiredTile[0] = TileID.Campfire;
            recipe.AddIngredient(mod.ItemType<Items.Misc.SoulVessel>());
            recipe.AddIngredient(ItemID.JungleSpores, 10);
            recipe.AddIngredient(ItemID.GillsPotion);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips) {
            tooltips.Add(new XToolTipLine(mod, "Lizardman", "Cannot breathe air", true));
            tooltips.Add(new XToolTipLine(mod, "Lizardman", "Breathe and swim in water", false));
            tooltips.Add(new XToolTipLine(mod, "Lizardman", "Regenerate life faster", false));
            tooltips.Add(new XToolTipLine(mod, "Lizardman", "Move faster in water and in the day", false));
            tooltips.Add(new XToolTipLine(mod, "Lizardman", "Sense enemies", false));
        }
    }
}
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Items.Misc {
    public class RobotSoul : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Robot Soul");
            Tooltip.SetDefault("Contains the power.exe!");
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
            player.GetModPlayer<XRPlayer>().ChangeRace(Race.Robot);
            return true;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.requiredTile[0] = TileID.Campfire;
            recipe.AddIngredient(mod.ItemType<Items.Misc.SoulVessel>());
            recipe.AddIngredient(ItemID.Wire, 100);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips) {
            tooltips.Add(new XToolTipLine(mod, "Robot", "You use mana as energy", false));
            tooltips.Add(new XToolTipLine(mod, "Robot", "Moving/Attacking drains energy", true));
            tooltips.Add(new XToolTipLine(mod, "Robot", "Standing still recharges energy", false));
            tooltips.Add(new XToolTipLine(mod, "Robot", "Water short-circuits you", true));
            tooltips.Add(new XToolTipLine(mod, "Robot", "More energy = more damage/speed", false));
            tooltips.Add(new XToolTipLine(mod, "Robot", "Less energy = less damage/speed", true));
            tooltips.Add(new XToolTipLine(mod, "Robot", "Reduced summon damage", true));
        }
    }
}
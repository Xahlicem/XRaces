using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Items.Misc {
    public class DemonSoul : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Demon Soul");
            Tooltip.SetDefault("Contains the powers of Hell!");
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
            player.GetModPlayer<XRPlayer>().ChangeRace(Race.Demon);
            return true;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.requiredTile[0] = TileID.Campfire;
            recipe.AddIngredient(mod.ItemType<Items.Misc.SoulVessel>(), 1);
            recipe.AddIngredient(ItemID.DemonBanner);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips) {
            TooltipLine l;
            l = new TooltipLine(mod, "Demon", "You take damage from water");
            l.isModifier = true;
            l.isModifierBad = true;
            tooltips.Add(l);
            l = new TooltipLine(mod, "Demon", "You can go in lava");
            l.isModifier = true;
            l.isModifierBad = false;
            tooltips.Add(l);
            l = new TooltipLine(mod, "Demon", "Mana cost changes based on depth");
            l.isModifier = true;
            l.isModifierBad = true;
            tooltips.Add(l);
            l = new TooltipLine(mod, "Demon", "+25% magic damage");
            l.isModifier = true;
            l.isModifierBad = false;
            tooltips.Add(l);
            l = new TooltipLine(mod, "Demon", "Reduced ranged damage");
            l.isModifier = true;
            l.isModifierBad = true;
            tooltips.Add(l);
            l = new TooltipLine(mod, "Demon", "More damage as you kill");
            l.isModifier = true;
            l.isModifierBad = false;
            tooltips.Add(l);
        }
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Items.Armor {
    [AutoloadEquip(EquipType.Head, EquipType.Face)]
    public class AntHead : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ant Head");
            Tooltip.SetDefault("Dirty!");
        }

        public override void SetDefaults() {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 9;
            item.vanity = true;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair) {
            drawHair = true;
        }

        public override bool DrawHead()
		{
			return true;
		}
    }
}
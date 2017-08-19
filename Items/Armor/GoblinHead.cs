using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Items.Armor {
    [AutoloadEquip(EquipType.Head)]
    public class GoblinHead : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Goblin Head");
            Tooltip.SetDefault("Bloody!");
        }

        public override void SetDefaults() {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 9;
            item.vanity = true;
        }

        public override bool DrawHead()
		{
			return false;
		}
    }
}
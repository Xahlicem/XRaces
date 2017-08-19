using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace XRaces.Items.Misc {
    public class SoulVesselTest : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Soul Vessel Test");
            Tooltip.SetDefault("Changes race");
        }

        public override void SetDefaults() {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.Mushroom);
            item.width = refItem.width;
            item.height = refItem.height;
            item.useStyle = refItem.useStyle;
            item.UseSound = refItem.UseSound;
            item.useAnimation = 20;
            item.useTime = 20;
            item.maxStack = 99;
            item.value = Item.buyPrice(0, 0, 6, 0);
            item.rare = 0;
            item.consumable = true;
        }

        public override bool UseItem(Player player) {
            XRPlayer xRPlayer = player.GetModPlayer<XRPlayer>();
            xRPlayer.ChangeRace(xRPlayer.race.NextEnum(), true);
            return true;
        }
    }
}
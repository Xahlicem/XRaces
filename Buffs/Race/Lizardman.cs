using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Buffs.Race

{

    public class Lizardman : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Lizardman");
            Description.SetDefault("Power of cold blooded!");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex) {
            XRPlayer modPlayer = player.GetModPlayer<XRPlayer>();
            player.statDefense += 5;
            player.statLifeMax2 = (int)(player.statLifeMax2 * 1.2);
            player.lifeRegenTime = 2;
            player.lifeRegen += player.statLifeMax2 / 50;

            if (Main.dayTime || modPlayer.wet) {
                player.moveSpeed *= 1.25f;
                player.maxRunSpeed *= 1.25f;
                player.accRunSpeed *= 1.25f;
            }

            player.detectCreature = true;
            player.accFlipper = true;

            player.breath -= 3;
            if (Main.time % 6 == 0) player.breath += 1;

            if (!modPlayer.wet && player.FindBuffIndex(BuffID.Gills) == -1 && player.armor[0].type != ItemID.FishBowl) {
                player.breathCD = 0;
                if (Main.time % 5 == 0) player.breath -= 1;
            } else player.breath += 4;
            if (player.breath <= 0) player.AddBuff(BuffID.Suffocation, 10);
        }
    }
}

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Buffs.Race

{

    public class Ant : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Ant");
            Description.SetDefault("Power of dirt!");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex) {
            XRPlayer modPlayer = player.GetModPlayer<XRPlayer>();
                    player.pickSpeed *= 0.75f;
                    player.maxRunSpeed += 1;
                    player.spikedBoots = 2;
        }
    }
}
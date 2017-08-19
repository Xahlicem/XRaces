using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Buffs.Race

{

    public class Shade : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Shade");
            Description.SetDefault("Power of darkness!");
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = false;
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex) {
            XRPlayer modPlayer = player.GetModPlayer<XRPlayer>();
            player.ignoreWater = true;
            player.breath = 100000;
            player.noKnockback = true;
            player.thrownVelocity *= 0.1f;
            player.minionDamage *= 1.25f;
            player.statLifeMax2 = (int)(player.statLifeMax2 * 0.5);
            player.dash += 1;
        }
    }
}
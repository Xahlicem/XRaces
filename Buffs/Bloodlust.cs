using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Buffs {

    public class Bloodlust : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Bloodlust");
            Description.SetDefault("Increases damage based on time left");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.persistentBuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
            longerExpertDebuff = false;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex) {
            float mul = (player.buffTime[buffIndex] / 120f) * 0.01f + 1;
            player.magicDamage *= mul;
            player.meleeDamage *= mul;
            player.rangedDamage *= mul;
            player.thrownDamage *= mul;
            player.minionDamage *= mul;
        }

        public override bool ReApply(Player player, int time, int buffIndex) {
            player.buffTime[buffIndex] += time;
            return true;
        }
    }
}
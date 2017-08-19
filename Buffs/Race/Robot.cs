using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Buffs.Race

{

    public class Robot : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Robot");
            Description.SetDefault("Power of batteries!");
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
            float mul = (0.75f + (((float)(player.statMana + 1) / (float) player.statManaMax2) * 0.50f));
            player.maxRunSpeed *= mul;
            player.moveSpeed *= mul;
            player.accRunSpeed = (mul < 1) ? 0 : player.accRunSpeed * mul;
            player.rangedDamage *= mul;
            player.meleeDamage *= mul;
            player.thrownDamage *= mul;
            player.meleeSpeed *= (0.50f + (((float)(player.statMana + 1) / (float) player.statManaMax2) * 0.75f));
            player.minionDamage *= 0.50f;
            player.thrownVelocity *= 1.5f;

            if (Main.time % 30 == 0) {
                player.statMana -= modPlayer.wet ? 5 : 1;
                if (player.statMana <= 0)
                    player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " needed to charge."), modPlayer.wet ? 25 : 5, 0, false, false, false, 0);
            }
            player.manaRegenCount = -10;
            player.manaRegenDelay = 100;
            player.manaRegen = -10;
            player.manaRegenBonus = (int)(player.manaRegenBonus * -3);
        }
    }
}
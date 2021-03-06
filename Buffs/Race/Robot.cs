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
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex) {
            XRPlayer modPlayer = player.GetModPlayer<XRPlayer>();
            player.ignoreWater = true;
            player.breath = 100000;
            float damageMul = (0.75f + (modPlayer.manaMaxMul * 0.50f));
            float speedMul = (0.50f + (modPlayer.manaMaxMul * 0.75f));
            player.maxRunSpeed *= damageMul;
            player.moveSpeed *= damageMul;
            player.accRunSpeed = (damageMul < 1) ? 0 : player.accRunSpeed * damageMul;
            player.rangedDamage *= damageMul;
            player.meleeDamage *= damageMul;
            player.thrownDamage *= damageMul;
            player.meleeSpeed *= speedMul;
            player.minionDamage *= 0.50f;
            player.thrownVelocity *= 1.5f;

            if (Main.time % 30 == 0) {
                player.statMana -= modPlayer.wet ? 5 : 1;
                if (player.statMana <= 0)
                    player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " needed to charge."), modPlayer.wet ? 25 : 5, 0, false, false, false, 0);
                if (modPlayer.idle >= 60 && modPlayer.idle < 300) player.statMana += 1;
                else if (modPlayer.idle >= 300) {
                    player.statMana += 2;
                    if (player.statMana < player.statManaMax2) player.ManaEffect(1);
                }
            }
            player.manaRegenCount = -10;
            player.manaRegenDelay = 100;
            player.manaRegen = -10;
            player.manaRegenBonus = (int)(player.manaRegenBonus * -3);
        }
    }
}
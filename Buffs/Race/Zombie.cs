using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Buffs.Race

{

    public class Zombie : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Zombie");
            Description.SetDefault("Power of decompisition!");
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
                    player.moveSpeed *= 0.75f;
                    player.maxRunSpeed *= 0.75f;
                    player.accRunSpeed = 0;
                    player.rangedDamage *= 0.25f;
                    player.thrownDamage *= 0.75f;
                    player.thrownVelocity *= 1.5f;
                    player.meleeDamage *= 1.5f;
                    player.meleeSpeed *= 0.5f;
                    player.statLifeMax2 = (int)(player.statLifeMax2 * 0.5);
                    player.lifeRegenTime = 5;
                    player.lifeRegen += player.statLifeMax2 / 25;
        }
    }
}
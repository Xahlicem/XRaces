using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Buffs.Race

{

    public class Demon : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Demon");
            Description.SetDefault("Power of Hell!");
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = false;
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex) {
            XRPlayer modPlayer = player.GetModPlayer<XRPlayer>();
            player.rangedDamage *= 0.75f;
            player.magicDamage *= 1.25f;
            player.manaCost *= 1.50f - (player.position.Y / Main.bottomWorld);

            player.buffImmune[BuffID.OnFire] = true;
            player.lavaRose = true;
            player.fireWalk = true;
            player.lavaMax = 600;
            if (Main.myPlayer == player.whoAmI && Main.time % 30 == 0 && modPlayer.wet) {
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " couldn't stand the water."), 5, 0, false, false, false, 0);
            }
            player.wings = 1;
            if (modPlayer.falling) {
                player.velocity.Y = 0.025f;
                player.fallStart = (int) player.position.Y >> 4;
            }
        }
    }
}
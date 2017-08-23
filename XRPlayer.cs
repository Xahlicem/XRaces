using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace XRaces {

    public class XRPlayer : ModPlayer {
        public Race race = Race.Human;
        public int hair = -1;
        public Color cHair = default(Color);
        public Color cEye = default(Color);
        public Color cSkin = default(Color);
        public bool wet = false;
        public bool falling = false;
        public int idle = 0;

        public override TagCompound Save() {
            return new TagCompound { { "xrRace", (byte) race }, { "xrHair", hair }, { "xrCHair", cHair }, { "xrCEye", cEye }, { "xrCSkin", cSkin }
            };
        }

        public override void Load(TagCompound tag) {
            race = (Race) tag.GetByte("xrRace");
            hair = tag.GetInt("xrHair");
            cHair = tag.Get<Color>("xrCHair");
            cEye = tag.Get<Color>("xrCEye");
            cSkin = tag.Get<Color>("xrCSkin");
        }

        public override void ResetEffects() {
            wet = false;
        }

        public override void UpdateDead() {
            wet = false;
        }

        public override void PreUpdateBuffs() {
            wet = (player.FindBuffIndex(BuffID.Wet)) != -1 || player.wet;
            if (player.lavaWet || player.honeyWet) {
                wet = false;
                player.ClearBuff(BuffID.Wet);
            }
            if (race != Race.Human) player.AddBuff(mod.BuffType(race.ToString()), 10);
        }

        public override void MeleeEffects(Item item, Rectangle hitbox) {
            /*
            byte pre = item.prefix;
            float scale = item.scale;
            item.CloneDefaults(item.type);
            item.prefix = pre;
            if (race == Race.Zombie) item.scale = item.scale * 1.5f;
            */
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
            if (race == Race.Slime && damageSource.SourceNPCIndex >= 0)
                if (Main.npc[damageSource.SourceNPCIndex].FullName.Contains("Slime")) {
                    if (damage <= player.statDefense) return false;
                }
            return true;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit) {
            if (race == Race.Demon && target.life <= 0) player.AddBuff(mod.BuffType<Buffs.Bloodlust>(), 300);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit) {
            if (race == Race.Demon && target.life <= 0) player.AddBuff(mod.BuffType<Buffs.Bloodlust>(), 300);
        }

        public override void FrameEffects() {
            if (falling) player.wingFrame = 2;
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers) {
            Main.playerTextures[0, 0] = XRaces.RaceTextures[(int) this.race, 0];
            Main.playerTextures[0, 1] = XRaces.RaceTextures[(int) this.race, 1];
            Main.playerTextures[0, 2] = XRaces.RaceTextures[(int) this.race, 2];
        }

        public override void SetupStartInventory(IList<Item> items) {
            Item item = new Item();
            item.SetDefaults(mod.ItemType<Items.Misc.SoulVessel>());
            item.stack = 1;
            items.Add(item);
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            falling = (!player.justJumped && triggersSet.Jump && player.velocity.Y >= 0.01f);
            if (!triggersSet.Down && !triggersSet.Up && !triggersSet.Left && !triggersSet.Right && !triggersSet.Grapple && !triggersSet.Jump && !triggersSet.Throw && !triggersSet.MouseLeft && !triggersSet.MouseRight) idle++;
            else idle = 0;
        }

        public override void clientClone(ModPlayer clone) {
            //Main.NewText(((XRPlayer) clientClone).race.ToString() + " CloneBefore");
			base.clientClone( clone );
			var myclone = (XRPlayer)clone;
            myclone.race = this.race;
            //Main.NewText(((XRPlayer) clientClone).race.ToString() + " CloneAfter");
        }

        public override void SendClientChanges(ModPlayer clientPlayer) {
            //Main.NewText(((XRPlayer) clientPlayer).race.ToString() + " Player");
        }

        public override void PostUpdate() {
            if (Main.netMode == NetmodeID.MultiplayerClient && player.Equals(Main.LocalPlayer)) {
                ModPacket packet = this.mod.GetPacket();

                packet.Write((byte) XRModMessageType.Race);
                packet.Write(this.player.whoAmI);
                packet.Write((byte) this.race);
                packet.Write(hair);
                packet.WriteRGB(cHair);
                packet.WriteRGB(cEye);
                packet.WriteRGB(cSkin);
                packet.Write(wet);
                packet.Write(falling);
                packet.Write(idle);

                packet.Send();
            }
        }

        public void ChangeRace(Race r, bool force = false) {
            if (race == r) {
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " didn't quite understand why!"), 10000, 0);
                return;
            }
            if (race != Race.Human && !force) {
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " angered the gods by trying to interbreed!"), 10000, 0);
                return;
            }

            if (cEye == default(Color)) cEye = player.eyeColor;
            if (cSkin == default(Color)) cSkin = player.skinColor;
            if (cHair == default(Color)) cHair = player.hairColor;
            if (hair == -1) hair = player.hair;

            for (int i = 0; i < 150; i++) {
                Dust.NewDust(player.position, 8, 8, DustID.Blood, Main.rand.NextFloat() * 10 - 5, Main.rand.NextFloat() * 10 - 5);
            }
            if (!force) player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " sheds their human flesh!"), 10000, 0);
            race = r;

            switch (race) {
                case Race.Human:
                    player.hair = hair;
                    player.hairColor = cHair;
                    player.eyeColor = cEye;
                    player.skinColor = cSkin;
                    break;
                case Race.Demon:
                    player.hair = 15;
                    player.hairColor = Color.DarkGray;
                    player.skinColor = new Color(140, 65, 65);
                    player.eyeColor = Color.Red;
                    break;
                case Race.Ant:
                    player.hair = 15;
                    player.skinColor = new Color(75, 35, 15);
                    player.eyeColor = Color.White;
                    break;
                case Race.Slime:
                    player.hair = hair;
                    player.hairColor = new Color(0, 50, 250, 50);
                    player.skinColor = new Color(100, 150, 255);
                    player.eyeColor = new Color(0, 50, 250, 50);
                    break;
                case Race.Zombie:
                    player.hair = hair;
                    player.hairColor = Color.DarkGray;
                    player.skinColor = new Color(215, 225, 135);
                    player.eyeColor = Color.Red;
                    break;
                case Race.Goblin:
                    player.hair = hair;
                    player.hairColor = cHair;
                    player.skinColor = new Color(95, 150, 160);
                    player.eyeColor = Color.Red;
                    break;
                case Race.Skeleton:
                    player.hair = 15;
                    player.skinColor = new Color(135, 135, 100);
                    player.eyeColor = Color.Red;
                    break;
                case Race.Lizardman:
                    player.hair = 15;
                    player.hairColor = Color.DarkGray;
                    player.skinColor = new Color(75, 135, 50);
                    player.eyeColor = Color.Red;
                    break;
                case Race.Shade:
                    player.hair = 15;
                    player.skinColor = Color.Black;
                    player.eyeColor = Color.White;
                    break;
                case Race.Robot:
                    player.hair = 15;
                    player.hairColor = Color.DarkGray;
                    player.skinColor = new Color(120, 120, 120);
                    player.eyeColor = Color.LightGreen;
                    break;
            }
        }
    }
}
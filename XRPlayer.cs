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

        public enum Race : byte {
            Human,
            Demon,
            Ant,
            Slime,
            Zombie,
            Goblin,
            Skeleton,
            Lizardman,
            Shade,
            Robot
        }
        public Race race = Race.Human;
        public int hair = -1;
        public int headStyle = 0;
        public Color cHair = default(Color);
        public Color cEye = default(Color);
        public Color cSkin = default(Color);
        public bool wet = false;
        public bool falling = false;

        public override TagCompound Save() {
            return new TagCompound { { "xrRace", (byte) race }, { "xrHeadStyle", headStyle }, { "xrHair", hair }, { "xrCHair", cHair }, { "xrCEye", cEye }, { "xrCSkin", cSkin }
            };
        }

        public override void Load(TagCompound tag) {
            race = (Race) tag.GetByte("xrRace");
            headStyle = tag.GetInt("xrHead");
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

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
            if (race == Race.Slime && damageSource.SourceNPCIndex >= 0)
                if (Main.npc[damageSource.SourceNPCIndex].FullName.Contains("Slime")) {
                    if (damage <= player.statDefense) return false;
                }
            return true;
        }

        public override void FrameEffects() {
            /*if (head != 0)
                if (head == -1) {
                    if (player.head <= 0) player.head = mod.GetItem(race.ToString() + "Head").item.headSlot;
                    else if (player.head == 10 || player.head == 23 || player.head == 12 || player.head == 26 || player.head == 28 || player.head == 51 || player.head == 62 || player.head == 97 || player.head == 113 || player.head == 106 || player.head == 129 || player.head == 133 || player.head == 136 || player.head == 132 || player.head == 178 || player.head == 181 || player.head == 184 || player.head == 190 || player.head == 191 || player.head == 198) player.face = mod.GetItem(race.ToString() + "Head").item.faceSlot;
                } else if (head != -2) player.head = (head == 93) ? head : mod.GetItem(race.ToString() + "Head").item.headSlot;
*/
            if (falling) player.wingFrame = 2;
        }

        public static readonly PlayerLayer RaceFace = new PlayerLayer("XRaces", "ModFace", PlayerLayer.Face, delegate(PlayerDrawInfo drawInfo) {
            if (drawInfo.shadow != 0f) {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("XRaces");
            Texture2D texture = mod.GetTexture("Race/" + drawPlayer.GetModPlayer<XRPlayer>().race.ToString() + "Face");
            int frameSize = texture.Height / 20;
            int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
            int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
            Rectangle rect = new Rectangle((drawPlayer.direction == 1) ? drawPlayer.bodyFrame.Left : drawPlayer.bodyFrame.Width, drawPlayer.bodyFrame.Top, drawPlayer.bodyFrame.Width, drawPlayer.bodyFrame.Height);
            DrawData data = new DrawData(texture, new Vector2(drawX, drawY), rect, Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f)), 0, new Vector2(texture.Width / 4f, frameSize / 2f), 1f, SpriteEffects.None, 10);
            data.ignorePlayerRotation = false;
            data.rotation = drawPlayer.fullRotation;
            Main.playerDrawData.Add(data);
        });

        public static readonly PlayerLayer RaceFaceAcc = new PlayerLayer("XRaces", "ModFaceAcc", PlayerLayer.FaceAcc, delegate(PlayerDrawInfo drawInfo) {
            if (drawInfo.shadow != 0f) {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("XRaces");
            Texture2D texture = mod.GetTexture("Race/" + drawPlayer.GetModPlayer<XRPlayer>().race.ToString() + "Face");
            int frameSize = texture.Height / 20;
            int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
            int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y - 3);
            Rectangle rect = new Rectangle((drawPlayer.direction == 1) ? drawPlayer.bodyFrame.Left : drawPlayer.bodyFrame.Width, drawPlayer.bodyFrame.Top, drawPlayer.bodyFrame.Width, drawPlayer.bodyFrame.Height);
            DrawData data = new DrawData(texture, new Vector2(drawX, drawY), rect, Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f)), 0, new Vector2(texture.Width / 4f, frameSize / 2f), 1f, SpriteEffects.None, 10);
            data.ignorePlayerRotation = false;
            data.rotation = drawPlayer.fullRotation;
            Main.playerDrawData.Add(data);
        });

        public override void ModifyDrawLayers(List<PlayerLayer> layers) {
            if (headStyle == 0) return;
            int i;
            for (i = 0; i < layers.Count; i++)
                if (layers[i].Name.Contains("Face")) {
                    if (headStyle == 2)layers[i].visible = false;
                    break;
                }
            if (!player.dead) layers.Insert(i + 1, (headStyle == 1)?RaceFace:RaceFaceAcc);
        }

        public override void SetupStartInventory(IList<Item> items) {
            Item item = new Item();
            item.SetDefaults(mod.ItemType<Items.Misc.SoulVessel>());
            item.stack = 1;
            items.Add(item);
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            falling = (!player.justJumped && triggersSet.Jump && player.velocity.Y >= 0.01f);
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
                    headStyle = 0;
                    break;
                case Race.Demon:
                    player.hairColor = Color.DarkGray;
                    player.skinColor = new Color(140, 65, 65);
                    player.eyeColor = Color.Red;
                    headStyle = 1;
                    break;
                case Race.Ant:
                    player.hair = 15;
                    player.skinColor = new Color(75, 35, 15);
                    player.eyeColor = Color.Black;
                    headStyle = 2;
                    break;
                case Race.Slime:
                    player.hair = hair;
                    player.hairColor = new Color(0, 50, 250, 50);
                    player.skinColor = new Color(100, 150, 255);
                    player.eyeColor = new Color(0, 50, 250, 50);
                    headStyle = 0;
                    break;
                case Race.Zombie:
                    player.hair = hair;
                    player.hairColor = Color.DarkGray;
                    player.skinColor = new Color(205, 255, 150);
                    player.eyeColor = Color.Red;
                    headStyle = 0;
                    break;
                case Race.Goblin:
                    player.hair = hair;
                    player.hairColor = cHair;
                    player.skinColor = new Color(95, 150, 160);
                    player.eyeColor = Color.Red;
                    headStyle = 2;
                    break;
                case Race.Skeleton:
                    player.hair = 15;
                    player.skinColor = new Color(155, 155, 115);
                    player.eyeColor = Color.White;
                    headStyle = 2;
                    break;
                case Race.Lizardman:
                    player.hair = 15;
                    player.hairColor = Color.DarkGray;
                    player.skinColor = new Color(75, 135, 50);
                    player.eyeColor = Color.Red;
                    headStyle = 2;
                    break;
                case Race.Shade:
                    player.hair = 15;
                    player.skinColor = Color.Black;
                    player.eyeColor = Color.Black;
                    headStyle = 2;
                    break;
                case Race.Robot:
                    player.hair = 15;
                    player.hairColor = Color.DarkGray;
                    player.skinColor = new Color(120, 120, 120);
                    player.eyeColor = Color.Green;
                    headStyle = 2;
                    break;
            }
        }
    }
}
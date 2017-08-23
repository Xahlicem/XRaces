using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace XRaces {
    class XRaces : Mod {
        public static Texture2D head, eyeWhites, eyes;
        public static Texture2D[, ] RaceTextures = new Texture2D[Enum.GetNames(typeof (Race)).Length, 3];
        public XRaces() {
            Properties = new ModProperties() {
            Autoload = true,
            AutoloadGores = true,
            AutoloadSounds = true
            };
        }

        public override void Load() {
            head = TextureManager.Load("Images/Player_0_0");
            eyeWhites = TextureManager.Load("Images/Player_0_1");
            eyes = TextureManager.Load("Images/Player_0_2");

            for (int i = 0; i < RaceTextures.Length / 3; i++) {
                String name = "Race/" + ((Race) i).ToString();
                if (TextureExists(name + "Head")) RaceTextures[i, 0] = (GetTexture(name + "Head"));
                else RaceTextures[i, 0] = head;
                if (TextureExists(name + "EyeWhites")) RaceTextures[i, 1] = (GetTexture(name + "EyeWhites"));
                else RaceTextures[i, 1] = eyeWhites;
                if (TextureExists(name + "Eyes")) RaceTextures[i, 2] = (GetTexture(name + "Eyes"));
                else RaceTextures[i, 2] = eyes;

            }
        }

        public override void Unload() {
            Main.playerTextures[0, 0] = head;
            Main.playerTextures[0, 1] = eyeWhites;
            Main.playerTextures[0, 2] = eyes;
        }

        public override void AddRecipeGroups() {
            RecipeGroup group = new RecipeGroup(() => Language.GetText("any") + " Soul", new int[] {
                this.ItemType<Items.Misc.AntSoul>(),
                    this.ItemType<Items.Misc.DemonSoul>(),
                    this.ItemType<Items.Misc.GoblinSoul>(),
                    this.ItemType<Items.Misc.LizardmanSoul>(),
                    this.ItemType<Items.Misc.RobotSoul>(),
                    this.ItemType<Items.Misc.ShadeSoul>(),
                    this.ItemType<Items.Misc.SkeletonSoul>(),
                    this.ItemType<Items.Misc.SlimeSoul>(),
                    this.ItemType<Items.Misc.ZombieSoul>()
            });
            RecipeGroup.RegisterGroup("XRSouls", group);
        }
        public override void HandlePacket(System.IO.BinaryReader reader, int whoAmI) {
            XRModMessageType msgType = (XRModMessageType) reader.ReadByte();
            Player player = Main.player[reader.ReadInt32()];
            XRPlayer modPlayer = player.GetModPlayer<XRPlayer>();

            switch (msgType) {
                case XRModMessageType.FromClient:
                    if (Main.netMode == NetmodeID.Server) {
                        Race race = (Race) reader.ReadByte();
                        modPlayer.race = race;
                        modPlayer.player.hair = reader.ReadInt32();
                        modPlayer.player.hairColor = reader.ReadRGB();
                        modPlayer.player.eyeColor = reader.ReadRGB();
                        modPlayer.player.skinColor = reader.ReadRGB();
                        modPlayer.wet = reader.ReadBoolean();
                        modPlayer.falling = reader.ReadBoolean();
                        modPlayer.idle = reader.ReadInt32();
                        modPlayer.manaMaxMul = reader.ReadSingle();

                        modPlayer.GetPacket((byte) XRModMessageType.FromServer).Send();
                        //NetMessage.BroadcastChatMessage(Terraria.Localization.NetworkText.FromLiteral(player.name + " " + race.ToString()), Microsoft.Xna.Framework.Color.White);
                    }
                    break;

                case XRModMessageType.FromServer:
                    if (!player.Equals(Main.LocalPlayer)) {
                        Race race = (Race) reader.ReadByte();
                        modPlayer.race = race;
                        modPlayer.player.hair = reader.ReadInt32();
                        modPlayer.player.hairColor = reader.ReadRGB();
                        modPlayer.player.eyeColor = reader.ReadRGB();
                        modPlayer.player.skinColor = reader.ReadRGB();
                        modPlayer.wet = reader.ReadBoolean();
                        modPlayer.falling = reader.ReadBoolean();
                        modPlayer.idle = reader.ReadInt32();
                        modPlayer.manaMaxMul = reader.ReadSingle();
                    }
                    break;

                default:
                    ErrorLogger.Log("XRaces: Unknown Message type: " + msgType);
                    break;
            }
        }
    }

    enum XRModMessageType : byte {
        FromServer,
        FromClient
    }

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
}
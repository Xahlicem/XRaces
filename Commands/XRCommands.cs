using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace XRaces.Commands {
    public class RaceCommands : ModCommand {
        public override CommandType Type {
            get { return CommandType.Chat; }
        }

        public override string Command {
            get { return "XRAdmin"; }
        }

        public override string Description {
            get { return "Use only if you know what you're doing!"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args) {
            if (args.Length >= 2) {
                if (!args[0].ToLower().Equals("xahlicem")) {
                    caller.Player.KillMe(PlayerDeathReason.ByOther(0), 100000, 0);
                    return;
                } else {
                    if (args[1].ToLower().Equals("race")) {
                        Race race;
                        if (Enum.TryParse(char.ToUpper(args[2][0]) + args[2].Substring(1), out race)) caller.Player.GetModPlayer<XRPlayer>().ChangeRace(race, true);
                    }
                }
            }
        }
    }

    public class SkinCommand : ModCommand {
        public override CommandType Type {
            get { return CommandType.Chat; }
        }

        public override string Command {
            get { return "Skin"; }
        }

        public override string Description {
            get { return "Usage: /Skin rrr ggg bbb"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args) {
            if (args.Length != 3) return;
            int r = int.Parse(args[0]);
            int g = int.Parse(args[1]);
            int b = int.Parse(args[2]);

            caller.Player.skinColor = new Color(r, g, b);
        }
    }

    public class EyeCommand : ModCommand {
        public override CommandType Type {
            get { return CommandType.Chat; }
        }

        public override string Command {
            get { return "Eye"; }
        }

        public override string Description {
            get { return "Usage: /Eye rrr ggg bbb"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args) {
            if (args.Length != 3) return;
            int r = int.Parse(args[0]);
            int g = int.Parse(args[1]);
            int b = int.Parse(args[2]);

            caller.Player.eyeColor = new Color(r, g, b);
        }
    }
}
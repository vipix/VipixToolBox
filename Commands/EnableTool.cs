using VipixToolBox;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace VipixToolBox.Commands
{
    public class EnableTool : ModCommand
    {
        public override CommandType Type => CommandType.World;
        public override string Command => "tool";
        public override string Usage => "\n/tool toolName <enable|disable>\n" +
                "/tool toolName";
        public override string Description => "enables or disables VipixToolBox tools";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Mod myMod = ModLoader.GetMod("VipixToolBox");
            Player player = caller.Player;
            bool allTools = false;
            var keys = new List<string>(VipixToolBoxWorld.toolEnabled.Keys);
            string displaykeys = "";
            foreach (string i in keys) displaykeys += i + ", ";

            string tool = args[0];
            if (keys.Contains(tool))
            {
                //you can put anything after a valid command
                if (args.Length >= 2)
                {
                    string mode = args[1];
                    if (mode == "enable")
                    {
                        VipixToolBoxWorld.toolEnabled[tool] = true;
                        caller.Reply(tool.ToString() + " has been enabled", new Color(0, 150, 0));
                    }
                    else if (mode == "disable")
                    {
                        VipixToolBoxWorld.toolEnabled[tool] = false;
                        caller.Reply(tool.ToString() + " has been disabled", new Color(200, 0, 0));
                    }
                    else
                    {
                        throw new UsageException(mode + " is not valid, please specify either enable|disable");
                    }
                }
                else
                {
                    VipixToolBoxWorld.toolEnabled[tool] = !VipixToolBoxWorld.toolEnabled[tool];
                    if (VipixToolBoxWorld.toolEnabled[tool]) Main.NewText(tool.ToString() + " has been enabled", 0, 150, 0);
                    else caller.Reply(tool.ToString() + " has been disabled", new Color(200, 0, 0));
                }
                if (tool == "all")
                {
                    bool temp = VipixToolBoxWorld.toolEnabled["all"];//can't make a foreach and modify the list within the foreach
                    for (int i = 0; i < keys.Count; i++)
                    {
                        VipixToolBoxWorld.toolEnabled[keys[i]] = temp;
                    }
                }
            }
            else
            {
                throw new UsageException(tool + " does not exist\navailable tools are " + displaykeys);
            }
        }
    }
}

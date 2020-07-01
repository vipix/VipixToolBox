using VipixToolBox;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace VipixToolBox.Commands
{
	public class CenterUI : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "centerUI";
		public override string Description => "Control interfaces position";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			Player player = caller.Player;
			VipixToolBoxPlayer myPlayer = player.GetModPlayer<VipixToolBoxPlayer>();
			/*
			bool choice;
			if (!bool.TryParse(args[0], out choice))
				throw new UsageException(args[0] + " is not a valid value\ncenterUI accepts <true|false>");
			else	myPlayer.centerUI = choice;*/
			if (args.Length <= 0)
			{
				caller.Reply("centerUI accepts <fixed|mouse|free>");
				return;
			}
			string mode = args[0];
			if (mode == "fixed")
			{
				myPlayer.centerUI = 0;
				caller.Reply("UI is now in " + mode + " mode", new Color(0, 150, 0));
			}
			else if (mode == "mouse")
			{
				myPlayer.centerUI = 1;
				caller.Reply("UI is now in " + mode + " mode", new Color(0, 150, 0));

			}
			else if (mode == "free")
			{
				myPlayer.centerUI = 2;
				caller.Reply("UI is now in " + mode + " mode", new Color(0, 150, 0));
			}
			else caller.Reply(mode + " is not a valid value\ncenterUI accepts <fixed|mouse|free>");
		}
	}
}

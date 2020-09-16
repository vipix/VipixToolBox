using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace VipixToolBox
{
	[Label("Server Config")]
	public class ServerConfig : ModConfig
	{
		/*      { "all", false },
                { "AutoHammer", true },
                { "BlockWand", true },
                { "ColorPalette", true },
                { "LevitationWand", true },
                { "RattlesnakeWand", true },
                { "StaffofRegrowthEdit", true },
                { "WallHammer", true }
		 */
		public override ConfigScope Mode => ConfigScope.ServerSide;

		public static ServerConfig Instance => ModContent.GetInstance<ServerConfig>();

		[Header("Toggle tools functionality")]

		[Label("Smartammer")]
		[Tooltip("Toggle Smartammer functionality")]
		[DefaultValue(true)]
		public bool AutoHammer;

		[Label("Greater Wand")]
		[Tooltip("Toggle Greater Wand functionality")]
		[DefaultValue(true)]
		public bool BlockWand;

		[Label("Color Palette")]
		[Tooltip("Toggle Color Palette functionality")]
		[DefaultValue(true)]
		public bool ColorPalette;

		[Label("Unstable Staff")]
		[Tooltip("Toggle Unstable Staff functionality")]
		[DefaultValue(true)]
		public bool LevitationWand;

		[Label("Rattlesnake Wand")]
		[Tooltip("Toggle Rattlesnake Wand functionality")]
		[DefaultValue(true)]
		public bool RattlesnakeWand;

		[Label("Improved Staff of Regrowth")]
		[Tooltip("Toggle improved Staff of Regrowth functionality")]
		[DefaultValue(true)]
		public bool StaffofRegrowthEdit;

		[Label("Wall Hammer")]
		[Tooltip("Toggle Wall Hammer functionality")]
		[DefaultValue(true)]
		public bool WallHammer;

		public static bool IsPlayerLocalServerOwner(int whoAmI)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				return Netplay.Connection.Socket.GetRemoteAddress().IsLocalHost();
			}

			for (int i = 0; i < Main.maxPlayers; i++)
			{
				RemoteClient client = Netplay.Clients[i];
				if (client.State == 10 && i == whoAmI && client.Socket.GetRemoteAddress().IsLocalHost())
				{
					return true;
				}
			}
			return false;
		}

		public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
		{
			if (Main.netMode == NetmodeID.SinglePlayer) return true;
			else if (!IsPlayerLocalServerOwner(whoAmI))
			{
				message = "Only the server owner is allowed to make changes to this config";
				return false;
			}
			return base.AcceptClientChanges(pendingConfig, whoAmI, ref message);
		}
	}
}

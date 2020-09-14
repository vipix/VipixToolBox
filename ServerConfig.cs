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

		[Header("Toggle Tools Functionality")]

		[Label("Auto Hammer")]
		[Tooltip("Toggle Auto Hammer Functionality")]
		[DefaultValue(true)]
		public bool AutoHammer;

		[Label("Block Wand")]
		[Tooltip("Toggle Block Wand Functionality")]
		[DefaultValue(true)]
		public bool BlockWand;

		[Label("Color Palette")]
		[Tooltip("Toggle Color Palette Functionality")]
		[DefaultValue(true)]
		public bool ColorPalette;

		[Label("Levitation Wand")]
		[Tooltip("Toggle Levitation Wand Functionality")]
		[DefaultValue(true)]
		public bool LevitationWand;

		[Label("Rattlesnake Wand")]
		[Tooltip("Toggle Rattlesnake Wand Functionality")]
		[DefaultValue(true)]
		public bool RattlesnakeWand;

		[Label("Staff of Regrowth Edit")]
		[Tooltip("Toggle Staff of Regrowth Edit Functionality")]
		[DefaultValue(true)]
		public bool StaffofRegrowthEdit;

		[Label("Wall Hammer")]
		[Tooltip("Toggle Wall Hammer Functionality")]
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
				message = "You are not the server owner so you can not change this config";
				return false;
			}
			return base.AcceptClientChanges(pendingConfig, whoAmI, ref message);
		}
	}
}

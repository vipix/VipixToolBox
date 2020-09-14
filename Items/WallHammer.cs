using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace VipixToolBox.Items
{
	public class WallHammer : ModItem
	{
		public int baseRange = 10;
		public int toolRange;
		public bool operationAllowed;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wall Hammer");
			Tooltip.SetDefault("Better calm your nerves on walls rather than enemies");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.WoodenSword);//not inspired for weapon stats
			item.damage = 8;
			item.width = 40;
			item.height = 40;
			item.useTime = 7;
			item.useAnimation = 7;

			item.value = Item.buyPrice(0, 15, 0, 0);
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}
		public override void HoldItem(Player player)
		{
			if (Main.netMode != NetmodeID.Server)
			{
				VipixToolBoxPlayer myPlayer = player.GetModPlayer<VipixToolBoxPlayer>();
				toolRange = Math.Max(baseRange, myPlayer.fargoRange);//blocks
				if (Vector2.Distance(player.Center, myPlayer.pointerCoord) < toolRange * 16 &&
					ServerConfig.Instance.WallHammer)
				{
					operationAllowed = true;
					player.showItemIcon = true;
				}
				else
				{
					operationAllowed = false;
					player.showItemIcon = false;
				}
			}
		}
		public override bool UseItem(Player player)
		{
			if (operationAllowed)
			{
				VipixToolBoxPlayer myPlayer = player.GetModPlayer<VipixToolBoxPlayer>();
				WorldGen.KillWall(myPlayer.pointedTileX, myPlayer.pointedTileY, false);
				NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, myPlayer.pointedTileX, myPlayer.pointedTileY, 1f, 0, 0, 0);
				if (Main.netMode == NetmodeID.MultiplayerClient) NetMessage.SendTileSquare(-1, myPlayer.pointedTileX, myPlayer.pointedTileY, 1);
			}
			//meh no control
			return true;
		}
	}
}

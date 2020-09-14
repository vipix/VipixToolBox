using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using VipixToolBox.UI;
using System;

namespace VipixToolBox.Items
{
	public class AutoHammer : ModItem
	{
		public int baseRange = 12;
		public int toolRange;
		public bool operationAllowed;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Smartammer");
			Tooltip.SetDefault("This hammer may be smarter than you");
		}
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.BoneSword );//why not
			item.damage = 10;
			item.useTime = 20;
			item.useAnimation = 20;
			item.width = 40;
			item.height = 40;

			item.value = Item.buyPrice(0, 15, 0, 0);//divided by 5, see Shops.cs for prices
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}
		public override void HoldItem(Player player)
		{
			VipixToolBoxPlayer myPlayer = player.GetModPlayer<VipixToolBoxPlayer>();
			if (Main.netMode != NetmodeID.Server && myPlayer.CursorReady)
			{
				//for showing the icon when an action is allowed
				toolRange = Math.Max(baseRange, myPlayer.fargoRange);//blocks
				if (myPlayer.pointedTile.active() &&
				myPlayer.pointedTileAbove.type != TileID.Trees &&
				Vector2.Distance(player.Center, myPlayer.pointerCoord) < toolRange * 16 &&
				Main.tileSolid[myPlayer.pointedTile.type] &&
				ServerConfig.Instance.AutoHammer)
				{
					//no edit under tree, probably other exceptions to add
					//empty tile is considered solid by Main.tileSolid, need to combine with tile.active()
					player.showItemIcon = true;//could the condition be checked only once in HoldItem OR CanUseItem ? problem wih UI
					operationAllowed = true;
				}
				else
				{
					player.showItemIcon = false;
					operationAllowed = false;
				}
			}
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.hardMode) add += 3;
		}

		public override bool CanUseItem(Player player)
		{
			VipixToolBoxPlayer myPlayer = player.GetModPlayer<VipixToolBoxPlayer>();
			//the damage will be updated everytime the player tries to use the hammer...
			//CanUseItem will be called only once, so the UI won't 'slide'
			if (operationAllowed)
			{
				//basically a transfer from continuously updated variables to periodically updated variables of modplayer
				myPlayer.tileX = myPlayer.pointedTileX;
				myPlayer.tileY = myPlayer.pointedTileY;
				myPlayer.tbMouseX = Main.mouseX;
				myPlayer.tbMouseY = Main.mouseY;
				HammerUI.visible = true;
			}
			return true;
		}
		public override void AddRecipes()
		{/*
			ModRecipe recipe = new ModRecipe(mod);
			//recipe.AddIngredient(ItemID.AntlionMandible, 2);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();*/
		}
	}
}

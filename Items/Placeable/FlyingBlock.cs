using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VipixToolBox.Tiles;

namespace VipixToolBox.Items.Placeable
{
	public class FlyingBlock : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("a Vipix Toolbox block");
			//should not be available
		}

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = ModContent.TileType<FlyingBlockTile>();
		}

		public override void AddRecipes()
		{/*
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this, 10);
			recipe.AddRecipe();*/
		}
	}
}

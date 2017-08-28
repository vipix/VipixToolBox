using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VipixToolBox.Items.Placeable
{
	public class dungeonpot : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Old Skull");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 26;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.rare = 0;
			item.value = 2;
			item.createTile = mod.TileType("dungeonpottile");
			item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe1 = new ModRecipe(mod);
			recipe1.AddIngredient(ItemID.ClayBlock, 4);
			recipe1.AddIngredient(ItemID.BlueBrick, 2);
			recipe1.AddTile(TileID.Furnaces);
			recipe1.SetResult(this);
			recipe1.AddRecipe();

			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(ItemID.ClayBlock, 4);
			recipe2.AddIngredient(ItemID.GreenBrick, 2);
			recipe2.AddTile(TileID.Furnaces);
			recipe2.SetResult(this);
			recipe2.AddRecipe();

			ModRecipe recipe3 = new ModRecipe(mod);
			recipe3.AddIngredient(ItemID.ClayBlock, 4);
			recipe3.AddIngredient(ItemID.PinkBrick, 2);
			recipe3.AddTile(TileID.Furnaces);
			recipe3.SetResult(this);
			recipe3.AddRecipe();
		}
	}
}

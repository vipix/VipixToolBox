using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VipixToolBox.Tiles
{
	public class FlyingBlockTE : ModTileEntity
	{
		public int timer = 0;
		public bool isDead = false;
		public int tileX = 1;
		public int tileY = 1;

		public override void Update()
		{
			Vector2 dustSpawn;
			dustSpawn = new Vector2(tileX,tileY);

			if (timer > 0) timer--;
			if ( timer%20 == 0 && timer > 0)
			{
				int dust = Dust.NewDust(dustSpawn*16, 10, 10, 15);//last number is the type
			}
			if ( timer < 20 && timer > 0)
			{
				int dust = Dust.NewDust(dustSpawn*16, 10, 10, 15);//last number is the type
			}
			if (timer == 0)
			{
				Tile tile = Main.tile[tileX,tileY-1];
				if (tile.type != TileID.Containers && /*tile.type != TileID.Teleporter &&*/ !isDead)
				{
					isDead = true;
					Main.tile[tileX,tileY].active(false);
					Main.PlaySound(SoundID.Grass);
					//WorldGen.SquareTileFrame(tileX,tileY+1, true);
					//no proper worldgen update, so that objects keep flying
					//Kill(tileX,tileX);//litterally a suicide (didn't work)
				}
			}
		}
		public override bool ValidTile(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			//return tile.active() && tile.type == mod.TileType("FlyingBlock") && tile.frameX == 0 && tile.frameY == 0;
			return tile.active() && tile.type == TileID.Dirt && tile.frameX == 0 && tile.frameY == 0;
		}
		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
    {
      return Place(i, j);
    }
	}
}

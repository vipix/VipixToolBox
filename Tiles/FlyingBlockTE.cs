using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace VipixToolBox.Tiles
{
	public class FlyingBlockTE : ModTileEntity
	{
		public int timer = 0;
		public bool isDead = false;
		public int tileX;
		public int tileY;

		public override void Update()
		{
			Vector2 dustSpawn;
			dustSpawn = new Vector2(tileX,tileY);

			if (!isDead) timer--;//that's dark. Why didn't you make isAlive instead.
			if ( timer == 30)
			{
				int dust = Dust.NewDust(dustSpawn*16, 10, 10, 15);//last number is the type
			}
			if ( timer < 15 && timer != 0)
			{
				int dust = Dust.NewDust(dustSpawn*16, 10, 10, 15);//last number is the type
			}
			if (!isDead && timer == 0)
			{
				isDead = true;
				Main.tile[tileX,tileY].active(false);
				Main.PlaySound(SoundID.Grass);
				//WorldGen.SquareTileFrame(tileX,tileY+1, true);
				//no proper worldgen update, so that objects keep flying
				//Kill(tileX,tileX);//litterally a suicide (didn't work)
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

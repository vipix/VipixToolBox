using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using VipixToolBox.UI;
using VipixToolBox.Items;

namespace VipixToolBox
{
    class VipixToolBox : Mod
    {
        private UserInterface colorUserInterface;
        private UserInterface hammerUserInterface;
        private UserInterface blockUserInterface;
        private UserInterface mossUserInterface;
        internal ColorUI colorUI;
        internal HammerUI hammerUI;
        internal BlockUI blockUI;
        internal MossUI mossUI;

        public static List<int> toolList;
        public static List<int> woods;
        public static string[] toolNames;

        public static List<int> paints;

        public static List<int> sandList;
        public static List<int> hardenedSandList;


        public static List<int> validBlocks;
        public static List<int> validItems;

        public static List<int> treeList;

        public static VipixToolBox Instance => ModContent.GetInstance<VipixToolBox>();

        public override void Load()
        {
            SetupClientUIs();
            LoadData();
            //AddGlobalItem("StaffofRegrowthEdit", new StaffofRegrowthEdit());
        }

        public override void Unload()
        {
            UnloadData();
        }

        private void LoadData()
        {
            toolList = new List<int>
            {
                TileID.LeafBlock,
                TileID.LivingWood,
                TileID.LivingMahoganyLeaves,
                TileID.LivingMahogany,
                TileID.Hive,
                TileID.BoneBlock
            };
            woods = new List<int>
            {
                ItemID.Wood,
                ItemID.Ebonwood,
                ItemID.RichMahogany,
                ItemID.Pearlwood,
                ItemID.Shadewood,
                ItemID.SpookyWood,
                ItemID.BorealWood,
                ItemID.PalmWood
            };
            toolNames = new[] { "a", "b", "c", "d", "e", "f" };

            paints = new List<int>();
            for (int i = 0; i < 27; i++) paints.Add(ItemID.RedPaint + i);//adding the item IDs from red paint to gray paint
            for (int i = 0; i < 3; i++) paints.Add(ItemID.BrownPaint + i);//the last three, brown, shadow and negative

            sandList = new List<int>
            {
                TileID.Sand,//alphabetical order
                TileID.Crimsand,
                TileID.Ebonsand,
                TileID.Pearlsand
            };
            hardenedSandList = new List<int>
            {
                TileID.HardenedSand,
                TileID.CrimsonHardenedSand,
                TileID.CorruptHardenedSand,
                TileID.HallowHardenedSand
            };//mirror order of sands

            validBlocks = new List<int>{
                0,1,6,7,8,9,38,39,40,45,46,47,53,54,57,59,75,76,188,112,116,118,119,120,
                121,122,123,130,131,137,140,145,146,147,148,150,151,152,153,154,155,156,
                160,161,166,167,168,169,175,176,177,189,193,195,196,194,197,198,202,163,
                164,200,206,224,229,230,137,137,137,137,234,272,248,249,250,170,262,263,
                264,265,266,267,268,273,274,284,311,312,313,315,325,326,327,328,329,336,
                340,341,342,343,344,345,346,347,348,350,351,357,367,368,369,370,371,379,
                385,396,397,398,399,400,401,402,403,404,407,408,409,415,416,417,418,
                30,157,159,208,251,252,253,321,322,2,70,179,180,181,182,183,184

            };//IDs of blocks that need 1 hit from a pickaxe with >100 power except the grass

            validItems = new List<int>{
                2,3,11,12,13,14,129,131,133,141,143,145,169,170,172,176,192,214,276,370,
                408,412,413,414,415,416,424,511,512,539,577,586,591,593,594,604,607,609,
                611,612,613,614,662,664,699,700,701,702,717,718,719,751,762,763,765,766,
                767,775,824,833,834,835,883,1103,1125,1127,1146,1147,1148,1149,1246,1344,
                1589,1591,1593,1872,1970,1971,1972,1973,1974,1975,1976,2119,2120,2173,
                2260,2261,2262,2435,2692,2693,2694,2695,2697,2701,2751,2752,2753,2754,
                2755,2787,2792,2793,2794,2860,2868,3066,3081,3086,3087,3100,3113,3214,
                3234,3271,3272,3274,3275,3276,3277,3338,3339,3347,3380,3460,3461,3573,
                3574,3575,3576,
                9,619,621,911,1725,1727,1729,2503,2504,2,176,1,1,1,1,1,1
            };//IDs of corresponding items

            treeList = new List<int>
            {
                TileID.Trees,
                TileID.MushroomTrees,
                TileID.ChristmasTree,
                TileID.PalmTree
            };
        }

        private void UnloadData()
        {
            toolList = null;
            woods = null;
            toolNames = null;

            paints = null;

            sandList = null;
            hardenedSandList = null;

            validBlocks = null;
            validItems = null;

            treeList = null;
        }

        private void SetupClientUIs()
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            //Activation of the UIs
            colorUI = new ColorUI();
            hammerUI = new HammerUI();
            blockUI = new BlockUI();
            mossUI = new MossUI();
            colorUI.Activate();
            hammerUI.Activate();
            blockUI.Activate();
            mossUI.Activate();
            colorUserInterface = new UserInterface();
            hammerUserInterface = new UserInterface();
            blockUserInterface = new UserInterface();
            mossUserInterface = new UserInterface();
            blockUserInterface.SetState(blockUI);
            colorUserInterface.SetState(colorUI);
            hammerUserInterface.SetState(hammerUI);
            mossUserInterface.SetState(mossUI);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));

            if (MouseTextIndex != -1)
            {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                "Vipix Toolbox",
                delegate
                {
                    if (ColorUI.visible)
                    {
                        colorUserInterface.Update(Main._drawInterfaceGameTime); //I don't understand
                        colorUI.Draw(Main.spriteBatch);
                    }
                    if (HammerUI.visible)
                    {
                        hammerUserInterface.Update(Main._drawInterfaceGameTime);    //I don't understand
                        hammerUI.Draw(Main.spriteBatch);
                    }
                    if (BlockUI.visible)
                    {
                        blockUserInterface.Update(Main._drawInterfaceGameTime); //I don't understand
                        blockUI.Draw(Main.spriteBatch);
                    }
                    if (MossUI.visible)
                    {
                        mossUserInterface.Update(Main._drawInterfaceGameTime);
                        mossUI.Draw(Main.spriteBatch);
                    }
                    return true;
                },
                    InterfaceScaleType.Game)
                    );
            }
        }
    }
}

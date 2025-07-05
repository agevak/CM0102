using System.Collections.Generic;
using System.IO;
using System.Linq;
using CM.Save;
using CM.Save.Model;

namespace CM
{
    public class SavePacker
    {
        public static void Main(string[] args)
        {
            foreach (string savFilename in Directory.GetFiles(".", "*.sav"))
            {
                string folder = Path.GetFileNameWithoutExtension(savFilename);
                SaveReader saveReader = new SaveReader();
                saveReader.Load(savFilename, true);
                List<Block> orderedBlocks = saveReader.blocks.OrderBy(x => x.Position).ToList();
                uint pos = orderedBlocks.First().Position;
                foreach (Block block in orderedBlocks)
                {
                    string blockFilename = Path.Combine(folder, block.Name);
                    block.dataBuffer = File.ReadAllBytes(blockFilename);
                    block.Position = pos;
                    block.Size = (uint)block.dataBuffer.Length;
                    pos += block.Size;
                }
                saveReader.Write(savFilename, saveReader.WasCompressed);
            }
        }
   }
}
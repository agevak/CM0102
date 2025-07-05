using System.IO;
using CM.Save;
using CM.Save.Model;

namespace CM
{
    public class SaveUnpacker
    {
        public static void Main(string[] args)
        {
            foreach (string savFilename in Directory.GetFiles(".", "*.sav"))
            {
                string folder = Path.GetFileNameWithoutExtension(savFilename);
                Directory.CreateDirectory(folder);
                SaveReader saveReader = new SaveReader();
                saveReader.Load(savFilename, true);
                foreach (Block block in saveReader.blocks)
                {
                    saveReader.DumpBlockToFile(block.Name, Path.Combine(folder, block.Name));
                }
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using CM.Model;
using CM.Save.Model;

namespace CM.Save
{
    public class SaveReader
    {
        public static readonly Encoding ENCODING = Encoding.GetEncoding("ISO-8859-1");

        public static string GetString(byte[] data, int startIndex)
        {
            int endIndex = Array.IndexOf(data, (byte)0, startIndex);
            return ENCODING.GetString(data, startIndex, endIndex - startIndex);
        }
        public static string GetString(byte[] data) => GetString(data, 0);
        public static string GetUnicodeString(byte[] data, int startIndex)
        {
            int endIndex = Array.IndexOf(data, (byte)0, startIndex);
            return Encoding.UTF32.GetString(data, startIndex, endIndex - startIndex);
        }
        public static string GetUnicodeString(byte[] data) => GetUnicodeString(data, 0);

        public bool WasCompressed;

        private Stream stream;

        public CMDB GetDB()
        {
            CMDB db = new CMDB()
            {
                FirstNames = BlockToObjects<TNames>("first_names.dat"),
                SecondNames = BlockToObjects<TNames>("second_names.dat"),
                CommonNames = BlockToObjects<TNames>("common_names.dat"),
                Clubs = BlockToObjects<TClub>("club.dat"),
                Nations = BlockToObjects<TNation>("nation.dat"),
                ClubCompetitions = BlockToObjects<TComp>("club_comp.dat"),
                Staffs = BlockToObjects<TStaff>("staff.dat"),
                Players = BlockToObjects<TPlayer>("player.dat"),
                Contracts = BlockToObjects<TContract>("contract.dat", true),
                GameDate = GetCurrentGameDate(),
                HumanClub = GetHumanClub()
            };
            db.Initialize();
            return db;
        }

        public TClub GetHumanClub()
        {
            string humanClubName = GetHumanClubName();
            List<TClub> clubs = BlockToObjects<TClub>("club.dat");
            TClub humanClub = clubs.FirstOrDefault(x => SaveReader.GetString(x.ShortName) == humanClubName);
            return humanClub;
        }

        public string GetHumanClubName()
        {
            Block generalBlock = FindBlock("general.dat");
            string humanClubName = SaveReader.GetString(generalBlock.dataBuffer, 0x824);
            return humanClubName;
        }

        public IList<int> GetHumanTeamSelection(int offset)
        {
            Block tacticBlock = FindBlock("tactics.dat");
            IList<int> result = new List<int>();
            using (BinaryReader br = new BinaryReader(new MemoryStream(tacticBlock.dataBuffer)))
            {
                br.BaseStream.Seek(offset, SeekOrigin.Begin);
                for (int i = 0; i < 20; i++) result.Add(br.ReadInt32());
            }
            return result;
        }

        public IList<Tactic> ExtractAITactics()
        {
            IList<Tactic> result = new List<Tactic>();
            Block block = FindBlock("tactics.dat");
            //File.WriteAllBytes("ExtractAITactics_ai.dat", block.dataBuffer);
            //saveReader.DumpBlockToFile("human_manager.dat", "ExtractAITactics_human.dat");
            using (BinaryReader br = new BinaryReader(new MemoryStream(block.dataBuffer)))
            {
                int baseOffset = 4, tactCountTotal = br.ReadInt32();
                for (int i = 0; i < Tactic.AI_PACK_FILENAMES.Length; i++, baseOffset += 0x18E3)
                {
                    if (i == 28) baseOffset += 4;
                    br.BaseStream.Seek(baseOffset, SeekOrigin.Begin);
                    Tactic tactic = new Tactic() { Name = GetString(br.ReadBytes(51)) };
                    tactic.First250 = br.ReadBytes(250);
                    tactic.Next1115 = br.ReadBytes(1115);
                    br.BaseStream.Seek(baseOffset + 0x188B, SeekOrigin.Begin);
                    tactic.Next88 = br.ReadBytes(88);
                    br.BaseStream.Seek(baseOffset + 0x5B4, SeekOrigin.Begin);
                    tactic.Last11 = br.ReadBytes(11);
                    result.Add(tactic);
                    //File.WriteAllBytes($"_{tactic.Name}.tct", tactic.ToTctFile());
                }
            }
            return result;
        }

        public void ReplaceAITacticsWithSameOne(Tactic newTactic)
        {
            ReplaceAITactics(Enumerable.Repeat(newTactic, Tactic.AI_PACK_FILENAMES.Length).ToList());
        }

        public void ReplaceAITactics(IList<Tactic> newTactics)
        {
            if (newTactics == null) throw new ArgumentNullException("newTactics must be not null.");
            if (newTactics.Count != Tactic.AI_PACK_FILENAMES.Length) throw new ArgumentException($"newTactics must contain exactly {Tactic.AI_PACK_FILENAMES.Length} elements.");
            foreach (Tactic tactic in newTactics) if (tactic == null) throw new ArgumentException("newTactics may not contain null's.");
            Block block = FindBlock("tactics.dat");
            using (BinaryWriter bw = new BinaryWriter(new MemoryStream(block.dataBuffer)))
            {
                int baseOffset = 4;
                for (int i = 0; i < Tactic.AI_PACK_FILENAMES.Length; i++, baseOffset += 0x18E3)
                {
                    if (i == 28) baseOffset += 4;
                    bw.BaseStream.Seek(baseOffset, SeekOrigin.Begin);
                    newTactics[i].WriteToSavFile(bw);
                }
            }
            //File.WriteAllBytes("ReplaceAITactics.dat", block.dataBuffer);
        }

        public void ReplaceHumanTactic(Tactic newTactic)
        {
            Block block = FindBlock("human_manager.dat");
            using (BinaryWriter bw = new BinaryWriter(new MemoryStream(block.dataBuffer)))
            {
                int baseOffset = 0x45D84;
                bw.BaseStream.Seek(baseOffset, SeekOrigin.Begin);
                newTactic.WriteToSavFile(bw);
            }
            //File.WriteAllBytes("ReplaceHumanTactic.dat", block.dataBuffer);
        }

        public void Close()
        {
            try { stream.Dispose(); } catch { }
        }

        public void Load(byte[] fileContent)
        {
            stream = new MemoryStream(fileContent);
            Load(true);
            try { stream.Dispose(); } catch { }
        }
        public void Load(string inFile, bool full)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(inFile, FileMode.Open, FileAccess.Read);
                stream = new BufferedStream(fs);
                Load(full);
                if (full) try { fs.Dispose(); } catch { }
            }
            catch (Exception e)
            {
                if (fs != null) try { fs.Dispose(); } catch { }
                throw;
            }
        }
        private void Load(bool full)
        {
            using (BinaryReader br = new BinaryReader(stream, ENCODING, true))
            {
                WasCompressed = br.ReadInt32() == 4;

                // Skip 4 bytes
                unknownHdrBytes = br.ReadInt32();

                // Read Blocks
                int blockCount = br.ReadInt32();
                for (int i = 0; i < blockCount; i++)
                {
                    Block block = new Block();
                    block.blockBuffer = br.ReadBytes(268);
                    blocks.Add(block);
                }

                // Read data or save stream for further load.
                if (full) foreach (var block in blocks) ReadBlockDataIfNotReadYet(block);
            }
        }

        public Block FindBlock(string blockName)
        {
            Block block = blocks.FirstOrDefault(x => x.Name == blockName);
            ReadBlockDataIfNotReadYet(block);
            return block;
        }
        private void ReadBlockDataIfNotReadYet(Block block)
        {
            if (block.dataBuffer != null) return;
            using (BinaryReader br = new BinaryReader(stream, ENCODING, true))
            {
                br.BaseStream.Seek(block.Position, SeekOrigin.Begin);

                // Fast load if uncompressed.
                if (!WasCompressed)
                {
                    block.dataBuffer = br.ReadBytes((int)block.Size);
                    return;
                }

                // Decompress.
                block.dataBuffer = new byte[block.Size];
                int bufPtr = 0;
                while (bufPtr < block.Size)
                {
                    byte b = br.ReadByte();
                    if (b <= 128) block.dataBuffer[bufPtr++] = (byte)b;
                    else
                    {
                        byte byteCount = (byte)(b - 128);
                        byte actualByte = br.ReadByte();
                        for (byte i = 0; i < byteCount; i++)
                            block.dataBuffer[bufPtr++] = actualByte;
                    }
                }
            }
        }

        public void DumpBlockToFile(string blockName, string fileName)
        {
            var block = FindBlock(blockName);
            using (var fout = File.Create(fileName))
                fout.Write(block.dataBuffer, 0, block.dataBuffer.Length);
        }

        public List<T> BlockToObjects<T>(byte[] blockDataBuffer, bool contractSpecific = false)
        {
            int maxSlices = -1;
            int startAt = 0;
            if (contractSpecific)
            {
                var intPreCount = BitConverter.ToInt32(blockDataBuffer, 0);
                var blockCount = BitConverter.ToInt32(blockDataBuffer, 4);
                startAt = (8 + intPreCount * 21);

                if (intPreCount > 0)
                {
                    blockCount = BitConverter.ToInt32(blockDataBuffer, startAt - 4);
                }
                maxSlices = blockCount;
            }

            List<byte[]> slices = SliceBlock(blockDataBuffer, Marshal.SizeOf(typeof(T)), startAt, maxSlices);
            List<T> ret = CastSlicesToObjects<T>(slices);
            return ret;
        }
        public List<T> BlockToObjects<T>(string blockName, bool contractSpecific = false)
        {
            var block = FindBlock(blockName);
            if (block == null) return null;
            return BlockToObjects<T>(block.dataBuffer, contractSpecific);
        }

        public byte[] ObjectsToBlock<T>(List<T> objects)
        {
            var slices = CastObjectsToSlices(objects);
            return Rebuild((byte[])null, slices, false);
        }
        public void ObjectsToBlock<T>(string blockName, List<T> objects, bool contractSpecific = false)
        {
            var block = FindBlock(blockName);
            if (block != null)
            {
                var slices = CastObjectsToSlices(objects);
                Rebuild(block, slices, contractSpecific);
            }
        }

        public List<string> NamesFromBlock(string blockName)
        {
            List<string> ret = null;
            var block = FindBlock(blockName);
            if (block != null)
            {
                ret = new List<string>();
                var slices = SliceBlock(block.dataBuffer, 60);
                foreach (var slice in slices)
                    ret.Add(GetTextFromBytes(slice, 50));
            }
            return ret;
        }

        public string GetTextFromBytes(byte[] bytes, int length = -1)
        {
            return latin1.GetString(bytes, 0, (length == -1) ? bytes.Length : length).TrimEnd('\0');
        }

        public DateTime GetCurrentGameDate()
        {
            var block = FindBlock("general.dat");
            return TCMDate.ToDateTime(new TCMDate(block.dataBuffer, 3944));
        }

        public List<TStaff> FindPlayer(string firstName, string lastName, List<TStaff> staff)
        {
            List<TStaff> ret = new List<TStaff>();
            if (firstNames == null)
            {
                firstNames = NamesFromBlock("first_names.dat");
                secondNames = NamesFromBlock("second_names.dat");
            }

            List<int> fname_idx = new List<int>();
            List<int> sname_idx = new List<int>();
            for (int i = 0; i < firstNames.Count(); i++)
            {
                if (string.Compare(firstName, firstNames[i], CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0)
                {
                    fname_idx.Add(i);
                }
            }
            for (int i = 0; i < secondNames.Count(); i++)
            {
                if (string.Compare(lastName, secondNames[i], CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0)
                {
                    sname_idx.Add(i);
                }
            }

            ret.AddRange(staff.FindAll(x => fname_idx.Contains(x.FirstName) && sname_idx.Contains(x.SecondName)));

            return ret;
        }

        List<byte[]> SliceBlock(byte[] blockDataBuffer, int sliceSize, int startAt = 0, int maxSlices = -1)
        {
            List<byte[]> slices = new List<byte[]>();
            for (int i = startAt; i < blockDataBuffer.Length; i += sliceSize)
            {
                var slice = new byte[sliceSize];
                if (i + sliceSize <= blockDataBuffer.Length)
                    Array.Copy(blockDataBuffer, i, slice, 0, sliceSize);
                slices.Add(slice);
                if (maxSlices != -1 && slices.Count >= maxSlices)
                    break;
            }
            return slices;
        }

        List<T> CastSlicesToObjects<T>(List<byte[]> slices)
        {
            var ret = new List<T>();
            foreach (var slice in slices)
            {
                int objSize = Marshal.SizeOf(typeof(T));
                var ptrObj = Marshal.AllocHGlobal(objSize);
                Marshal.Copy(slice, 0, ptrObj, objSize);
                var obj = (T)Marshal.PtrToStructure(ptrObj, typeof(T));
                ret.Add(obj);
                Marshal.FreeHGlobal(ptrObj);
            }
            return ret;
        }

        List<byte[]> CastObjectsToSlices<T>(List<T> objects)
        {
            var ret = new List<byte[]>();
            foreach (var obj in objects)
            {
                int objSize = Marshal.SizeOf(typeof(T));
                byte[] arr = new byte[objSize];
                IntPtr ptr = Marshal.AllocHGlobal(objSize);
                Marshal.StructureToPtr(obj, ptr, true);
                Marshal.Copy(ptr, arr, 0, objSize);
                Marshal.FreeHGlobal(ptr);
                ret.Add(arr);
            }
            return ret;
        }

        byte[] Rebuild(byte[] blockDataBuffer, List<byte[]> slices, bool contractSpecific = false)
        {
            var totalSliceSize = slices.Sum(x => x.Length);
            byte[] newDataBuffer;
            int ptr = 0;

            if (contractSpecific)
            {
                var intPreCount = BitConverter.ToInt32(blockDataBuffer, 0);
                var count = BitConverter.ToInt32(blockDataBuffer, 4);
                ptr = (8 + (intPreCount * 21));
                /*
                newDataBuffer = new byte[totalSliceSize + ptr + 13];
                Array.Copy(block.dataBuffer, 0, newDataBuffer, 0, ptr);

                // Write new Block Count
                if (intPreCount == 0)
                    Array.Copy(BitConverter.GetBytes(slices.Count), 0, newDataBuffer, 4, 4);

                newDataBuffer[newDataBuffer.Length - 1] = 1;
                */
                // Simplified version (as we are not expanding or contracting this right now)
                newDataBuffer = new byte[blockDataBuffer.Length];
                Array.Copy(blockDataBuffer, 0, newDataBuffer, 0, blockDataBuffer.Length);
            }
            else
                newDataBuffer = new byte[totalSliceSize];

            foreach (var slice in slices)
            {
                Array.Copy(slice, 0, newDataBuffer, ptr, slice.Length);
                ptr += slice.Length;
            }

            return newDataBuffer;
        }
        void Rebuild(Block block, List<byte[]> slices, bool contractSpecific = false)
        {
            block.dataBuffer = Rebuild(block.dataBuffer, slices, contractSpecific);
        }

        public void Write(string outFile, bool compressed)
        {
            // Read data which was not read yet.
            foreach (var block in blocks) ReadBlockDataIfNotReadYet(block);

            using (var fout = File.Create(outFile))
            using (var bw = new BinaryWriter(fout))
            {
                // 3 is uncompressed, 4 is compressed (swap depending on what you're doing)
                bw.Write(compressed ? 4 : 3);

                // Unknown Bytes
                bw.Write(unknownHdrBytes);

                // Write Block Count
                bw.Write(blocks.Count);

                // Skip to go write the data
                fout.Seek((blocks.Count * 268) + 12, SeekOrigin.Begin);

                // Write Block Data
                foreach (var block in blocks)
                {
                    block.Position = (uint)fout.Position;
                    block.Size = (uint)block.dataBuffer.Length;
                    bw.Write(compressed ? Compress(block.dataBuffer) : block.dataBuffer);
                }

                // Write Block Headers (with updated positions)
                fout.Seek(12, SeekOrigin.Begin);
                foreach (var block in blocks)
                {
                    bw.Write(block.blockBuffer);
                }
            }
        }

        byte[] Compress(byte[] buffer)
        {
            var ms = new MemoryStream();
            int lastByte = -1;
            int lastByteCount = 0;
            for (var i = 0; i < buffer.Length; i++)
            {
                var b = buffer[i];

                if (lastByte == -1)
                    lastByte = b;

                if (b != lastByte || lastByteCount >= 126)
                {
                    if (lastByteCount == 1 && lastByte < 128)
                        ms.WriteByte((byte)lastByte);
                    else
                    {
                        ms.WriteByte((byte)(lastByteCount + 128));
                        ms.WriteByte((byte)lastByte);
                    }

                    lastByte = b;
                    lastByteCount = 1;
                }
                else
                {
                    lastByteCount++;
                }
            }

            // Write last part
            if (lastByteCount == 1 && lastByte < 128)
                ms.WriteByte((byte)lastByte);
            else
            {
                ms.WriteByte((byte)(lastByteCount + 128));
                for (int j = 0; j < lastByteCount; j++)
                    ms.WriteByte((byte)lastByte);
            }

            return ms.ToArray();
        }

        int unknownHdrBytes = 0x16;
        Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");
        public List<Block> blocks = new List<Block>();
        List<string> firstNames = null;
        List<string> secondNames = null;
    }
}

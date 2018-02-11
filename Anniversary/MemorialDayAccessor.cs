using System;
using System.IO;
using System.Windows.Forms;

namespace Anniversary
{
    public static class MemorialDayAccessor
    {
        public static MemorialDayCollection Load(string path)
        {
            MemorialDayCollection memorialDays = new MemorialDayCollection();
            try
            {
                using (FileStream data = new FileStream(path, FileMode.OpenOrCreate))
                {
                    if (data.Length == 0)
                        throw new Exception("Bad File");
                    BinaryReader br = new BinaryReader(data);
                    uint count = br.ReadUInt32();
                    for (int i = 0; i < count; i++)
                    {
                        UInt32 y = br.ReadUInt32();
                        UInt32 m = br.ReadUInt32();
                        UInt32 w = br.ReadUInt32();
                        UInt32 d = br.ReadUInt32();

                        memorialDays.Add(new MemorialDay(y, m, d,
                            br.ReadString(),
                            br.ReadString(),
                            br.ReadString()));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n\n加载纪念日数据失败。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return memorialDays;
        }

        public static void SaveMemorialDaysData(MemorialDayCollection memorialDays, string path)
        {
            using (FileStream data = new FileStream("./Plugin/Anniversary/md.dat", FileMode.Create))
            {
                BinaryWriter bw = new BinaryWriter(data);
                bw.Write((UInt32)memorialDays.Count);
                foreach (MemorialDay md in memorialDays)
                {
                    bw.Write((UInt32)md.Date.Year);
                    bw.Write((UInt32)md.Date.Month);
                    bw.Write((UInt32)(md.Date.Week.HasValue ? md.Date.Week.Value : uint.MaxValue));
                    bw.Write((UInt32)md.Date.Day);
                    bw.Write(md.Title);
                    bw.Write(md.Description);
                    bw.Write(md.ImagePath);
                }
            }
        }
    }
}

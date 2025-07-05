using System;
using System.Collections.Generic;
using System.IO;
using CM.Model;
using static System.Windows.Forms.AxHost;

namespace CM.Save
{
    public class CMDBCacher
    {
        private readonly IDictionary<string, DateTime> timestamps = new Dictionary<string, DateTime>();
        private readonly IDictionary<string, CMDB> dbs = new Dictionary<string, CMDB>();

        public CMDBCacher() { }

        public CMDB Load(string filename)
        {
            // Check if already cached.
            DateTime timestamp = new FileInfo(filename).LastWriteTime;
            if (timestamps.ContainsKey(filename) && timestamp == timestamps[filename]) return dbs[filename];

            // Load.
            SaveReader saveReader = new SaveReader();
            saveReader.Load(filename, false);
            CMDB db = saveReader.GetDB();
            db.Filename = filename;
            saveReader.Close();

            // Save to cache.
            timestamps[filename] = timestamp;
            dbs[filename] = db;

            return db;
        }
    }
}

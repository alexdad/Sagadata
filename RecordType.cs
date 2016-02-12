﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{
    public abstract class RecordType
    {
        protected FormGlob m_glob;

        public RecordType(FormGlob glob)
        {
            m_glob = glob;
        }
        public abstract string Class();
        public abstract void Initialize();
        public abstract bool ReadFile();
        public abstract bool DownloadFile();
        public abstract bool UploadFile();
        public abstract void ShowCount();
        public abstract void DoSelection();
        public abstract void SortRecords(string hdr, Record[] temp);

        private void ReplaceRecordList(Record[] target)
        {
            m_glob.DataList.Clear();
            foreach (Record s in target)
                m_glob.DataList.Add(s);
        }



        public bool ReadRecordsFile<T>() where T : Record
        {
            m_glob.DataList.Clear();
            m_glob.RecordsAsRead.Clear();
            bool success = false;
            try
            {
                string[] sts = File.ReadAllLines(m_glob.FilePath);

                ParseHeaders(sts[0].Split(','));

                for (int s = 1; s < sts.Length; s++)
                {
                    string safeStr = SafeGuard(sts[s]);
                    T st = ParseValues<T>(s, safeStr.Split(','));
                    m_glob.DataList.Add(st);
                    m_glob.RecordsAsRead.Add(st.Key, st);
                    FormGlob.AccumulateID(st.Id);
                }
                success = true;
            }
            catch(Exception) {}
            return success;
        }

        public void WriteRecordsFile()
        {
            if (File.Exists(m_glob.FilePath))
            {
                string bkup = DecideBackup();
                if (File.Exists(bkup))
                    File.Delete(bkup);
                File.Move(m_glob.FilePath, bkup);
            }

            using (StreamWriter sw = new StreamWriter(m_glob.FilePath))
            {
                WriteHeader(sw);
                WriteValues(sw);
            }
        }

        public string WriteTempFile()
        {
            string tempFile = Path.GetTempFileName().ToLower().Replace(".tmp", ".csv");
            using (StreamWriter sw = new StreamWriter(tempFile))
            {
                WriteHeader(sw);
                WriteValues(sw);
            }
            return tempFile;
        }

        public string DecideBackup()
        {
            string prefix = Path.Combine(m_glob.BackupLocation, m_glob.CurrentModeName + ".backup");
            DateTime maxDt = DateTime.MinValue;
            const int maxBackup = 100;
            int index = -1;
            for (int i = 0; i < maxBackup; i++)
            {
                string fl = prefix + i.ToString() + ".csv";
                if (File.Exists(fl) &&
                    File.GetLastWriteTime(fl) > maxDt)
                {
                    maxDt = File.GetLastWriteTime(fl);
                    index = i;
                }
            }
            if (index >= 0)
                index++;
            else
                index = 0;

            return prefix + index.ToString() + ".csv";
        }

        public T ParseValues<T>(int ind, string[] vals) where T : Record
        {
            T st = Activator.CreateInstance<T>();

            for (int i = 0; i < m_glob.Placements.Length; i++)
            {
                int fld = m_glob.Placements[i];
                if (fld < 0)
                    continue;
                Validations tp = m_glob.Schema[fld].Validation;
                string val = vals[i];
                if (tp == Validations.Ignore)
                    continue;

                // Check validations here

                bool success = st.Set(m_glob.Schema[fld].Header, val);
                if (!success)
                    MessageBox.Show("Bad value");
            }

            return st;
        }

        void WriteValues(StreamWriter sw)
        {
            foreach (Record s in m_glob.DataList)
            {
                Record st = s;
                if (m_glob.RecordsAsRead.ContainsKey(st.Key))
                {
                    Record oldSt = m_glob.RecordsAsRead[st.Key];
                    if (RecordDiffers(st, oldSt))
                    {
                        st.Changed = DateTime.Now;
                        st.ChangedBy = FormGlob.Client;
                    }
                }

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < m_glob.Schema.Length; i++)
                {
                    string safeValue = st.Get(m_glob.Schema[i].Header);
                    if (safeValue == null)
                        safeValue = "";
                    safeValue = safeValue.Replace('"', ' ');
                    safeValue = safeValue.Replace(',', ';');
                    sb.Append("\"");
                    sb.Append(safeValue);
                    sb.Append("\",");
                }
                sw.WriteLine(sb.ToString());
            }
        }
        public void ParseHeaders(string[] hdrs)
        {
            m_glob.Placements = new int[hdrs.Length];

            for (int j = 0; j < hdrs.Length; j++)
                m_glob.Placements[j] = -1;

            for (int i = 0; i < hdrs.Length; i++)
            {
                for (int j = 0; j < m_glob.Schema.Length; j++)
                {
                    if (m_glob.Schema[j].Header.ToLower() == hdrs[i].ToLower())
                        m_glob.Placements[i] = j;
                }
            }
        }

        void WriteHeader(StreamWriter sw)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < m_glob.Schema.Length; i++)
            {
                sb.Append(m_glob.Schema[i].Header);
                sb.Append(",");
            }
            sw.WriteLine(sb.ToString());
        }

        private string SafeGuard(string s)
        {
            int quotes = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ',' && quotes % 2 == 1)
                    sb.Append(' ');
                else
                {
                    sb.Append(s[i]);
                    if (s[i] == '\"')
                        quotes++;
                }
            }
            return sb.ToString();
        }

        public T[] ReadCloudFile<T>(string cloudFile) where T : Record
        {
            if (!File.Exists(cloudFile))
                throw new Exception("Cannot find cloud file");

            string[] sts = File.ReadAllLines(cloudFile);
            T[] recs = new T[sts.Length - 1];

            ParseHeaders(sts[0].Split(','));

            for (int s = 1; s < sts.Length; s++)
            {
                string safeStr = SafeGuard(sts[s]);
                recs[s - 1] = ParseValues<T> (s, safeStr.Split(','));
            }
            return recs;
         }

        public bool Download<T>() where T : Record
        {
            bool success = false;
            switch (m_glob.CloudType)
            {
                case Clouds.Google:
                    success = GDrive.Ops.DownloadDataFile(m_glob.CurrentModeName + ".csv", m_glob.CloudLocation);
                    if (!success)
                        MessageBox.Show("Cannot download from Google. Continuing to use local file.");
                    break;
                case Clouds.Azure:
                case Clouds.Dir:
                default:
                    break;
            }

            if (success)
            {
                m_glob.LastDownloadText = "Last download: " + DateTime.Now.ToShortTimeString();
                T[] temp = ReadCloudFile<T>(m_glob.CloudLocation);
                MergeBack(temp);
            }
            ShowCount();
            return success;
        }

        public bool Upload<T>() where T : Record
        {
            Record[] temp = ReadCloudFile<T>(m_glob.CloudLocation);
            MergeBack(temp);
            WriteRecordsFile();
            m_glob.LastDownloadText  = "Last download: " + DateTime.Now.ToShortTimeString();

            if (File.Exists(m_glob.CloudLocation))
                File.Delete(m_glob.CloudLocation);

            File.Copy(m_glob.FilePath, m_glob.CloudLocation);

            bool success = false;
            switch (m_glob.CloudType)
            {
                case Clouds.Google:
                    success = GDrive.Ops.UploadDataFile(m_glob.CloudLocation, m_glob.CurrentModeName + ".csv");
                    break;
                case Clouds.Azure:
                case Clouds.Dir:
                default:
                    break;
            }
            if (!success)
                MessageBox.Show("Cannot upload to the cloud. Local file is OK.");
            else
                m_glob.LastUploadText = "Last upload: " + DateTime.Now.ToShortTimeString();

            return success;
        }
        public T[] ForkOut<T>(int addedPositions) where T : Record
        {
            T[] temp = new T[m_glob.DataList.Count + addedPositions];
            int i = 0;
            foreach (T s in m_glob.DataList)
                temp[i++] = s;
            return temp;
        }


        public void MergeBack<T>(T[] target) where T : Record
        {
            Dictionary<string, T> dict = new Dictionary<string, T>();
            foreach (T s in m_glob.DataList)
            {
                if (!IsRecordDeleted(s))
                    dict.Add(s.Key, s);
            }

            foreach (T t in target)
            {
                if (!IsRecordDeleted(t))
                {
                    if (!dict.ContainsKey(t.Key))
                        dict.Add(t.Key, t);
                    else if (RecordDiffers(t, dict[t.Key]))
                    {
                        if (dict[t.Key].Changed < t.Changed)
                        {
                            dict[t.Key] = t;        // Target had newer, restoring back
                            MessageBox.Show(String.Format(
                                "Merge conflict: {0} edited {1} at {2}; not taking yours!",
                                               t.ChangedBy,
                                               t.Description,
                                               t.Changed));
                        }
                    }
                }
            }

            m_glob.DataList.Clear();
            foreach (T s in dict.Values)
                m_glob.DataList.Add(s);

            m_glob.DeletedKeys.Clear();
        }

        public void EndSelectionMode()
        {
            if (m_glob.SelectionMode)
            {
                Record[] temp = ForkOut<Record>(0);
                RestoreRecordList();
                MergeBack(temp);
                m_glob.SelectionMode = false;
            }
        }

        public void ReplaceRecordList<T>(T[] target)
        {
            m_glob.DataList.Clear();
            foreach (T s in target)
                m_glob.DataList.Add(s);
        }
        public void StashRecordList()
        {
            m_glob.SavedFullListDuringSelection = ForkOut<Record>(0);
        }

        public void RestoreRecordList()
        {
            m_glob.DataList.Clear();
            foreach (Record s in m_glob.SavedFullListDuringSelection)
                m_glob.DataList.Add(s);
            m_glob.SavedFullListDuringSelection = null;
        }
        public bool IsRecordDeleted(Record s)
        {
            return m_glob.DeletedKeys.Contains(s.Key);
        }

        public bool RecordDiffers(Record s1, Record s2)
        {
            for (int i = 0; i < m_glob.Schema.Length; i++)
            {
                string hdr = m_glob.Schema[i].Header;
                if (hdr == "DateTimeChanged" ||
                    hdr == "ChangedBy")
                    continue;
                string val1 = s1.Get(hdr);
                string val2 = s2.Get(hdr);
                if (Empty(val1) && Empty(val2))
                    continue;

                if (val1 != val2)
                {
                    m_glob.Changed = true;
                    return true;
                }
            }
            return false;
        }

        public static bool Empty(string s)
        {
            return (s == null || s.Trim().Length == 0 || s == "?");
        }
        public bool Fit(string what, string where, bool fExact)
        {
            if (where == null || where == "")
                return false;
            else if (what == null || what == "" || what == "?")
                return true;
            else if (fExact)
                return (where.ToLower().Trim() == what.ToLower().Trim());
            else
                return (where.ToLower().Contains(what.ToLower()));
        }

    }

}
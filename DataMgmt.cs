using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Students
{
    public partial class Form1 : Form
    {
        static int s_lastColumnSorted = -1;
        static bool s_needToReverse = false;


        private Student[] ForkOut(int addedPositions)
        {
            Student[] temp = new Student[studentList.Count + addedPositions];
            int i = 0;
            foreach (Student s in studentList)
                temp[i++] = s;
            return temp;
        }
        private bool IsStudentDeleted(Student s)
        {
            return m_deletedKeys.Contains(s.Key);
        }

        private static bool Empty(string s)
        {
            return (s == null || s.Trim().Length == 0 || s == "?");
        }
        public bool StudentDiffers(Student s1, Student s2)
        {
            for (int i = 0; i < m_schema.Length; i++)
            {
                string hdr = m_schema[i].Header;
                if (hdr == "DateTimeChanged" ||
                    hdr == "ChangedBy")
                    continue;
                string val1 = s1.Get(hdr);
                string val2 = s2.Get(hdr);
                if (Empty(val1) && Empty(val2))
                    continue;

                if (val1 != val2)
                {
                    m_bChanged = true;
                    return true;
                }
            }
            return false;
        }

        private void MergeBack(Student[] target)
        {
            Dictionary<string, Student> dict = new Dictionary<string, Student>();
            foreach (Student s in studentList)
            {
                if (!IsStudentDeleted(s))
                    dict.Add(s.Key, s);
            }

            foreach (Student t in target)
            {
                if (!IsStudentDeleted(t))
                {
                    if (!dict.ContainsKey(t.Key))
                        dict.Add(t.Key, t);
                    else if (StudentDiffers(t, dict[t.Key]))
                    {
                        if (dict[t.Key].Changed < t.Changed)
                        {
                            dict[t.Key] = t;        // Target had newer, restoring back
                            MessageBox.Show(String.Format(
                                "Merge conflict: {0} edited {1} at {2}; not taking yours!",
                                               t.ChangedBy,
                                               t.FirstName + " " + t.LastName,
                                               t.Changed));
                        }
                    }
                }
            }

            studentList.Clear();
            foreach (Student s in dict.Values)
                studentList.Add(s);

            m_deletedKeys.Clear();
        }

        private void ReplaceStudentList(Student[] target)
        {
            studentList.Clear();
            foreach (Student s in target)
                studentList.Add(s);
        }

        private void StashStudentList()
        {
            m_savedFullListDuringSelection = ForkOut(0);
        }

        private void RestoreStudentList()
        {
            studentList.Clear();
            foreach (Student s in m_savedFullListDuringSelection)
                studentList.Add(s);
            m_savedFullListDuringSelection = null;
        }

        private bool Fit(string what, string where, bool fExact)
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
        private void DoSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                StashStudentList();
            }

            studentList.Clear();
            foreach (Student s in m_savedFullListDuringSelection)
            {
                if (!Fit(m_selectionStatus, s.Status, true))
                    continue;
                if (!Fit(m_selectionLearns, s.LearningLanguage, true))
                    continue;
                if (!Fit(m_selectionSpeaks, s.NativeLanguage, true))
                    continue;
                if (!Fit(m_selectionFirstName, s.FirstName, false))
                    continue;
                if (!Fit(m_selectionLastName, s.LastName, false))
                    continue;
                if (!Fit(m_selectionSource, s.Source, true))
                    continue;
                if (!Fit(m_selectionLevel, s.Level, true))
                    continue;

                studentList.Add(s);
            }
            ShowStudentCount();
        }

        void SortStudents(string hdr, Student[] temp)
        {
            switch (hdr)
            {
                case "Status":
                    Array.Sort(temp, new ComparerByStatus());
                    break;
                case "Changed":
                    Array.Sort(temp, new ComparerByChanged());
                    break;
                case "Learning":
                    Array.Sort(temp, new ComparerByLearns());
                    break;
                case "Other":
                    Array.Sort(temp, new ComparerByOther());
                    break;
                case "Level":
                    Array.Sort(temp, new ComparerByLevel());
                    break;
                case "Native":
                    Array.Sort(temp, new ComparerBySpeaks());
                    break;
                case "First Name":
                    Array.Sort(temp, new ComparerByFirstName());
                    break;
                case "Last Name":
                    Array.Sort(temp, new ComparerByLastName());
                    break;
                case "Source":
                    Array.Sort(temp, new ComparerBySource());
                    break;
                default:
                    s_needToReverse = false;
                    break;
            }
            if (s_needToReverse)
                Array.Reverse(temp);
        }
    }
}
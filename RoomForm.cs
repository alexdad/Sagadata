using System;
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
    public partial class FormGlob : Form
    {
        string m_RoomSelectionCapacity;
        string m_RoomSelectionName;
        string m_RoomSelectionRank;
        string m_RoomSelectionTags;

        delegate bool EvaluateRoom(Room t);

        private void DropRoomSelection()
        {
            m_RoomSelectionCapacity = null;
            m_RoomSelectionName = null;
            m_RoomSelectionRank = null;
            m_RoomSelectionTags = null;
        }

        private void RoomToFormConst1()
        {
            m_dataTypes.Add(Modes.Rooms, typeof(Room));
            m_recordTypes.Add(Modes.Rooms, new RoomType(this));
        }
        private void RoomToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.Rooms.ToString() );
        }

        public void DoRoomSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                CurrentType.StashRecordList();
            }

            DataList.Clear();
            foreach (Room s in m_recordTypes[Modes.Rooms].SavedFullListDuringSelection)
            {
                // TODO - make it >= number
                if (!CurrentType.Fit(m_RoomSelectionCapacity, s.Capacity.ToString(), true))
                    continue;
                if (!CurrentType.Fit(m_RoomSelectionName, s.Name, true))
                    continue;
                // TODO - make it >= number
                if (!CurrentType.Fit(m_RoomSelectionRank, s.Rank.ToString(), true))
                    continue;
                if (!CurrentType.Fit(m_RoomSelectionTags, s.Tags, false))
                    continue;
                DataList.Add(s);
            }
            ShowCurrentCount();
        }
        List<Room> ActiveRooms()
        {
            return FindRooms(t => t.Actual);
        }

        List<Room> FindRooms(EvaluateRoom comp)
        {
            List<Room> rooms = new List<Room>();
            foreach (var tt in this.roomList.List)
            {
                Room t = tt as Room;
                if (comp(t))
                    rooms.Add(t);
            }
            return rooms;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{

    public class RoomType : RecordType
    {
        public RoomType(FormGlob glob) : base(glob)
        {
        }

        public override string Class()
        {
            return "Room";
        }
        public override void Initialize()
        {

        }
        public override bool ReadFile()
        {
            return ReadRecordsFile<Room>();
        }
        public override bool DownloadFile()
        {
            return Download<Room>();
        }
        public override bool UploadFile()
        {
            return Upload<Room>();
        }

        public override void ShowCount()
        {
            m_glob.ShowRoomCount();
        }

        public override void DoSelection()
        {
            m_glob.DoRoomSelection();
        }

        public override void SortRecords(string hdr, Record[] temp)
        {
            SortRooms(hdr, temp as Room[]);
        }
        public void SortRooms(string hdr, Room[] temp)
        {
            switch (hdr)
            {
                case "Capacity":
                    Array.Sort(temp, new Room.ComparerByCapacity());
                    break;
                case "Name":
                    Array.Sort(temp, new Room.ComparerByName());
                    break;
                case "Rank":
                    Array.Sort(temp, new Room.ComparerByRank());
                    break;
                case "Tags":
                    Array.Sort(temp, new Room.ComparerByTags());
                    break;
                default:
                    Record.NeedToReverse = false;
                    break;
            }
            if (Record.NeedToReverse)
                Array.Reverse(temp);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    public class Room : Record
    {
        public int Capacity { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public string Tags { get; set; }
        public int ColorR { get; set; }
        public int ColorG { get; set; }
        public int ColorB { get; set; }

        public override string Description
        {
            get { return Name; }
        }

        public Color RoomColor
        {
            get { return Color.FromArgb(ColorR, ColorG, ColorB);  }
        }
        public override string Abbreviation
        {
            get
            {
                return (Name.Length > 0 ? Name.Substring(0, 1) : "X");
            }
        }

        public override bool Set(string field, string fieldValue)
        {
            string value = SetRecordFields(field, fieldValue);
            if (value.Length == 0)
                return true;
            switch (field)
            {
                case "Capacity":
                    this.Capacity = int.Parse(value);
                    break;
                case "Name":
                    this.Name = value;
                    break;
                case "Rank":
                    this.Rank = int.Parse(value);
                    break;
                case "Tags":
                    this.Tags = value;
                    break;
                case "ColorR":
                    this.ColorR = int.Parse(value);
                    break;
                case "ColorG":
                    this.ColorG = int.Parse(value);
                    break;
                case "ColorB":
                    this.ColorB = int.Parse(value);
                    break;
                default:
                    throw new Exception("unknown field " + field);
            }
            return true;
        }

        public Room()
        {
            Capacity = -1;
            Name = "";
            Rank = -1;
            Tags = "";
        }

        public override string Get(string field)
        {
            string v = GetRecordFields(field);
            if (v != null)
                return v;

            switch (field)
            {
                case "Name":
                    return Name;
                case "Capacity":
                    return Capacity.ToString();
                case "Rank":
                    return Rank.ToString();
                case "Tags":
                    return Tags;
                case "ColorR":
                    return ColorR.ToString();
                case "ColorG":
                    return ColorG.ToString();
                case "ColorB":
                    return ColorB.ToString();
                default:
                    throw new Exception("unknown field " + field);

            }
        }

        #region "Comparers"
        public class ComparerByName : IComparer<Room>
        {
            public int Compare(Room y, Room x)
            {
                return y.Name.CompareTo(x.Name);
            }
        }
        public class ComparerByCapacity : IComparer<Room>
        {
            public int Compare(Room y, Room x)
            {
                return y.Capacity.CompareTo(x.Capacity);
            }
        }
        public class ComparerByRank : IComparer<Room>
        {
            public int Compare(Room y, Room x)
            {
                return y.Rank.CompareTo(x.Rank);
            }
        }
        public class ComparerByTags : IComparer<Room>
        {
            public int Compare(Room y, Room x)
            {
                return y.Tags.CompareTo(x.Tags);
            }
        }
        #endregion
    }
}
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GCal
{
    public class CalEvent
    {
        public CalEvent(
            DateTime Start, 
            DateTime End,
            DateTime? Created,
            DateTime? Updated,
            string Location,
            string Description,
            string Summary,
            string Status,
            string Id,
            string Creator)
        {
            this.Start = Start;
            this.End = End;
            this.Created = Created;
            this.Updated = Updated;
            this.Location = Location;
            this.Description = Description;
            this.Summary = Summary;
            this.Status = Status;
            this.Id = Id;
            this.Creator = Creator;
        }
        public CalEvent(
            DateTime Start,
            DateTime End,
            string Location,
            string Description,
            string Summary)
        {
            this.Start = Start;
            this.End = End;
            this.Location = Location;
            this.Description = Description;
            this.Summary = Summary;
            this.Created = null;
            this.Updated = null;
            this.Status = null;
            this.Creator = null;
            this.Id = null;
        }

        public DateTime Start;
        public DateTime End;
        public DateTime? Created;
        public DateTime? Updated;
        public string Location;
        public string Description;
        public string Summary;
        public string Status;
        public string Id;
        public string Creator;

        public string LastTouched
        {
            get
            {
                DateTime last = DateTime.MinValue;
                if (Created.HasValue && Created > last)
                    last = Created.Value;
                if (Updated.HasValue && Updated > last)
                    last = Updated.Value;
                if (last == DateTime.MinValue)
                    return "";
                else
                    return last.ToString();

            }
        }
    };

    public class Ops
    {
        static string[] RWScopes = { CalendarService.Scope.Calendar };
        static string[] ROScopes = { CalendarService.Scope.CalendarReadonly };

        private static CalendarService GetCalendarService()
        {
            UserCredential credential;

            using (var stream = new FileStream(
                RecordKeeper.FormGlob.Bindings.AuthFile, 
                FileMode.Open, 
                FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal);

                credPath = Path.Combine(credPath, ".credentials/sagalingua1214");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    RWScopes,
                    RecordKeeper.FormGlob.Bindings.CalUser,  // "user",  
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;

            }

            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = RecordKeeper.FormGlob.Bindings.ApplicationName,
            });
        }

        public static List<CalEvent> ReadEvents(
            DateTime dtMin, 
            DateTime dtMax,
            string calendarId)
        {
            CalendarService service = GetCalendarService();

            EventsResource.ListRequest request = service.Events.List(calendarId);
            request.TimeMin = dtMin;
            request.TimeMax = dtMax;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10000;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            List<CalEvent> calEvents = new List<CalEvent>();
            Events events = request.Execute();
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    calEvents.Add(
                        new CalEvent(
                            (eventItem.Start.DateTime.HasValue ? 
                                    (DateTime)eventItem.Start.DateTime : 
                                    DateTime.Now),
                            (eventItem.End.DateTime.HasValue ?
                                    (DateTime)eventItem.End.DateTime :
                                    DateTime.Now),
                            eventItem.Created,
                            eventItem.Updated,
                            eventItem.Location,
                            eventItem.Description,
                            eventItem.Summary,
                            eventItem.Status,
                            eventItem.Id,
                            eventItem.Creator.DisplayName));
                }
            }
            return calEvents;
        }

        public static bool DeleteAllEvents(
            DateTime dtMin, 
            DateTime dtMax,
            string calendarId)
        {
            CalendarService service = GetCalendarService();

            EventsResource.ListRequest request = service.Events.List(calendarId);
            request.TimeMin = dtMin;
            request.TimeMax = dtMax;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10000;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();
            bool success = true;
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    EventsResource.DeleteRequest del = service.Events.Delete(
                        calendarId, eventItem.Id);

                    string res = del.Execute();
                    if (res != null && res.Trim().Length != 0)
                        success = false;
                }
            }
            return success;
        }
        public static bool WriteCalendarEvents(List<CalEvent> events, string calendarId)
        {
            CalendarService service = GetCalendarService();

            foreach (CalEvent ce in events)
            {
                Event evt = new Event
                {
                    Summary = ce.Summary,
                    Location = ce.Location,
                    Description = ce.Description,
                    Start = new EventDateTime()
                    {
                        DateTime = ce.Start,
                        TimeZone = "America/Los_Angeles"
                    },
                    End = new EventDateTime()
                    {
                        DateTime = ce.End,
                        TimeZone = "America/Los_Angeles"
                    }
                };

                EventsResource.InsertRequest req = service.Events.Insert(evt, calendarId);
                Event result = req.Execute();
            }

            return true;
        }

        public static List<GCal.CalEvent> GetOperationalEvents(DateTime dtMin, DateTime dtMax)
        {
            return ReadEvents(dtMin, dtMax, 
                              RecordKeeper.FormGlob.Bindings.OperationalCalendarID);
        }

        public static bool WriteSystemCalendarEvents(List<CalEvent> events)
        {
            return WriteCalendarEvents(events, 
                             RecordKeeper.FormGlob.Bindings.SystemCalendarID);
        }

        public static bool DeleteAllSystemCalendarEvents(DateTime dtMin, DateTime dtMax)
        {
            return DeleteAllEvents(dtMin, dtMax, 
                            RecordKeeper.FormGlob.Bindings.SystemCalendarID);
        }
    }
}


/*

Event evt = new Event
{
    Summary = "Appointment",
    Location = "Somewhere",
    Start = new EventDateTime()
    {
        DateTime = new DateTime(2016, 2, 28, 10, 0, 0),
        TimeZone = "America/Los_Angeles"
    },
    End = new EventDateTime()
    {
        DateTime = new DateTime(2016, 2, 28, 12, 30, 0),
        TimeZone = "America/Los_Angeles"
    }
    //,
    //Recurrence = new String[] { "RRULE:FREQ=WEEKLY;BYDAY=MO"   },
    //Attendees = new List<EventAttendee>()
    //{ new EventAttendee() { Email = "johndoe@gmail.com" } }
};

EventsResource.InsertRequest req = service.Events.Insert(evt, CalendarID);
Event result = req.Execute();
*/

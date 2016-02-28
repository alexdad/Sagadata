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
            string Status)
        {
            this.Start = Start;
            this.End = End;
            this.Created = Created;
            this.Updated = Updated;
            this.Location = Location;
            this.Description = Description;
            this.Summary = Summary;
            this.Status = Status;
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
        }

        public DateTime Start;
        public DateTime End;
        public DateTime? Created;
        public DateTime? Updated;
        public string Location;
        public string Description;
        public string Summary;
        public string Status;
    };

    public class Ops
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        const string ApplicationName = "Sagalingua1";
        const string User = "Galia Dadiomova";
        const string CalendarID = "6tgvdlnrp7i7h7uhjtnt9cjh8o@group.calendar.google.com";

        private static CalendarService GetCalendarService()
        {
            UserCredential credential;

            using (var stream = new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal);

                credPath = Path.Combine(credPath, ".credentials/sagalingua1214");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    User,  // "user",  
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;

            }

            // Create Google Calendar API service.
            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public static List<CalEvent> ReadEvents(DateTime dtMin, DateTime dtMax)
        {
            CalendarService service = GetCalendarService();

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List(CalendarID);
            request.TimeMin = dtMin;
            request.TimeMax = dtMax;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
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
                            eventItem.Status));
                }
            }
            return calEvents;
        }

        public static bool DeleteAllEvents(DateTime dtMin, DateTime dtMax)
        {
            CalendarService service = GetCalendarService();

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List(CalendarID);
            request.TimeMin = dtMin;
            request.TimeMax = dtMax;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 1000;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();
            List<string> results = new List<string>();
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    EventsResource.DeleteRequest del = service.Events.Delete(
                        CalendarID, eventItem.Id);

                    string res = del.Execute();
                    results.Add(res);
                }
            }
            return true;
        }
        public static bool WriteCalendarEvents(List<CalEvent> events)
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

                EventsResource.InsertRequest req = service.Events.Insert(evt, CalendarID);
                Event result = req.Execute();
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
            return true;
        }
    }
}
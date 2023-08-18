namespace DAL.Entities
{
    public class LogEntry
    {


        public int Id { get; set; }

        public int LogLevel { get; set; }

        public string Message { get; set; }


        public int ?ClientId { get; set; }        
        public int? Action { get; set; }  
            public DateTime Timestamp { get; set; }
    }
}

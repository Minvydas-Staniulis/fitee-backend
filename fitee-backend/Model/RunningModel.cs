namespace fitee_backend.Model
{
    public class RunningModel
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public float distance { get; set; } 

        public TimeSpan running_time { get; set; }
    }
}

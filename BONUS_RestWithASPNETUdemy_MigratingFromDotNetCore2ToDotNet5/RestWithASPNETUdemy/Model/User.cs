using System.Runtime.Serialization;

namespace RestWithASPNETUdemy.Model
{
    public class User
    {

        public long? Id { get; set; }
        public string Login { get; set; }
        public string AccessKey { get; set; }
    }
}

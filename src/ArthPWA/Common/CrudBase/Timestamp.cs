using System;

namespace ArthPWA.Common.CrudBase
{
    public sealed class Userstamp
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }

    public sealed class Timestamp
    {
        public DateTime On { get; set; }
        public Userstamp By { get; set; }
    }

    public interface ITimeMarkable
    {
        Timestamp Created { get; set; }
        Timestamp LastModified { get; set; }
    }
}
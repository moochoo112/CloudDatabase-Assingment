
using Newtonsoft.Json;

namespace Domain
{
    public class User
    {
        public int UserID { get; set; }
        [JsonRequired]
        public string Firstname { get; set; }
        [JsonRequired]
        public string Lastname { get; set; }
        [JsonRequired]
        public string Email { get; set; }
        [JsonRequired]
        public double YearlyIncome { get; set; }
        public double MortageOffer { get; set; }

    }
}

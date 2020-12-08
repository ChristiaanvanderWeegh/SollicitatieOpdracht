using System.ComponentModel.DataAnnotations;

namespace OAuth2Api.Models
{
    public class AuthorizeRequestModel
    {
        [Required]
        public string username { get; set; }
        public string code { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string redirect_uri { get; set; }
        public string scope { get; set; }
        public string state { get; set; }
    }
}

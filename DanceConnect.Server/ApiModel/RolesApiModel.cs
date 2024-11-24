using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanceConnect.Server.ApiModel
{
    public class RoleApiModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<ClaimApiModel> Claims { get; set; }
    }
    public class ClaimApiModel {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}

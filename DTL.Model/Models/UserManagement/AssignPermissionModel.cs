using System;

namespace DTL.Model.Models.UserManagement
{
    public class AssignPermissionModel : BaseModel
    {
        public int AssignPermissionId { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public bool Create { get; set; }
        public bool View { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool IsDeleted { get; set; }
        public string UserId { get; set; }
    }
}

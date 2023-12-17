﻿using System.Collections.Generic;

namespace Elearning.Shared.DTOs.IdentityUserDTOs
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<CheckBoxViewModel> Roles { get; set; }
    }
}
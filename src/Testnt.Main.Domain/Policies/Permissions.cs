using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Testnt.Main.Domain.Policies
{
    public enum Permissions : short
    {
        [Display(GroupName = "Project", Name = "Read", Description = "Can read project")]
        ProjectRead = 0x10,
        [Display(GroupName = "Project", Name = "Create", Description = "Can create a Project entry")]
        ProjectCreate = 0x11,
        [Display(GroupName = "Project", Name = "Update", Description = "Can update a Project")]
        ProjectUpdate = 0x12,
        [Display(GroupName = "Project", Name = "Delete", Description = "Can delete a Project")]
        ProjectDelete = 0x13,

        [Display(GroupName = "Project", Name = "Alter project", Description = "Can do anything on a project")]
        ProjectAdmin = 0x14,


        [Display(GroupName = "Feature", Name = "Read", Description = "Can read Feature")]
        FeatureRead = 0x20,
        [Display(GroupName = "Feature", Name = "Create", Description = "Can create a Feature entry")]
        FeatureCreate = 0x21,
        [Display(GroupName = "Feature", Name = "Update", Description = "Can update a Feature")]
        FeatureUpdate = 0x22,
        [Display(GroupName = "Feature", Name = "Delete", Description = "Can delete a Feature")]
        FeatureDelete = 0x23,

        [Display(GroupName = "Feature", Name = "Alter Feature", Description = "Can do anything on a Feature")]
        FeatureAdmin = 0x24,
    }
}

using System.ComponentModel.DataAnnotations;

namespace InsonusK.ValidationAttributes.ValidateObject.Test.TestClasses
{
    public class MainClass_Required2
    {
        [Required, ValidateObject]
        public SubClass SubClass { get; set; }
    }
}
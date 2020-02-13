using System.ComponentModel.DataAnnotations;

namespace InsonusK.ValidationAttributes.ValidateObject.Test.TestClasses
{
    public class MainClass_Required
    {
        [ValidateObject, Required]
        public SubClass SubClass { get; set; }
    }
}
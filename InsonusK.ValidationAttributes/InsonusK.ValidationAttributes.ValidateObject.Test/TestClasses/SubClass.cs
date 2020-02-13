using System.ComponentModel.DataAnnotations;

namespace InsonusK.ValidationAttributes.ValidateObject.Test.TestClasses
{
    public class SubClass
    {
        [Required, MinLength(2)]
        public string StringValue { get; set; }
        
        [Required, MinLength(2)]
        public string StringValue2 { get; set; }
    }
}
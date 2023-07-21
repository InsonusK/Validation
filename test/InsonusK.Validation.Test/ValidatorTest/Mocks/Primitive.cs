using System.ComponentModel.DataAnnotations;

namespace InsonusK.Validation.Test.ValidatorTest.Mocks;
public class IntPrimitive : IValidatableObject
{
  private int _value;

  public IntPrimitive(int value)
  {
    _value = value;
  }

  public int Value
  {
    get { return _value; }
    set { _value = value; }
  }

  public bool IsValid
  {
    get { return _value >= 0; } // Example validation logic
  }
  
  public static implicit operator IntPrimitive(int s)
  {
    return new IntPrimitive(s);
  }

  public static implicit operator int(IntPrimitive p)
  {
    return p.Value;
  }
  public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    var res = new List<ValidationResult>();
    if (Value > 3)
      res.Add(new ValidationResult(">3"));
    if (Value > 4)
      res.Add(new ValidationResult(">4"));
    return res;
  }
}
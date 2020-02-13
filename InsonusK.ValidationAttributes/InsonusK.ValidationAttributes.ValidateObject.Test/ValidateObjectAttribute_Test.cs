using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using InsonusK.ValidationAttributes.ValidateObject.Test.TestClasses;
using NUnit.Framework;

namespace InsonusK.ValidationAttributes.ValidateObject.Test
{
    public class ValidateObjectAttribute_Test
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// If set [ValidateObject(true)] and set value to null
        /// Validation will be success
        /// No matter validate AllProperties or RequiredProperties
        /// </summary>
        [Test]
        public void Okey_If_SubClassNull_And_Optional()
        {
            // Array
            MainClass_Optional _mainClassOptional = new MainClass_Optional();
            var _results = new List<ValidationResult>();
            var _context = new ValidationContext(_mainClassOptional);

            // Act
            var _resultAsserted = Validator.TryValidateObject(_mainClassOptional, _context, _results, true);

            // Assert
            Assert.IsTrue(_resultAsserted);
            Assert.IsTrue(_results.Count == 0);
        }

        /// <summary>
        /// If set [ValidateObject(false)] and set value to null
        /// Validation will be false and return errors
        /// </summary>
        [Test]
        public void Error_If_SubClassNull_And_Required()
        {
            // Array
            MainClass_Required _mainClassRequired = new MainClass_Required();
            var _results = new List<ValidationResult>();
            var _context = new ValidationContext(_mainClassRequired);

            // Act
            var _resultAsserted = Validator.TryValidateObject(_mainClassRequired, _context, _results, true);

            // Assert
            Assert.IsFalse(_resultAsserted);
            Assert.IsTrue(_results.Count > 0);

            foreach (ValidationResult _validationResult in _results)
            {
                Console.WriteLine($"{_validationResult.MemberNames}: {_validationResult.ErrorMessage}");
            }
        }

        /// <summary>
        /// If set [ValidateObject(false)] and set value to null
        /// Validation will be false and return errors
        /// </summary>
        [Test]
        public void Error_If_SubClassNull_And_Required2()
        {
            // Array
            MainClass_Required2 _mainClassRequired = new MainClass_Required2();
            var _results = new List<ValidationResult>();
            var _context = new ValidationContext(_mainClassRequired);

            // Act
            var _resultAsserted = Validator.TryValidateObject(_mainClassRequired, _context, _results, true);

            // Assert
            Assert.IsFalse(_resultAsserted);
            Assert.IsTrue(_results.Count > 0);

            foreach (ValidationResult _validationResult in _results)
            {
                Console.WriteLine($"{_validationResult.MemberNames}: {_validationResult.ErrorMessage}");
            }
        }

        /// <summary>
        /// Validation success
        /// </summary>
        [Test]
        public void Okey_SubClassValues_Ok_And_Optional()
        {
            // Array
            MainClass_Optional _mainClassOptional = new MainClass_Optional
            {
                SubClass = new SubClass {StringValue = "qwe", StringValue2 = "asd"}
            };

            var _results = new List<ValidationResult>();
            var _context = new ValidationContext(_mainClassOptional);

            // Act
            var _resultAsserted = Validator.TryValidateObject(_mainClassOptional, _context, _results, true);

            // Assert
            Assert.IsTrue(_resultAsserted);
            Assert.IsTrue(_results.Count == 0);
        }
        /// <summary>
        /// Validation success
        /// </summary>
        [Test]
        public void Okey_SubClassValues_Ok_And_Required()
        {
            // Array
            MainClass_Required _mainClassRequired = new MainClass_Required
            {
                SubClass = new SubClass {StringValue = "qwe", StringValue2 = "asd"}
            };

            var _results = new List<ValidationResult>();
            var _context = new ValidationContext(_mainClassRequired);

            // Act
            var _resultAsserted = Validator.TryValidateObject(_mainClassRequired, _context, _results, true);

            // Assert
            Assert.IsTrue(_resultAsserted);
            Assert.IsTrue(_results.Count == 0);
        }
        /// <summary>
        /// Validation success
        /// </summary>
        [Test]
        public void Okey_SubClassValues_Ok_And_Required2()
        {
            // Array
            MainClass_Required2 _mainClassRequired = new MainClass_Required2
            {
                SubClass = new SubClass {StringValue = "qwe", StringValue2 = "asd"}
            };

            var _results = new List<ValidationResult>();
            var _context = new ValidationContext(_mainClassRequired);

            // Act
            var _resultAsserted = Validator.TryValidateObject(_mainClassRequired, _context, _results, true);

            // Assert
            Assert.IsTrue(_resultAsserted);
            Assert.IsTrue(_results.Count == 0);
        }
        /// <summary>
        /// One of the values has mistake
        /// </summary>
        [Test]
        public void Error_SubClassValues_OneMistake_And_Optional()
        {
            // Array
            MainClass_Optional _mainClassOptional = new MainClass_Optional
            {
                SubClass = new SubClass {StringValue = "qwe", StringValue2 = "a"}
            };

            var _results = new List<ValidationResult>();
            var _context = new ValidationContext(_mainClassOptional);

            // Act
            var _resultAsserted = Validator.TryValidateObject(_mainClassOptional, _context, _results, true);

            // Assert
            WriteValidationResult(_results);
            Assert.IsFalse(_resultAsserted);
            Assert.AreEqual(1, _results.Count);
        }
        /// <summary>
        /// One of the values has mistake
        /// </summary>
        [Test]
        public void Error_SubClassValues_OneMistake_And_Required()
        {
            // Array
            MainClass_Required _mainClassRequired = new MainClass_Required
            {
                SubClass = new SubClass {StringValue = "qwe", StringValue2 = "a"}
            };

            var _results = new List<ValidationResult>();
            var _context = new ValidationContext(_mainClassRequired);

            // Act
            var _resultAsserted = Validator.TryValidateObject(_mainClassRequired, _context, _results, true);

            // Assert
            WriteValidationResult(_results);
            Assert.IsFalse(_resultAsserted);
            Assert.AreEqual(1, _results.Count);
        }
        /// <summary>
        /// One of the values has mistake
        /// </summary>
        [Test]
        public void Error_SubClassValues_OneMistake_And_Required2()
        {
            // Array
            MainClass_Required2 _mainClassRequired = new MainClass_Required2
            {
                SubClass = new SubClass {StringValue = "qwe", StringValue2 = "a"}
            };

            var _results = new List<ValidationResult>();
            var _context = new ValidationContext(_mainClassRequired);

            // Act
            var _resultAsserted = Validator.TryValidateObject(_mainClassRequired, _context, _results, true);

            // Assert
            WriteValidationResult(_results);
            Assert.IsFalse(_resultAsserted);
            Assert.AreEqual(1, _results.Count);
        }
        /// <summary>
        /// Both of the values have mistake
        /// </summary>
        [Test]
        public void Error_SubClassValues_BothMistake_And_Optional()
        {
            // Array
            MainClass_Optional _mainClassOptional = new MainClass_Optional
            {
                SubClass = new SubClass {StringValue = "q", StringValue2 = "a"}
            };

            var _results = new List<ValidationResult>();
            var _context = new ValidationContext(_mainClassOptional);

            // Act
            var _resultAsserted = Validator.TryValidateObject(_mainClassOptional, _context, _results, true);

            // Assert
            WriteValidationResult(_results);
            Assert.IsFalse(_resultAsserted);
            Assert.AreEqual(1, _results.Count);
            Assert.AreEqual(2, (_results[0] as CompositeValidationResult).Results.Count());
        }
        /// <summary>
        /// Both of the values have mistake
        /// </summary>
        [Test]
        public void Error_SubClassValues_BothMistake_And_Required()
        {
            // Array
            MainClass_Required _mainClassRequired = new MainClass_Required
            {
                SubClass = new SubClass {StringValue = "q", StringValue2 = "a"}
            };

            var _results = new List<ValidationResult>();
            var _context = new ValidationContext(_mainClassRequired);

            // Act
            var _resultAsserted = Validator.TryValidateObject(_mainClassRequired, _context, _results, true);

            // Assert
            WriteValidationResult(_results);
            Assert.IsFalse(_resultAsserted);
            Assert.AreEqual(1, _results.Count);
            Assert.AreEqual(2, (_results[0] as CompositeValidationResult).Results.Count());
        }
        /// <summary>
        /// Both of the values have mistake
        /// </summary>
        [Test]
        public void Error_SubClassValues_BothMistake_And_Required2()
        {
            // Array
            MainClass_Required2 _mainClassRequired = new MainClass_Required2
            {
                SubClass = new SubClass {StringValue = "q", StringValue2 = "a"}
            };

            var _results = new List<ValidationResult>();
            var _context = new ValidationContext(_mainClassRequired);

            // Act
            var _resultAsserted = Validator.TryValidateObject(_mainClassRequired, _context, _results, true);

            // Assert
            WriteValidationResult(_results);
            Assert.IsFalse(_resultAsserted);
            Assert.AreEqual(1, _results.Count);
            Assert.AreEqual(2, (_results[0] as CompositeValidationResult).Results.Count());
        }

        private void WriteValidationResult(List<ValidationResult> results)
        {
            foreach (ValidationResult _validationResult in results)
            {
                Console.WriteLine($"{_validationResult.MemberNames}: {_validationResult.ErrorMessage}");
                if (_validationResult is CompositeValidationResult _compositeValidationResult)
                {
                    foreach (ValidationResult _subResult in _compositeValidationResult.Results)
                    {
                        Console.WriteLine($"--{_subResult.MemberNames}: {_subResult.ErrorMessage}");
                    }
                }
            }
        }
    }
}
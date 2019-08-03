using FileStorage.DbModels;
using FileStorage.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace FileStorage.Tests.UnitTests.Models
{
    public class FileInfoDtoTests
    {
        [Fact]
        public void IndexProperty_IsRequired()
        {
            var dto = GetValidFileInfoDto();
            dto.Index = null;

            (var isValid, var validationResults) = Validate(dto);

            Assert.False(isValid);
            Assert.Equal(nameof(dto.Index), validationResults.GetSingleMemberName());
        }
        [Fact]
        public void IndexProperty_IfMaxLengthExceeded_ValidationFails()
        {
            var dto = GetValidFileInfoDto();
            dto.Index = GenerateStringOfLengthGreaterThen(FileInfo.MaxIndexLength);

            (var isValid, var validationResults) = Validate(dto);

            Assert.True(dto.Index.Length > FileInfo.MaxIndexLength);
            Assert.False(isValid);
            Assert.Equal(nameof(dto.Index), validationResults.GetSingleMemberName());

        }
        [Theory]
        [InlineData("0")]
        [InlineData("1.0")]
        [InlineData("1.a")]
        [InlineData("1.!")]
        [InlineData("1.1.1!1.1")]
        [InlineData("1.1.11.1.")]
        [InlineData("1.1.11..1")]
        public void IndexProperty_IfInvalidFormat_ValidationFails(string index)
        {
            var dto = GetValidFileInfoDto();
            dto.Index = index;

            (var isValid, _) = Validate(dto);

            Assert.False(isValid);
        }
        [Theory]
        [InlineData("1")]
        [InlineData("1.1")]
        [InlineData("1.1.11.1")]
        public void IndexProperty_IfValidFormat_ValidationPasses(string index)
        {
            var dto = GetValidFileInfoDto();
            dto.Index = index;

            (var isValid, _) = Validate(dto);

            Assert.True(isValid);
        }

        [Fact]
        public void NameProperty_IsRequired()
        {
            var dto = GetValidFileInfoDto();
            dto.Name = null;

            (var isValid, var validationResults) = Validate(dto);

            Assert.False(isValid);
            Assert.Equal(nameof(dto.Name), validationResults.GetSingleMemberName());
        }
        [Fact]
        public void NameProperty_IfMaxLenghtExceeded_ValidationFails()
        {
            var dto = GetValidFileInfoDto();
            dto.Name = GenerateStringOfLengthGreaterThen(FileInfo.MaxNameLength);

            (var isValid, var validationResults) = Validate(dto);

            Assert.True(dto.Name.Length > FileInfo.MaxNameLength);
            Assert.False(isValid);
            Assert.Equal(nameof(dto.Name), validationResults.GetSingleMemberName());

        }

        [Fact]
        public void CategoryProperty_IsRequired()
        {
            var dto = GetValidFileInfoDto();
            dto.Category = null;

            (var isValid, var validationResults) = Validate(dto);

            Assert.False(isValid);
            Assert.Equal(nameof(dto.Category), validationResults.GetSingleMemberName());
        }
        [Fact]
        public void CategoryProperty_IfMaxLengthExceeded_ValidationFails()
        {
            var dto = GetValidFileInfoDto();
            dto.Category = GenerateStringOfLengthGreaterThen(FileInfo.MaxCategoryNameLength);

            (var isValid, var validationResults) = Validate(dto);

            Assert.True(dto.Category.Length > FileInfo.MaxCategoryNameLength);
            Assert.False(isValid);
            Assert.Equal(nameof(dto.Category), validationResults.GetSingleMemberName());

        }


        private FileInfoDto GetValidFileInfoDto() =>
            new FileInfoDto
            {
                Index = "1.1",
                Name = "Name",
                Size = 5,
                Category = "Category"
            };
        private static string GenerateStringOfLengthGreaterThen(int maxLength) => 
            string.Join(string.Empty, Enumerable.Repeat("1", maxLength + 1));
        private (bool isValid, List<ValidationResult> validationResults) Validate(object obj)
        {
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(
                instance: obj,
                validationContext: new ValidationContext(obj),
                validationResults: validationResults,
                validateAllProperties: true);

            return (isValid, validationResults);
        }
    }

    internal static class ValidationResultExtensions
    {
        public static string GetSingleMemberName(this IEnumerable<ValidationResult> results) =>
            results.Single().MemberNames.Single();
    }
}

using FileStorage.DbModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileStorage.Models
{
    public class FileInfoDto: IValidatableObject
    {
        [Required]
        [StringLength(FileInfo.MaxIndexLength)]
        public string Index { get; set; }

        [Required]
        [StringLength(FileInfo.MaxNameLength)]
        public string Name { get; set; }
        
        public long Size { get; set; }

        [Required]
        [StringLength(FileInfo.MaxCategoryNameLength)]
        public string Category { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!FileInfoValidator.OnlyDigitsAndDotsIn(Index))
                yield return new ValidationResult("Only dots and digits can be present in index", 
                    new[] { nameof(Index) });

            if(!FileInfoValidator.PatterIsValidIn(Index))
                yield return new ValidationResult("Index pattern is invalid", new[] { nameof(Index) });

            if (!FileInfoValidator.LastCharacterIsDigitIn(Index))
                yield return new ValidationResult("Last character must be digit", new[] { nameof(Index) });

            if (Index.Split('.').Any(e => e == "0"))
                yield return new ValidationResult("Stand along 0 digits are not allowed", new[] { nameof(Index) });
        }
    }

    internal static class FileInfoValidator
    {
        private static Regex _lastCharacterIsNumberRegex = new Regex(@"\d$", RegexOptions.Compiled);
        internal static bool LastCharacterIsDigitIn(string str) => _lastCharacterIsNumberRegex.IsMatch(str);

        private static Regex _onlyNumbersAndDotsRegex = new Regex(@"[^\d.]", RegexOptions.Compiled);
        internal static bool OnlyDigitsAndDotsIn(string str) => !(_onlyNumbersAndDotsRegex.IsMatch(str));

        private static Regex _indexPatterIsValidRegex = new Regex(@"^([\d]+[.]?)+", RegexOptions.Compiled);
        internal static bool PatterIsValidIn(string str) => 
            _indexPatterIsValidRegex.Match(str).Length == str.Trim().Length;
    }
}

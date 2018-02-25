using System.Globalization;
using System.Text;

namespace Conjugate.Conjugation {
    class EncodingUtilities {
        public static Encoding ISO_8859_1 = Encoding.GetEncoding("iso-8859-1");

        /* RemoveDiacritics,
         * Sourced from: https://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net
         */
        public static string RemoveDiacritics(string text) {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString) {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark) {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}

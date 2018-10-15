using System;
using System.Collections.ObjectModel;

namespace Galatea.Speech
{
    internal static class Phonemes
    {
        public static PhonemeCollection GetPhonemesSapi4()
        {
            PhonemeCollection result = new PhonemeCollection("SAPI 4")
            {
                new Phoneme {Id = 101, PhonemeType = PhonemeType.Vowel, Description = "A", MouthPosition = MouthPosition.Open3},
                //new Phoneme {Id = 105, PhonemeType = PhonemeType.Vowel, Description = "E", MouthPosition = MouthPosition.Open3},
                new Phoneme {Id = 97, PhonemeType = PhonemeType.Vowel, Description = "E", MouthPosition = MouthPosition.Open4},
                new Phoneme {Id = 111, PhonemeType = PhonemeType.Vowel, Description = "O", MouthPosition = MouthPosition.LittleOoh},
                new Phoneme {Id = 117, PhonemeType = PhonemeType.Vowel, Description = "U", MouthPosition = MouthPosition.LittleOoh},
                new Phoneme {Id = 106, PhonemeType = PhonemeType.Vowel, Description = "U = E, O", MouthPosition = MouthPosition.Open3},
                new Phoneme {Id = 603, PhonemeType = PhonemeType.Vowel, Description = "eh (F, M)", MouthPosition = MouthPosition.Open3},
                new Phoneme {Id = 594, PhonemeType = PhonemeType.Vowel, Description = "uh (the)", MouthPosition = MouthPosition.Open4},
                new Phoneme {Id = 601, PhonemeType = PhonemeType.Vowel, Description = "uh (the)", MouthPosition = MouthPosition.Open4},
                new Phoneme {Id = 230, PhonemeType = PhonemeType.Vowel, Description = "ah (that)", MouthPosition = MouthPosition.Open4},
                new Phoneme {Id = 618, PhonemeType = PhonemeType.Vowel, Description = "ih (this)", MouthPosition = MouthPosition.Open4},
                new Phoneme {Id = 596, PhonemeType = PhonemeType.Vowel, Description = "ih (this)", MouthPosition = MouthPosition.Open3},
                new Phoneme {Id = 609, PhonemeType = PhonemeType.Vowel, Description = "aw (dog)", MouthPosition = MouthPosition.LittleOoh},
                new Phoneme {Id = 650, PhonemeType = PhonemeType.Vowel, Description = "oo (wood)", MouthPosition = MouthPosition.LittleOoh},
                new Phoneme {Id = 98, PhonemeType = PhonemeType.Consonant, Description = "B", MouthPosition = MouthPosition.Open1},
                new Phoneme {Id = 115, PhonemeType = PhonemeType.Consonant, Description = "C, S", MouthPosition = MouthPosition.Open1},
                new Phoneme {Id = 105, PhonemeType = PhonemeType.Consonant, Description = "D", MouthPosition = MouthPosition.Open2},
                new Phoneme {Id = 102, PhonemeType = PhonemeType.Consonant, Description = "F", MouthPosition = MouthPosition.Open1},
                new Phoneme {Id = 100, PhonemeType = PhonemeType.Consonant, Description = "G", MouthPosition = MouthPosition.Open1},
                new Phoneme {Id = 865, PhonemeType = PhonemeType.Consonant, Description = "G, H, J", MouthPosition = MouthPosition.Open2},
                new Phoneme {Id = 107, PhonemeType = PhonemeType.Consonant, Description = "K", MouthPosition = MouthPosition.Open2},
                new Phoneme {Id = 108, PhonemeType = PhonemeType.Consonant, Description = "L", MouthPosition = MouthPosition.Open2},
                new Phoneme {Id = 109, PhonemeType = PhonemeType.Consonant, Description = "M", MouthPosition = MouthPosition.Open1},
                new Phoneme {Id = 110, PhonemeType = PhonemeType.Consonant, Description = "N", MouthPosition = MouthPosition.Open2},
                new Phoneme {Id = 112, PhonemeType = PhonemeType.Consonant, Description = "P", MouthPosition = MouthPosition.Open1},
                new Phoneme {Id = 635, PhonemeType = PhonemeType.Consonant, Description = "R", MouthPosition = MouthPosition.Open3},
                new Phoneme {Id = 593, PhonemeType = PhonemeType.Consonant, Description = "R", MouthPosition = MouthPosition.Open4},
                new Phoneme {Id = 652, PhonemeType = PhonemeType.Consonant, Description = "R", MouthPosition = MouthPosition.Open4},
                new Phoneme {Id = 116, PhonemeType = PhonemeType.Consonant, Description = "T", MouthPosition = MouthPosition.Open2},
                new Phoneme {Id = 118, PhonemeType = PhonemeType.Consonant, Description = "V", MouthPosition = MouthPosition.Open1},
                new Phoneme {Id = 119, PhonemeType = PhonemeType.Consonant, Description = "W", MouthPosition = MouthPosition.Open3},
                new Phoneme {Id = 122, PhonemeType = PhonemeType.Consonant, Description = "Z", MouthPosition = MouthPosition.Open4},
                new Phoneme {Id = 240, PhonemeType = PhonemeType.Consonant, Description = "The", MouthPosition = MouthPosition.Open1},
                new Phoneme {Id = 952, PhonemeType = PhonemeType.Consonant, Description = "The", MouthPosition = MouthPosition.Open2},
                new Phoneme {Id = 643, PhonemeType = PhonemeType.Consonant, Description = "She", MouthPosition = MouthPosition.Open3},
                new Phoneme {Id = 104, PhonemeType = PhonemeType.Consonant, Description = "He", MouthPosition = MouthPosition.Open2},
                new Phoneme {Id = 331, PhonemeType = PhonemeType.Consonant, Description = "ng (long)", MouthPosition = MouthPosition.Open3},
            };

            return result;
        }

        public static PhonemeCollection GetPhonemesSapi5()
        {
            PhonemeCollection result = new PhonemeCollection("SAPI 5")
            {
                new Phoneme {Id = 15, Description = "List 1", MouthPosition = MouthPosition.Open1},
                new Phoneme {Id = 17, Description = "List 1", MouthPosition = MouthPosition.Open1},
                new Phoneme {Id = 18, Description = "List 1", MouthPosition = MouthPosition.Open1},
                new Phoneme {Id = 21, Description = "List 1", MouthPosition = MouthPosition.Open1},
                new Phoneme {Id = 14, Description = "List 2", MouthPosition = MouthPosition.Open2},
                new Phoneme {Id = 16, Description = "List 2", MouthPosition = MouthPosition.Open2},
                new Phoneme {Id = 19, Description = "List 2", MouthPosition = MouthPosition.Open2},
                new Phoneme {Id = 20, Description = "List 2", MouthPosition = MouthPosition.Open2},
                new Phoneme {Id = 4, Description = "List 3", MouthPosition = MouthPosition.Open3},
                new Phoneme {Id = 6, Description = "List 3", MouthPosition = MouthPosition.Open3},
                new Phoneme {Id = 9, Description = "List 3", MouthPosition = MouthPosition.Open3},
                new Phoneme {Id = 12, Description = "List 3", MouthPosition = MouthPosition.Open3},
                new Phoneme {Id = 1, Description = "List 4", MouthPosition = MouthPosition.Open4},
                new Phoneme {Id = 2, Description = "List 4", MouthPosition = MouthPosition.Open4},
                new Phoneme {Id = 3, Description = "List 4", MouthPosition = MouthPosition.Open4},
                new Phoneme {Id = 11, Description = "List 4", MouthPosition = MouthPosition.Open4},
                new Phoneme {Id = 7, Description = "List 5", MouthPosition = MouthPosition.LittleOoh},
                new Phoneme {Id = 8, Description = "List 5", MouthPosition = MouthPosition.LittleOoh},
                new Phoneme {Id = 5, Description = "List 6", MouthPosition = MouthPosition.BigSmile},
                new Phoneme {Id = 10, Description = "List 6", MouthPosition = MouthPosition.BigSmile},
                new Phoneme {Id = 13, Description = "List 6", MouthPosition = MouthPosition.BigSmile},
            };

            return result;
        }
    }
}
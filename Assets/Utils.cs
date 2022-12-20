using System.Text;

namespace Assets
{
    internal class Utils
    {
        /// <summary>
        /// Remplace les lettres avec des accents par des lettres normales
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string RemoveSpecialChars(string word)
        {
            StringBuilder stringBuilder = new StringBuilder(word);
            for (int i = 0; i < word.Length; i++)
            {
                if (stringBuilder[i] == 'è' || stringBuilder[i] == 'ê' || stringBuilder[i] == 'é')
                {
                    stringBuilder[i] = 'e';
                }
                else if (stringBuilder[i] == 'à' || stringBuilder[i] == 'â')
                {
                    stringBuilder[i] = 'a';
                }
                else if (stringBuilder[i] == 'ç')
                {
                    stringBuilder[i] = 'c';
                }
                else if (stringBuilder[i] == 'ô')
                {
                    stringBuilder[i] = 'o';
                }
                else if (stringBuilder[i] == 'ï' || stringBuilder[i] == 'î')
                {
                    stringBuilder[i] = 'i';
                }
                else if (stringBuilder[i] == 'ù' || stringBuilder[i] == 'û')
                {
                    stringBuilder[i] = 'u';
                }
            }
            return stringBuilder.ToString(); ;
        }
    }
}
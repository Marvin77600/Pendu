using System.Collections.Generic;

namespace Pendu
{
    public class WrittenCharacters
    {
        /// <summary>
        /// Ajoute un caractère dans la liste des caractères s'il n'existe pas déjà
        /// </summary>
        /// <param name="c"></param>
        public bool AddChar(char c)
        {
            bool flag = false;
            if (Chars.Contains(c)) flag = false;
            if (!Chars.Contains(c))
            {
                Chars.Add(c);
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Retourne les lettres non valides trouvés par joueur
        /// </summary>
        public string GetCharacters
        {
            get
            {
                string str = string.Empty;
                foreach (var c in Chars)
                {
                    str += $"[{c}] ";
                }
                return str;
            }
        }

        private List<char> Chars = new List<char>();
    }
}
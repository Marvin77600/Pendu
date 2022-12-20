namespace Assets
{
    using System.Text;

    namespace Pendu
    {
        public class Word
        {
            public Word(string name)
            {
                Name = name;
            }

            /// <summary>
            /// Remplace tous les caractères du mot par des underscores
            /// </summary>
            public string HiddenWord
            {
                get
                {
                    StringBuilder stringBuilder = new StringBuilder(Name);
                    for (int i = 0; i < Name.Length; i++)
                    {
                        stringBuilder[i] = '_';
                    }
                    return stringBuilder.ToString();
                }
            }

            /// <summary>
            /// Renvoi une mise à jour du mot à chercher
            /// </summary>
            /// <param name="name"></param>
            /// <param name="inputChar"></param>
            /// <returns></returns>
            public string UpdateDisplayedWord(string name, char inputChar)
            {
                StringBuilder stringBuilder = new StringBuilder(name);
                for (int i = 0; i < Name.Length; i++)
                {
                    var c = Name[i];

                    if (c == inputChar)
                    {
                        stringBuilder[i] = inputChar;
                    }
                }
                return stringBuilder.ToString();
            }

            public string Name { get; set; }
        }
    }
}
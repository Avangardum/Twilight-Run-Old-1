using System.Collections.Generic;

namespace TwilightRun
{
    public class TagManager
    {
        public enum Tag
        {
            PlayerLight,
            PlayerDark
        }

        private static Dictionary<Tag, string> _tagNames;

        static TagManager()
        {
            _tagNames = new Dictionary<Tag, string>();
            _tagNames.Add(Tag.PlayerLight, "Player Light");
            _tagNames.Add(Tag.PlayerDark, "Player Dark");
        }

        public static string GetTagName(Tag tag) => _tagNames[tag];
    }

}
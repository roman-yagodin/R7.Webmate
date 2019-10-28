﻿namespace R7.Webmate.Core.Text.Commands
{
	public class ReplaceCommand: TextCommandBase
	{
        // TODO: Add mirror property "Text"
        public string Pattern { get; set; }

        // TODO: Rename to "With"
        public string Replacement { get; set; }

        public ReplaceCommand ()
        {}

        public ReplaceCommand (string pattern, string replacement)
		{
			Pattern = pattern;
			Replacement = replacement;
		}

		public override string Execute (string value)
		{
			return value.Replace (Pattern, Replacement);
		}
	}

	public class TrimCommand: TextCommandBase
	{
        public TrimCommand ()
        {}

        public string TrimChars { get; set; }

        public TrimCommand (string trimChars = null)
		{
            if (!string.IsNullOrEmpty (trimChars)) {
                TrimChars = trimChars;
            }
		}

		public override string Execute (string value)
		{
			if (!string.IsNullOrEmpty (TrimChars)) {
                return value.Trim (TrimChars.ToCharArray ());
            }
    		return value.Trim ();
		}
	}

	public class AppendCommand: TextCommandBase
    {
        public string Text { get; set; }

        public AppendCommand ()
        {}

		public AppendCommand (string text)
		{
			Text = text;
		}

		public override string Execute (string value)
		{
			return value + Text;
		}
	}

	public class PrependCommand: TextCommandBase
    {
        public string Text { get; set; }

        public PrependCommand ()
        {}

		public PrependCommand (string text)
		{
			Text = text;
		}

		public override string Execute (string value)
		{
		    return Text + value;
		}
	}
}


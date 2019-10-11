﻿using System.Collections.Generic;
using R7.Webmate.Core.Text.Commands;

namespace R7.Webmate.Core.Text.Processings
{
    public abstract class TextProcessingBase: ITextProcessing
    {
        public ITextProcessingParams Params { get; set; }

        public IList<ITextCommand> Commands { get; set; } = new List<ITextCommand> ();

        public void AddCommands (params ITextCommand [] commands)
        {
            foreach (var command in commands) {
                Commands.Add (command);
            }
        }

        public virtual string Execute (string text)
        {
            return Execute (text, null);
        }

        public virtual string Execute (string text, ITextProcessingParams textProcessingParams)
		{
			Params = textProcessingParams;

            foreach (var command in Commands) {
                text = command.Execute (text);
            }

            return text;
		}
	}
}


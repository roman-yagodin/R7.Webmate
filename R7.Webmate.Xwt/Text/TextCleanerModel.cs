﻿using NGettext;
using R7.Webmate.Core.Text;
using R7.Webmate.Core.Text.Processings;

namespace R7.Webmate.Xwt.Text
{
    public class TextCleanerModel: TextCleanerModelBase
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        public ITextProcessing TextToAsciiProcessing = TextProcessingLoader.Load ("text-to-ascii.yml");

        public ITextProcessing TextToTextProcessing = TextProcessingLoader.Load ("text-to-text.yml");

        public ITextProcessing TextToHtmlProcessing = TextProcessingLoader.Load ("text-to-html.yml");

        public HtmlToHtmlProcessing HtmlToHtmlProcessing;

        public TextCleanerModel ()
        {
            HtmlToHtmlProcessing = new HtmlToHtmlProcessing ();
            HtmlToHtmlProcessing.TextToTextProcessing = TextToTextProcessing;
        }

        public override void Process ()
        {
            Results.Clear ();

            if (!string.IsNullOrEmpty (Source)) {
                if (HtmlHelper.IsHtml (Source)) {
                    Results.Add (new TextCleanerResult {
                        Text = HtmlToHtmlProcessing.Process (Source),
                        Label = T.GetString ("HTML"),
                        Format = TextCleanerResultFormat.HTML
                    });
                }
                else {
                    var unicodeText = new TextCleanerResult {
                        Text = TextToTextProcessing.Process (Source),
                        Label = T.GetString ("Text"),
                        Format = TextCleanerResultFormat.Text
                    };

                    Results.Add (unicodeText);

                    Results.Add (new TextCleanerResult {
                        Text = TextToAsciiProcessing.Process (unicodeText.Text),
                        Label = T.GetString ("ASCII text"),
                        Format = TextCleanerResultFormat.Text
                    });

                    Results.Add (new TextCleanerResult {
                        Text = TextToHtmlProcessing.Process (unicodeText.Text),
                        Label = T.GetString ("HTML"),
                        Format = TextCleanerResultFormat.HTML
                    });
                }
            }
        }
    }
}
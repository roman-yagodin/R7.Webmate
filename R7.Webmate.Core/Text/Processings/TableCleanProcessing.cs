namespace R7.Webmate.Core.Text.Processings
{
    public class TableCleanProcessing: TextProcessingBase
    {
        public ITextProcessing TableCleanTextProcessing { get; set; }

        public HtmlToHtmlProcessing HtmlToHtmlProcessing { get; set; }

        public override string Execute (string text)
        {
            return TableCleanTextProcessing.Execute (HtmlToHtmlProcessing.Execute (text));

            /*
            if (tableCleanerParams.SetCssClass)
                text = text.Replace ("<table", string.Format (
                    "<table class=\"{0}\"", tableCleanerParams.TableCssClass));

            if (tableCleanerParams.SetWidth)
                text = text.Replace ("<table", string.Format (
                    "<table width=\"{0}{1}\"", tableCleanerParams.TableWidth, tableCleanerParams.TableWidthUnits));
            */
        }
    }
}

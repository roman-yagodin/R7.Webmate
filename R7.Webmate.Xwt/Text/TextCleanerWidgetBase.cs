using NGettext;
using R7.Webmate.Text.Models;
using Xwt;
using Xwt.Drawing;

namespace R7.Webmate.Xwt.Text
{
    public class TextCleanerWidgetBase: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected TextCleanerModelBase Model;

        protected VBox vboxResults = new VBox ();

        protected virtual void ShowResults ()
        {
            vboxResults.Clear ();
            var index = 0;
            foreach (var result in Model.Results) {
                AddResult (++index, result);
            }
        }

        protected virtual void AddResult (int index, TextResult result)
        {
            var lblResult = new TextViewLabel ();
            lblResult.Text = result.Text;

            if (!string.IsNullOrEmpty (result.TextColor)) {
                lblResult.TextColor = Color.FromName (result.TextColor);
            }

            var vboxResult = new VBox ();
            vboxResult.MarginLeft = 5;
            vboxResult.MarginRight = 5;
            vboxResult.MarginBottom = 3;
            vboxResult.PackStart (lblResult, false, true);

            var frmResult = new Frame ();
            frmResult.Label = string.Format (T.GetString ("Result #{0} - {1}"), index, T.GetString (result.Label));
            frmResult.Content = vboxResult;
            vboxResults.PackStart (frmResult);
        }
    }
}

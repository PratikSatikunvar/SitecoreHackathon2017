using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Feature.BulkItemEditor.API;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Common.Website.sitecore.admin
{
    public partial class Dummy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlDownloadLanguage.DataSource = BindLanguages();
                ddlDownloadLanguage.DataBind();
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDownloadParentID.Text))
            {
                var itemUpdater = new ItemUpdater();
                itemUpdater.DownloadExcel(txtDownloadParentID.Text, ddlDownloadLanguage.SelectedValue);
            }
        }

        private ListItemCollection BindLanguages()
        {
            ListItemCollection Languages = new ListItemCollection();
            Sitecore.Data.Database master = Sitecore.Data.Database.GetDatabase("master");
            var langVersions = new List<Language>();
            var installedLanguages = LanguageManager.GetLanguages(master);

            foreach (var language in installedLanguages)
            {
                Languages.Add(new ListItem() { Text = language.Name, Value = language.CultureInfo.TwoLetterISOLanguageName });
            }
            return Languages;
        }
    }
}
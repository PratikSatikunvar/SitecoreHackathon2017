using Sitecore.Feature.BulkItemEditor.Models;
using Sitecore.Feature.BulkItemEditor.API;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Sitecore.Globalization;
using Sitecore.Data.Managers;

namespace Sitecore.Common.Website.sitecore.admin
{
    public partial class BulkItemEditor : System.Web.UI.Page
    {
        public List<int> Rows
        {
            get
            {
                if (ViewState["Rows"] == null)
                {
                    ViewState["Rows"] = new List<int>() { 1 };
                }
                return (List<int>)ViewState["Rows"];
            }
            set
            {
                ViewState["Rows"] = value;
            }
        }

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rpBulkItemEditor.DataSource = Rows;
                rpBulkItemEditor.DataBind();

                ddlDownloadLanguage.DataSource = BindLanguages();
                ddlDownloadLanguage.DataBind();
            }
        }

        protected void lnkAddNewRow_Click(object sender, EventArgs e)
        {
            var tempRows = (List<int>)ViewState["Rows"];
            tempRows.Add(0);
            Rows = tempRows;

            rpBulkItemEditor.DataSource = Rows;
            rpBulkItemEditor.DataBind();
        }

        #endregion Events

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rpBulkItemEditor.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var model = new ItemCreatorModel();

                    var ParentNode = (TextBox)item.FindControl("txtParentNode");
                    var TemplateID = (TextBox)item.FindControl("txtTemplateID");
                    var NoOfItems = (TextBox)item.FindControl("txtNoOfItems");
                    var Languages = (CheckBoxList)item.FindControl("cblLanguage");

                    model.NumberOfItems = System.Convert.ToInt32(NoOfItems.Text);
                    model.ParentNode = ParentNode.Text;
                    model.TemplateId = TemplateID.Text;
                    model.Languages = Languages.SelectedValue;

                    var ItemCreator = new ItemCreator();
                    ItemCreator.ItemGenerator(model);
                }
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

        protected void rpBulkItemEditor_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var cblLanguage = (CheckBoxList)e.Item.FindControl("cblLanguage");
            var languages = BindLanguages();
            foreach (ListItem item in languages)
            {
                cblLanguage.Items.Add(item);
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
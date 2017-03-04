using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Data;
using Sitecore.Globalization;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Web.Hosting;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;

namespace Sitecore.Feature.BulkItemEditor.API
{
    public class ItemUpdater
    {
        /// <summary>
        /// Download the item data which needed to be updated.
        /// </summary>
        /// <param name="parentItemId"> Id of the parent item whose children needed to fetch </param>
        /// <param name="language"> Language in which item's data needed to be downloaded </param>
        public void DownloadExcel(string parentItemId, string language)
        {
            try
            {
                Sitecore.Data.Database master = Sitecore.Data.Database.GetDatabase("master");

                Language languageItem;
                Language.TryParse(language, out languageItem);
                var parentItem = master.GetItem(parentItemId, languageItem);
                if (parentItem != null)
                {
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Customers.xls"));
                    HttpContext.Current.Response.ContentType = "application/ms-excel";

                    var children = parentItem.Children;
                    string fieldLabel = string.Empty, fieldValue = string.Empty;
                    foreach (Item item in children)
                    {
                        HttpContext.Current.Response.Write(item.ID.ToString() + "\n");
                        fieldLabel = fieldValue = string.Empty;
                        item.Fields.ReadAll();
                        foreach (Field field in item.Fields.Where(d => !d.Shared && !d.Name.StartsWith("__") && d.Name.Trim() != ""))
                        {
                            fieldLabel += field.Name + "\t";
                            fieldValue += item[field.Name] + "\t";
                        }
                        HttpContext.Current.Response.Write(fieldLabel + "\n");
                        HttpContext.Current.Response.Write(fieldValue + "\n");
                    }

                    HttpContext.Current.Response.End();

                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in method DownloadData. parentItemID: " + parentItemId + ", Language: " + language + ex.ToString(), ex);
            }
        }

        /// <summary>
        /// Create directory for given path if not exist.
        /// </summary>
        /// <param name="FilePath"> pPath of the file. </param>
        public void createDirectory(string FilePath)
        {
            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(FilePath);
                fileInfo.Directory.Create(); // If the directory already exists, this method does nothing.
            }
            catch (Exception ex)
            {
                Log.Error("Error in method createDirectory. FilePath: " + FilePath + ex.ToString(), ex);
            }
        }
    }
}
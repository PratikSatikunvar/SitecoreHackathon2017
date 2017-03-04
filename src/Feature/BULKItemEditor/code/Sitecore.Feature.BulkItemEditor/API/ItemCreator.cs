using Sitecore.Feature.BulkItemEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Data;
using Sitecore.Globalization;

namespace Sitecore.Feature.BulkItemEditor.API
{
    public class ItemCreator
    {
        public string ItemGenerator(ItemCreatorModel model)
        {
            string log = null;
            try
            {
                for (int i = 0; i < model.NumberOfItems; i++)
                {
                    Sitecore.Data.Database masterDB = Sitecore.Context.Database;
                    #region Item Creation
                    bool IsParentId = ID.IsID(model.ParentNode);
                    Item parentItem;
                    if (!IsParentId)
                    {
                        parentItem = masterDB.GetItem(model.ParentNode);
                    }
                    else
                    {
                        parentItem = masterDB.GetItem(ID.Parse(model.ParentNode));
                    }
                    TemplateItem template = masterDB.GetTemplate(model.TemplateId);
                    #endregion End Item Creation
                    
                    Item newItem = parentItem.Add("Item-" + i, template);

                    #region Adding Language Versions

                    foreach (string languageCode in model.Languages)
                    {
                        using (new LanguageSwitcher(languageCode))
                        {
                            newItem.Editing.BeginEdit();

                            newItem.Versions.AddVersion();

                            newItem.Editing.EndEdit();
                        }
                    }
                    
                    #endregion
                }

            }
            catch (Exception ex)
            {
                log += ex.Message;
                log += ex.StackTrace;
            }


            return log;
        }
    }
}
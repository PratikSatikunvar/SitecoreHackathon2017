using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.BulkItemEditor.Models
{
    public class ItemCreatorModel
    {
        public string ParentNode { get; set; }
        public string TemplateId { get; set; }
        public int NumberOfItems { get; set; }
        public List<string> Languages { get; set; }
    }
}
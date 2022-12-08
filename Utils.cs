using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAB_Session05_Skills
{
    internal static class Utils
    {
        public static void MyStaticMethod()
        {
            TaskDialog.Show("test", "This is my static method!");
        }
        public static FamilySymbol GetFamilySymbolByName(Document doc, string familyName, string familySymbolName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(FamilySymbol));

            foreach (FamilySymbol fs in collector)
            {
                if (fs.Name == familySymbolName && fs.FamilyName == familyName)
                    return fs;
            }

            return null;
        }
    }
}

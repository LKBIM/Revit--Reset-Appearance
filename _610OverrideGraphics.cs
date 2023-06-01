using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Linq;

namespace LKBIM
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class _610OverrideGraphics : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            //Get active view
            View view = doc.ActiveView;
            //Get elements in view
            var items = new FilteredElementCollector(doc, doc.ActiveView.Id).Cast<Element>().ToList();
            //Transaction
            OverrideGraphicSettings a = new OverrideGraphicSettings();
            try
            {
                using (Transaction trans = new Transaction(doc, "Override"))
                {
                    trans.Start();

                    foreach (var i in items)
                    {
                        view.SetElementOverrides(i.Id, a);
                    }
                    trans.Commit();
                }
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;

            }
        }
    }
}

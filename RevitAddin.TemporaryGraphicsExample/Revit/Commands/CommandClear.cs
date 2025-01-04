using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAddin.TemporaryGraphicsExample.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandClear : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            Document document = uiapp.ActiveUIDocument.Document;
            using (var temporaryGraphicsManager = TemporaryGraphicsManager.GetTemporaryGraphicsManager(document))
            {
                temporaryGraphicsManager.Clear();
            }

            return Result.Succeeded;
        }
    }

}

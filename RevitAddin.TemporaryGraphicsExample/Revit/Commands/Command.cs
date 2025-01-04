using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.IO;
using System.Reflection;

namespace RevitAddin.TemporaryGraphicsExample.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        private Random Random = new Random();
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            View view = uidoc.ActiveView;
            Selection selection = uidoc.Selection;

            try
            {
                using (var temporaryGraphicsManager = TemporaryGraphicsManager.GetTemporaryGraphicsManager(document))
                {
                    while (true)
                    {
                        var point = selection.PickPoint();

                        var imagePath = Images[Index++ % Images.Length];
                        var data = new InCanvasControlData(imagePath, point);
                        var indexClick = temporaryGraphicsManager.AddControl(data, ElementId.InvalidElementId);
                    }
                }
            }
            catch { }

            return Result.Succeeded;
        }

        public static int Index = 0;
        public string Location => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public string[] Images => Directory.GetFiles(Location, "*.bmp");

    }
}

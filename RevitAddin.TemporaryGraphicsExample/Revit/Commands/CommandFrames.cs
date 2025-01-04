using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.IO;
using System.Reflection;

namespace RevitAddin.TemporaryGraphicsExample.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandFrames : IExternalCommand
    {
        public string Location => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            var framesDirectory = "D:\\Downloads\\Download\\Bad Apple\\frames-bmp"; // 3 GB of files...
            var frames = Directory.GetFiles(framesDirectory, "*.bmp");

            int i = 0;
            int indexClick = -1;
            Document document = uiapp.ActiveUIDocument.Document;
            using (var temporaryGraphicsManager = TemporaryGraphicsManager.GetTemporaryGraphicsManager(document))
            {
                temporaryGraphicsManager.Clear();
                foreach (var frame in frames)
                {
                    i++;
                    if (i % 10 == 0)
                    {
                        using var data = new InCanvasControlData(frame);
                        if (indexClick == -1)
                        {
                            indexClick = temporaryGraphicsManager.AddControl(data, ElementId.InvalidElementId);
                        }
                        else
                        {
                            temporaryGraphicsManager.UpdateControl(indexClick, data);
                        }
                        uiapp.ActiveUIDocument.RefreshActiveView();
                        RevitRibbonController.ApplicationIdle();
                    }
                    if (i % 150 == 0)
                    {
                        if (Console.CapsLock)
                        {
                            break;
                        }
                    }
                }
            }


            return Result.Succeeded;
        }
    }
}

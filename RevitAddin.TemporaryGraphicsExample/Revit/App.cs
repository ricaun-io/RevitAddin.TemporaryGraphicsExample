using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System;

namespace RevitAddin.TemporaryGraphicsExample.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public ClickHandler clickHandle = new ClickHandler();
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("Graphics");

            //ribbonPanel.CreatePushButton<Commands.CommandFrames>("Frames")
            //    .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico");

            ribbonPanel.CreatePushButton<Commands.Command>("Temporary")
                .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico");

            ribbonPanel.CreatePushButton<Commands.CommandClear>("Clear")
                .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico");

            clickHandle.AddServer();

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();

            clickHandle.RemoveServer();

            return Result.Succeeded;
        }
    }

}
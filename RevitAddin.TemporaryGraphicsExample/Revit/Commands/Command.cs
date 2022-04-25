using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExternalService;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace RevitAddin.TemporaryGraphicsExample.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            Selection selection = uidoc.Selection;

            var clickHandle = new ClickHandler();
            clickHandle.AddServer();

            using (var temporaryGraphicsManager = TemporaryGraphicsManager.GetTemporaryGraphicsManager(document))
            {
                temporaryGraphicsManager.Clear();
                var point = selection.PickPoint();
                var imagePath = Path.Combine(Location, "image.bmp");
                var data = new InCanvasControlData(imagePath, point);
                var indexClick = temporaryGraphicsManager.AddControl(data, ElementId.InvalidElementId);
            }

            return Result.Succeeded;
        }

        public string Location => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }

    public class ClickHandler : ITemporaryGraphicsHandler
    {
        public void AddServer()
        {
            Guid guid = GetServerId();
            var services = (MultiServerService)ExternalServiceRegistry.GetService(GetServiceId());

            if (services.IsRegisteredServerId(guid))
                services.RemoveServer(guid);

            services.AddServer(this);
            services.SetActiveServers(new List<Guid> { guid });
        }

        public string GetDescription()
        {
            return "Click Temporary Graphics Handler";
        }

        public string GetName()
        {
            return "Click Temporary Graphics Handler";
        }

        public Guid GetServerId()
        {
            return new Guid("7B469077-9F7C-4CCF-9746-BD0DE41D3610");
        }

        public ExternalServiceId GetServiceId()
        {
            return ExternalServices.BuiltInExternalServices.TemporaryGraphicsHandlerService;
        }

        public string GetVendorId()
        {
            return "ricaun";
        }

        public void OnClick(TemporaryGraphicsCommandData data)
        {
            System.Windows.MessageBox.Show($"TemporaryGraphics {data.Index}");
        }
    }
}

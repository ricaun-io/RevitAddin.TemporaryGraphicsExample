using Autodesk.Revit.DB.ExternalService;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

namespace RevitAddin.TemporaryGraphicsExample.Revit.Commands
{
    class ClickHandler : ITemporaryGraphicsHandler
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

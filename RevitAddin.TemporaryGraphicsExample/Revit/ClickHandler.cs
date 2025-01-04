using Autodesk.Revit.DB.ExternalService;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

namespace RevitAddin.TemporaryGraphicsExample.Revit
{
    public class ClickHandler : ITemporaryGraphicsHandler
    {
        private Guid serverId;
        public ClickHandler() : this(Guid.NewGuid())
        {
        }
        public ClickHandler(Guid serverId)
        {
            this.serverId = serverId;
        }

        public void AddServer()
        {
            Guid guid = GetServerId();
            var services = (MultiServerService)ExternalServiceRegistry.GetService(GetServiceId());

            if (services.IsRegisteredServerId(guid))
                return;

            services.AddServer(this);
            var activeServerIds = services.GetActiveServerIds();
            activeServerIds.Add(guid);
            services.SetActiveServers(activeServerIds);
        }

        public void RemoveServer()
        {
            Guid guid = GetServerId();
            var services = (MultiServerService)ExternalServiceRegistry.GetService(GetServiceId());

            if (services.IsRegisteredServerId(guid))
            {
                var activeServerIds = services.GetActiveServerIds();
                activeServerIds.Remove(guid);
                services.SetActiveServers(activeServerIds);
                services.RemoveServer(guid);
            }
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
            return serverId;
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
            System.Windows.MessageBox.Show($"TemporaryGraphics {data.Index} {data.Document.Application.ActiveAddInId?.GetAddInName()}");
        }
    }
}

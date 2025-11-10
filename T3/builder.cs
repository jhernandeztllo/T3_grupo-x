using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace T3
{
    public class DeliveryRoute
    {
        public string RouteId { get; set; }
        public List<string> Stages { get; set; } = new List<string>();
        public string TransportType { get; set; }
        public string DocumentType { get; set; }
    }
     
    public interface IDeliveryRouteBuilder
    {
        void SetRouteId(string id);
        void AddWarehouse(string warehouseId);
        void AddDocumentValidation(string docType);
        void AssignTransport(string transportType);
        DeliveryRoute Build();
    }

    public class DeliveryRouteBuilder : IDeliveryRouteBuilder
    {
        private DeliveryRoute route = new DeliveryRoute();

        public void SetRouteId(string id) => route.RouteId = id;
        public void AddWarehouse(string warehouseId) => route.Stages.Add($"Almacén {warehouseId}");
        public void AddDocumentValidation(string docType) => route.DocumentType = docType;
        public void AssignTransport(string transportType) => route.TransportType = transportType;
        public DeliveryRoute Build() => route;
    }

    public class DeliveryRouteDirector
    {
        public DeliveryRoute CreateStandardRoute(IDeliveryRouteBuilder builder)
        {
            builder.SetRouteId("R-001");
            builder.AddWarehouse("N1");
            builder.AddDocumentValidation("DNI");
            builder.AssignTransport("Camión");
            return builder.Build();
        }
    }
}

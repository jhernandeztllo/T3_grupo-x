using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3
{
    public interface IShippingProvider
    {
        string RequestPickup(PickupRequest request);
        string GetTracking(string trackingId);
    }

    public class PickupRequest
    {
        public string OrderId { get; set; }
        public string PickupAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public double Weight { get; set; }
    }

    public class RappiEnviosAdapter : IShippingProvider
    {
        public string RequestPickup(PickupRequest request)
        {
            return "RappiEnvios: Solicitud de recojo recibida.";
        }

        public string GetTracking(string trackingId)
        {
            return "RappiEnvios: Pedido en ruta.";
        }
    }

    public class OlvaCourierAdapter : IShippingProvider
    {
        public string RequestPickup(PickupRequest request)
        {
            return "OlvaCourier: Pedido programado.";
        }

        public string GetTracking(string trackingId)
        {
            return "OlvaCourier: Entregado.";
        }
    }

    public class ShippingProviderFactory
    {
        public IShippingProvider GetProvider(string providerName)
        {
            switch (providerName)
            {
                case "RappiEnvios": return new RappiEnviosAdapter();
                case "OlvaCourier": return new OlvaCourierAdapter();
                default: return null;
            }
        }
    }
}

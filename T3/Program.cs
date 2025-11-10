using System;
using System.Collections.Generic;

namespace T3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA LOGIPACK S.A.C. ===\n");

            
            Console.WriteLine("→ DEMOSTRANDO PATRÓN OBSERVER\n");

            var order = new Order { OrderId = "PED-001", State = "Creado" };

            order.Attach(new ClientNotifier());
            order.Attach(new ProviderNotifier());
            order.Attach(new DashboardNotifier());

            order.ChangeState("Validado");
            Console.ReadLine();

            order.ChangeState("En Ruta");
            Console.ReadLine();

            order.ChangeState("Entregado");
            Console.ReadLine();

            Console.WriteLine("\n PATRÓN ADAPTER\n");

            var factory = new ShippingProviderFactory();

            var providers = new List<IShippingProvider>
            {
                factory.GetProvider("RappiEnvios"),
                factory.GetProvider("OlvaCourier")
            };

            foreach (var provider in providers)
            {
                var pickup = provider.RequestPickup(new PickupRequest
                {
                    OrderId = order.OrderId,
                    PickupAddress = "Almacén N2",
                    DeliveryAddress = "Av. Los Olivos 456",
                    Weight = 4.75
                });

                Console.WriteLine(pickup);
            }

            Console.ReadLine();

            Console.WriteLine("\n PATRÓN BUILDER\n");

            var builder = new DeliveryRouteBuilder();
            var director = new DeliveryRouteDirector();

            var route = director.CreateStandardRoute(builder);
            Console.WriteLine($"Ruta generada: {route.RouteId}");

            route.Stages.Add("Almacén N1");
            route.Stages.Add("Almacén N3");
            route.DocumentType = "Carnet de Extranjería";
            route.TransportType = "Motocicleta";

            Console.WriteLine("\nDetalles de la ruta:");
            Console.WriteLine($"- ID: {route.RouteId}");
            Console.WriteLine($"- Documentación: {route.DocumentType}");
            Console.WriteLine($"- Transporte asignado: {route.TransportType}");
            Console.WriteLine($"- Etapas:");

            foreach (var stage in route.Stages)
                Console.WriteLine($"  * {stage}");

            Console.ReadLine();

            Console.WriteLine("\nFin de la demostración. Presiona ENTER para salir.");
            Console.ReadLine();
        }
    }
    public class ProviderNotifier : IOrderObserver
    {
        public void Update(OrderEvent orderEvent)
        {
            Console.WriteLine($"[Proveedor] Pedido {orderEvent.OrderId} ahora está {orderEvent.ToState}");
        }
    }

    public class ClientNotifier : IOrderObserver
    {
        public void Update(OrderEvent orderEvent)
        {
            Console.WriteLine($"[Cliente] Pedido {orderEvent.OrderId} cambió a {orderEvent.ToState}");
        }
    }

    public class DashboardNotifier : IOrderObserver
    {
        public void Update(OrderEvent orderEvent)
        {
            Console.WriteLine($"[Dashboard] Estado actualizado: {orderEvent.ToState}");
        }
    }
}

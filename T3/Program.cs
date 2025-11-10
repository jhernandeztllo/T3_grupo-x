using System;
using T3;

namespace T3

{
    public class ConsoleOrderObserver : IOrderObserver
    {
        public void Update(OrderEvent orderEvent)
        {
            Console.WriteLine($"[EVENTO] Pedido {orderEvent.OrderId}: {orderEvent.FromState} → {orderEvent.ToState} ({orderEvent.Timestamp})");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var order = new Order { OrderId = "P-001", State = "Creado" };
            var observer = new ConsoleOrderObserver();
            order.Attach(observer);


            Console.WriteLine("=== Sistema de Gestión de Pedidos ===");
            Console.WriteLine($"Pedido {order.OrderId} creado con estado inicial: {order.State}");

            while (true)
            {
                string nextState = GetNextState(order.State);
                if (string.IsNullOrEmpty(nextState))
                {
                    Console.WriteLine("No hay más transiciones posibles. Fin del proceso.");
                    break;
                }
                Console.WriteLine($"\nEstado actual: {order.State}");
                Console.WriteLine($"Seleccione el siguiente estado válido: {nextState}");
                Console.Write("Ingrese su elección: ");
                string input = Console.ReadLine();
                if (input.Equals("Salir", StringComparison.OrdinalIgnoreCase))
                    break;
                if (!IsValidTransition(order.State, input))
                {
                    Console.WriteLine(" Intente nuevamente.");
                    continue;
                }
                order.ChangeState(input);

                if (input == "Entregado" || input == "Cancelado")
                {
                    Console.WriteLine("Proceso finalizado.");
                    break;
                }
            }
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
        static string GetNextState(string currentState)
        {
            return currentState switch
            {
                "Creado" => "Validado / Cancelado",
                "Validado" => "En Almacén / Cancelado",
                "En Almacén" => "Despachado / Cancelado",
                "Despachado" => "En Ruta",
                "En Ruta" => "Entregado / Cancelado",
                _ => ""
            };
        }
        static bool IsValidTransition(string current, string next)

        {
            switch (current)
            {
                case "Creado": return next == "Validado" || next == "Cancelado";
                case "Validado": return next == "En Almacén" || next == "Cancelado";
                case "En Almacén": return next == "Despachado" || next == "Cancelado";
                case "Despachado": return next == "En Ruta";
                case "En Ruta": return next == "Entregado" || next == "Cancelado";
                default: return false;
            }
        }
    }
}

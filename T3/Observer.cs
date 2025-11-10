using System;
using System.Collections.Generic;

namespace T3
{
    public class OrderEvent
    {
        public string OrderId { get; set; }
        public string FromState { get; set; }
        public string ToState { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public interface IOrderObserver
    {
        void Update(OrderEvent orderEvent);
    }

    public interface IOrderSubject
    {
        void Attach(IOrderObserver observer);
        void Detach(IOrderObserver observer);
        void Notify(OrderEvent orderEvent);
    }

    public class Order : IOrderSubject
    {
        private readonly List<IOrderObserver> observers = new List<IOrderObserver>();
        public string OrderId { get; set; }
        public string State { get; set; }

        public void Attach(IOrderObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }

        public void Detach(IOrderObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(OrderEvent orderEvent)
        {
            foreach (var obs in observers)
            {
                obs.Update(orderEvent);
            }
        }

        public void ChangeState(string newState)
        {
            var evt = new OrderEvent
            {
                OrderId = this.OrderId,
                FromState = this.State,
                ToState = newState,
                Timestamp = DateTime.Now
            };

            this.State = newState;
            Notify(evt);
        }
    }
}

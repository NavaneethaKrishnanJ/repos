using Models;
using Repos;

namespace BusniessService
{
    public class services
    {
        Repository _repos;

        List<OrderDataModel> _orders;

        List<FlightScheduleModel> _flightSchedules;

        public services() 
        { 
            _repos = new Repository();
        }

        public void LoadFlightDetails()
        {
             _flightSchedules = _repos.InitializeFlights();
            _orders = _repos.LoadorderDetails();

            var unscheduledOrders= DistributeOrdersToFlights(_orders, _flightSchedules);
            PrintUnscheduledOrders(unscheduledOrders);
        }

        private  List<OrderDataModel> DistributeOrdersToFlights(List<OrderDataModel> orders, List<FlightScheduleModel> flights)
        {
            var flightMap = new Dictionary<string, List<FlightScheduleModel>>();
            List<string> distinctcities= new List<string>();

            foreach (var flight in flights)
            {
                distinctcities.Add(flight.Destination);
                if (!flightMap.ContainsKey(flight.Destination))
                {
                    flightMap[flight.Destination] = new List<FlightScheduleModel>();
                }
                flightMap[flight.Destination].Add(flight);
            }

            var destinationOrders = new Dictionary<string, Queue<OrderDataModel>>();
            foreach (var order in orders)
            {
                if (distinctcities.Contains(order.Destination))
                {
                    if (!destinationOrders.ContainsKey(order.Destination))
                    {
                        destinationOrders[order.Destination] = new Queue<OrderDataModel>();
                    }
                    destinationOrders[order.Destination].Enqueue(order);
                }
            }

            var unscheduledOrders = new List<OrderDataModel>();

            List<FlightScheduleModel> fs = new List<FlightScheduleModel>();
            int flightNo = 1;
            foreach (string destination in destinationOrders.Keys)
            {

                var ordersQueue = destinationOrders[destination];
                var availableFlights = flightMap[destination];
               
                
                foreach (var flight in availableFlights)
                {
                    for (int i = 1; i <= 2; i++) 
                    { 
                        FlightScheduleModel flightSchedule = new FlightScheduleModel();
                        OrderDataModel ors = new OrderDataModel();
                        
                        flightSchedule.Destination = flight.Destination;
                        flightSchedule.source = "YUL";
                        flightSchedule.FlightNumber = flightNo;
                        flightSchedule.Day = i;
                        while (ordersQueue.Count > 0)
                        {
                            ors=ordersQueue.Dequeue();
                            flightSchedule.Orders.Add(ors);
                            if (flightSchedule.Orders.Count >= 20) { break; }
                        }
                        
                        fs.Add(flightSchedule);
                        flightNo++;
                    }
                    
                }
                

                while (ordersQueue.Count > 0)
                {
                    unscheduledOrders.Add(ordersQueue.Dequeue());
                }
                
            }
            PrintFlightAssignments(fs);
            return unscheduledOrders;
        }

        private static void PrintFlightAssignments(List<FlightScheduleModel> flights)
        {
            foreach (var flight in flights)
            {
                Console.WriteLine($"FlightNo {flight.FlightNumber},Depature {flight.source}, Arrival {flight.Destination}, Days {flight.Day}");
                foreach (var order in flight.Orders)
                {
                    Console.WriteLine($"Order: Order-{order.Id}, FlightNo {flight.FlightNumber},Depature {flight.source}, Arrival {flight.Destination}, Days {flight.Day}");
                }

                Console.WriteLine();
            }
        }

        private static void PrintUnscheduledOrders(List<OrderDataModel> unscheduledOrders)
        {
            if (unscheduledOrders.Count > 0)
            {
                Console.WriteLine("Unscheduled Orders:");
                foreach (var order in unscheduledOrders)
                {
                    Console.WriteLine($"  Order ID: {order.Id}, Destination: {order.Destination}");
                }
                Console.WriteLine();
            }
        }



    }
}

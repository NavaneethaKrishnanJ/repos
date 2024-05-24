using Models;
using System.Text.Json;

namespace Repos
{
    public class Repository
    {
        protected string _orderfile { get; set; }

        protected string _flightScheduleFile { get; set; }

        protected string _basePath = "C:\\Users\\kavya\\source\\repos\\Repository\\Data\\";

        public Repository() {

            _orderfile = _basePath + "coding-assigment-orders.json";
            _flightScheduleFile = _basePath + "FlightSchedule.json";
        }


        public List<FlightScheduleModel> InitializeFlights()
        {
            try
            {
                var flightdetails = LoadFlightDetailsFromJson(_flightScheduleFile);
                return flightdetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<FlightScheduleModel> { new FlightScheduleModel() };
        }



        public List<OrderDataModel> LoadorderDetails()
        {
            try
            {
                var loadData = LoadOrderDataFromJson(_orderfile);
                return loadData;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<OrderDataModel> { new OrderDataModel() };
        }

        protected List<FlightScheduleModel> LoadFlightDetailsFromJson(string filename)
        {
            try
            {
                string str = File.ReadAllText(filename);
                               
                var flightdetails = JsonSerializer.Deserialize<Dictionary<string, FlightScheduleModel>>(str);
                 List<FlightScheduleModel> flights = new List<FlightScheduleModel>();
                foreach(var f in flightdetails.Values)
                {
                    FlightScheduleModel flight = new FlightScheduleModel();
                    flight.source = f.source;
                    flight.Destination=f.Destination;
                    flight.FlightNumber = f.FlightNumber;
                    flights.Add(flight);
                }
                return flights; 
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return new List<FlightScheduleModel>();
        }

        protected List<OrderDataModel> LoadOrderDataFromJson(string filename)
        {
            try
            {
                List<OrderDataModel> orders= new List<OrderDataModel>();
                string jsonstring = File.ReadAllText(filename);

                var orderData = JsonSerializer.Deserialize<Dictionary<string, OrderDataModel>>(jsonstring);
                int orderNo = 1;
                foreach(var o in orderData.Values)
                {
                    OrderDataModel orderDataModel = new OrderDataModel();

                    orderDataModel.Destination = o.Destination;
                    orderDataModel.Id = orderNo;
                    orderNo++;
                    orders.Add(orderDataModel);
                }

                return orders;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<OrderDataModel>();
        }

    }
}

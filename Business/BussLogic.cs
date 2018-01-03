using System;
using DataLayer;
using Entity;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace Business
{
    public class BussLogic
    {
        #region Manager
        public static string AddManger(string M_Name, string M_Pwd, long M_Contact, string M_email)
        {
            Manager ManAdd = new Manager();
            ManAdd.name = M_Name;
            ManAdd.password = M_Pwd;
            ManAdd.m_contact = M_Contact;
            ManAdd.m_email = M_email;
            string alert;
            string AddMan_res = HRSData.AddManager(ManAdd);

            if (AddMan_res.CompareTo("no_M_Id") == 0)
            {
                alert = "Manager Not Added";
            }
            else
            {
                alert =  AddMan_res;
            }
            return alert;
        }
        public static string UpdateManger(string M_id, string M_Name, string M_Pwd, long M_Contact, string M_email)
        {
            Manager ManAdd = new Manager();
            ManAdd.id = M_id;
            ManAdd.name = M_Name;
            ManAdd.password = M_Pwd;
            ManAdd.m_contact = M_Contact;
            ManAdd.m_email = M_email;


            string UpMan_res = HRSData.UpdateManager(ManAdd);


            return UpMan_res;
        }
        public static Manager RetriveManger(string M_MangID)
        {
            Manager Mang_Get = DataLayer.HRSData.GetManager(M_MangID);
            return Mang_Get;
        }
        #endregion

        #region DataSuply
        public static DataSet AllAvilableHotel()
        {
            return HRSData.AvilabeHotels();


        }
        public static DataSet AllManagedHotel(string managerid)
        {
            return HRSData.MangerHotel(managerid);


        }
        public static DataSet AllCities()
        {


            return HRSData.GetCities();


        }
        public static DataSet AllStates()
        {
            return HRSData.GetStates();


        }
        public static DataSet FilteredHotel(string city, string country, DateTime indate, DateTime outdate)
        {
            return HRSData.FilterHotel(city, country, indate, outdate);
           
           
        }

        #endregion

        #region Helpers
        public static int FindCost(string hotel_id, int adult, int child,int type, DateTime checkin, DateTime checkout)
        {


            int cost_ac_adult;
            int cost_non_ac_adult;
            int cost_ac_child;
            int cost_non_ac_child;
            int total;
            int nights = (checkout - checkin).Days;


            int check = DataLayer.HRSData.FindPrice(hotel_id, out cost_ac_adult, out cost_ac_child, out cost_non_ac_adult, out cost_non_ac_child);
            if (type == 0)
            { //ac
                total = cost_ac_adult * adult + cost_ac_child * child;
            }
            else total = cost_non_ac_adult * adult + cost_non_ac_child * child;

            total *= nights;


            return total;




        }

        public static string AuthrizeCard(string bankid, int ccn, string ctype, string name, DateTime date, int cvc, int ano, int pin)
        {
            Card_details Cd = new Card_details();
            Cd.Bank_id = bankid;
            Cd.card_no = ccn;
            Cd.card_type = ctype;
            Cd.name_on_card = name;
            Cd.exp_date = date;
            Cd.cvc_no = cvc;
            Cd.account_no = ano;
            Cd.pin = pin;

            string pay = HRSData.Authorizepin(Cd);
            return pay;



        }

        public static int CusLogin(string name, string pass)
        {
            return HRSData.AuthorizeCustomer(name, pass);

        }

        public static int EmailVerify(string Email)
        {
            int result=1;
            int a= HRSData.AuthorizeCustomerEmail(Email);
            int b = HRSData.AuthorizeManagerEmail(Email);
            if (a == 0 && b == 0)
                result = 0;
            return result;



        }
        public static int ManLogin(string name, string pass)
        {
            return HRSData.AuthorizeManager(name, pass);


        }
        #endregion

        #region Customer
        public static string AddCustomer(string C_Name, long C_Contact, string C_email, DateTime C_Dob, int C_PinCode, string C_State, string C_City, string C_Pwd)
        {
            Customer CustAdd = new Customer();
            CustAdd.name = C_Name;
            CustAdd.c_contact = C_Contact;
            CustAdd.c_email = C_email;
            CustAdd.Dob = C_Dob;
            CustAdd.pincode = C_PinCode;
            PinCode pin = new PinCode();
            pin.city = C_City;
            pin.state = C_State;
            pin.pin = C_PinCode;
            CustAdd.PinCode1 = pin;
            CustAdd.password = C_Pwd;

            string AddCust_res = HRSData.AddCustomer(CustAdd);
            string alert;
            if (AddCust_res.CompareTo("no_C_Id") == 0)
            {
                alert = "Customer Not Added";
            }
            else
            {
                alert =  AddCust_res.ToString();
            }
            return alert;
        }

        public static Customer RetriveCustomer(string cus_id)
        {
            Customer cus = new Customer();

            cus = HRSData.GetCustomer(cus_id);

            return cus;
        }
        public static string UpdateCustomer(string C_ID, string C_Name, long C_Contact, string C_email, DateTime C_Dob, int C_PinCode, string C_Country, string C_City, string C_Pwd)
        {
            Customer CustAdd = new Customer();
            CustAdd.id = C_ID;
            CustAdd.name = C_Name;
            CustAdd.c_contact = C_Contact;
            CustAdd.c_email = C_email;
            CustAdd.Dob = C_Dob;
            CustAdd.pincode = C_PinCode;
            PinCode pin = new PinCode();
            pin.city = C_City;
            pin.state = C_Country;
            pin.pin = C_PinCode;
            CustAdd.PinCode1 = pin;
            CustAdd.password = C_Pwd;

            string AddCust_res = HRSData.UpdateCustomer(CustAdd);


            return AddCust_res;
        }
        #endregion

        #region Hotel
        public static Hotel RetriveHotel(string hotel_id, out int cost_ac_adult, out int cost_ac_child, out int cost_non_ac_adult, out int cost_non_ac_child)
        {
            Hotel R_id = new Hotel();
            R_id = HRSData.RetriveHotels(hotel_id);

            DataLayer.HRSData.FindPrice(hotel_id, out cost_ac_adult, out cost_ac_child, out cost_non_ac_adult, out cost_non_ac_child);


            return R_id;
        }
        public static string AddHotel(string name, string hotelDesrp, int pincode, string country, string city, string ManagerId, int No_AC_Rooms, int No_Non_AC_Rooms, int price_ac_adult, int price_ac_child, int price_nonac_adult, int price_nonac_child)
        {
            string HotelID = HRSData.AddHoteldataLayer(name, hotelDesrp, pincode, country, city, ManagerId, No_AC_Rooms, No_Non_AC_Rooms);
            HRSData.acroom(HotelID, price_ac_adult, price_ac_child);
            HRSData.nonacroom(HotelID, price_nonac_adult, price_nonac_child);

            return HotelID;
        }
        public static string UpdateHotel(string ManagerId, string HotelId, string HotelName, string HotelDsrrp, int pincode, string Country, string City, int No_Of_Ac_Rooms, int No_Of_Non_Ac_Rooms, int price_ac_adult, int price_ac_child, int price_nonac_adult, int price_nonac_child)
        {
            Hotel update = new Hotel();
            update.hotel_description = HotelDsrrp;
            update.id = HotelId;
            update.name = HotelName;
            update.manager_id = ManagerId;
            update.pincode = pincode;
            PinCode pin = new PinCode();
            pin.pin = pincode;
            pin.state = Country;
            pin.city = City;
            update.PinCode1 = pin;
            update.no_of_ac_rooms = No_Of_Ac_Rooms;
            update.no_of_non_ac_rooms = No_Of_Non_Ac_Rooms;




            return HRSData.UpdateHoteldata(update, price_ac_adult, price_ac_child, price_nonac_adult, price_nonac_child);

        }
        #endregion

        #region Booking
        public static string AddBooking(string c_id, string h_id, int ac, int nonac, string t_id, DateTime book, DateTime cin, DateTime cout, int adult, int child)
        {
            Booking b = new Booking();

            b.Customer_id = c_id;
            b.Hotel_id = h_id;
            b.no_adult = adult;
            b.no_child = child;
            b.No_Of_AC_Rooms = ac;
            b.No_Of_Non_AC_Rooms = nonac;
            b.T_id = t_id;
            b.Date_booking = book;
            b.Date_checkin = cin;
            b.Date_checkout = cout;

            string s = HRSData.AddBooking(b);
            return s;

        }

            public static string AddPayment(string hotel_id, string customer_id, string booking_name, int cost, int ccn, string type)
        {
            Payment p = new Payment();
            p.customer_Id = customer_id;
            p.Hotel_id = hotel_id;
            p.booking_name = booking_name;
            p.total_Cost = cost;
            p.card_number = ccn;
            p.card_type = type;

            string res = HRSData.savepaymentdetails(p);
            return res;

        }

        public static Booking RetriveBooking(string BookingId)
        {
            Booking B_details = new Booking();
            B_details = HRSData.GetBookingDetails(BookingId);
            return B_details;

        }

        public static Booking RetriveBookingByCustomer(string CustomerId)
        {
            Booking B_details = new Booking();
            B_details = HRSData.GetBookingDetailsCustomer(CustomerId);
            return B_details;

        }

        public static string UpdateBooking(string BookingId, int No_Of_Ac_Rooms, int No_Of_Non_Ac_Rooms, int No_Of_Adults, int No_Of_Children, DateTime Date_Of_ChekInn, DateTime Date_Of_CheckOut)
        {
            string result;
            result = HRSData.UpdateBookingDetails(BookingId, No_Of_Ac_Rooms, No_Of_Non_Ac_Rooms, No_Of_Adults, No_Of_Children, Date_Of_ChekInn, Date_Of_CheckOut);

            return result;
        }
        #endregion


    }
}

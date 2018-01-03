using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Entity;
using System.Collections.Generic;
namespace DataLayer
{
    public class HRSData
    {
        #region Hotel
        public static string AddHoteldataLayer(string name, string hotelDesrp, int pincode, string country, string city, string ManagerId, int No_AC_Rooms, int No_Non_AC_Rooms)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("InHotel", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter out_hotel_id = new SqlParameter();
            out_hotel_id.ParameterName = "@hotel_id";
            out_hotel_id.SqlDbType = SqlDbType.VarChar;
            out_hotel_id.Direction = ParameterDirection.Output;
            out_hotel_id.Size = 7;
            cmd.Parameters.Add(out_hotel_id);



            cmd.Parameters.Add(new SqlParameter("@name", name));
            cmd.Parameters.Add(new SqlParameter("@hotel_description", hotelDesrp));
            cmd.Parameters.Add(new SqlParameter("@pincode", pincode));
            cmd.Parameters.Add(new SqlParameter("@state", country));
            cmd.Parameters.Add(new SqlParameter("@city", city));
            cmd.Parameters.Add(new SqlParameter("@manager_id", ManagerId));
            cmd.Parameters.Add(new SqlParameter("@ac_room", No_AC_Rooms));
            cmd.Parameters.Add(new SqlParameter("@non_ac_room", No_Non_AC_Rooms));




            int num = cmd.ExecuteNonQuery();
            string result;
            if (num > 0)
                result = cmd.Parameters["@hotel_id"].Value.ToString();
            else result = "Sorry.. Failed to add Hotel";
            con.Close();
            return result;
        }
        public static Hotel RetriveHotels(string ret_id)
        {
            Hotel R_id = new Hotel();
            R_id.id = ret_id;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("GetHotelDetails", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@hotel_id", R_id.id));


            SqlParameter out_name = new SqlParameter();
            out_name.ParameterName = "@name";
            out_name.SqlDbType = SqlDbType.VarChar;
            out_name.Direction = ParameterDirection.Output;
            out_name.Size = 50;
            cmd.Parameters.Add(out_name);



            SqlParameter out_hotel_description = new SqlParameter();
            out_hotel_description.ParameterName = "@hotel_description";
            out_hotel_description.SqlDbType = SqlDbType.VarChar;
            out_hotel_description.Direction = ParameterDirection.Output;
            out_hotel_description.Size = 1000;
            cmd.Parameters.Add(out_hotel_description);



            SqlParameter out_pincode = new SqlParameter();
            out_pincode.ParameterName = "@pincode";
            out_pincode.SqlDbType = SqlDbType.Int;
            out_pincode.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_pincode);

            SqlParameter out_country = new SqlParameter();
            out_country.ParameterName = "@state";
            out_country.SqlDbType = SqlDbType.VarChar;
            out_country.Size = 20;
            out_country.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_country);

            SqlParameter out_city = new SqlParameter();
            out_city.ParameterName = "@city";
            out_city.SqlDbType = SqlDbType.VarChar;
            out_city.Size = 20;
            out_city.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_city);


            SqlParameter out_manager_id = new SqlParameter();
            out_manager_id.ParameterName = "@manager_id";
            out_manager_id.SqlDbType = SqlDbType.VarChar;
            out_manager_id.Direction = ParameterDirection.Output;
            out_manager_id.Size = 10;
            cmd.Parameters.Add(out_manager_id);



            SqlParameter out_ac_room = new SqlParameter();
            out_ac_room.ParameterName = "@ac_room";
            out_ac_room.SqlDbType = SqlDbType.Int;
            out_ac_room.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_ac_room);


            SqlParameter out_non_ac_rooms = new SqlParameter();
            out_non_ac_rooms.ParameterName = "@non_ac_room";
            out_non_ac_rooms.SqlDbType = SqlDbType.Int;
            out_non_ac_rooms.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_non_ac_rooms);




            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();


            //  int result = 
            //  cmd.ExecuteReader();

            // if (result == 1)
            {
                R_id.id = R_id.ToString();
                R_id.name = (cmd.Parameters["@name"].Value.ToString());
                R_id.hotel_description = (cmd.Parameters["@hotel_description"].Value.ToString());
                R_id.pincode = Convert.ToInt32(cmd.Parameters["@pincode"].Value.ToString());

                PinCode pin = new PinCode();
                pin.pin = Convert.ToInt32(cmd.Parameters["@pincode"].Value.ToString());

                pin.state = (cmd.Parameters["@state"].Value.ToString());
                pin.city = (cmd.Parameters["@city"].Value.ToString());
                R_id.PinCode1 = pin;
                R_id.manager_id = (cmd.Parameters["@manager_id"].Value.ToString());
                R_id.no_of_ac_rooms = Convert.ToInt32(cmd.Parameters["@ac_room"].Value.ToString());
                R_id.no_of_non_ac_rooms = Convert.ToInt32(cmd.Parameters["@non_ac_room"].Value.ToString());



            }
            // else
            {
                //  R_id.id = "Unable to Retrive Details";

            }
            con.Close();
            return R_id;

        }
        public static string UpdateHoteldata(Hotel update, int price_ac_adult, int price_ac_child, int price_nonac_adult, int price_nonac_child)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UpdateHotel", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@hotel_id", update.id));
            cmd.Parameters.Add(new SqlParameter("@name", update.name));
            cmd.Parameters.Add(new SqlParameter("@hotel_description", update.hotel_description));
            cmd.Parameters.Add(new SqlParameter("@pin", update.pincode));
            cmd.Parameters.Add(new SqlParameter("@state", update.PinCode1.state));
            cmd.Parameters.Add(new SqlParameter("@city", update.PinCode1.city));
            cmd.Parameters.Add(new SqlParameter("@manager_id", update.manager_id));
            cmd.Parameters.Add(new SqlParameter("@ac_room", update.no_of_ac_rooms));
            cmd.Parameters.Add(new SqlParameter("@non_ac_room", update.no_of_non_ac_rooms));


            cmd.Parameters.Add(new SqlParameter("@price_adult_ac", price_ac_adult));
            cmd.Parameters.Add(new SqlParameter("@price_adult_nonac", price_nonac_adult));
            cmd.Parameters.Add(new SqlParameter("@price_child_ac", price_ac_child));
            cmd.Parameters.Add(new SqlParameter("@price_child_nonac", price_nonac_child));


            int res = cmd.ExecuteNonQuery();
            con.Close();
            string result;
            if (res > 0)
            {
                result = "Succes";

            }
            else result = "Not Sucessful";

            return result;


        }
        public static void acroom(string hotel_id, int adult, int child)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("InRoom", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@hotel_id", hotel_id));
            cmd.Parameters.Add(new SqlParameter("@price_adult", adult));
            cmd.Parameters.Add(new SqlParameter("@price_child", child));
            cmd.Parameters.Add(new SqlParameter("@room_no", 1));
            cmd.Parameters.Add(new SqlParameter("@room_type", "AC"));
            cmd.ExecuteNonQuery();

            con.Close();
        }
        public static void nonacroom(string hotel_id, int adult, int child)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("InRoom", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@hotel_id", hotel_id));
            cmd.Parameters.Add(new SqlParameter("@price_adult", adult));
            cmd.Parameters.Add(new SqlParameter("@price_child", child));
            cmd.Parameters.Add(new SqlParameter("@room_no", 2));
            cmd.Parameters.Add(new SqlParameter("@room_type", "NON AC"));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        #endregion

        #region datasuply
        public static DataSet AvilabeHotels()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("hotel_room_avilablity", con);
            cmd.CommandType = CommandType.StoredProcedure;


            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            con.Close();
            return ds;


        }

     

        public static DataSet MangerHotel(string M_id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("GetManagerHotels2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@manager_id", M_id));



            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            con.Close();
            return ds;


        }
        public static DataSet FilterHotel(string city, string country, DateTime indate, DateTime outdate)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();
            DateTime dt = new DateTime();
            dt = DateTime.MinValue;
            SqlCommand cmd = new SqlCommand("hotel_room_avilablity_CCIO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@state", country);
            if (DateTime.Equals(indate, dt))
            {
                cmd.Parameters.AddWithValue("@indate", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@indate", indate);
            }

            if (DateTime.Equals(outdate, dt))
            {
                cmd.Parameters.AddWithValue("@outdate", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@outdate", outdate);
            }

            con.Close();
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            return ds;
        }
        public static DataSet GetCities()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("GetCities", con);
            cmd.CommandType = CommandType.StoredProcedure;


            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            con.Close();
            return ds;
        }
        public static DataSet GetStates()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("GetStates", con);
            cmd.CommandType = CommandType.StoredProcedure;


            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            con.Close();
            return ds;
        }
        #endregion

        #region Helpers
        public static int FindPrice(string hotel_id, out int price_ac_adult, out int price_ac_child, out int price_non_ac_adult, out int price_non_ac_child)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("Price", con);
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.Add(new SqlParameter("@hotel_id", hotel_id));

            SqlParameter out_price_adult_ac_room = new SqlParameter();
            out_price_adult_ac_room.ParameterName = "@price_adult_ac_room";
            out_price_adult_ac_room.SqlDbType = SqlDbType.Int;
            out_price_adult_ac_room.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_price_adult_ac_room);

            SqlParameter out_price_child_ac_room = new SqlParameter();
            out_price_child_ac_room.ParameterName = "@price_child_ac_room";
            out_price_child_ac_room.SqlDbType = SqlDbType.Int;
            out_price_child_ac_room.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_price_child_ac_room);

            SqlParameter out_price_adult_non_ac_room = new SqlParameter();
            out_price_adult_non_ac_room.ParameterName = "@price_adult_non_ac_room";
            out_price_adult_non_ac_room.SqlDbType = SqlDbType.Int;
            out_price_adult_non_ac_room.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_price_adult_non_ac_room);

            SqlParameter out_price_child_non_ac_room = new SqlParameter();
            out_price_child_non_ac_room.ParameterName = "@price_child_non_ac_room";
            out_price_child_non_ac_room.SqlDbType = SqlDbType.Int;
            out_price_child_non_ac_room.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_price_child_non_ac_room);

            int result = cmd.ExecuteNonQuery();
            if (result == -1)
                {


                price_ac_adult = Convert.ToInt32(cmd.Parameters["@price_adult_ac_room"].Value.ToString());
                price_ac_child = Convert.ToInt32(cmd.Parameters["@price_child_ac_room"].Value.ToString());
                price_non_ac_adult = Convert.ToInt32(cmd.Parameters["@price_adult_non_ac_room"].Value.ToString());
                price_non_ac_child = Convert.ToInt32(cmd.Parameters["@price_child_non_ac_room"].Value.ToString());



            }
            else
            {
                price_ac_adult = 0;
                price_ac_child = 0;
                price_non_ac_adult = 0;
                price_non_ac_child = 0;

            }
            con.Close();
            return result;




        }
        public static int AuthorizeCustomer(string cid, string password)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_customer_login", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter out_customerstatus = new SqlParameter();
            out_customerstatus.ParameterName = "@customerstatus";
            out_customerstatus.SqlDbType = SqlDbType.Int;
            out_customerstatus.Direction = ParameterDirection.Output;
            out_customerstatus.Size = 50;
            cmd.Parameters.Add(out_customerstatus);

            cmd.Parameters.Add(new SqlParameter("@username", cid));
            cmd.Parameters.Add(new SqlParameter("@password", password));

            int result = cmd.ExecuteNonQuery();
            int res = int.Parse(cmd.Parameters["@customerstatus"].Value.ToString());
            con.Close();
            return res;

        }

        public static int AuthorizeCustomerEmail(string Email)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_emailverifyC", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter out_customerstatus = new SqlParameter();
            out_customerstatus.ParameterName = "@customerstatus";
            out_customerstatus.SqlDbType = SqlDbType.Int;
            out_customerstatus.Direction = ParameterDirection.Output;
            out_customerstatus.Size = 50;
            cmd.Parameters.Add(out_customerstatus);

            cmd.Parameters.Add(new SqlParameter("@email", Email));

            int result = cmd.ExecuteNonQuery();
            int res = int.Parse(cmd.Parameters["@customerstatus"].Value.ToString());
            con.Close();
            return res;

        }

        public static int AuthorizeManagerEmail(string Email)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_emailverifyM", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter out_customerstatus = new SqlParameter();
            out_customerstatus.ParameterName = "@customerstatus";
            out_customerstatus.SqlDbType = SqlDbType.Int;
            out_customerstatus.Direction = ParameterDirection.Output;
            out_customerstatus.Size = 50;
            cmd.Parameters.Add(out_customerstatus);

            cmd.Parameters.Add(new SqlParameter("@email", Email));

            int result = cmd.ExecuteNonQuery();
            int res = int.Parse(cmd.Parameters["@customerstatus"].Value.ToString());
            con.Close();
            return res;

        }



        public static int AuthorizeManager(string mid, string password)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Managers_login", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter out_Managersstatus = new SqlParameter();
            out_Managersstatus.ParameterName = "@Managersstatus";
            out_Managersstatus.SqlDbType = SqlDbType.Int;
            out_Managersstatus.Direction = ParameterDirection.Output;
            out_Managersstatus.Size = 50;
            cmd.Parameters.Add(out_Managersstatus);

            cmd.Parameters.Add(new SqlParameter("@username", mid));
            cmd.Parameters.Add(new SqlParameter("@password", password));
            int result = cmd.ExecuteNonQuery();
            //if (result == 1)
            //{
            //    auto1 = "Authorized ";

            //}
            //else
            //{

            //    auto1 = "not Authorized ";
            //}
            con.Close();
            int res = int.Parse(cmd.Parameters["@Managersstatus"].Value.ToString());
            return res;

        }
        public static string Authorizepin(Card_details cd_details)

        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_payment", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter out_pin = new SqlParameter();
            out_pin.ParameterName = "@paystatus";
            out_pin.SqlDbType = SqlDbType.Int;
            out_pin.Direction = ParameterDirection.Output;
            out_pin.Size = 50;
            cmd.Parameters.Add(out_pin);
            cmd.Parameters.Add(new SqlParameter("@Bank_Id ", cd_details.Bank_id));
            cmd.Parameters.Add(new SqlParameter("@CreditCardNo", cd_details.card_no));
            cmd.Parameters.Add(new SqlParameter("@Cardtype", cd_details.card_type));
            cmd.Parameters.Add(new SqlParameter("@NameOnCard", cd_details.name_on_card));
            cmd.Parameters.Add(new SqlParameter("@ExpiryDate", cd_details.exp_date.ToString()));
            cmd.Parameters.Add(new SqlParameter("@CVV", cd_details.cvc_no));
            cmd.Parameters.Add(new SqlParameter("@AccountNumber", cd_details.account_no));
            cmd.Parameters.Add(new SqlParameter("@Pin", cd_details.pin));
            string autopin;
            try
            {
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception )
            {
                autopin = "Error";
            }
            if (int.Parse(cmd.Parameters["@paystatus"].Value.ToString()) == 1)
            {
                autopin = "Payment Successful";

            }
            else
            {

                autopin = "Payment Error ";
            }
            con.Close();
            return autopin;

        }
        public static string savepaymentdetails(Payment paymentdetails)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_store_payment", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter out_paymentid = new SqlParameter();
            out_paymentid.ParameterName = "@payment_id";
            out_paymentid.SqlDbType = SqlDbType.VarChar;
            out_paymentid.Direction = ParameterDirection.Output;
            out_paymentid.Size = 8;
            cmd.Parameters.Add(out_paymentid);




            cmd.Parameters.Add(new SqlParameter("@Hotel_id", paymentdetails.Hotel_id));
            cmd.Parameters.Add(new SqlParameter("@customer_Id", paymentdetails.customer_Id));
            cmd.Parameters.Add(new SqlParameter("@booking_name", paymentdetails.booking_name));
            cmd.Parameters.Add(new SqlParameter("@total_Cost", paymentdetails.total_Cost));
            cmd.Parameters.Add(new SqlParameter("@card_number", paymentdetails.card_number));
            cmd.Parameters.Add(new SqlParameter("@card_type", paymentdetails.card_type));

            int result = 0;
            try
            {
                result = cmd.ExecuteNonQuery();
            }
            catch
            {
                paymentdetails.id = "error";
            }
            if (result == 1)
            {
                paymentdetails.id = cmd.Parameters["@payment_id"].Value.ToString();

            }
            else
            {
                paymentdetails.id = "noID";

            }
            con.Close();
            return paymentdetails.id;

        }

        #endregion

        #region Customer
        public static string AddCustomer(Customer C_details)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("InCustomerDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter out_CustomerID = new SqlParameter();
            out_CustomerID.ParameterName = "@customer_id";
            out_CustomerID.SqlDbType = SqlDbType.VarChar;
            out_CustomerID.Direction = ParameterDirection.Output;
            out_CustomerID.Size = 15;
            cmd.Parameters.Add(out_CustomerID);


            cmd.Parameters.Add(new SqlParameter("@c_name", C_details.name));
            cmd.Parameters.Add(new SqlParameter("@c_contact", C_details.c_contact));
            cmd.Parameters.Add(new SqlParameter("@c_email", C_details.c_email));
            cmd.Parameters.Add(new SqlParameter("@dob", C_details.Dob));
            cmd.Parameters.Add(new SqlParameter("@Pin", C_details.pincode));
            cmd.Parameters.Add(new SqlParameter("@state", C_details.PinCode1.state));
            cmd.Parameters.Add(new SqlParameter("@city", C_details.PinCode1.city));
            cmd.Parameters.Add(new SqlParameter("@password", C_details.password));

            int result = cmd.ExecuteNonQuery();
            if (result >= 1)
            {
                C_details.id = cmd.Parameters["@customer_id"].Value.ToString();
            }
            else
            {
                C_details.id = "no_C_Id";

            }
            con.Close();
            return C_details.id;
        }
        public static Customer GetCustomer(string C_custID)
        {
            Customer C_getdetails = new Customer();
            C_getdetails.id = C_custID;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetCustomerDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Customer_id", C_custID));

            SqlParameter out_CustomerName = new SqlParameter();

            out_CustomerName.ParameterName = "@name";
            out_CustomerName.SqlDbType = SqlDbType.VarChar;
            out_CustomerName.Direction = ParameterDirection.Output;
            out_CustomerName.Size = 50;
            cmd.Parameters.Add(out_CustomerName);


            SqlParameter out_CustomerContact = new SqlParameter();
            out_CustomerContact.ParameterName = "@c_contact";
            out_CustomerContact.SqlDbType = SqlDbType.BigInt;
            out_CustomerContact.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_CustomerContact);

            SqlParameter out_CustomerEmail = new SqlParameter();
            out_CustomerEmail.ParameterName = "@c_email";
            out_CustomerEmail.SqlDbType = SqlDbType.VarChar;
            out_CustomerEmail.Direction = ParameterDirection.Output;
            out_CustomerEmail.Size = 30;

            cmd.Parameters.Add(out_CustomerEmail);
            SqlParameter out_CustomerDob = new SqlParameter();
            out_CustomerDob.ParameterName = "@dob";
            out_CustomerDob.SqlDbType = SqlDbType.Date;
            out_CustomerDob.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(out_CustomerDob);
            SqlParameter out_CustomerPin = new SqlParameter();
            out_CustomerPin.ParameterName = "@pincode";
            out_CustomerPin.SqlDbType = SqlDbType.Int;
            out_CustomerPin.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(out_CustomerPin);
            SqlParameter out_CustomerCity = new SqlParameter();
            out_CustomerCity.ParameterName = "@city";
            out_CustomerCity.SqlDbType = SqlDbType.VarChar;
            out_CustomerCity.Direction = ParameterDirection.Output;
            out_CustomerCity.Size = 20;
            cmd.Parameters.Add(out_CustomerCity);


            SqlParameter out_CustomerCountry = new SqlParameter();
            out_CustomerCountry.ParameterName = "@state";
            out_CustomerCountry.SqlDbType = SqlDbType.VarChar;
            out_CustomerCountry.Direction = ParameterDirection.Output;
            out_CustomerCountry.Size = 10;
            cmd.Parameters.Add(out_CustomerCountry);


            SqlParameter out_CustomerPassword = new SqlParameter();
            out_CustomerPassword.ParameterName = "@password";
            out_CustomerPassword.SqlDbType = SqlDbType.VarChar;
            out_CustomerPassword.Direction = ParameterDirection.Output;
            out_CustomerPassword.Size = 25;
            cmd.Parameters.Add(out_CustomerPassword);

            int result = cmd.ExecuteNonQuery();


            if (result == -1)
            {
                C_getdetails.id = C_custID;
                C_getdetails.c_contact = long.Parse(cmd.Parameters["@c_contact"].Value.ToString());
                C_getdetails.name = cmd.Parameters["@name"].Value.ToString();
                C_getdetails.c_email = cmd.Parameters["@c_email"].Value.ToString();
                C_getdetails.Dob = Convert.ToDateTime(cmd.Parameters["@dob"].Value.ToString());
                C_getdetails.pincode = int.Parse(cmd.Parameters["@pincode"].Value.ToString());
                C_getdetails.password = cmd.Parameters["@password"].Value.ToString();

                PinCode pin = new PinCode();
                pin.city = cmd.Parameters["@city"].Value.ToString();
                pin.state = cmd.Parameters["@state"].Value.ToString();
                pin.pin = int.Parse(cmd.Parameters["@pincode"].Value.ToString());
                C_getdetails.PinCode1 = pin;
            }
            else
            {
                C_getdetails.id = "C_Idnot";
            }
            con.Close();
            return C_getdetails;
        }
        public static string UpdateCustomer(Customer C_details)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("UpdateCustomerDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new SqlParameter("@Customer_id", C_details.id));

            cmd.Parameters.Add(new SqlParameter("@name", C_details.name));
            cmd.Parameters.Add(new SqlParameter("@c_contact", C_details.c_contact));
            cmd.Parameters.Add(new SqlParameter("@c_email", C_details.c_email));
            cmd.Parameters.Add(new SqlParameter("@dob", C_details.Dob));
            cmd.Parameters.Add(new SqlParameter("@Pin", C_details.pincode));
            cmd.Parameters.Add(new SqlParameter("@state", C_details.PinCode1.state));
            cmd.Parameters.Add(new SqlParameter("@city", C_details.PinCode1.city));
            cmd.Parameters.Add(new SqlParameter("@password", C_details.password));

            int result = cmd.ExecuteNonQuery();
            string res = "UnSuccessful";
            if (result == 1)

            {
                res = "Successful";

            }
            con.Close();
            return res;

        }
        #endregion

        #region Manager

        public static string AddManager(Manager M_details)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("InManagerDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter out_ManagerID = new SqlParameter();
            out_ManagerID.ParameterName = "@manager_id";
            out_ManagerID.SqlDbType = SqlDbType.VarChar;
            out_ManagerID.Direction = ParameterDirection.Output;
            out_ManagerID.Size = 15;
            cmd.Parameters.Add(out_ManagerID);



            cmd.Parameters.Add(new SqlParameter("@m_name", M_details.name));
            cmd.Parameters.Add(new SqlParameter("@m_password", M_details.password));
            cmd.Parameters.Add(new SqlParameter("@m_contact", M_details.m_contact));
            cmd.Parameters.Add(new SqlParameter("@m_email", M_details.m_email));

            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                M_details.id = cmd.Parameters["@manager_id"].Value.ToString();
            }
            else
            {
                M_details.id = "no_M_Id";

            }
            con.Close();
            return M_details.id;
        }
        public static Manager GetManager(string M_mangID)
        {
            Manager M_getdetails = new Manager();
            M_getdetails.id = M_mangID;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("GetManagerDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter out_CustomerName = new SqlParameter();
            out_CustomerName.ParameterName = "@name";
            out_CustomerName.SqlDbType = SqlDbType.VarChar;
            out_CustomerName.Direction = ParameterDirection.Output;
            out_CustomerName.Size = 50;
            cmd.Parameters.Add(out_CustomerName);

            SqlParameter out_CustomerPassword = new SqlParameter();
            out_CustomerPassword.ParameterName = "@password";
            out_CustomerPassword.SqlDbType = SqlDbType.VarChar;
            out_CustomerPassword.Direction = ParameterDirection.Output;
            out_CustomerPassword.Size = 25;
            cmd.Parameters.Add(out_CustomerPassword);

            SqlParameter out_CustomerContact = new SqlParameter();
            out_CustomerContact.ParameterName = "@m_contact";
            out_CustomerContact.SqlDbType = SqlDbType.BigInt;
            out_CustomerContact.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_CustomerContact);

            SqlParameter out_CustomerEmail = new SqlParameter();
            out_CustomerEmail.ParameterName = "@m_email";
            out_CustomerEmail.SqlDbType = SqlDbType.VarChar;
            out_CustomerEmail.Direction = ParameterDirection.Output;
            out_CustomerEmail.Size = 30;
            cmd.Parameters.Add(out_CustomerEmail);

            cmd.Parameters.Add(new SqlParameter("@manager_id", M_getdetails.id));


            int result = cmd.ExecuteNonQuery();
            if (result == -1)
            {
                M_getdetails.id = cmd.Parameters["@manager_id"].Value.ToString();
                M_getdetails.name = cmd.Parameters["@name"].Value.ToString();
                M_getdetails.password = cmd.Parameters["@password"].Value.ToString();
                M_getdetails.m_contact = long.Parse(cmd.Parameters["@m_contact"].Value.ToString());
                M_getdetails.m_email = cmd.Parameters["@m_email"].Value.ToString();
            }
            else
            {
                M_getdetails.id = "M_Idnot";

            }
            con.Close();
            return M_getdetails;
        }
        public static string UpdateManager(Manager M_details)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("UpdateManagerDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.Add(new SqlParameter("@manager_id", M_details.id));

            cmd.Parameters.Add(new SqlParameter("@name", M_details.name));
            cmd.Parameters.Add(new SqlParameter("@password", M_details.password));
            cmd.Parameters.Add(new SqlParameter("@m_contact", M_details.m_contact));
            cmd.Parameters.Add(new SqlParameter("@m_email", M_details.m_email));

            int result = cmd.ExecuteNonQuery();
            string res = "UnSuccessful";
            if (result == 1)
            {
                res = "Successful";

            }
            con.Close();
            return res;

        }
        #endregion

        #region Booking
        public static string AddBooking(Booking B_details)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("InBooking", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter out_BookingID = new SqlParameter();
            out_BookingID.ParameterName = "@Booking_id";
            out_BookingID.SqlDbType = SqlDbType.VarChar;
            out_BookingID.Direction = ParameterDirection.Output;
            out_BookingID.Size = 50;
            cmd.Parameters.Add(out_BookingID);

            cmd.Parameters.Add(new SqlParameter("@Customer_id", B_details.Customer_id));
            cmd.Parameters.Add(new SqlParameter("@Hotel_id", B_details.Hotel_id));
            int x = int.Parse(B_details.No_Of_AC_Rooms.ToString());
            cmd.Parameters.Add(new SqlParameter("@No_Of_AC_Rooms", x));
            x = int.Parse(B_details.No_Of_Non_AC_Rooms.ToString());
            cmd.Parameters.Add(new SqlParameter("@No_Of_Non_AC_Rooms", x));
            cmd.Parameters.Add(new SqlParameter("@T_id", B_details.T_id));

            cmd.Parameters.Add(new SqlParameter("@Date_booking", Convert.ToDateTime(B_details.Date_booking.ToString())));
            cmd.Parameters.Add(new SqlParameter("@Date_checkin", Convert.ToDateTime(B_details.Date_checkin.ToString())));
            cmd.Parameters.Add(new SqlParameter("@Date_checkout", Convert.ToDateTime(B_details.Date_checkout.ToString())));
            x = int.Parse(B_details.no_adult.ToString());
            cmd.Parameters.Add(new SqlParameter("@no_adult", x));
            x = int.Parse(B_details.no_child.ToString());
            cmd.Parameters.Add(new SqlParameter("@no_child", x));
            int result = 0;
            string res;


            result = cmd.ExecuteNonQuery();

            con.Close();
            if (result == 1)
            {
                res = cmd.Parameters["@Booking_id"].Value.ToString();

            }
            else
            {
                res = "Unable to generate ID";

            }

            return res;

        }
        public static Booking GetBookingDetails(string BookingID)
        {
            Booking B_details = new Booking();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetBookingDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            // new SqlParameter("@Booking_id", BookingID);
            cmd.Parameters.AddWithValue("@Booking_id", BookingID);

            SqlParameter Out_HotelID = new SqlParameter();
            Out_HotelID.ParameterName = "@Hotel_id";
            Out_HotelID.SqlDbType = SqlDbType.VarChar;
            Out_HotelID.Direction = ParameterDirection.Output;
            Out_HotelID.Size = 50;
            cmd.Parameters.Add(Out_HotelID);

            SqlParameter Out_Booking_Date = new SqlParameter();
            Out_Booking_Date.ParameterName = "@Booking_Date";
            Out_Booking_Date.SqlDbType = SqlDbType.Date;
            Out_Booking_Date.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(Out_Booking_Date);

            SqlParameter Out_Arrival_Date = new SqlParameter();
            Out_Arrival_Date.ParameterName = "@Arrival_Date";
            Out_Arrival_Date.SqlDbType = SqlDbType.Date;
            Out_Arrival_Date.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(Out_Arrival_Date);

            SqlParameter Out_Departure_Date = new SqlParameter();
            Out_Departure_Date.ParameterName = "@Departure_Date";
            Out_Departure_Date.SqlDbType = SqlDbType.Date;
            Out_Departure_Date.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(Out_Departure_Date);

            SqlParameter Out_Number_of_Adults = new SqlParameter();
            Out_Number_of_Adults.ParameterName = "@Number_of_Adults";
            Out_Number_of_Adults.SqlDbType = SqlDbType.Int;
            Out_Number_of_Adults.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(Out_Number_of_Adults);

            SqlParameter Out_Number_of_Children = new SqlParameter();
            Out_Number_of_Children.ParameterName = "@Number_of_Children";
            Out_Number_of_Children.SqlDbType = SqlDbType.Int;
            Out_Number_of_Children.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(Out_Number_of_Children);

            SqlParameter Out_Number_of_Nights = new SqlParameter();
            Out_Number_of_Nights.ParameterName = "@Number_of_Nights";
            Out_Number_of_Nights.SqlDbType = SqlDbType.Int;
            Out_Number_of_Nights.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(Out_Number_of_Nights);

            SqlParameter Out_Number_of_Ac_Rooms = new SqlParameter();
            Out_Number_of_Ac_Rooms.ParameterName = "@Number_of_AC_Rooms";
            Out_Number_of_Ac_Rooms.SqlDbType = SqlDbType.Int;
            Out_Number_of_Ac_Rooms.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(Out_Number_of_Ac_Rooms);

            SqlParameter Out_Number_of_Non_AC_Rooms = new SqlParameter();
            Out_Number_of_Non_AC_Rooms.ParameterName = "@Number_of_Non_AC_Rooms";
            Out_Number_of_Non_AC_Rooms.SqlDbType = SqlDbType.Int;
            Out_Number_of_Non_AC_Rooms.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(Out_Number_of_Non_AC_Rooms);

            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            B_details.Booking_id = BookingID;
            B_details.Hotel_id = Out_HotelID.SqlValue.ToString();
            B_details.no_adult = Convert.ToInt32(cmd.Parameters["@Number_of_Adults"].Value.ToString());
            B_details.no_child = Convert.ToInt32(cmd.Parameters["@Number_of_Children"].Value.ToString());
            B_details.no_nights = Convert.ToInt32(cmd.Parameters["@Number_of_Nights"].Value.ToString());
            B_details.No_Of_AC_Rooms = Convert.ToInt32(cmd.Parameters["@Number_of_AC_Rooms"].Value.ToString());
            B_details.No_Of_Non_AC_Rooms = Convert.ToInt32(cmd.Parameters["@Number_of_Non_AC_Rooms"].Value.ToString());
            B_details.Date_booking = Convert.ToDateTime(cmd.Parameters["@Booking_Date"].Value.ToString());
            B_details.Date_checkin = Convert.ToDateTime(cmd.Parameters["@Arrival_Date"].Value.ToString());
            B_details.Date_checkout = Convert.ToDateTime(cmd.Parameters["@Departure_Date"].Value.ToString());
            con.Close();
            return B_details;
        }
        public static Booking GetBookingDetailsCustomer(string CustomerId)
        {
            Booking model = new Booking();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetBookingDetailsCustomer", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Customer_id", CustomerId));

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                model.Booking_id = (string)reader[0];
                model.Customer_id = (string)reader[1];
                model.Hotel_id = (string)reader[2];
                model.No_Of_AC_Rooms = (int)reader[3];
                model.No_Of_Non_AC_Rooms = (int)reader[4];
                model.T_id = (string)reader[5];
                model.Date_booking = (DateTime)reader[6];
                model.Date_checkin = (DateTime)reader[7];
                model.Date_checkout = (DateTime)reader[8];
                model.no_adult = (int)reader[9];
                model.no_child = (int)reader[10];
                model.no_nights = (int)reader[11];




            }
            else
            {
                model.Booking_id = "No Booking";

            }
            return model;


        }
        public static string UpdateBookingDetails(string BookingId, int No_Of_Ac_Rooms, int No_Of_Non_Ac_Rooms, int No_Of_Adults, int No_Of_Children, DateTime Date_Of_ChekInn, DateTime Date_Of_CheckOut)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UpdateBookingDetails", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Booking_id", BookingId));
            cmd.Parameters.Add(new SqlParameter("@No_Of_Ac_Rooms", No_Of_Ac_Rooms));
            cmd.Parameters.Add(new SqlParameter("@No_Of_Non_Ac_Rooms", No_Of_Non_Ac_Rooms));
            cmd.Parameters.Add(new SqlParameter("@No_Of_Adults", No_Of_Adults));
            cmd.Parameters.Add(new SqlParameter("@No_Of_Children", No_Of_Children));
            cmd.Parameters.Add(new SqlParameter("@Date_Of_ChekInn", Date_Of_ChekInn));
            cmd.Parameters.Add(new SqlParameter("@Date_Of_CheckOut", Date_Of_CheckOut));

            int num = cmd.ExecuteNonQuery();
            string result;
            if (num > 0)
                result = "Booking Details Updated Succesfully";
            else result = "Sorry.. Failed to update details";
            con.Close();
            return result;
        }
        #endregion

    }
}


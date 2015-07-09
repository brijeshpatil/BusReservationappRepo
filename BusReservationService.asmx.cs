using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BusReservationWebServiceWebApplication
{
    /// <summary>
    /// Summary description for BusReservationService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BusReservationService : System.Web.Services.WebService
    {

        SqlConnection con = new SqlConnection(@"Server=0398993d-d6ad-43e4-afca-a4d000bb6e21.sqlserver.sequelizer.com;Database=db0398993dd6ad43e4afcaa4d000bb6e21;User ID=czsmvzeuseqqadhz;Password=8Q5qU2s7dpQRMwJQc6e6M4SpFv6aG6gNHP6okiUnMTyHnAjvJ5Ldetrd6PSx5sxg;");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;


        //[WebMethod]
        //public string HelloWorld() {
        //    return "Hello World";
        //}

        [WebMethod]
        public DataSet CheckUserLogin(string UserName, string Password)
        {
            da = new SqlDataAdapter("select * from registeredUser where login=@UName and pass=@Pass", con);
            da.SelectCommand.Parameters.AddWithValue("@UName", UserName);
            da.SelectCommand.Parameters.AddWithValue("@Pass", Password);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetActiveReservationEntry(string Exp)
        {
            da = new SqlDataAdapter("select distinct * from reservationEntry where loginID like @Exp+'%' and mytripStatus='active'", con);
            da.SelectCommand.Parameters.AddWithValue("@Exp", Exp);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        [WebMethod]
        public DataSet GetBusinfoForBoarding(string BoardingPOint)
        {
            da = new SqlDataAdapter("select businfo.*,bpoints.boardingPoint from busInfo left join bpoints on busInfo.busName=bpoints.travelName where boardingPoint like @BName+'%'", con);
            da.SelectCommand.Parameters.AddWithValue("@BName", BoardingPOint);
            ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        [WebMethod]
        public DataSet GetBusInfoForDroppingPoint(string DroppingPoint)
        {
            da = new SqlDataAdapter("select businfo.*,dpoint.droppingPoint from busInfo left join dpoint on busInfo.busName=dpoint.travelName where departurePoint like @DPoint+'%'", con);
            da.SelectCommand.Parameters.AddWithValue("@DPoint", DroppingPoint);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        [WebMethod]
        public DataSet SearchBusByRating(string CityFrom, string CityTo)
        {
            da = new SqlDataAdapter("select distinct * from busInfo where cityFrom like @From+'%' and cityTo like @To+'%' order by ratings desc", con);
            da.SelectCommand.Parameters.AddWithValue("@From", CityFrom);
            da.SelectCommand.Parameters.AddWithValue("@To", CityTo);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetBusInfoForBoardingByRating(string BoardingPoint)
        {
            da = new SqlDataAdapter("select businfo.*,bpoints.boardingPoint from busInfo left join bpoints on busInfo.busName=bpoints.travelName where boardingPoint like @BName+'%' order by ratings desc", con);
            da.SelectCommand.Parameters.AddWithValue("@BName", BoardingPoint);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetBusInfoForBoardingByBusFair(string BoardingPoint)
        {
            da = new SqlDataAdapter("select businfo.*,bpoints.boardingPoint from busInfo left join bpoints on busInfo.busName=bpoints.travelName where boardingPoint like @BName+'%' order by rate asc", con);
            da.SelectCommand.Parameters.AddWithValue("@BName", BoardingPoint);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetBusInfoByBusFairLtoH(string CityFrom, string CityTO)
        {
            da = new SqlDataAdapter("select * from busInfo where cityFrom like @From+'%' and cityTo like @To+'%' order by rate asc", con);
            da.SelectCommand.Parameters.AddWithValue("@From", CityFrom);
            da.SelectCommand.Parameters.AddWithValue("@To", CityTO);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetBusInfoForBoardingByTravelHours(string BoardingPoints)
        {
            da = new SqlDataAdapter("select businfo.*,bpoints.boardingPoint from busInfo left join bpoints on busInfo.busName=bpoints.travelName where boardingPoint like @BName+'%' order by TravelHours asc", con);
            da.SelectCommand.Parameters.AddWithValue("@BName", BoardingPoints);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetBusInfoByTravelHoursLtoH(string CityFrom, string CityTo)
        {
            da = new SqlDataAdapter("select * from busInfo where cityFrom like @From+'%' and cityTo like @To+'%' order by TravelHours asc", con);
            da.SelectCommand.Parameters.AddWithValue("@From", CityFrom);
            da.SelectCommand.Parameters.AddWithValue("@To", CityTo);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetSeatStatus(string SeatStatus)
        {
            da = new SqlDataAdapter("select * from seatStatus where travelName like @Status+'%'", con);
            da.SelectCommand.Parameters.AddWithValue("@Status", SeatStatus);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet SearchBus(string BusName, string CityFrom, string CityTO)
        {
            da = new SqlDataAdapter("select * from busInfo where busName like @BName+'%' and cityFrom like @From+'%' and cityTo like @To+'%'", con);
            da.SelectCommand.Parameters.AddWithValue("@BName", BusName);
            da.SelectCommand.Parameters.AddWithValue("@From", CityFrom);
            da.SelectCommand.Parameters.AddWithValue("@To", CityTO);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetBoardingPointByTravelName(string TravelName, string CityFrom, string CityTo)
        {
            da = new SqlDataAdapter("select * from bpoints where travelName like @TName+'%' and cityFrom like @From+'%' and cityTo like @To+'%' ", con);
            da.SelectCommand.Parameters.AddWithValue("@TName", TravelName);
            da.SelectCommand.Parameters.AddWithValue("@From", CityFrom);
            da.SelectCommand.Parameters.AddWithValue("@To", CityTo);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetAllACbusses(string CityFrom, string CityTo)
        {
            da = new SqlDataAdapter("select * from busInfo where cityFrom like @From+'%' and cityTo like @To+'%' and busType = 'A/C Seater/Sleeper(2+1)'", con);
            da.SelectCommand.Parameters.AddWithValue("@From", CityFrom);
            da.SelectCommand.Parameters.AddWithValue("@To", CityTo);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetAllNonACbusses(string CityFrom, string CityTO)
        {
            da = new SqlDataAdapter("select * from busInfo where cityFrom like @From+'%' and cityTo like @To+'%' and busType = 'Non A/C Seater/Sleeper(2+1)'", con);
            da.SelectCommand.Parameters.AddWithValue("@From", CityFrom);
            da.SelectCommand.Parameters.AddWithValue("@To", CityTO);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetAllVOLVObusses(string CityFrom, string CityTo)
        {
            da = new SqlDataAdapter("select * from busInfo where cityFrom like @From+'%' and cityTo like @To+'%' and busType = 'Volvo A/C Multi Axle semi sleeper(2+2)'", con);
            da.SelectCommand.Parameters.AddWithValue("@From", CityFrom);
            da.SelectCommand.Parameters.AddWithValue("@To", CityTo);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetBoardingPoints(string CityFrom, string CityTo)
        {
            da = new SqlDataAdapter("select boardingPoint from bpoints where cityFrom like @From+'%' and cityTo like @To+'%'", con);
            da.SelectCommand.Parameters.AddWithValue("@From", CityFrom);
            da.SelectCommand.Parameters.AddWithValue("@To", CityTo);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetAllDropingPointsByCity(string CityFrom, string CityTo)
        {
            da = new SqlDataAdapter("select droppingPoint from dpoint where cityFrom like @From+'%' and cityTo like @To+'%'", con);
            da.SelectCommand.Parameters.AddWithValue("@From", CityFrom);
            da.SelectCommand.Parameters.AddWithValue("@To", CityTo);
            ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        [WebMethod]
        public DataSet GetLiveTrackingInfoOfBus(string CityFrom, string CityTo)
        {
            da = new SqlDataAdapter("select distinct * from busInfo where cityFrom like @From+'%' and cityTo like @To+'%' and liveTracking like 'With live tracking", con);
            da.SelectCommand.Parameters.AddWithValue("@From", CityFrom);
            da.SelectCommand.Parameters.AddWithValue("@To", CityTo);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetHighRatedBusses(string CityFrom, string CityTo)
        {
            da = new SqlDataAdapter("select distinct * from busInfo where cityFrom like @From+'%' and cityTo like @To+'%' and ratedBus like 'high rated' order by ratings desc", con);
            da.SelectCommand.Parameters.AddWithValue("@From", CityFrom);
            da.SelectCommand.Parameters.AddWithValue("@To", CityTo);
            ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        [WebMethod]
        public string UpdateUserOTP(int OTP, string LoginName)
        {
            string Status = "";
            cmd = new SqlCommand("update registeredUser set OTP=@otp where login like @Login+'%'", con);
            cmd.Parameters.AddWithValue("@otp", OTP);
            cmd.Parameters.AddWithValue("@Login", LoginName);
            con.Open();
            Status = cmd.ExecuteNonQuery().ToString();
            con.Close();
            if (Status == "")
            {
                return "Update Failed";
            }
            else
            {
                return "Updated";
            }


        }

        [WebMethod]
        public DataSet GetReservationDetailByLogin(string LoginName)
        {
            da = new SqlDataAdapter("select distinct * from reservationEntry where loginID like @Login+'%' and ticketStatus='active' and mytripStatus='active", con);
            da.SelectCommand.Parameters.AddWithValue("@Login", LoginName);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public string ResetPassword(string LoginName, string Password)
        {
            string Status = "";
            cmd = new SqlCommand("update registeredUser set pass=@pass+'%' where login like @Login+'%'", con);
            cmd.Parameters.AddWithValue("@pass", Password);
            cmd.Parameters.AddWithValue("@Login", LoginName);
            con.Open();
            Status = cmd.ExecuteNonQuery().ToString();
            con.Close();
            if (Status == "")
            {
                return "Update Failed";
            }
            else
            {
                return "Updated";
            }
        }


        [WebMethod]
        public string RegNewUser(string fname, string eid, string login, string pass, int no, string addr, int OTP, string lname)
        {
            string Status = "";
            cmd = new SqlCommand("insert into registeredUser values(@fname,@eid,@login,@pass,@no,@addr,'active',@OTP,@lname)", con);
            cmd.Parameters.AddWithValue("@fname", fname);
            cmd.Parameters.AddWithValue("@eid", eid);
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@pass", pass);
            cmd.Parameters.AddWithValue("@no", no);
            cmd.Parameters.AddWithValue("@addr", addr);
            cmd.Parameters.AddWithValue("@OTP", OTP);
            cmd.Parameters.AddWithValue("@lname", lname);
            con.Open();
            Status = cmd.ExecuteNonQuery().ToString();
            con.Close();
            if (Status == "")
            {
                return "Registration Failed";
            }
            else
            {
                return "Registered";
            }
        }

        [WebMethod]
        public string UpdateUserProfile(string fname, string eid, string login, string newlogin, string pass, int no, string addr, int OTP, string lname)
        {
            string Status = "";
            cmd = new SqlCommand("update registeredUser set firstName=@fname,eid=@eid,login=@newlogin,pass=@pass,no=@no,addr=@addr,lastName=@lname where login=@login", con);
            cmd.Parameters.AddWithValue("@fname", fname);
            cmd.Parameters.AddWithValue("@eid", eid);
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@newlogin", newlogin);
            cmd.Parameters.AddWithValue("@pass", pass);
            cmd.Parameters.AddWithValue("@no", no);
            cmd.Parameters.AddWithValue("@addr", addr);
            cmd.Parameters.AddWithValue("@OTP", OTP);
            cmd.Parameters.AddWithValue("@lname", lname);
            con.Open();
            Status = cmd.ExecuteNonQuery().ToString();
            con.Close();
            if (Status == "")
            {
                return "Registration Failed";
            }
            else
            {
                return "Registered";
            }
        }

        [WebMethod]
        public DataSet GetReservationInfoByTicket(string TicketNo)
        {
            da = new SqlDataAdapter("select * from reservationEntry where ticketNumber like @TNo+''", con);
            da.SelectCommand.Parameters.AddWithValue("@TNo", TicketNo);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public string CancelTicket(string TicketNo)
        {
            string Status = "";
            cmd = new SqlCommand("update reservationEntry set ticketStatus='inactive' where ticketNumber=@TNO", con);
            cmd.Parameters.AddWithValue("@TNO", TicketNo);
            con.Open();
            Status = cmd.ExecuteNonQuery().ToString();
            con.Close();
            if (Status == "")
            {
                return "update Failed";
            }
            else
            {
                return "updated";
            }
        }

        [WebMethod]
        public string DeleteCancelledSeats(string TicketNo)
        {
            string Status = "";
            cmd = new SqlCommand("delete from seatStatus where ticketNumber=@TNO", con);
            cmd.Parameters.AddWithValue("@TNO", TicketNo);
            con.Open();
            Status = cmd.ExecuteNonQuery().ToString();
            con.Close();
            if (Status == "")
            {
                return "delete Failed";
            }
            else
            {
                return "deleted";
            }
        }

        [WebMethod]
        public string UpdatePreviousTrips(string LoginName)
        {
            string Status = "";
            cmd = new SqlCommand("update reservationEntry set mytripStatus='inactive' where loginID like @login+'%'", con);
            cmd.Parameters.AddWithValue("@login", LoginName);
            con.Open();
            Status = cmd.ExecuteNonQuery().ToString();
            con.Close();
            if (Status == "")
            {
                return "update Failed";
            }
            else
            {
                return "updated";
            }
        }

        [WebMethod]
        public DataSet GetReservationDetailsByEmail(string TicketNO, string EmailID)
        {
            da = new SqlDataAdapter("select * from reservationEntry where ticketNumber like @TNO+'%' and eid like @Email+'%'", con);
            da.SelectCommand.Parameters.AddWithValue("@TNO", TicketNO);
            da.SelectCommand.Parameters.AddWithValue("@Email", EmailID);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetPreviousTrips(string LoginName)
        {
            da = new SqlDataAdapter("select distinct * from reservationEntry where loginID like @login+'%'", con);
            da.SelectCommand.Parameters.AddWithValue("@login", LoginName);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet GetLastSequenceNo()
        {
            da = new SqlDataAdapter("select * from reservationEntry order by sequenceNumber desc limit 1", con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public string BookTicket(string cityFrom, string cityTo, string eid, string mobno, string fullName, string age, string gender, string seatNo, string priceTotal, string travelTime, string travelName, string travelType, string boardingPoint, DateTime dateOfTravelling, string loginID, string ticketNumber, string departurePoint, string departingTime, int sequenceNumber)
        {
            string Status = "";
            cmd = new SqlCommand("insert into reservationEntry (cityFrom, cityTo, eid, mobno,fullName, age, gender,seatNo,priceTotal,travelTime,travelName,travelType,boardingPoint,dateOfTravelling,loginID,ticketNumber,departurePoint,departingTime,ticketStatus,mytripStatus,sequenceNumber) values(@cityFrom,@cityTo,@eid,@mobno,@fullName,@age,@gender,@seatNo,@priceTotal,@travelTime,@travelName,@travelType,@boardingPoint,@dateOfTravelling,@loginID,@ticketNumber,@departurePoint,@departingTime,'active','active',@sequenceNumber)", con);
            cmd.Parameters.AddWithValue("@cityFrom", cityFrom);
            cmd.Parameters.AddWithValue("@cityTo", cityTo);
            cmd.Parameters.AddWithValue("@eid", eid);
            cmd.Parameters.AddWithValue("@mobno", mobno);
            cmd.Parameters.AddWithValue("@fullName", fullName);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@seatNo", seatNo);
            cmd.Parameters.AddWithValue("@priceTotal", priceTotal);
            cmd.Parameters.AddWithValue("@travelTime", travelTime);
            cmd.Parameters.AddWithValue("@travelName", travelName);
            cmd.Parameters.AddWithValue("@travelType", travelType);
            cmd.Parameters.AddWithValue("@boardingPoint", boardingPoint);
            cmd.Parameters.AddWithValue("@dateOfTravelling", dateOfTravelling);
            cmd.Parameters.AddWithValue("@loginID", loginID);
            cmd.Parameters.AddWithValue("@ticketNumber", ticketNumber);
            cmd.Parameters.AddWithValue("@departurePoint", departurePoint);
            cmd.Parameters.AddWithValue("@departingTime", departingTime);
            cmd.Parameters.AddWithValue("@sequenceNumber", sequenceNumber);
            con.Open();
            Status = cmd.ExecuteNonQuery().ToString();
            con.Close();
            if (Status == "")
            {
                return "insert Failed";
            }
            else
            {
                return "inserted";
            }

        }

        [WebMethod]
        public string InsertSeat(string travelName, string seatNumber, DateTime travelDate, string ticketNumber)
        {
            string Status = "";
            cmd = new SqlCommand("insert into seatStatus (travelName,seatStatus,seatNumber,travelDate,ticketNumber)values(@travelName,'1',@seatNumber,@travelDate,@ticketNumber)", con);
            cmd.Parameters.AddWithValue("@travelName", travelName);
            cmd.Parameters.AddWithValue("@seatNumber", seatNumber);
            cmd.Parameters.AddWithValue("@travelDate", travelDate);
            cmd.Parameters.AddWithValue("@ticketNumber", ticketNumber);
            con.Open();
            Status = cmd.ExecuteNonQuery().ToString();
            con.Close();
            if (Status == "")
            {
                return "insert Failed";
            }
            else
            {
                return "inserted";
            }

        }

    }
}

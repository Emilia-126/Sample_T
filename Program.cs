using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Test
namespace ConsoleApp1
{
    class Program
    {

        private static ILog log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {

            //log4net.Config.XmlConfigurator.Configure();
            DataTable dtA = initDATA();
            initDATAT_VALUE(ref dtA);

            DataTable dtB = initDATA();
            initDATAT_VALUE(ref dtB);

            string[] checks = new string[] { "3" , "7", "9"};
            foreach (DataRow row in dtB.Rows)
            {
                if (checks.Contains(row["IDENTIFY"].ToString().Substring(9, 1)))
                {
                    row["BOOKING_DTE"] = DBNull.Value;
                    row.AcceptChanges();
                }
                Console.WriteLine(row["IDENTIFY"].ToString() + " row Satate : " + row.RowState);

                var aRow = dtA.AsEnumerable().Where(o => o["IDENTIFY"].ToString() == row["IDENTIFY"].ToString()).SingleOrDefault();
                Console.WriteLine(aRow["IDENTIFY"].ToString() + " aRow Satate : " + aRow.RowState);

                if (!aRow.ItemArray.SequenceEqual(row.ItemArray))
                {
                    Console.WriteLine(row["IDENTIFY"].ToString() + " aRow not equals row");

                }
            }

            Console.WriteLine("End.");

            Console.ReadLine();        
        }

        static DataTable initDATA()
        {
            DataTable dtTABLE = new DataTable();

            dtTABLE.Columns.Add("IDENTIFY", typeof(string));
            dtTABLE.Columns.Add("BOOKING_NBR", typeof(string));
            dtTABLE.Columns.Add("BOOKING_DTE", typeof(DateTime));
            dtTABLE.Columns.Add("LOG_DTE", typeof(DateTime));

            return dtTABLE;
        }

        static void initDATAT_VALUE(ref DataTable datas)
        {
            DateTime LOG_DTE = new DateTime(2024, 9, 9);
            
            for (var i= 0; i <=10; i++)
            {
                TimeSpan time1 = new TimeSpan(5+i,  i, 10 + i, 10 + i);
                TimeSpan time2 = new TimeSpan(0, 0, 50 +i, 10 + i);

                string a = i.ToString().PadLeft(3, '0');
                string b = i.ToString().PadLeft(2, '0');

                datas.Rows.Add("4346009" + a, "BX-202401" + b, (DateTime)DateTime.Now + time1, (DateTime)LOG_DTE + time2);

            }
        
        }

    }
}

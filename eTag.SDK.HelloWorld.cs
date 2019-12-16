/* ==============================================================
                eTag.SDK HelloWorld (.NET Framework 4.0)
    Author:     Huang Hai Peng
    Email:      huanghaipeng@etag-tech.com
    Date:       2019-12-12
    Summary:    This HelloWorld demo show how to use eTag SDK.
   ============================================================== */
using eTag.SDK;
using eTag.SDK.Entity;
using eTag.SDK.Enum;
using eTag.SDK.Event;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace eTag.HelloWorld
{
    class Program
    {
        // Your AP's shop code here:
        static string SHOP_CODE = "0001";
        // Your AP's ID here:
        static string STATION_ID = "01";
        // Your ESL tags' ID here:
        static string[] ESL_ID = new string[] { "04018B2B", "0401C5DE", "0704FA24" };
        // Your PTL tags' ID here:
        static string[] PTL_ID = new string[] { "13053E14", "13053DE5", "13053DCC" };

        /// <summary>
        /// The main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            // Register station event handler and result event handler
            Console.WriteLine("Hello World!");
            Server.Instance.StationEventHandler += Instance_StationEventHandler;
            Server.Instance.ResultEventHandler += Instance_ResultEventHandler;
            // Start SDK
            Server.Instance.Start();

            // Press any key to start the 1st demo
            // Text mode
            Console.WriteLine("\r\nPress any key to start the 1st demo: Text mode");
            Console.ReadKey();

            var tag0 = GetTextTagEntity(ESL_ID[0], r.Next(65535));
            var tag1 = GetTextTagEntity(ESL_ID[1], r.Next(65535));
            var result0 = Server.Instance.Send(SHOP_CODE, STATION_ID, new List<TagEntity> { tag0, tag1 }, true, true);
            Console.WriteLine("Send Result:" + result0);
            Console.ReadKey();

            // Press any key to start the 2nd demo
            // Image mode
            Console.WriteLine("\r\nPress any key to start the 2nd demo: Image mode");
            Console.ReadKey();

            var tag2 = GetImageTagEntity(ESL_ID[2], r.Next(65535));
            var result1 = Server.Instance.Send(SHOP_CODE, STATION_ID, tag2, true, true);
            Console.WriteLine("Send Result:" + result0);
            Console.ReadKey();

            #region NOTE: ONLY FOR PTL
            /*
            // Press any key to start the 3rd demo
            // PTL mode
            Console.WriteLine("\r\nPress any key to start the 3rd demo: PTL mode");
            Console.ReadKey();

            var tag3 = GetPTLTagEntity(PTL_ID[0], r.Next(65535));
            var tag4 = GetPTLTagEntity(PTL_ID[1], r.Next(65535));
            var tag5 = GetPTLTagEntity(PTL_ID[2], r.Next(65535));
            var result2 = Server.Instance.Send(SHOP_CODE, STATION_ID, new List<TagEntity> { tag3, tag4, tag5 }, true, true);
            Console.WriteLine("Send Result:" + result2);
            Console.ReadKey();
            */
            #endregion

            // Exit
            Console.ReadKey();
            Console.ReadKey();
        }

        /// <summary>
        /// Get text tag entity
        /// </summary>
        /// <param name="tagID">Tag ID</param>
        /// <param name="token">Token</param>
        /// <returns>Tag entity</returns>
        private static TagEntity GetTextTagEntity(string tagID, int token)
        {
            return new TagEntity
            {
                TagID = tagID,                              // Tag ID, 
                Token = token,                              // Token
                G = true,                                   // Green color LED light turn on
                Before = false,                             // After refresh screen, flashing LED light
                Times = 60,                                 // LED light flashing times
                DataList = new List<DataEntity>             // Data list
                {
                    new TextEntity                          // Add a text entity
                    {
                        ID = 0,
                        Top = 1,                            // Location, top 1px
                        Left = 1,                           // Location, left 1px
                        Data = "Hello World! 你好 ABC123",    // Text data
                        InvertColor = true,                 // Yes, invert color
                        TextSize = TextSize.u16px           // Text size is 16px
                    },
                    new PriceEntity
                    {
                        ID = 1,
                        Top = 20,
                        Left = 10,
                        Data = "$9.87",
                        Color = FontColor.Red,
                        PriceSize = PriceSize.p32_16px
                    },
                    new BarcodeEntity
                    {
                        ID = 2,
                        Top = 60,
                        Left = 5,
                        Data = "1234567890",
                        BarcodeType = BarcodeType.Code128,
                        Height = 30
                    }
                },
            };
        }

        /// <summary>
        /// Get image tag entity
        /// </summary>
        /// <param name="tagID">Tag ID</param>
        /// <param name="token">Token</param>
        /// <returns>Tag entity</returns>
        private static TagEntity GetImageTagEntity(string tagID, int token)
        {
            return new TagEntity
            {
                TagID = tagID,                              // Tag ID, 
                Token = token,                              // Token
                G = true,                                   // Green color LED light turn on
                Before = false,                             // After refresh screen, flashing LED light
                Times = 60,                                 // LED light flashing times
                DataList = new List<DataEntity>             // Data list
                {
                    new ImageEntity                          // Add a text entity
                    {
                        ID = 0,
                        Top = 1,                            // Location, top 1px
                        Left = 1,                           // Location, left 1px
                        Data = new Bitmap("Test.bmp"),      // Image data
                    },
                },
            };
        }

        /// <summary>
        /// Get PTL tag entity
        /// </summary>
        /// <param name="tagID">Tag ID</param>
        /// <param name="token">Token</param>
        /// <returns>PTL tag entity</returns>
        private static TagEntity GetPTLTagEntity(string tagID, int token)
        {
            return new TagEntity
            {
                TagID = tagID,
                Token = token,
                G = true,
                Times = 100,
                DataList = new List<DataEntity>
                {
                    new SEG290Entity
                    {
                        Data = "1234",
                        AutoClean = true
                    }
                },
            };
        }

        /// <summary>
        /// Instance of result event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Instance_ResultEventHandler(object sender, ResultEventArgs e)
        {
            Console.WriteLine("Shop Code:{0}, AP:{1}, Result Type:{2}, Count:{3}", e.ShopCode, e.StationID, e.ResultType, e.ResultList.Count);
            foreach (var item in e.ResultList)
            {
                Console.WriteLine(" >> Tag ID:{0}, Status:{1}, Temperature:{2}, Power:{3}, Signal:{4}, Key: {5},Token:{6}, PLT:{7}",
                    item.TagID, item.TagStatus, item.Temperature, item.PowerValue, item.Signal, item.KeyType, item.Token, item.PtlNumber);
            }
        }

        /// <summary>
        /// Instance of station event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Instance_StationEventHandler(object sender, StationEventArgs e)
        {
            Console.WriteLine("Shop Code:{0} AP: {1} IP:{2} Online:{3}", e.ShopCode, e.StationID, e.IP, e.Online);
        }
    }
}

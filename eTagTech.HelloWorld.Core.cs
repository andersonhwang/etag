/* ==============================================================
                eTagTech.SDK.Core HelloWorld (.NET Core 3.1)
    Author:     Huang Hai Peng
    Email:      huanghaipeng@etag-tech.com
    Date:       2020-03-09
    Summary:    This HelloWorld demo show how to use eTag SDK.
   ============================================================== */
using eTag.SDK.Core;
using eTag.SDK.Core.Entity;
using eTag.SDK.Core.Enum;
using eTag.SDK.Core.Event;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace eTagTech.HelloWorld.Core
{
    class Program
    {
        // Your AP's shop code here:
        static string SHOP_CODE = "0001";
        // Your AP's ID here:
        static string STATION_ID = "01";
        // Your ESL tags' ID here:
        static string[] ESL_ID = new string[] { "002C63", "01C5DE", "04FA24" };
        // Your PTL tags' ID here:
        static string[] PTL_ID = new string[] { "1706F21D", "1706F21E", "13047CBA" };

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
            //var tag1 = GetTextTagEntity(ESL_ID[1], r.Next(65535));
            //var result0 = Server.Instance.Send(SHOP_CODE, STATION_ID, new List<TagEntity> { tag0, tag1 }, true, true);
            var result0 = Server.Instance.Send(SHOP_CODE, STATION_ID, new List<TagEntity> { tag0 }, true, true);
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

            // Press any key to start the 3rd demo
            // PTL mode - PTL290X
            Console.WriteLine("\r\nPress any key to start the 3rd demo: PTL mode - PTL290X");
            Console.ReadKey();

            var tag3 = GetPTLTagEntity5(PTL_ID[0], r.Next(65535), r.Next(99999));
            var tag4 = GetPTLTagEntity5(PTL_ID[1], r.Next(65535), r.Next(99999));
            var result2 = Server.Instance.Send(SHOP_CODE, STATION_ID, new List<TagEntity> { tag3, tag4 }, true, true);
            Console.WriteLine("Send Result:" + result2);
            Console.ReadKey();
            // Also you can change the pick number and press OK button to return.

            // Press any key to start the 4th demo
            // PTL mode - PTL4
            Console.WriteLine("\r\nPress any key to start the 4th demo: PTL mode - PTL4");
            Console.ReadKey();

            var tag5 = GetPTLTagEntity4(PTL_ID[2], r.Next(65535), r.Next(9999));
            var result3 = Server.Instance.Send(SHOP_CODE, STATION_ID, new List<TagEntity> { tag5 }, true, true);
            Console.WriteLine("Send Result:" + result2);
            Console.ReadKey();
            // Also you can change the pick number and press OK button to return.

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
                TagID = tagID,                                  // Tag ID, 
                Token = token,                                  // Token,
                TagType = ESLType.ESL213RH,
                G = false,                                      // Green color LED light turn on
                Before = false,                                 // After refresh screen, flashing LED light
                Times = 0,                                      // LED light flashing times
                DataList = new List<DataEntity>                 // Data list
                {
                    new RectangleEntity
                    {
                        Height = 122,
                        Width = 250,
                        ID = 0,
                        Left = 1,
                        Top = 1,
                        Color = FontColor.Red,
                        InvertColor  = true,
                    },
                    new PriceEntity
                    {
                        PriceSize = PriceSize.p24_12px,
                        Data = "12.90€",
                        ID = 1,
                        Top = 60,
                        Left = 1,
                        Color = FontColor.Red,
                    },
                    new TextEntity
                    {
                        TextSize = TextSize.u12px,
                        Data = "322.50 €/L",
                        ID = 2,
                        Top = 90,
                        Left = 45,
                        Color = FontColor.Black,
                    },
                    new TextEntity
                    {
                        TextSize = TextSize.u12px,
                        Data = "5038483985076",
                        ID = 3,
                        Top = 40,
                        Left  = 1,
                        Color = FontColor.Black,
                    },
                    new TextEntity
                    {
                        TextSize = TextSize.u16px,
                        Data = "SENSILUBE L",
                        ID = 4,
                        Top = 1,
                        Left = 1,
                        Color = FontColor.Black,
                    },
                    new TextEntity
                    {
                        TextSize = TextSize.u12px,
                        Data = "ST",
                        ID = 5,
                        Top = 90,
                        Left = 1,
                        Color = FontColor.Black,
                    },
                    new TextEntity
                    {
                        TextSize = TextSize.u12px,
                        Data = "16",
                        ID = 6,
                        Top = 90,
                        Left = 20,
                        Color = FontColor.Black,
                    },
                    new LineEntity
                    {
                        ID = 7,
                        LineType = LineType.Vline,
                        Data = 132,
                        Top = 10,
                        Left = 106,
                        Color = FontColor.Black,
                    },
                    new PriceEntity
                    {
                        PriceSize = PriceSize.p24_12px,
                        Data = "9.90€",
                        ID = 9,
                        Top = 60,
                        Left = 110,
                        Color = FontColor.Red,
                    },
                    new TextEntity
                    {
                        TextSize = TextSize.u12px,
                        Data = "ST",
                        ID = 10,
                        Top = 90,
                        Left = 110,
                        Color = FontColor.Black,
                    },
                    new TextEntity
                    {
                        TextSize = TextSize.u12px,
                        Data = "3401040395076",
                        ID = 11,
                        Top = 40,
                        Left = 110,
                        Color = FontColor.Black,
                    },
                    new TextEntity
                    {
                        TextSize = TextSize.u12px,
                        Data = "6",
                        ID = 12,
                        Top = 90,
                        Left = 130,
                        Color = FontColor.Black,
                    },
                    new TextEntity
                    {
                        TextSize = TextSize.u12px,
                        Data = "330.00 €/L",
                        ID = 13,
                        Top = 90,
                        Left = 150,
                        Color = FontColor.Black,
                    },
                    new TextEntity
                    {
                        TextSize = TextSize.u16px,
                        Data = "SAFORELLE L",
                        ID = 14,
                        Top = 1,
                        Left = 110,
                        Color = FontColor.Black,
                    },
                    new TextEntity
                    {
                        TextSize = TextSize.u16px,
                        Data = "UBR GEL FL",
                        ID = 15,
                        Top = 20,
                        Left = 110,
                        Color = FontColor.Black,
                    },
                    new TextEntity
                    {
                        TextSize = TextSize.u12px,
                        Data = "width ",
                        ID = 16,
                        Top = 2,
                        Left = 212,
                        Color = FontColor.Black,
                    },
                    new TextEntity
                    {
                        TextSize = TextSize.u12px,
                        Data = "height ",
                        ID = 17,
                        Top = 104,
                        Left = 2,
                        Color = FontColor.Black,
                    },
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
                TagID = "04FA24",                           // Tag ID, 
                TagType = ESLType.ESL213R,
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
                        Data = new Bitmap("1.bmp"),         // Image data
                    },
                },
            };
        }

        /// <summary>
        /// Get PTL tag entity with PTL4
        /// </summary>
        /// <param name="tagID">Tag ID</param>
        /// <param name="token">Token</param>
        /// <returns>PTL tag entity</returns>
        private static TagEntity GetPTLTagEntity4(string tagID, int token, int number)
        {
            return new TagEntity
            {
                TagID = tagID,                  // Tag ID
                Token = token,                  // Token
                G = true,                       // Green LED light
                Times = 100,                    // LED refreshing times, 0.25/sec(or depends on hardware config)
                DataList = new List<DataEntity> // Data list(only one PickNumberEntity)
                {
                    new PickNumberEntity        // Pick number entity
                    {
                        Data = number,          // Data
                        AutoClean = false        // Auto clean
                    }
                },
            };
        }

        /// <summary>
        /// Get PTL tag entity with PTL290X
        /// </summary>
        /// <param name="tagID">Tag ID</param>
        /// <param name="token">Token</param>
        /// <returns>PTL tag entity</returns>
        private static TagEntity GetPTLTagEntity5(string tagID, int token, int number)
        {
            return new TagEntity
            {
                TagID = tagID,                      // Tag ID
                Token = token,                      // Token
                G = true,                           // Green LED light
                Times = 100,                        // LED refreshing times, 0.25/sec(or depends on hardware config)
                DataList = new List<DataEntity>     // Data list
                {
                    new TextEntity                  // Text entity
                    {
                        Top = 1,                    // Location(default is 1)
                        Left = 1,                   // Location(default is 1)
                        Data = "Hello World!",      // Hello World text
                        TextSize = TextSize.u12px,  // Text size
                    },
                    new BarcodeEntity               // Barcode entity
                    {
                        Top = 20,                   // Location
                        Left = 1,                   // Location(default is 1)
                        Data = "123456789",         // Barcode value
                        BarcodeType = BarcodeType.Code128,  // Barcode type, default is Code128
                    },
                    new PickNumberEntity            // Pick number entity
                    {
                        Top = 50,                   // Location 
                        Left = 100,                 // Location
                        Data = number,              // Pick number
                        AutoClean = false,          // Auto clean screen after press OK button: False
                        NumberSize = NumberSize.n32_16px,   // Number size, default is 32*16px
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

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using AspNet7.Core.Entities;

namespace AspNet7.Infrastructure.Data
{
    public class AspNet7DataSeed
    {

        public static async Task SeedAsync(AspNet7DataContext aspNet7DataContext, bool migrateDatabase = false)
        {
            // TODO: Only run this if using a real database
            if (migrateDatabase)
                await aspNet7DataContext.Database.MigrateAsync();
            await aspNet7DataContext.Database.MigrateAsync();

            // categories - specifications
            await SeedCategoriesAsync(aspNet7DataContext);

            // products
            await SeedProductsAsync(aspNet7DataContext);

            //addresses
            await SeedAddressesAsync(aspNet7DataContext);

            // customers
            await SeedCustomersAsync(aspNet7DataContext);

            // order and order items
            await SeedOrderAndItemsAsync(aspNet7DataContext);

            // users
            await SeedUsersAsync(aspNet7DataContext);
        }

        private static async Task SeedCategoriesAsync(AspNet7DataContext dataContext)
        {
            if (!dataContext.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category() { Name = "Laptop", Description= "Laptop", IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now}, // 1
                    new Category() { Name = "Drone", Description= "Drone", IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now}, // 2
                    new Category() { Name = "TV & Audio", Description= "TV & Audio", IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now}, // 3
                    new Category() { Name = "Phone & Tablet", Description= "Phone & Tablet", IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now}, // 4
                    new Category() { Name = "Camera & Printer", Description= "Camera & Printer", IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now}, // 5
                    new Category() { Name = "Games", Description= "Games", IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now}, // 6
                    new Category() { Name = "Accessories", Description= "Accessories", IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now}, // 7
                    new Category() { Name = "Watch", Description= "Watch", IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now}, // 8
                    new Category() { Name = "Home & Kitchen Appliances", Description= "Home & Kitchen Appliances", IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now} // 9
                };

                await dataContext.Categories.AddRangeAsync(categories);
                await dataContext.SaveChangesAsync();
            }
        }

        private static async Task SeedProductsAsync(AspNet7DataContext aspNet5DataContext)
        {
            if (!aspNet5DataContext.Products.Any())
            {
                 var products = new List<Product>
                {
                    // Phone
                    new Product()
                    {
                        Code = "ONEPLUS-3T",
                        Name = "OnePlus 3T",
                        Summary = "3T by One_Plus (Gunmetal, 6GB RAM + 64GB Memory)",
                        Description = "The 3T stays true to our Never Settle approach. Impressive specs mean nothing if you don’t have a great experience every time you pick up your phone. The 3T delivers the best user experience, thanks to the latest hardware upgrades and carefully tested software enhancements. It was developed with care, craftsmanship and in collaboration with our community.",
                        ImageFile = "product-1.png",
                        UnitPrice = 295,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Phone & Tablet").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "ONEPLUS-8",
                        Name = "OnePlus 8",
                        Summary = "OnePlus 8 packs an octa-core setup comprising of 2.96GHz single-core, 2.42GHz tri-core, 1.8GHz quad-core, Kryo 485 processors. It is seated on Qualcomm Snapdragon 855 Plus chipset, which helps to handle the overall processing with great efficiency. There is an Adreno 640 GPU and a massive 8GB RAM",
                        ImageFile = "product-17.png",
                        UnitPrice = 285,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Phone & Tablet").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "REDMEN8-PRO",
                        Name = "Redmi Note 8 Pro",
                        Summary = "Redmi Note 8 Pro(Neptune Blue, 4GB RAM, 64GB Storage)",
                        Description = "Colour: Blue | Size name: 64 GB The 16cm(6.3) FHD+ display with 2.5D glass design. Qualcomm Snapdragon 665 octa-core processor with 11nm architecture; coupled with MIUI 10 system level optimisations enables low power consumption for a longer battery life. 48MP AI Quad camera with Ultra-wide angle lens and Super macro lens. Gorilla glass 5 design on both front and back for better durability. Dedicated SD card slot along with 2 nano SIM slots 4000mAh battery with 18W fast charger inside the box Note: Incase of any issue with the product, kindly contact brand at 1800 103 6286 between Service hours: 09: 00-21: 00.",
                        ImageFile = "product-24.png",
                        UnitPrice = 360,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Phone & Tablet").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "GALAXY-M31",
                        Name = "Samsung Galaxy M31",
                        Summary = "Samsung Galaxy M31 (Ocean Blue, 6GB RAM, 128GB Storage)",
                        Description = "With the Samsung Galaxy M31, Samsung re-introduces the powerful 6000 mAh battery along with all round features comprising of a 64 MP rear camera in Quad camera set up and an immersive sAmoled screen.",
                        ImageFile = "product-19.png",
                        UnitPrice = 220,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Phone & Tablet").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    // Camera                
                    new Product()
                    {
                        Code = "NIKON-D3500",
                        Name = "Nikon D3500 Digital SLR Camera",
                        Summary = "Nikon D3500 W/AF-P DX Nikkor 18-55mm f/3.5-5.6G VR with 16GB Memory Card and Carry Case (Black)",
                        Description = "The great companion for your photography needs, the advanced Nikon D3500 24 MP (18-55mm Lens) DSLR Camera is not only feature-rich, but also ergonomically designed and lightweight. The Nikon D3500, which is designed to be as flexible and intuitive as possible, while still offering the imaging capabilities you expect from a DSLR. Utilizing a DX-format 24.2MP CMOS sensor and EXPEED 4 image processor, the D3500 provides a native sensitivity range from ISO 100-25600 to suit working in a variety of lighting conditions. Effective pixels: 24.2 Megapixel Dust-reduction System Camera Format : DX / (1.5x Crop Factor) Memory Card Type: SD, SDHC, SDXC.",
                        ImageFile = "product-5.png",
                        UnitPrice = 145,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Camera & Printer").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "NIKON-D5600",
                        Name = "Nikon D5600 Digital Camera",
                        Summary = "Nikon D5600 Digital Camera 18-55mm VR Kit (Black)",
                        Description = "The new D5600 features 24.2 effective megapixels, an EXPEED 4 image-processing engine, and an ISO range of 100-25600 that captures beautiful and vibrant imagery, full HD videos and time-lapse movies even in low light situations. Inspiration also comes easy when you discover new perspectives with the vari-angle LCD monitor and intuitive touch interface with Bluetooth and the Nikon SnapBridge app for automatic transfer of images to your compatible smart devices, the D5600 is perfect for the connected world.",
                        ImageFile = "product-6.png",
                        UnitPrice = 199,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Camera & Printer").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "CANON-1500D",
                        Name = "Canon EOS 1500D 24.1MP Digital SLR Camera",
                        Summary = "Canon EOS 1500D 24.1MP Digital SLR Camera (Black) with 18-55 and 55-250mm is II Lens, 16GB Card and Carry Case",
                        Description = "All camera users, even beginners, will be able to capture amazing images and movies with this DSLR camera, which is equipped with a 24.1-megapixel APS-C-size CMOS sensor and an optical viewfinder for an authentic DSLR shooting experience. Capturing sharp images is easy thanks to the fast, accurate AF and the large grip that provides a firm, steady hold on the camera. Built-in Wi-Fi / NFC connectivity enables the seamless upload of photos and videos to social media.",
                        ImageFile = "product-7.png",
                        UnitPrice = 580,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Camera & Printer").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "PANASONIC-G7",
                        Name = "Panasonic LUMIX G7",
                        Summary = "Panasonic LUMIX G7 16.00 MP 4K Mirrorless Interchangeable Lens Camera Kit with 14-42 mm Lens (Black)",
                        Description = "The professional grade Panasonic LUMIX 4K Digital Camera DMC G7K accepts over 24 compact lens options built on the next generation interchangeable lens camera (ILC) standard (Micro Four Thirds) pioneered by Panasonic. Its “mirrorless” design enables a lighter, more compact camera body while also offering cutting edge video, audio, creative controls, wireless, intelligent focusing and exposure technologies not possible with traditional DSLRs. With the exclusive LUMIX 4K PHOTO (~8MP, 30/60 fps), simply pause that perfect moment from video to produce printable high resolution photos. A high resolution 17.5mm, 0.7x OLED eye viewfinder (2,360K dot) matches up to exactly how you intended to see the image even under direct sunlight. For connectivity convenience, the G7 includes 3.5mm and 2.5mm microphone & remote ports, USB 2.0 and a micro HDMI (Type D) terminal. It’s also compatible with newer UHS I/UHS II SDXC/SDHC SD cards capable of storing high resolution 4K videos and meeting the demands of 4K Photo and RAW mode burst shooting.",
                        ImageFile = "product-8.png",
                        UnitPrice = 320,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Camera & Printer").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    // Printer
                    new Product()
                    {
                        Code = "CANON-G2012",
                        Name = "Canon Pixma G2012 InkJet printer",
                        Summary = "Canon Pixma G2012 All-in-One Ink Tank Colour Printer (Black)",
                        Description = "Canon PIXMA G2012 NEW! Refillable Ink Tank All-In-One for High Volume Printing, Designed for high volume printing at low running cost - Print, Scan & Copy - ISO Standard print speed (A4): up to 8.8ipm (mono) / 5.0ipm (colour) - Photo Speed (4 x 6): 60sec",
                        ImageFile = "product-11.png",
                        UnitPrice = 210,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Camera & Printer").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    // Game                
                    new Product()
                    {
                        Code = "XBOX-ONES",
                        Name = "Xbox One S 1TB Console",
                        Summary = "Xbox One S 1TB Console – Roblox Bundle",
                        Description = "Own the Xbox One S 1TB Roblox Bundle and explore millions of immersive 3D worlds with a free-to-play download of Roblox, one wireless controller, and 1-Month of Xbox Game Pass Ultimate for unlimited access to over 100 games right out of the box.* Also included are three exclusive Roblox avatar bundles (Kijo the Vengeful Samurai, Metal Menace Mech, and Brawk Tyson: Featherweight Champ) and accessories (Kidomaru the Cursed Blade, Mecha Domino Crown, and World Championship Belt), as well as 2,500 Robux. Roblox digital content requires Xbox Live Gold (subscription sold separately).",
                        ImageFile = "product-3.png",
                        UnitPrice = 295,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Games").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "FANTOMP-XBOX",
                        Name = "Fantom Drives 1TB Xbox One",
                        Summary = "Fantom Drives 1TB Xbox One X SSD (Solid State Drive) - XSTOR - Easy Attachment Design for Seamless Look with 3 USB Ports 2.5 Inches - XOXA1000S",
                        Description = "You can get this FD 1 TB Xbox One X SSD - XSTOR Easy Attach Design for Seamless Look with 3 USB Ports .",
                        ImageFile = "product-13.png",
                        UnitPrice = 285,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Games").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    // Laptop      
                    new Product()
                    {
                        Code = "ACRE-ASPIRE3",
                        Name = "Acer Aspire 3 Laptop",
                        Summary = "Acer Aspire 3 Intel i3-10th Gen 15.6 - inch 1920 x 1080 Thin and Light Laptop (4GB Ram/1TB HDD/Window 10/Intel UHD Graphics/Black/1.9 kgs), A315-56",
                        Description = "Enjoy Premium touch and feel experience with the lastest Aspire 3 Thin series of Notebooks. The notebook features a 15.6 inch display, 4GB of DDR4 memory and a 1 TB fast HDD for superior performance at all conditions. A dream notebook at an attractive price point has always been Aspire 3's biggest USP.",
                        ImageFile = "product-1.png",
                        UnitPrice = 295,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Laptop").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    // Drone                
                    new Product()
                    {
                        Code = "DJI-TELLO",
                        Name = "DJI Tello Nano Drone (White)",
                        Summary = "DJI Tello Nano Drone (White) | 5MP Camera | 720p Recording | Intel Processor | Up to 13 mins of Flight time",
                        Description = "New to Drones No Problem: Flying Tello couldn’t be easier! Just pull out your phone to fly anytime or anywhere with intuitive controls. See the World from the Sky: Whether you’re at a park, in the office, or hanging out at home, you can always take off and experience the world from exciting new perspectives. Tello has two antennas that make video transmission extra stable and a high-capacity battery that offers impressively long flights times. Fantastic Features for Endless Enjoyment: Thanks to all the tech that Tello’s packing, like a flight controller powered by DJI, you can perform awesome and complex tricks and with just a tap on screen.5 Flying has never been so fun and easy! Capture Great Pictures and Videos: Equipped with a high-quality image processor, Tello shoots incredible photos and videos. Even if you don’t know how to fly, you can record pro-level videos with EZ Shots and share them on social media from your smartphone. Relax! Tello’s Super Safe: Tello's lightweight, yet durable design combined with software and hardware protections make it so you can always fly with confidence. Learn & Create: Play is an essential part of learning, so we made Tello programmable with Scratch, an MIT-developed coding system allows kids and teens to learn the basics of programming while having fun. If you’re a more advanced user, you can also develop software applications for Tello using the Tello SDK.",
                        ImageFile = "product-2.png",
                        UnitPrice = 275,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Drone").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    // Accessories
                    new Product()
                    {
                        Code = "ROXO-P47",
                        Name = "ROXO P47 Wireless Bluetooth Portable Sports Headphone",
                        Summary = "ROXO P47 Wireless Bluetooth Portable Sports Headphones with Microphone, Stereo Fm, Memory Card (Gold)",
                        Description = "With a combination of great sound, ergonomic design, and modern appeal the headphones are a great fit for anyone who wants to enjoy their tunes. Wear your headphones all day without discomfort. Alter the headband to fit perfectly and securely on your head. Ergonomic design provides remarkable comfort from soft leather ear cups. Made of quality materials, our headphones are ergonomically designed and perfect for daily use. Play your favorite PC games, sit back and enjoy the music tracks you love, or watch the latest popular movies with unparalleled sound quality in the comfort of your home. Moreover, the headphones are flexible enough to fit all head shapes, but not too loose so that they keep falling off - talk about many birds with one stone. Advanced active noise reduction technology quells airplane cabin noise, city traffic or a busy office, makes you focus on what you want to hear, enjoy your music, movies and videos. The noise cancellation function can work well both in wire and wireless mode. This Bluetooth headphones are equipped with Upgraded Soft Ear Cushions, which not only make it much more durability and comfort, but also make customers enjoy this quality, Long-listen feast. And the Skin texture, lightweight comfortable around-ear fit you can wear all day long. Answer the call, volume, ANC on/off, tuning control located on the headphones. Modern and stylish stretch design with reflective metal, giving the headphones a luxurious sense,while being sturdy and durable, storage for many years is still new. Simple and practical, unique style, obsessive listening. Built-in microphone design, hands-free calling, easy to switch to the next / last song, wireless headphones to enjoy it at your own volume range to experience the fun of headphones.",
                        ImageFile = "product-4.png",
                        UnitPrice = 110,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Accessories").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    // Watch
                    new Product()
                    {
                        Code = "SCLOUT-W34",
                        Name = "SClout W34 Fit Pro Series 4 - Smart Watch",
                        Summary = "SClout W34 Fit Pro Series 4 - Smart Watch with Calling Feature/Fitness Band/ECG Monitor/Activity Tracker/Full Touch Colored Display/Heart Rate Sensor/Notification Alert/Camera Control Features (Black).",
                        Description = "Bluetooth version 3.0, 4.0 support Bluetooth Calling Feature, 1.54 inch Full Touch HD Display, ECG data analysis Pedometer Time display, Step, Calories, distance, Sport mode, Raise your hand to brighten, Sleep monitoring, Running track, Anti-lost reminder, Bluetooth push SMS WeChat QQ news notification, Sedentary reminder, Alarm reminder,, Charging method USB charging, Case alloy Strap TPU material",
                        ImageFile = "product-9.png",
                        UnitPrice = 365,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Watch").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "MI-SMARTB4",
                        Name = "Mi Smart Band 4- No.1 Fitness Band",
                        Summary = "Mi Smart Band 4- No.1 Fitness Band, Up-to 20 Days Battery Life, Color AMOLED Full-Touch Screen, Waterproof with Music Control and Unlimited Watch Faces",
                        Description = "Step Up, Live More with the all new Mi Smart Band 4 which comes with a 39% larger, color AMOLED full touch display.",
                        ImageFile = "product-10.png",
                        UnitPrice = 189,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Watch").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    // TV & Audio
                    new Product()
                    {
                        Code = "MI-TV4APRO",
                        Name = "Mi TV 4A PRO 80 cm (32 inches) HD Ready Android LED TV",
                        Summary = "Mi TV 4A PRO 80 cm (32 inches) HD Ready Android LED TV (Black) | With Data Saver",
                        Description = "The Mi TV 4A Pro 32 features a HD-Ready display that provides an incredible level of detail and color fidelity for truly entertaining viewing. The 20W speakers with DTS-HD deliver room-filling sound to complete the spectacle. Exciting binge-worthy content comes to the improved PatchWall 3.0 comes with popular apps Netflix, Prime Video, Disney+Hotstar, YouTube, Zee5 and more. Entertainment unlimited with upto 20+ content partners deeply integrated in Mi TV and 5000+ apps and games available on Play Store.",
                        ImageFile = "product-12.png",
                        UnitPrice = 210,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "TV & Audio").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "UA32T4340AKXXL",
                        Name = "Samsung 80 cms HD LED Smart TV",
                        Summary = "Samsung 80 cm (32 Inches) Wondertainment Series HD Ready LED Smart TV UA32T4340AKXXL (Glossy Black) (2020 Model)",
                        Description = "Samsung 80 cm (32 Inches) Wondertainment Series HD Ready LED Smart TV UA32T4340AKXXL (Glossy Black) (2020 Model)",
                        ImageFile = "product-16.png",
                        UnitPrice = 360,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "TV & Audio").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "TCL-65X4US",
                        Name = "TCL 163.8 cm (65 inches) X4 65X4US 4K QLED Certified Android Smart TV (Gray)",
                        Summary = "TCL 163.8 cm (65 inches) X4 65X4US 4K QLED Certified Android Smart TV (Gray)",
                        Description = "Global top 2 Television Brand India’s first android QLED TVTCL’s 65X4 has been designed to deliver the best, most immersive audio/video experience. The Quantum Dot QLED technology redefines on-screen viewing by replicating real-world color volumes, while MEMC (120 Hz) enhanced with TCL’s proprietary algorithm ensures that viewers can experience every detail of fast-moving, action-packed content on both TV and multimedia signals. Harman Kardon speakers deliver powerful, high-impact sounds, while Dolby’s advanced DTS post-processing technology delivers immersive surround sound by creating a rich acoustic field. Powered by 64-bit Quad-core CPU and Dual-core GPU, along with a 2.5 GB RAM and 16 GB storage, the 65X4 enables seamless multitasking and reduces non-responsiveness. And that’s not all! With advanced features such as voice search and Chromecast built-in, the 65X4 ensures that users have a non-stop, high-quality, smart entertainment at their fingertips.",
                        ImageFile = "product-18.png",
                        UnitPrice = 185,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "TV & Audio").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    // Home & Kitchen Appliances
                    new Product()
                    {
                        Code = "PANASONIC-ST266BFDG",
                        Name = "Panasonic 20L Solo Microwave Oven",
                        Summary = "Panasonic 20L Solo Microwave Oven (NN-ST266BFDG, Black, 51 Auto Menus)",
                        Description = "Bake scrumptious and healthy dishes instantly in the Panasonic NN-ST266BFDG Solo Microwave. This 20 L microwave allows you to cook food efficiently owing to its multistage cooking mechanism and five power levels. Its child-lock feature ensures safety. Make the most of its 51 auto-cook menus and whip up tasty dishes without any hassle. 20 Litres Capacity 5 Power Levels 51 auto cook menu Vapour Clean.",
                        ImageFile = "product-14.png",
                        UnitPrice = 210,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Home & Kitchen Appliances").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "IFB-25SC4",
                        Name = "IFB 25 Ltrs Convection Microwave Oven",
                        Summary = "IFB 25 L Convection Microwave Oven (25SC4, Metallic Silver)",
                        Description = "Keep your food warm for as long as an hour with IFB 25SC4 Microwave Oven. With features including deodorise and steam clean, it’s not just good food on the table, it’s also about a spotlessly clean oven at the end of the day even with a lot of cooking.",
                        ImageFile = "product-15.png",
                        UnitPrice = 365,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Home & Kitchen Appliances").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "MORPHY-AT-201",
                        Name = "Morphy Richards Toaster (White)",
                        Summary = "Morphy Richards AT-201 2-Slice 650-Watt Pop-Up Toaster (White)",
                        Description = "The Morphy Richards at 201 pop up toaster serves as an efficient kitchen aid that dishes out crispy toasts in minutes. It's an easy way to prepare light and uniformly browned toasts for breakfast. The automatic pop-up function of this Morphy Richards toaster helps you relax or do other household chores while it takes out your toasts at the right time. The hi-lift feature removes small slices of bread and makes it easy to clean the toaster. With 650W of power consumption, it is efficient enough to prepare toasts for you entire family.",
                        ImageFile = "product-20.png",
                        UnitPrice = 185,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Home & Kitchen Appliances").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "NB9-1249 / NB-201",
                        Name = "Nutribullet PRO High Speed Blender/Mixer/Smoothie Maker",
                        Summary = "Nutribullet PRO High Speed Blender/Mixer/Smoothie Maker - 900 Watts - 12 Pcs Set;Gold",
                        Description = "NutriBullet PRO - 12Pc Blender/Mixer System features a powerful 900-watt motor with unique extraction blades and exclusive cyclonic action to break down, pulverize, and emulsify whole fruits and vegetables better than any standard blender or juicer, creating silky-smooth nutrient- extracted beverages that nourish your system from the inside out. Great for those who are active in their lives and proactive about their health. The NutriBullet PRO makes ultra-nutritious NutriBlast smoothies to enjoy at home or on the go. Its compact size and simple assembly fit onto any countertop, while its cups, blades, and accessories rinse clean under the tap or in the dishwasher. NutriBullet PRO is easy to use and easy to clean, and even comes with membership to a recipe site that has hundreds of smoothie recipes.",
                        ImageFile = "product-21.png",
                        UnitPrice = 185,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Home & Kitchen Appliances").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "HMB55C453X",
                        Name = "Bosch 32 L Convection Microwave Oven",
                        Summary = "Bosch 32 L Convection Microwave Oven (HMB55C453X, Stainless Steel and Black)",
                        Description = "A complete cooking device with 5 modes of cooking: Convection for baking, Grilling, Combination, Tandoori, Reheating along with defrosting. Comes with a 2 year warranty on whole appliance and 7 year warranty on the Magnetron. Brings in perfection in cooking with perfectly even heating.",
                        ImageFile = "product-22.png",
                        UnitPrice = 185,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Home & Kitchen Appliances").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Product()
                    {
                        Code = "SR-WA22H(E)",
                        Name = "Panasonic 5.4-Litre Automatic Rice Cooker",
                        Summary = "Panasonic SR-WA22H(E) 5.4-Litre Automatic Rice Cooker (Apple Green)",
                        Description = "Panasonic presents rice cooker made of metal in apple green colour. This cooker is of 5.4 litres in capacity and keep away from corrosive liquids.",
                        ImageFile = "product-23.png",
                        UnitPrice = 130,
                        UnitsInStock = 10,
                        Star = 4.3,
                        CategoryId = aspNet5DataContext.Categories.FirstOrDefault(c => c.Name == "Home & Kitchen Appliances").CategoryId,
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    }
                };

                await aspNet5DataContext.AddRangeAsync(products);
                await aspNet5DataContext.SaveChangesAsync();
            }
        }

        private static async Task SeedAddressesAsync(AspNet7DataContext aspNet5DataContext)
        {
            if (!aspNet5DataContext.Addresses.Any())
            {
                var addresses = new List<Address>()
                {
                    new Address
                    {
                        AddressType ="Home Address",
                        AddressLine = "12 Homefield park",
                        City = "London",
                        Country = "UK",
                        State = "Surrey",
                        ZipCode = "SM12AN",
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Address
                    {
                        AddressType ="Office Address",
                        AddressLine = "1 St. Nicholas Way",
                        City = "London",
                        Country = "UK",
                        State = "Surrey",
                        ZipCode = "SM11AN",
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Address
                    {
                        AddressType ="Home Address",
                        AddressLine = "14 Weymouth court",
                        City = "Brooklyn",
                        Country = "US",
                        State = "New York",
                        ZipCode = "NY012",
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Address
                    {
                        AddressType = "Office Address",
                        AddressLine = "18 Litchfield road",
                        City = "Manhattan",
                        Country = "US",
                        State = "New York",
                        ZipCode = "NY033",
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    }
                };
                await aspNet5DataContext.AddRangeAsync(addresses);
                await aspNet5DataContext.SaveChangesAsync();
            }
        }

        private static async Task SeedCustomersAsync(AspNet7DataContext aspNet5DataContext)
        {
            if (!aspNet5DataContext.Customers.Any())
            {
                var customers = new List<Customer>()
                {
                    new Customer
                    {
                        Name = "John",
                        Surname = "Lewis",
                        Phone = "+4475267560",
                        DefaultAddressId = aspNet5DataContext.Addresses.FirstOrDefault(a => a.AddressType =="Home Address" && a.AddressLine == "12 Homefield park").AddressId,
                        Email = "AspNet5@outlook.com",
                        CitizenId = "55555555555",
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Customer
                    {
                        Name = "Adrian",
                        Surname = "Taylor",
                        Phone = "+4475267561",
                        DefaultAddressId = aspNet5DataContext.Addresses.FirstOrDefault(a => a.AddressType =="Office Address" && a.AddressLine == "18 Litchfield road").AddressId,
                        Email = "AspNet5@outlook.com",
                        CitizenId = "11111111111",
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    }
                };
                await aspNet5DataContext.AddRangeAsync(customers);
                await aspNet5DataContext.SaveChangesAsync();
            }
        }

        private static async Task SeedOrderAndItemsAsync(AspNet7DataContext aspNet5DataContext)
        {
            if (!aspNet5DataContext.Orders.Any())
            {
                var cust1 = aspNet5DataContext.Customers.FirstOrDefault(c => c.Name == "John");
                var cust2 = aspNet5DataContext.Customers.FirstOrDefault(c => c.Name == "Adrian");

                var orders = new List<Order>()
                {
                    new Order
                    {
                        CustomerId = cust1.CustomerId,
                        BillingAddressId = cust1.DefaultAddressId,
                        ShippingAddressId = cust1.DefaultAddressId,
                        Status = OrderStatus.Draft,
                        OrderItems = new List<OrderItem>
                        {
                            new OrderItem
                            {
                                ProductId = aspNet5DataContext.Products.AsNoTracking().FirstOrDefault(p => p.Code == "ONEPLUS-8").ProductId,
                                Quantity = 2,
                                UnitPrice = 295,
                                IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                            },
                            new OrderItem
                            {
                                ProductId = aspNet5DataContext.Products.AsNoTracking().FirstOrDefault(p => p.Code == "XBOX-ONES").ProductId,
                                Quantity = 1,
                                UnitPrice = 295,
                                IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                            },
                            new OrderItem
                            {
                                ProductId = aspNet5DataContext.Products.AsNoTracking().FirstOrDefault(p => p.Code == "MORPHY-AT-201").ProductId,
                                Quantity = 1,
                                UnitPrice = 185,
                                IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                            }
                        },
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    },
                    new Order
                    {
                        CustomerId = cust2.CustomerId,
                        BillingAddressId = cust2.DefaultAddressId,
                        ShippingAddressId = cust2.DefaultAddressId,
                        Status = OrderStatus.Draft,
                        OrderItems = new List<OrderItem>
                        {
                            new OrderItem
                            {
                                ProductId = aspNet5DataContext.Products.AsNoTracking().FirstOrDefault(p => p.Code == "ONEPLUS-8").ProductId,
                                Quantity = 2,
                                UnitPrice = 295,
                                IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                            },
                            new OrderItem
                            {
                                ProductId = aspNet5DataContext.Products.AsNoTracking().FirstOrDefault(p => p.Code == "XBOX-ONES").ProductId,
                                Quantity = 1,
                                UnitPrice = 295,
                                IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                            },
                            new OrderItem
                            {
                                ProductId = aspNet5DataContext.Products.AsNoTracking().FirstOrDefault(p => p.Code == "MORPHY-AT-201").ProductId,
                                Quantity = 1,
                                UnitPrice = 185,
                                IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                            }
                        },
                        IsDeleted=false, CreatedBy="admin", CreatedDate=DateTime.Now
                    }
                };

                await aspNet5DataContext.AddRangeAsync(orders);
                await aspNet5DataContext.SaveChangesAsync();
            }
        }

        private static async Task SeedUsersAsync(AspNet7DataContext aspNetDataContext)
        {
            if (!aspNetDataContext.Users.Any())
            {
                await Adduser(aspNetDataContext, "admin", "Welcome@123");
                await Adduser(aspNetDataContext, "isaac", "Welcome@123");
                //await aspNetDataContext.SaveChangesAsync();
            }
        }

        private static async Task Adduser(AspNet7DataContext aspNetDataContext, string userName, string password)
        {
            var user = aspNetDataContext.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                var hmac = new HMACSHA512();
                var passwordSaltHash = Tuple.Create(hmac.Key, hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));

                User newUser = new User() { UserName = userName, PasswordHash = passwordSaltHash.Item2, PasswordSalt = passwordSaltHash.Item1, IsDeleted = false, CreatedBy = "admin", CreatedDate = DateTime.Now };

                var result = await aspNetDataContext.Users.AddAsync(newUser);
                await aspNetDataContext.SaveChangesAsync();
            }
        }
    }
}

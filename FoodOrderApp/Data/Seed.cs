using FoodOrderApp.Models;
using FoodOrderApp.Data.Enum;
using FoodOrderApp.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace FoodOrderApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                // Foods
                if (!context.Foods.Any())
                {
                    context.Foods.AddRange(new List<Food>()
                    {
                        new Food
                        {
                            Id = "F001",
                            Name = "Nấm đùi gà xào cháy tỏi",
                            Image = "/img/products/nam-dui-ga-chay-toi.jpeg",
                            Description = "Một Món chay ngon miệng với nấm đùi gà thái chân hương, xào săn với lửa và thật nhiều tỏi băm, nêm nếm với mắm và nước tương chay, món ngon đưa cơm và rất dễ ăn cả cho người lớn và trẻ nhỏ.",
                            Price = 200000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F002",
                            Name = "Rau xào ngũ sắc",
                            Image = "/img/products/rau-xao-ngu-sac.png",
                            Description = "Rau củ quả theo mùa tươi mới xào với nước mắm chay, gia vị để giữ được hương vị ngọt tươi nguyên thủy của rau củ, một món nhiều vitamin và chất khoáng, rất dễ ăn.",
                            Price = 180000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food()
                        {
                            Id = "F003",
                            Name = "Bánh lava phô mai nướng",
                            Image = "/img/products/banh_lava_pho_mai_nuong.jpeg",
                            Description = "Bánh mang một vẻ ngoài hấp dẫn khó cưỡng, bạt bánh xốp mềm, lớp nhân kim sa trứng muối vàng óng láng mịn, bùi béo, sốt phomai nướng xém mặt thơm ngậy",
                            Price = 180000,
                            FoodCategory = FoodCategory.MonTrangMieng
                        },
                        new Food
                        {
                            Id = "F004",
                            Name = "Set lẩu thái Tomyum",
                            Image = "/img/products/lau_thai.jpg",
                            Description = "Lẩu Thái là món ăn xuất phát từ món canh chua Tom yum nổi tiếng của Thái Lan. Nước lẩu có hương vị chua chua cay cay đặc trưng. Các món nhúng lẩu gồn thịt bò, hải sản, rau xanh và các loại nấm.",
                            Price = 200000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F005",
                            Name = "Cơm chiên cua",
                            Image = "/img/products/com_chien_cua.png",
                            Description = "Lẩu Thái là món ăn xuất phát từ món canh chua Tom yum nổi tiếng của Thái Lan. Nước lẩu có hương vị chua chua cay cay đặc trưng. Các món nhúng lẩu gồn thịt bò, hải sản, rau xanh và các loại nấm.",
                            Price = 200000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F006",
                            Name = "Súp bào ngư hải sâm (1 phần)",
                            Image = "/img/products/sup-bao-ngu-hai-sam.jpeg",
                            Description = "Súp bào ngư Bếp Hoa có bào ngư kết hợp cùng sò điệp, tôm tươi... được hầm trong nhiều giờ với rau củ & nấm đông trùng tạo ra vị ngọt tự nhiên hiếm thấy. Một món ăn khiến cả người ốm cũng thấy ngon miệng đó ạ.",
                            Price = 540000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F007",
                            Name = "Tai cuộn lưỡi",
                            Image = "/img/products/tai-cuon-luoi.jpeg",
                            Description = "Tai heo được cuộn bên trong cùng phần thịt lưỡi heo. Phần tai bên ngoài giòn dai, phần thịt lưỡi bên trong vẫn mềm, có độ ngọt tự nhiên của thịt. Tai cuộn lưỡi được chấm với nước mắm và tiêu đen.",
                            Price = 340000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F008",
                            Name = "Xíu mại tôm thịt 10 viên",
                            Image = "/img/products/xiu_mai_tom_thit_10_vien.jpg",
                            Description = "Quý khách hấp chín trước khi ăn. Những miếng há cảo, sủi cảo, hoành thánh với phần nhân tôm, sò điệp, hải sản tươi ngon hay nhân thịt heo thơm ngậy chắc chắn sẽ khiến bất kỳ ai thưởng thức đều cảm thấy rất ngon miệng.",
                            Price = 140000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F009",
                            Name = "Trà phô mai kem sữa",
                            Image = "/img/products/tra-pho-mai-kem-sua.jpg",
                            Description = "Món Nước uống vừa béo ngậy, chua ngọt đủ cả mà vẫn có vị thanh của trà.",
                            Price = 34000,
                            FoodCategory = FoodCategory.NuocUong
                        },
                        new Food
                        {
                            Id = "F010",
                            Name = "Trà đào chanh sả",
                            Image = "/img/products/tra-dao-chanh-sa.jpg",
                            Description = "Trà đào chanh sả có vị đậm ngọt thanh của đào, vị chua chua dịu nhẹ của chanh và hương thơm của sả.",
                            Price = 25000,
                            FoodCategory = FoodCategory.NuocUong
                        },
                        new Food
                        {
                            Id = "F011",
                            Name = "Bánh chuối nướng",
                            Image = "/img/products/banh-chuoi-nuong.jpeg",
                            Description = "Bánh chuối nướng béo ngậy mùi nước cốt dừa cùng miếng chuối mềm ngon sẽ là Món tráng miệng phù hợp với mọi người.",
                            Price = 60000,
                            FoodCategory = FoodCategory.MonTrangMieng
                        },
                        new Food
                        {
                            Id = "F012",
                            Name = "Há cảo sò điệp (10 viên)",
                            Image = "/img/products/ha_cao.jpg",
                            Description = "Những miếng há cảo, sủi cảo, hoành thánh với phần nhân tôm, sò điệp, hải sản tươi ngon hay nhân thịt heo thơm ngậy chắc chắn sẽ khiến bất kỳ ai thưởng thức đều cảm thấy rất ngon miệng.",
                            Price = 140000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F013",
                            Name = "Chả rươi (100gr)",
                            Image = "/img/products/thit_nuong.jpg",
                            Description = "Chả rươi luôn mang đến hương vị khác biệt và 'gây thương nhớ' hơn hẳn so với các loại chả khác. Rươi béo càng ăn càng thấy ngậy. Thịt thơm quyện mùi thì là và vỏ quýt rất đặc sắc. Chắc chắn sẽ là một món ăn rất hao cơm",
                            Price = 60000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F014",
                            Name = "Nộm gà Hội An (1 phần)",
                            Image = "/img/products/nom_ga_hoi_an.png",
                            Description = "Nộm gà làm từ thịt gà ri thả đồi. Thịt gà ngọt, săn được nêm nếm vừa miệng, bóp thấu với các loại rau tạo thành món nộm thơm ngon, đậm đà, giải ngán hiệu quả.",
                            Price = 60000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F015",
                            Name = "Set bún cá (1 set 5 bát)",
                            Image = "/img/products/set_bun_ca.jpg",
                            Description = "Bún cá được làm đặc biệt hơn với cá trắm lọc xương và chiên giòn, miếng cá nhúng vào nước dùng ăn vẫn giòn dai, thơm ngon vô cùng.",
                            Price = 60000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F016",
                            Name = "Bún cá (1 phần)",
                            Image = "/img/products/set_bun_ca.jpg",
                            Description = "Bún cá được làm đặc biệt hơn với cá trắm lọc xương và chiên giòn, miếng cá nhúng vào nước dùng ăn vẫn giòn dai, thơm ngon vô cùng",
                            Price = 60000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F017",
                            Name = "Xôi trắng hành phi (1 phần)",
                            Image = "/img/products/bun_ca_hanh_phi.jpeg",
                            Description = "Bún cá được làm đặc biệt hơn với cá trắm lọc xương và chiên giòn, miếng cá nhúng vào nước dùng ăn vẫn giòn dai, thơm ngon vô cùng",
                            Price = 60000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F018",
                            Name = "Tôm sú lột rang thịt (1 phần)",
                            Image = "/img/products/tom_su_luot_ran_thit.png",
                            Description = "Tôm sú tươi rim với thịt. rim kỹ, vừa lửa nên thịt và tôm săn lại, ngấm vị, càng ăn càng thấy ngon.",
                            Price = 60000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F019",
                            Name = "Bánh cookie dừa",
                            Image = "/img/products/banh_cookie_dua.jpeg",
                            Description = "Bánh cookie dừa ngọt vừa miệng, dừa bào tươi nhào bánh nướng giòn tan, cắn vào thơm lừng, giòn rụm",
                            Price = 130000,
                            FoodCategory = FoodCategory.MonAnVat
                        },
                        new Food
                        {
                            Id = "F020",
                            Name = "Cá chiên giòn sốt mắm Thái",
                            Image = "/img/products/sot_mam_thai.jpeg",
                            Description = "Bánh cookie dừa ngọt vừa miệng, dừa bào tươi nhào bánh nướng giòn tan, cắn vào thơm lừng, giòn rụm",
                            Price = 130000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F021",
                            Name = "Tôm sú rang muối",
                            Image = "/img/products/tom-su-rang-muoi.jpeg",
                            Description = "Từng chú tôm sú được chọn lựa kĩ càng mỗi ngày, đảm bảo là tôm tươi sống, vẫn còn đang bơi khỏe. Tôm rang muối vừa đậm đà lại vẫn giữ được vị ngọt tự nhiên của tôm sú.",
                            Price = 550000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F022",
                            Name = "Tôm sú rang bơ tỏi (1 suất)",
                            Image = "/img/products/tom-su-rang-bo-toi.jpeg",
                            Description = "Tôm được chiên vàng giòn bên ngoài, bên trong thịt tôm vẫn mềm, kết hợp cùng sốt bơ tỏi thơm nức . Tôm tươi được Bếp Hoa chiên theo bí quyết riêng nên phần thịt tôm bên trong sẽ có hương vị thơm ngon đặc biệt, sốt bơ tỏi béo ngậy hấp dẫn. Ăn kèm bánh mỳ rất hợp",
                            Price = 550000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F023",
                            Name = "Combo Vịt quay và gỏi vịt",
                            Image = "/img/products/combo-vitquay-va-goivit.jpeg",
                            Description = "Combo vịt quay Bếp Hoa + gỏi vịt bắp cải size đại cực kỳ thích hợp cho những bữa ăn cần nhiều rau, nhiều đạm mà vẫn đảm bảo ngon miệng. Vịt quay chuẩn Macao giòn da thấm thịt, thêm phần gỏi vịt chua chua ngọt ngọt, rau tươi giòn ăn chống ngán, cân bằng dinh dưỡng.",
                            Price = 510000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F024",
                            Name = "Set cá cơm tầm",
                            Image = "/img/products/set_ca_tam.jpg",
                            Description = "Một 1 set với 3 món ngon mỹ mãn đủ 4 người ăn no, bếp trưởng tự tay chọn từng con cá tầm tươi đủ chất lượng để chế biến đủ 3 món gỏi, nướng, canh chua 10 điểm cho chất lượng.",
                            Price = 950000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F025",
                            Name = "Chả ốc 1 phần",
                            Image = "/img/products/cha_oc_1_phan.jpeg",
                            Description = "Chả ốc với ốc giòn tan, băm rối, trộn với thịt, lá lốt, rau thơm, nêm nếm vừa ăn và viên tròn, chiên cho giòn ngoài mềm trong. Ăn chả ốc kẹp với rau sống và chấm mắm chua ngọt cực kỳ đưa vị.",
                            Price = 350000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F026",
                            Name = "Gà ủ muối thảo mộc (1 con)",
                            Image = "/img/products/ga-u-muoi-thao-moc.png",
                            Description = "Gà ủ muối tuyển chọn từ gà ri tươi, ủ muối chín tới với gia vị thảo mộc tự nhiên, da gà mỏng, thịt chắc ngọt.",
                            Price = 450000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F027",
                            Name = "Gà không lối thoát (1 con)",
                            Image = "/img/products/ga-khong-loi-thoat.png",
                            Description = "Gà mái ghẹ size 1.4kg sơ chế sạch sẽ, tẩm ướp gia vị đậm đà, bọc vào trong xôi dẻo từ nếp cái hoa vàng, chiên cho giòn mặt ngoài. Khi ăn cắt phần xôi là gà thơm ngon nghi ngút khói, thịt gà ngấm mềm thơm, miếng xôi ngọt tự nhiên từ thịt gà ăn cực kỳ hấp dẫn.",
                            Price = 520000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F028",
                            Name = "Cá chiên giòn mắm Thái (1 con)",
                            Image = "/img/products/ca-chien-gion-mam-thai.jpeg",
                            Description = "Cá tươi bếp làm sạch, lạng đôi, ướp cho ngấm và chiên vàng giòn. Thịt cá bên trong óng ánh nước, mềm ngọt, bên ngoài giòn tan hấp dẫn. Thêm sốt mắm Thái đầu bếp làm công thức riêng, vị mắm chua ngọt cay the cực kỳ hợp với cá giòn nóng hổi.",
                            Price = 350000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F029",
                            Name = "Chân giò chiên giòn mắm Thái",
                            Image = "/img/products/chan-gio-chien-gion-mam-thai.jpeg",
                            Description = "Chân giò lợn đen chọn loại ngon, tỉ lệ nạc mỡ đều đặn, bếp xâm bì cẩn thận và ướp thật ngon, chiên vàng giòn nổi bóng, khi ăn chấm mắm chua ngọt cay cay cực kỳ ngon miệng.",
                            Price = 420000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F030",
                            Name = "Chả cốm (500gr)",
                            Image = "/img/products/cha-com.png",
                            Description = "Cốm mộc làng Vòng hạt dẹt dẻo và thơm đặc biệt, thịt lợn tươi phải chọn phần thịt vai xay vừa mềm lại không bở, trộn đều với cốm, nêm với mắm ngon, gia vị đơn giản và quật hỗn hợp thịt xay và cốm đến khi nào thật chắc và dẻo. Viên mỗi bánh chả phải đều tay, hấp sơ qua cho thành hình, khi ăn mới chiên vàng. Chả cốm khi cắn vào phải giòn và lại thật mềm, tứa nước trong miệng. Cốm dẻo dẻo cuộn trong thịt thơm ngon lạ kỳ.",
                            Price = 175000,
                            FoodCategory = FoodCategory.MonMan
                        }
                    });
                    context.SaveChanges();
                }

                // Order
                if (!context.Orders.Any())
                {
                    context.Orders.AddRange(new List<Order>()
                    {
                        new Order()
                        {
                            OrderDate =  new DateTime(2023, 11, 28, 12, 0, 0),
                            ReceiveDate = new DateTime(2023, 11, 28, 12, 30, 0),
                            OrderStatus = OrderStatusCategory.DangXuLy,
                            FormDelivery = FormDeliveryCategory.GiaoTanNoi,
                            Receiver = "Đình Nhật",
                            Location = "Liên Chiểu, Đà Nẵng",
                            PhoneNumber = "09878787654",
                            Note = "Không cần dụng cụ ăn uống",
                            AppUserId = "e0d5d7f5-71bc-472e-be95-38669cce1849", // Assuming you have an AppUser with this Id
                            Foods = new List<OrderDetail>
                            {
                                new OrderDetail { FoodId = "F001", Quantity = 2 },
                                new OrderDetail { FoodId = "F002", Quantity = 1 }
                            }
                        },
                         new Order()
                        {
                            OrderDate =  new DateTime(2023, 11, 28, 14, 0, 0),
                            ReceiveDate = new DateTime(2023, 11, 28, 14, 30, 0),
                            OrderStatus = OrderStatusCategory.DangXuLy,
                            FormDelivery = FormDeliveryCategory.GiaoTanNoi,
                            Receiver = "Đức Quang",
                            Location = "Hoàng Diệu, Hải Châu, Đà Nẵng",
                            PhoneNumber = "09878787254",
                            Note = "Không cần dụng cụ ăn uống",
                            AppUserId = "af689223-b6c2-4713-b6a6-2a443d1b4eae", // Assuming you have an AppUser with this Id
                            Foods = new List<OrderDetail>
                            {
                                new OrderDetail { FoodId = "F003", Quantity = 3 },
                                new OrderDetail { FoodId = "F004", Quantity = 2 }
                            }
                        }
                    });
                    context.SaveChanges();
                }

                // Cart
                if (!context.Carts.Any())
                {
                    context.Carts.AddRange(new List<Cart>()
                    {
                        new Cart()
                        {
                            Id = "CA001",
                            AppUserId = "e0d5d7f5-71bc-472e-be95-38669cce1849",
                            Foods = new List<CartDetail>
                            {
                                new CartDetail { Id = "CD001", FoodId = "F001", Quantity = 2, Noted = "Noted1"},
                                new CartDetail { Id = "CD002", FoodId = "F002", Quantity = 2, Noted = "Noted2"}
                            }
                        },
                        new Cart()
                        {
                            Id = "CA002",
                            AppUserId = "af689223-b6c2-4713-b6a6-2a443d1b4eae",
                            Foods = new List<CartDetail>
                            {
                                new CartDetail { Id = "CD003", FoodId = "F001", Quantity = 2},
                                new CartDetail { Id = "CD004", FoodId = "F002", Quantity = 2 }
                            }
                        },
                        new Cart()
                        {
                            Id = "CA003",
                            AppUserId = "4cac0fab-a14d-4f83-a1f1-398b5652604d",
                            Foods = new List<CartDetail>
                            {
                                new CartDetail { Id = "CD005", FoodId = "F001", Quantity = 2},
                                new CartDetail { Id = "CD006", FoodId = "F002", Quantity = 2 }
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        // Seed Users Data
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }
        }

        // Seeding Admin User Data
        public static async Task SeedAdminUsers(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "admin1@email.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        Name="Pham Van Nhat Huy1",
                        UserName = "Nhathuy1",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "@Abc123.");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}

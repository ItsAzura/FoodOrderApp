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
                        new Food()
                        {
                            Id = "F001",
                            Name = "Nấm đùi gà xào cháy tỏi",
                            Image = "~/img/products/nam-dui-ga-chay-toi.jpeg",
                            Description = "Một Món chay ngon miệng với nấm đùi gà thái chân hương, xào săn với lửa và thật nhiều tỏi băm, nêm nếm với mắm và nước tương chay, món ngon đưa cơm và rất dễ ăn cả cho người lớn và trẻ nhỏ.",
                            Price = 200000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food()
                        {
                            Id = "F002",
                            Name = "Rau xào ngũ sắc",
                            Image = "~/img/products/rau-xao-ngu-sac.png",
                            Description = "Rau củ quả theo mùa tươi mới xào với nước mắm chay, gia vị để giữ được hương vị ngọt tươi nguyên thủy của rau củ, một món nhiều vitamin và chất khoáng, rất dễ ăn.",
                            Price = 180000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food()
                        {
                            Id = "F003",
                            Name = "Bánh lava phô mai nướng",
                            Image = "~/img/products/banh_lava_pho_mai_nuong.jpeg",
                            Description = "Bánh mang một vẻ ngoài hấp dẫn khó cưỡng, bạt bánh xốp mềm, lớp nhân kim sa trứng muối vàng óng láng mịn, bùi béo, sốt phomai nướng xém mặt thơm ngậy",
                            Price = 180000,
                            FoodCategory = FoodCategory.MonTrangMieng
                        },
                        new Food()
                        {
                            Id = "F004",
                            Name = "Set lẩu thái Tomyum",
                            Image = "~/img/products/lau_thai.jpg",
                            Description = "Lẩu Thái là món ăn xuất phát từ món canh chua Tom yum nổi tiếng của Thái Lan. Nước lẩu có hương vị chua chua cay cay đặc trưng. Các món nhúng lẩu gồn thịt bò, hải sản, rau xanh và các loại nấm.",
                            Price = 200000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F006",
                            Name = "Súp bào ngư hải sâm (1 phần)",
                            Image = "~/img/products/sup-bao-ngu-hai-sam.jpeg",
                            Description = "Súp bào ngư Bếp Hoa có bào ngư kết hợp cùng sò điệp, tôm tươi... được hầm trong nhiều giờ với rau củ & nấm đông trùng tạo ra vị ngọt tự nhiên hiếm thấy. Một món ăn khiến cả người ốm cũng thấy ngon miệng đó ạ.",
                            Price = 540000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F007",
                            Name = "Tai cuộn lưỡi",
                            Image = "~/img/products/tai-cuon-luoi.jpeg",
                            Description = "Tai heo được cuộn bên trong cùng phần thịt lưỡi heo. Phần tai bên ngoài giòn dai, phần thịt lưỡi bên trong vẫn mềm, có độ ngọt tự nhiên của thịt. Tai cuộn lưỡi được chấm với nước mắm và tiêu đen.",
                            Price = 340000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F008",
                            Name = "Xíu mại tôm thịt 10 viên",
                            Image = "~/img/products/xiu_mai_tom_thit_10_vien.jpg",
                            Description = "Quý khách hấp chín trước khi ăn. Những miếng há cảo, sủi cảo, hoành thánh với phần nhân tôm, sò điệp, hải sản tươi ngon hay nhân thịt heo thơm ngậy chắc chắn sẽ khiến bất kỳ ai thưởng thức đều cảm thấy rất ngon miệng.",
                            Price = 140000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F009",
                            Name = "Trà phô mai kem sữa",
                            Image = "~/img/products/tra-pho-mai-kem-sua.jpg",
                            Description = "Món Nước uống vừa béo ngậy, chua ngọt đủ cả mà vẫn có vị thanh của trà.",
                            Price = 34000,
                            FoodCategory = FoodCategory.NuocUong
                        },
                        new Food
                        {
                            Id = "F010",
                            Name = "Trà đào chanh sả",
                            Image = "~/img/products/tra-dao-chanh-sa.jpg",
                            Description = "Trà đào chanh sả có vị đậm ngọt thanh của đào, vị chua chua dịu nhẹ của chanh và hương thơm của sả.",
                            Price = 25000,
                            FoodCategory = FoodCategory.NuocUong
                        },
                        new Food
                        {
                            Id = "F011",
                            Name = "Bánh chuối nướng",
                            Image = "~/img/products/banh-chuoi-nuong.jpeg",
                            Description = "Bánh chuối nướng béo ngậy mùi nước cốt dừa cùng miếng chuối mềm ngon sẽ là Món tráng miệng phù hợp với mọi người.",
                            Price = 60000,
                            FoodCategory = FoodCategory.MonTrangMieng
                        },
                        new Food
                        {
                            Id = "F012",
                            Name = "Há cảo sò điệp (10 viên)",
                            Image = "~/img/products/ha_cao.jpg",
                            Description = "Những miếng há cảo, sủi cảo, hoành thánh với phần nhân tôm, sò điệp, hải sản tươi ngon hay nhân thịt heo thơm ngậy chắc chắn sẽ khiến bất kỳ ai thưởng thức đều cảm thấy rất ngon miệng.",
                            Price = 140000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F013",
                            Name = "Chả rươi (100gr)",
                            Image = "~/img/products/thit_nuong.jpg",
                            Description = "Chả rươi luôn mang đến hương vị khác biệt và 'gây thương nhớ' hơn hẳn so với các loại chả khác. Rươi béo càng ăn càng thấy ngậy. Thịt thơm quyện mùi thì là và vỏ quýt rất đặc sắc. Chắc chắn sẽ là một món ăn rất hao cơm",
                            Price = 60000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F014",
                            Name = "Nộm gà Hội An (1 phần)",
                            Image = "~/img/products/nom_ga_hoi_an.png",
                            Description = "Nộm gà làm từ thịt gà ri thả đồi. Thịt gà ngọt, săn được nêm nếm vừa miệng, bóp thấu với các loại rau tạo thành món nộm thơm ngon, đậm đà, giải ngán hiệu quả.",
                            Price = 60000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F015",
                            Name = "Set bún cá (1 set 5 bát)",
                            Image = "~/img/products/set_bun_ca.jpg",
                            Description = "Bún cá được làm đặc biệt hơn với cá trắm lọc xương và chiên giòn, miếng cá nhúng vào nước dùng ăn vẫn giòn dai, thơm ngon vô cùng.",
                            Price = 60000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F016",
                            Name = "Bún cá (1 phần)",
                            Image = "~/img/products/set_bun_ca.jpg",
                            Description = "Bún cá được làm đặc biệt hơn với cá trắm lọc xương và chiên giòn, miếng cá nhúng vào nước dùng ăn vẫn giòn dai, thơm ngon vô cùng",
                            Price = 60000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F017",
                            Name = "Xôi trắng hành phi (1 phần)",
                            Image = "~/img/products/bun_ca_hanh_phi.jpeg",
                            Description = "Bún cá được làm đặc biệt hơn với cá trắm lọc xương và chiên giòn, miếng cá nhúng vào nước dùng ăn vẫn giòn dai, thơm ngon vô cùng",
                            Price = 60000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F018",
                            Name = "Tôm sú lột rang thịt (1 phần)",
                            Image = "~/img/products/tom_su_luot_ran_thit.png",
                            Description = "Tôm sú tươi rim với thịt. rim kỹ, vừa lửa nên thịt và tôm săn lại, ngấm vị, càng ăn càng thấy ngon.",
                            Price = 60000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F019",
                            Name = "Bánh cookie dừa",
                            Image = "~/img/products/banh_cookie_dua.jpeg",
                            Description = "Bánh cookie dừa ngọt vừa miệng, dừa bào tươi nhào bánh nướng giòn tan, cắn vào thơm lừng, giòn rụm",
                            Price = 130000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F020",
                            Name = "Cá chiên giòn sốt mắm Thái",
                            Image = "~/img/products/sot_mam_thai.jpeg",
                            Description = "Bánh cookie dừa ngọt vừa miệng, dừa bào tươi nhào bánh nướng giòn tan, cắn vào thơm lừng, giòn rụm",
                            Price = 130000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F021",
                            Name = "Gà chiên nước mắm Phan Thiết",
                            Image = "~/img/products/ga_chien_phan_thiet.jpeg",
                            Description = "Gà ta chiên giòn, nước mắm Phan Thiết thơm ngon, ăn kèm xôi mềm, ngon mắt",
                            Price = 120000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F022",
                            Name = "Bún riêu cua",
                            Image = "~/img/products/bun_rieu_cua.jpeg",
                            Description = "Bún riêu cua hấp dẫn với nước dùng thơm béo, cua tươi ngon, ăn kèm rau sống và bún mềm",
                            Price = 95000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F023",
                            Name = "Mì Quảng tôm thịt",
                            Image = "~/img/products/mi_quang_tom_thit.jpeg",
                            Description = "Mì Quảng tôm thịt đậm đà, ăn kèm nước lèo ngon, rau sống và bún giòn",
                            Price = 85000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F024",
                            Name = "Bánh mì chảo",
                            Image = "~/img/products/banh_mi_chao.jpeg",
                            Description = "Bánh mì chảo giòn thơm, ăn kèm nước sốt cay nồng và thịt bò xay thơm ngon",
                            Price = 75000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F025",
                            Name = "Bánh mì pate sốt BBQ",
                            Image = "~/img/products/banh_mi_pate_bbq.jpeg",
                            Description = "Bánh mì pate sốt BBQ thơm ngon, ăn kèm rau sống và thịt gà bóp thơm",
                            Price = 65000,
                            FoodCategory = FoodCategory.MonMan
                        },

                        new Food
                        {
                            Id = "F026",
                            Name = "Cơm rang dưa bò",
                            Image = "~/img/products/com_rang_dua_bo.jpeg",
                            Description = "Cơm rang dưa bò ngon mắt, thơm béo, ăn kèm rau sống và trứng chiên",
                            Price = 90000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F027",
                            Name = "Gỏi cá trích",
                            Image = "~/img/products/goi_ca_trich.jpeg",
                            Description = "Gỏi cá trích với nước mắm Phan Thiết thơm ngon, ăn kèm rau sống và bún mềm",
                            Price = 85000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F028",
                            Name = "Bún bò Huế",
                            Image = "~/img/products/bun_bo_hue.jpeg",
                            Description = "Bún bò Huế đậm đà, thơm ngon, ăn kèm rau sống và bún giòn",
                            Price = 110000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F029",
                            Name = "Bánh mì trứng ốp-la",
                            Image = "~/img/products/banh_mi_trung_op_la.jpeg",
                            Description = "Bánh mì trứng ốp-la giòn thơm, ăn kèm nước sốt cay nồng và thịt bò xay thơm ngon",
                            Price = 70000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F030",
                            Name = "Bánh canh cua",
                            Image = "~/img/products/banh_canh_cua.jpeg",
                            Description = "Bánh canh cua hấp dẫn với nước dùng thơm béo, cua tươi ngon, ăn kèm rau sống và bún mềm",
                            Price = 100000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F031",
                            Name = "Phở bò tái",
                            Image = "~/img/products/pho_bo_tai.jpeg",
                            Description = "Phở bò tái nấu theo kiểu truyền thống, thơm ngon, ăn kèm rau sống và bún giòn",
                            Price = 90000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F032",
                            Name = "Gỏi xoài tôm",
                            Image = "~/img/products/goi_xoai_tom.jpeg",
                            Description = "Gỏi xoài tôm ngon mắt, thơm ngon, ăn kèm rau sống và bún mềm",
                            Price = 80000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F033",
                            Name = "Bánh mì cá mòi",
                            Image = "~/img/products/banh_mi_ca_moi.jpeg",
                            Description = "Bánh mì cá mòi giòn thơm, ăn kèm nước sốt cay nồng và thịt cá mòi xay thơm ngon",
                            Price = 75000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F034",
                            Name = "Bún mắm cá linh",
                            Image = "~/img/products/bun_mam_ca_linh.jpeg",
                            Description = "Bún mắm cá linh hấp dẫn với nước mắm thơm ngon, cá linh tươi ngon, ăn kèm rau sống và bún mềm",
                            Price = 95000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F035",
                            Name = "Lẩu mì cay Hàn Quốc",
                            Image = "~/img/products/lau_mi_cay_han_quoc.jpeg",
                            Description = "Lẩu mì cay Hàn Quốc thơm ngon, cay nồng, ăn kèm nước lèo và thịt bò thơm ngon",
                            Price = 110000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F036",
                            Name = "Bánh mì chả cá",
                            Image = "~/img/products/banh_mi_cha_ca.jpeg",
                            Description = "Bánh mì chả cá giòn thơm, ăn kèm nước sốt cay nồng và chả cá thơm ngon",
                            Price = 70000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F037",
                            Name = "Bún đậu mắm tôm",
                            Image = "~/img/products/bun_dau_mam_tom.jpeg",
                            Description = "Bún đậu mắm tôm ngon mắt, thơm béo, ăn kèm rau sống và bún giòn",
                            Price = 85000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F038",
                            Name = "Gỏi cá trích xanh",
                            Image = "~/img/products/goi_ca_trich_xanh.jpeg",
                            Description = "Gỏi cá trích xanh với nước mắm Phan Thiết thơm ngon, ăn kèm rau sống và bún mềm",
                            Price = 88000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F039",
                            Name = "Bánh mì pate sốt cay",
                            Image = "~/img/products/banh_mi_pate_sot_cay.jpeg",
                            Description = "Bánh mì pate sốt cay thơm ngon, ăn kèm rau sống và thịt gà bóp thơm",
                            Price = 72000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F040",
                            Name = "Cơm chiên dưa bò",
                            Image = "~/img/products/com_chien_dua_bo.jpeg",
                            Description = "Cơm chiên dưa bò ngon mắt, thơm béo, ăn kèm rau sống và trứng chiên",
                            Price = 95000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F041",
                            Name = "Gỏi cuốn tôm thịt",
                            Image = "~/img/products/goi_cuon_tom_thit.jpeg",
                            Description = "Gỏi cuốn tôm thịt tươi ngon, ăn kèm nước mắm pha chua ngọt và đậu phộng giã nhuyễn",
                            Price = 78000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F042",
                            Name = "Bún ốc cay Hà Nội",
                            Image = "~/img/products/bun_oc_cay_ha_noi.jpeg",
                            Description = "Bún ốc cay Hà Nội thơm ngon, cay nồng, ăn kèm nước lèo và ốc tươi ngon",
                            Price = 88000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F043",
                            Name = "Lẩu hải sản cay",
                            Image = "~/img/products/lau_hai_san_cay.jpeg",
                            Description = "Lẩu hải sản cay nồng, ăn kèm nước lèo và đủ loại hải sản tươi ngon",
                            Price = 120000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F044",
                            Name = "Bánh mì bò kho",
                            Image = "~/img/products/banh_mi_bo_kho.jpeg",
                            Description = "Bánh mì bò kho thơm ngon, ăn kèm rau sống và thịt bò kho mềm ngon",
                            Price = 85000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F045",
                            Name = "Bún chả cá Hải Phòng",
                            Image = "~/img/products/bun_cha_ca_hai_phong.jpeg",
                            Description = "Bún chả cá Hải Phòng ngon mắt, thơm ngon, ăn kèm nước mắm pha chua ngọt",
                            Price = 90000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F046",
                            Name = "Gỏi bưởi tôm thịt",
                            Image = "~/img/products/goi_buoi_tom_thit.jpeg",
                            Description = "Gỏi bưởi tôm thịt thơm ngon, ăn kèm nước mắm pha chua ngọt và đậu phộng giã nhuyễn",
                            Price = 82000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F047",
                            Name = "Bún bò Huế",
                            Image = "~/img/products/bun_bo_hue.jpeg",
                            Description = "Bún bò Huế thơm ngon, nước dùng đậm đà, ăn kèm rau sống và bánh ướt giòn",
                            Price = 95000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F048",
                            Name = "Lẩu ếch cay",
                            Image = "~/img/products/lau_ech_cay.jpeg",
                            Description = "Lẩu ếch cay nồng, ăn kèm nước lèo và ếch tươi ngon",
                            Price = 110000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F049",
                            Name = "Bánh mì trứng ốp-la",
                            Image = "~/img/products/banh_mi_trung_op_la.jpeg",
                            Description = "Bánh mì trứng ốp-la thơm ngon, ăn kèm rau sống và trứng ốp-la giòn",
                            Price = 70000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F050",
                            Name = "Gỏi cá trích mắm tôm",
                            Image = "~/img/products/goi_ca_trich_mam_tom.jpeg",
                            Description = "Gỏi cá trích mắm tôm thơm ngon, ăn kèm rau sống và bún mềm",
                            Price = 88000,
                            FoodCategory = FoodCategory.MonMan
                        },
                        new Food
                        {
                            Id = "F051",
                            Name = "Bún ốc mít",
                            Image = "~/img/products/bun_oc_mit.jpeg",
                            Description = "Bún ốc mít thơm ngon, ăn kèm nước lèo và ốc mít tươi ngon",
                            Price = 95000,
                            FoodCategory = FoodCategory.MonMan
                        },

                        new Food
                        {
                            Id = "F052",
                            Name = "Cơm chiên cá mặn",
                            Image = "~/img/products/com_chien_ca_man.jpeg",
                            Description = "Cơm chiên cá mặn thơm ngon, ăn kèm nước mắm pha chua ngọt và rau sống",
                            Price = 85000,
                            FoodCategory = FoodCategory.MonMan
                        },

                        new Food
                        {
                            Id = "F053",
                            Name = "Bánh canh cua",
                            Image = "~/img/products/banh_canh_cua.jpeg",
                            Description = "Bánh canh cua thơm ngon, nước dùng đậm đà, ăn kèm rau sống và bánh canh giòn",
                            Price = 98000,
                            FoodCategory = FoodCategory.MonMan
                        },

                        new Food
                        {
                            Id = "F054",
                            Name = "Bún riêu cua",
                            Image = "~/img/products/bun_rieu_cua.jpeg",
                            Description = "Bún riêu cua thơm ngon, nước dùng đậm đà, ăn kèm rau sống và bún giòn",
                            Price = 90000,
                            FoodCategory = FoodCategory.MonMan
                        },

                        new Food
                        {
                            Id = "F055",
                            Name = "Bánh mì chảo lòng đỏ",
                            Image = "~/img/products/banh_mi_chao_long_do.jpeg",
                            Description = "Bánh mì chảo lòng đỏ thơm ngon, ăn kèm rau sống và lòng đỏ mềm ngon",
                            Price = 75000,
                            FoodCategory = FoodCategory.MonMan
                        },

                        new Food
                        {
                            Id = "F056",
                            Name = "Mì quảng gà",
                            Image = "~/img/products/mi_quang_ga.jpeg",
                            Description = "Mì quảng gà thơm ngon, nước dùng đậm đà, ăn kèm rau sống và gà tươi ngon",
                            Price = 92000,
                            FoodCategory = FoodCategory.MonMan
                        },

                        new Food
                        {
                            Id = "F057",
                            Name = "Lẩu cá kèo",
                            Image = "~/img/products/lau_ca_keo.jpeg",
                            Description = "Lẩu cá kèo thơm ngon, ăn kèm nước lèo và cá kèo tươi ngon",
                            Price = 105000,
                            FoodCategory = FoodCategory.MonMan
                        },

                        new Food
                        {
                            Id = "F058",
                            Name = "Bánh mì xíu mại",
                            Image = "~/img/products/banh_mi_xiu_mai.jpeg",
                            Description = "Bánh mì xíu mại thơm ngon, ăn kèm rau sống và xíu mại giòn",
                            Price = 78000,
                            FoodCategory = FoodCategory.MonMan
                        },

                        new Food
                        {
                            Id = "F059",
                            Name = "Gỏi gà bắp cải",
                            Image = "~/img/products/goi_ga_bap_cai.jpeg",
                            Description = "Gỏi gà bắp cải thơm ngon, ăn kèm nước mắm pha chua ngọt và bắp cải giòn",
                            Price = 87000,
                            FoodCategory = FoodCategory.MonMan
                        },

                        new Food
                        {
                            Id = "F060",
                            Name = "Bún đậu mắm tôm",
                            Image = "~/img/products/bun_dau_mam_tom.jpeg",
                            Description = "Bún đậu mắm tôm thơm ngon, ăn kèm nước mắm pha chua ngọt và đậu giòn",
                            Price = 92000,
                            FoodCategory = FoodCategory.MonMan
                        },
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
                            Id = "O001",
                            OrderDate =  new DateTime(2023, 11, 28, 12, 0, 0),
                            ReceiveDate = new DateTime(2023, 11, 28, 12, 30, 0),
                            OrderStatus = OrderStatusCategory.DangXuLy,
                            FormDelivery = FormDeliveryCategory.GiaoTanNoi,
                            Receiver = "Đình Nhật",
                            Location = "Liên Chiểu, Đà Nẵng",
                            PhoneNumber = "09878787654",
                            Note = "Không cần dụng cụ ăn uống",
                            AppUserId = "bc8af0a8-b3a5-4f95-8678-f696820f480e", // Assuming you have an AppUser with this Id
                            Foods = new List<OrderDetail>
                            {
                                new OrderDetail { Id = "OD001", FoodId = "F001", Quantity = 2 },
                                new OrderDetail { Id = "OD002", FoodId = "F002", Quantity = 1 }
                            }
                        },
                         new Order()
                        {
                            Id = "O002",
                            OrderDate =  new DateTime(2023, 11, 28, 14, 0, 0),
                            ReceiveDate = new DateTime(2023, 11, 28, 14, 30, 0),
                            OrderStatus = OrderStatusCategory.DangXuLy,
                            FormDelivery = FormDeliveryCategory.GiaoTanNoi,
                            Receiver = "Đức Quang",
                            Location = "Hoàng Diệu, Hải Châu, Đà Nẵng",
                            PhoneNumber = "09878787254",
                            Note = "Không cần dụng cụ ăn uống",
                            AppUserId = "280e5fa1-e8fa-440e-8e78-2a221c64450c", // Assuming you have an AppUser with this Id
                            Foods = new List<OrderDetail>
                            {
                                new OrderDetail { Id = "OD003", FoodId = "F003", Quantity = 3 },
                                new OrderDetail { Id = "OD004", FoodId = "F004", Quantity = 2 }
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
                            AppUserId = "bc8af0a8-b3a5-4f95-8678-f696820f480e",
                            Foods = new List<CartDetail>
                            {
                                new CartDetail { Id = "CD001", FoodId = "F001", Quantity = 2},
                                new CartDetail { Id = "CD002", FoodId = "F002", Quantity = 2 }
                            }
                        },
                        new Cart()
                        {
                            Id = "CA002",
                            AppUserId = "280e5fa1-e8fa-440e-8e78-2a221c64450c",
                            Foods = new List<CartDetail>
                            {
                                new CartDetail { Id = "CD003", FoodId = "F001", Quantity = 2},
                                new CartDetail { Id = "CD004", FoodId = "F002", Quantity = 2 }
                            }
                        },
                        new Cart()
                        {
                            Id = "CA003",
                            AppUserId = "e1b475e4-b413-4c37-9b98-b2e3b8569842",
                            Foods = new List<CartDetail>
                            {
                                new CartDetail { Id = "CD005", FoodId = "F001", Quantity = 2},
                                new CartDetail { Id = "CD006", FoodId = "F002", Quantity = 2 }
                            }
                        }
                    });
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

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "adminAccount@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "Admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Name = "Phạm Văn Nhật Huy",
                        PhoneNumber = "012345678976",
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "ducquang@gmail.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "DucQuang",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Name = "Trần Đức Quang",
                        PhoneNumber = "02374857675"
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string appUserEmail2 = "hongquang@gmail.com";
                var appUser2 = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser2 == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "HongQuang",
                        Email = appUserEmail2,
                        EmailConfirmed = true,
                        Name = "Mai Hồng Quang",
                        PhoneNumber = "09876567453"
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string appUserEmail3 = "dinhnhat@gmail.com";
                var appUser3 = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser3 == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "DinhNhat",
                        Email = appUserEmail3,
                        EmailConfirmed = true,
                        Name = "Trương Đình Nhật",
                        PhoneNumber = "08756475865"
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}

using FoodOrderApp.Data.Enum.EnumAttributes;

namespace FoodOrderApp.Data.Enum
{
    public enum FoodCategory
    {
        [DisplayText("Món chay")]
        MonChay,
        [DisplayText("Món mặn")]
        MonMan,
        [DisplayText("Món lẩu")]
        MonLau,
        [DisplayText("Món ăn vặt")]
        MonAnVat,
        [DisplayText("Món tráng miệng")]
        MonTrangMieng,
        [DisplayText("Nước uống")]
        NuocUong
    }
}

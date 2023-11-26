using FoodOrderApp.Data.Enum.EnumAttributes;

namespace FoodOrderApp.Data.Enum
{
    public enum OrderStatusCategory
    {
        [DisplayText("Đang xử lý")]
        DangXuLy,
        [DisplayText("Đã xử lý")]
        DaXuLy
    }
}

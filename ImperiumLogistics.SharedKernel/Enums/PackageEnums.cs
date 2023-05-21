using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel.Enums
{
    public enum PackageStatus
    {
        AvailableForPickUp,
        PickedUp,
        WareHouse,
        InDelivery,
        UnDelivered,
        SLABreach,
        Delivered
    }

    public static class PackageStatusExtension
    {
        public static string GetString(this PackageStatus status)
        {
            return Enum.GetName(typeof(PackageStatus), status) ?? string.Empty;
        }
    }
}

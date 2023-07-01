using ImperiumLogistics.Domain.RiderAggregate;
using ImperiumLogistics.SharedKernel.Query;

namespace ImperiumLogistics.Infrastructure.RiderHandlers
{
    public class DateFilter : RiderFilterHandler
    {
        public override void Apply(ref IQueryable<Rider> data, QueryRequest queryRequest)
        {
            var dateFilter = queryRequest.DateFilter;
            if (dateFilter != null && HasValidDates(dateFilter.From, dateFilter.To))
            {
                var _data = data.Where(item => item.DateCreated >= dateFilter.From &&
                                                  item.DateCreated <= dateFilter.To);

                data = _data;
            }

            if (successor != null && queryRequest != null)
            {
                successor.Apply(ref data, queryRequest);
            }
        }
        private bool HasValidDates(DateTime? from, DateTime? to)
        {
            if (from == null || from.Value == DateTime.MinValue)
            {
                return false;
            }

            if (to == null || to.Value == DateTime.MinValue)
            {
                return false;
            }

            return true;
        }
    }
}

using RadioManager.Domain.Shows.Events;
using RadioManager.Domain.Shows.ValueObjects;
using RadioManager.Domain.Types;

namespace RadioManager.Domain.Shows.Aggregates
{
    public sealed class Show : AggregateRoot
    {
        public ShowTitle Title { get; private set; }
        public Presenter Presenter { get; private set; }
        public TimeSlot TimeSlot { get; private set; }

        private Show() { }
        private Show(AggregateId id, ShowTitle title, Presenter presenter, TimeSlot timeSlot)
        {
            Id = id;
            Title = title;
            Presenter = presenter;
            TimeSlot = timeSlot;
        }

        public static Show Create(string title, string presenter, DateTime startTime, int durationMinutes)
        {
            var id = AggregateId.Create();
            var showTitle = ShowTitle.Create(title);
            var showPresenter = Presenter.Create(presenter);
            var timeSlot = TimeSlot.Create(startTime, durationMinutes);

            var show = new Show(id, showTitle, showPresenter, timeSlot);
            show.AddEvent(new ShowCreatedEvent(
                show.Id,
                show.Title,
                show.TimeSlot.StartTime,
                show.Presenter));

            return show;
        }
    }
}

using System;
using ConfApp.Services.Telemetry.Events;

namespace ConfApp.Services.Telemetry.Events
{
    public class UserFavoritesATalkEvent : EventBase
    {
        public UserFavoritesATalkEvent(bool isFavorite, string talkId) : base(EventTitles.UserFavoritesATalk)
        {
            IsFavorite = isFavorite;
            TalkId = talkId;
        }

        public bool IsFavorite
        {
            get => GetValue<bool>(nameof(IsFavorite));
            set => SetValue(nameof(IsFavorite), value);
        }

        public string TalkId
        {
            get => GetValue<string>(nameof(TalkId));
            set => SetValue(nameof(TalkId), value);
        }
    }

    public class UserChecksInAtServiceAppointmentEvent : UserActionServiceAppointmentEvent
    {
        public UserChecksInAtServiceAppointmentEvent(bool isGeofenceTriggered) :
            base(EventTitles.UserChecksInAtServiceAppointment, isGeofenceTriggered)
        {
        }
    }

    public class UserChecksOutOfServiceAppointmentEvent : UserActionServiceAppointmentEvent
    {
        public UserChecksOutOfServiceAppointmentEvent(bool isGeofenceTriggered) :
            base(EventTitles.UserChecksOutOfServiceAppointment, isGeofenceTriggered)
        {
        }
    }

    public class UserActionServiceAppointmentEvent : EventBase
    {
        public UserActionServiceAppointmentEvent(string name, bool isGeofenceTriggered)
            : base(name)
        {
            IsGeofenceTriggered = isGeofenceTriggered;
        }

        public bool IsGeofenceTriggered
        {
            get => GetValue<bool>(nameof(IsGeofenceTriggered));
            set => SetValue(nameof(IsGeofenceTriggered), value);
        }

        public double UserLatitude
        {
            get => GetValue<double>(nameof(UserLatitude));
            set => SetValue(nameof(UserLatitude), value);
        }

        public double UserLongitude
        {
            get => GetValue<double>(nameof(UserLongitude));
            set => SetValue(nameof(UserLongitude), value);
        }

        public double AccuracyInMeters
        {
            get => GetValue<double>(nameof(AccuracyInMeters));
            set => SetValue(nameof(AccuracyInMeters), value);
        }

        public string EmployeeGPID
        {
            get => GetValue<string>(nameof(EmployeeGPID));
            set => SetValue(nameof(EmployeeGPID), value);
        }

        public string EmployeeName
        {
            get => GetValue<string>(nameof(EmployeeName));
            set => SetValue(nameof(EmployeeName), value);
        }

        public string ServiceAppointmentSalesforceId
        {
            get => GetValue<string>(nameof(ServiceAppointmentSalesforceId));
            set => SetValue(nameof(ServiceAppointmentSalesforceId), value);
        }

        public DateTimeOffset ServiceAppointmentScheduledStartDate
        {
            get => GetValue<DateTimeOffset>(nameof(ServiceAppointmentScheduledStartDate));
            set => SetValue(nameof(ServiceAppointmentScheduledStartDate), value);
        }

        public DateTimeOffset ServiceAppointmentScheduledEndDate
        {
            get => GetValue<DateTimeOffset>(nameof(ServiceAppointmentScheduledEndDate));
            set => SetValue(nameof(ServiceAppointmentScheduledEndDate), value);
        }

        public string ServiceAppointmentType
        {
            get => GetValue<string>(nameof(ServiceAppointmentType));
            set => SetValue(nameof(ServiceAppointmentType), value);
        }

        public string LocationId
        {
            get => GetValue<string>(nameof(LocationId));
            set => SetValue(nameof(LocationId), value);
        }
    }
}
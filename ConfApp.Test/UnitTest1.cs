using System;
using System.Diagnostics;
using ConfApp.Services.Telemetry.Events;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ConfApp.Test
{
    public class TelemetryEventBaseTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreatesAnEventBaseSuccessfully()
        {
            var sot = new EventBase("TestEvent");
            var p = sot.Parameters;
            Assert.IsNotNull(sot);
        }

        [Test]
        public void CreateAnEventAndSerializeItSuccessfully()
        {
            var now = DateTimeOffset.Now;
            var sot = new UserChecksInAtServiceAppointmentEvent(true)
            {
                UserLatitude = 39.137230,
                UserLongitude = -76.691680,
                AccuracyInMeters = 5,
                EmployeeGPID = "123456",
                EmployeeName = "Claudio Sanchez",
                ServiceAppointmentSalesforceId = "abcde3023423423",
                ServiceAppointmentScheduledStartDate = now.AddMinutes(5),
                ServiceAppointmentScheduledEndDate = now.AddMinutes(65),
                ServiceAppointmentType = "Account",
                LocationId = "abc4444444444444"
            };

            var json = JsonConvert.SerializeObject(sot, Formatting.Indented);
            Debug.WriteLine(json);
            Assert.IsNotNull(sot);
        }

        [Test]
        public void DeserializeProperties()
        {
            var data =
                "{\"ServiceAppointmentSalesforceId\":\"abcde3023423423\",\"ServiceAppointmentScheduledStartDate\":\"6/16/2020 5:49:30 PM -04:00\",\"Name\":\"User Checks-out of Service Appointment\",\"Latitude\":\"39.1192626953125\",\"ServiceAppointmentType\":\"Account\",\"TimeStamp\":\"6/16/2020 5:44:30 PM -04:00\",\"EmployeeName\":\"Claudio Sanchez\",\"UserLongitude\":\"-76.69168\",\"AccuracyInMeters\":\"5\",\"EmployeeGPID\":\"123456\",\"ServiceAppointmentScheduledEndDate\":\"6/16/2020 6:49:30 PM -04:00\",\"IsGeofenceTriggered\":\"True\",\"LocationId\":\"abc4444444444444\",\"UserLatitude\":\"39.13723\",\"Longitude\":\"-76.643133262819\"}";
            var r = JsonConvert.DeserializeObject(data);
        }

        [Test]
        public void DeserializeAppCenterMessage()
        {
            var data =
                "{\r\n    \"AppBuild\": \"1.0\",\r\n    \"AppId\": \"8b3a2f3f-679b-4c74-a501-928569896708\",\r\n    \"UserId\": \"None\",\r\n    \"AppNamespace\": \"com.megsoft.confapp\",\r\n    \"AppVersion\": \"56\",\r\n    \"CarrierCountry\": \"None\",\r\n    \"CarrierName\": \"None\",\r\n    \"CorrelationId\": \"53fe29d8-42a1-4587-85e4-1b2ed17a37f2\",\r\n    \"CountryCode\": \"None\",\r\n    \"EventId\": \"98f46561-a48b-4f11-9a25-2b8b9ef1bef5\",\r\n    \"EventName\": \"User Checks-out of Service Appointment\",\r\n    \"IngressTimestamp\": \"2020-06-16T21:44:41.083Z\",\r\n    \"InstallId\": \"46719d73-b954-4987-a257-003a636aa64e\",\r\n    \"IsTestMessage\": \"False\",\r\n    \"LiveUpdateDeploymentKey\": \"None\",\r\n    \"LiveUpdatePackageHash\": \"None\",\r\n    \"LiveUpdateReleaseLabel\": \"None\",\r\n    \"Locale\": \"en_US\",\r\n    \"MessageId\": \"c3ccd472-631b-41f4-97ab-dcc28e87f40a\",\r\n    \"MessageType\": \"EventLog\",\r\n    \"Model\": \"iPhone12,3\",\r\n    \"OemName\": \"Apple\",\r\n    \"OsApiLevel\": \"None\",\r\n    \"OsBuild\": \"17F75\",\r\n    \"OsName\": \"iOS\",\r\n    \"OsVersion\": \"13.5\",\r\n    \"Properties\": \"{\\\"ServiceAppointmentSalesforceId\\\":\\\"abcde3023423423\\\",\\\"ServiceAppointmentScheduledStartDate\\\":\\\"6/16/2020 5:49:30 PM -04:00\\\",\\\"Name\\\":\\\"User Checks-out of Service Appointment\\\",\\\"Latitude\\\":\\\"39.1192626953125\\\",\\\"ServiceAppointmentType\\\":\\\"Account\\\",\\\"TimeStamp\\\":\\\"6/16/2020 5:44:30 PM -04:00\\\",\\\"EmployeeName\\\":\\\"Claudio Sanchez\\\",\\\"UserLongitude\\\":\\\"-76.69168\\\",\\\"AccuracyInMeters\\\":\\\"5\\\",\\\"EmployeeGPID\\\":\\\"123456\\\",\\\"ServiceAppointmentScheduledEndDate\\\":\\\"6/16/2020 6:49:30 PM -04:00\\\",\\\"IsGeofenceTriggered\\\":\\\"True\\\",\\\"LocationId\\\":\\\"abc4444444444444\\\",\\\"UserLatitude\\\":\\\"39.13723\\\",\\\"Longitude\\\":\\\"-76.643133262819\\\"}\",\r\n    \"ScreenSize\": \"2436x1125\",\r\n    \"SdkName\": \"appcenter.ios\",\r\n    \"SdkVersion\": \"3.1.1\",\r\n    \"SessionId\": \"8d01c38e-c4e7-4d9a-b741-e9b22a5bfe20\",\r\n    \"Timestamp\": \"2020-06-16T21:44:30.821Z\",\r\n    \"TimeZoneOffset\": \"-PT4H\",\r\n    \"WrapperRuntimeVersion\": \"12.6.0\",\r\n    \"WrapperSdkName\": \"appcenter.xamarin\",\r\n    \"WrapperSdkVersion\": \"3.2.1\"\r\n}";
            dynamic AppCenterMessage = JsonConvert.DeserializeObject(data);
            var p = AppCenterMessage.Properties;
            var jsonString = JsonConvert.SerializeObject(AppCenterMessage);
        }
    }
}
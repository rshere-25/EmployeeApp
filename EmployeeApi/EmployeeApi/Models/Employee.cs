using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmployeeApi.Models
{
    public class Employee
    {
        [Key]
        [JsonPropertyName("empID")]
        public int EmpID { get; set; }

        [Required]
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        [Required]
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        [Required]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [Required]
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [Required]
        [JsonPropertyName("dob")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DOB { get; set; }
    }

    // Custom converter for handling "dd-MM-yyyy" date format
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private const string DateFormat = "dd-MM-yyyy";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? dateString = reader.GetString();
            return DateTime.ParseExact(dateString!, DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormat));
        }
    }
}

using System.Text.Json.Serialization;

// APIからのJSONレスポンス全体を格納するクラス
public class PostalCloudResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    [JsonPropertyName("results")]
    public List<AddressResult>? Results { get; set; }
    [JsonPropertyName("status")]
    public int Status { get; set; }
}

// 住所情報の結果を格納するクラス
public class AddressResult
{
    [JsonPropertyName("address1")]
    public string? Prefecture { get; set; }
    [JsonPropertyName("address2")]
    public string? City { get; set; }
    [JsonPropertyName("address3")]
    public string? Town { get; set; }
    [JsonPropertyName("kana1")]
    public string? PrefectureKana { get; set; }
    [JsonPropertyName("kana2")]
    public string? CityKana { get; set; }
    [JsonPropertyName("kana3")]
    public string? TownKana { get; set; }
    [JsonPropertyName("prefcode")]
    public string? PrefectureCode { get; set; }
    [JsonPropertyName("zipcode")]
    public string? Zipcode { get; set; }
}
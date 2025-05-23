namespace VPMSWeb.Models
{
    public class ResponseStatusObject
    {
        public int? StatusCode { get; set; }
        public Boolean? isDoctApptOverlap { get; set; }
        public Boolean? isPatientAppOverlap { get; set; }
        public Boolean? isRecordExists { get; set; }
        public int? TotalRecords { get; set; }
    }

    public class ResponseBannerUploadObject
    {
        public int? StatusCode { get; set; }
        public Boolean? isBannerSelected { get; set; }
        public Boolean? isFileUploadSuccess { get; set; }
        public Boolean? isNoChanges { get; set; }
    }

    public class ResponseBlogUploadObject
    {
        public int? StatusCode { get; set; }
        public Boolean? isThumbnailSelected { get; set; }
        public Boolean? isThumbnailUploadSuccess { get; set; }
        public Boolean? isNoChanges { get; set; }
    }
}

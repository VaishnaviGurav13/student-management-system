using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace StudentManagement_Service.StudentData
{
    [DataContract]
    public class StudentData
    {
        [DataMember]
        public int StudentId { get; set; } = 0;
        [DataMember]
        public string Fullname { get; set; } = string.Empty;
        [DataMember]
        public int Class { get; set; } = 0;
        [DataMember]
        public string Gender { get; set; } = string.Empty;
        [DataMember]
        public string EmailID { get; set; } = string.Empty;
        [DataMember]
        public string Status { get; set; } = string.Empty;
        [DataMember]
        public string PhoneNum { get; set; } = string.Empty;
        [DataMember]
        public string Username { get; set; } = string.Empty;
        [DataMember]
        public string Role { get; set; } = string.Empty;
        [DataMember]
        public string Password { get; set; } = string.Empty;
        [DataMember]
        public List<CoursesData> Courses { get; set; } = new List<CoursesData>();
        [DataMember]
        public List<int> CourseId { get; set; } = new List<int>();
    }

    [DataContract]
    public class CoursesData
    {
        [DataMember]
        public int CourseId { get; set; } = 0;
        [DataMember]
        public string CourseName { get; set; } = string.Empty;
    }

    [DataContract]
    public class CountData
    {
        [DataMember]
        public int TotalStudentCount { get; set; } = 0;
        [DataMember]
        public int PendingStudentCount { get; set; } = 0;
        [DataMember]
        public int AcceptedStudentCount { get; set; } = 0;
        [DataMember]
        public int RejectedStudentCount { get; set; } = 0;
        [DataMember]
        public int TotalCourseCount { get; set; } = 0;
        [DataMember]
        public List<StudentData> StudentData { get; set; } = new List<StudentData>();
    }

    [DataContract]
    public class ErrorData
    {
        [DataMember]
        public int ErrorCode { get; set; } = 0;
        [DataMember]
        public string ErrorMsg { get; set; } = string.Empty;
    }
}
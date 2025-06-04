using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace BPH_ER_Smart_Kiosk.Models
{
    public class NHSOPersonDataModel
    {
        private static NHSOPersonDataModel? _instance;

        public static NHSOPersonDataModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NHSOPersonDataModel();
                return _instance;
            }
        }

        [JsonProperty("pid")] public string? Pid { get; set; }
        [JsonProperty("titleName")] public string? TitleName { get; set; }
        [JsonProperty("fname")] public string? Fname { get; set; }
        [JsonProperty("lname")] public string? Lname { get; set; }
        [JsonProperty("nation")] public string? Nation { get; set; }
        [JsonProperty("birthDate")] public string? BirthDate { get; set; }
        [JsonProperty("sex")] public string? Sex { get; set; }
        [JsonProperty("transDate")] public DateTime? TransDate { get; set; }
        [JsonProperty("mainInscl")] public string? MainInscl { get; set; }
        [JsonProperty("subInscl")] public string? SubInscl { get; set; }
        [JsonProperty("age")] public string? Age { get; set; }
        [JsonProperty("checkDate")] public DateTime? CheckDate { get; set; }
        [JsonProperty("claimTypes")] public List<Claim>? ClaimTypes { get; set; }
        [JsonProperty("correlationId")] public string? CorrelationId { get; set; }
        [JsonProperty("hospSub")] public Hospital? HospSub { get; set; }
        [JsonProperty("hospMainOp")] public Hospital? HospMainOp { get; set; }
        [JsonProperty("hospMain")] public Hospital? HospMain { get; set; }
        [JsonProperty("paidModel")] public string? PaidModel { get; set; }

        [JsonProperty("mainInsclCode")] public string? MainInsclCode { get; set; }
        [JsonProperty("subInsclCode")] public string? SubInsclCode { get; set; }

        public NHSOPersonDataModel() { }

        public static NHSOPersonDataModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new NHSOPersonDataModel();
            }
            return _instance;
        }

        public void SetPersonData(NHSOPersonDataModel data)
        {
            if (data == null) return;
            if (data.Pid != null) Pid = data.Pid;
            if (data.Fname != null) Fname = data.Fname;
            if (data.Lname != null) Lname = data.Lname;
            if (data.Nation != null) Nation = data.Nation;
            if (data.BirthDate != null) BirthDate = data.BirthDate;
            if (data.Sex != null) Sex = data.Sex;
            if (data.TransDate.HasValue) TransDate = data.TransDate;
            if (data.MainInscl != null)
            {
                MainInscl = data.MainInscl;
                MainInsclCode = ExtractCodeFromBrackets(data.MainInscl);
            }
            if (data.SubInscl != null)
            {
                SubInscl = data.SubInscl;
                SubInsclCode = ExtractCodeFromBrackets(data.SubInscl);
            }
            if (data.Age != null) Age = data.Age;
            if (data.CheckDate.HasValue) CheckDate = data.CheckDate;
            if (data.ClaimTypes != null) ClaimTypes = data.ClaimTypes;
            if (data.CorrelationId != null) CorrelationId = data.CorrelationId;
            if (data.HospSub != null) HospSub = data.HospSub;
            if (data.HospMainOp != null) HospMainOp = data.HospMainOp;
            if (data.HospMain != null) HospMain = data.HospMain;
            if (data.PaidModel != null) PaidModel = data.PaidModel;
        }

        public void Clear()
        {
            Pid = null;
            TitleName = null;
            Fname = null;
            Lname = null;
            Nation = null;
            BirthDate = null;
            Sex = null;
            TransDate = null;
            MainInscl = null;
            SubInscl = null;
            MainInsclCode = null;
            SubInsclCode = null;
            Age = null;
            CheckDate = null;
            ClaimTypes = null;
            CorrelationId = null;
            HospSub = null;
            HospMainOp = null;
            HospMain = null;
            PaidModel = null;
        }

        private string? ExtractCodeFromBrackets(string input)
        {
            var match = Regex.Match(input, @"\((.*?)\)");
            return match.Success ? match.Groups[1].Value : null;
        }
    }

    public class Claim
    {
        [JsonProperty("claimType")]
        public string? ClaimType { get; set; }

        [JsonProperty("claimTypeName")]
        public string? ClaimTypeName { get; set; }
    }

    public class Hospital
    {
        [JsonProperty("hcode")]
        public string? Hcode { get; set; }

        [JsonProperty("hname")]
        public string? Hname { get; set; }
    }

    class DocumentDetail
    {
        private static DocumentDetail? _instance;
        public string? QN { get; set; }        // หมายเลขคิว
        public string? QRCode { get; set; }   // QR Code
        public string? CID { get; set; }      // เลขบัตรประชาชน
        public string? Name { get; set; }     // ชื่อ-นามสกุล
        public string? Age { get; set; }      // อายุ
        public string? HN { get; set; }       // เลขประจำตัวผู้ป่วย
        public string? VN { get; set; }
        public string? Address { get; set; }  // ที่อยู่
        public string? InsuranceRights { get; set; }   // สิทธิที่ใช้
        public string? PrimaryHospital { get; set; }   // สถานบริการหลัก
        public string? AuthenStatus { get; set; }      // สถานะการยืนยันตัวตน
        public string? SecondaryHospital { get; set; } // สถานบริการรอง
        public string? TreatmentHospital { get; set; } // โรงพยาบาลรักษา
        public string? Department { get; set; }        // แผนก
        public string? DrugAllergy { get; set; }       // แพ้ยา
        public string? Clinic { get; set; }            // คลินิก
        public DateTime? RecordDate { get; set; }      // วันที่บันทึก
        public TimeSpan? RecordTime { get; set; }      // เวลาบันทึก
        public DocumentDetail() { }

        public static DocumentDetail GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DocumentDetail();
            }
            return _instance;
        }
        public void Clear()
        {
            QN = null;
            QRCode = null;
            CID = null;
            Name = null;
            Age = null;
            HN = null;
            VN = null;
            Address = null;
            InsuranceRights = null;
            PrimaryHospital = null;
            AuthenStatus = null;
            SecondaryHospital = null;
            TreatmentHospital = null;
            Department = null;
            DrugAllergy = null;
            Clinic = null;
            RecordDate = null;
            RecordTime = null;
        }
    }
}

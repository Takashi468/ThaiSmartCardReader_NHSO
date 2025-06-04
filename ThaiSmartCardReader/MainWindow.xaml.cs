using System.Diagnostics;
using System.Windows;
using ThaiNationalIDCard;
using ThaiSmartCardReader.Services;

namespace ThaiSmartCardReader
{
    public partial class MainWindow : Window
    {
        private readonly APIService _apiService = new APIService();
        public ThaiIDCard idcard;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            idcard = new ThaiIDCard();
            readCard(); // อ่านบัตรทันทีเมื่อโหลดหน้าต่าง
            string ss = "";
            try
            {
                ThaiIDCard idcard = new ThaiIDCard();
                string[] readers = idcard.GetReaders();

                if (readers == null) return;
                foreach (string r in readers)
                {
                    ss = r;
                }
                idcard.MonitorStart(ss);
                idcard.eventCardInsertedWithPhoto += new handleCardInserted(CardInserted);
                idcard.eventCardRemoved += new handleCardRemoved(RemoverCard);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("ไม่พบเครื่องอ่านบัตร");
            }
        }

        public void CardInserted(Personal personal)
        {
            if (personal == null)
            {
                if (idcard.ErrorCode() > 0)
                {
                    Debug.WriteLine(idcard.Error());
                }
                return;
            }
            Dispatcher.InvokeAsync(async () =>
            {
                if (personal.PhotoRaw != null)
                {
                    var image = personal.ConvertByteArrayToBitmapImage(personal.PhotoRaw);
                    Image_PersonalPhoto.Source = image;
                }
                Debug.WriteLine(personal.Citizenid);
                Debug.WriteLine(personal.Birthday?.ToString("dd/MM/yyyy"));
                Debug.WriteLine(personal.Sex);
                Debug.WriteLine(personal.Th_Prefix);
                Debug.WriteLine(personal.Th_Firstname);
                Debug.WriteLine(personal.Th_Lastname);
                Debug.WriteLine(personal.En_Prefix);
                Debug.WriteLine(personal.En_Firstname);
                Debug.WriteLine(personal.En_Lastname);
                Debug.WriteLine(personal.Issue.ToString("dd/MM/yyyy")); // วันออกบัตร
                Debug.WriteLine(personal.Expire.ToString("dd/MM/yyyy")); // วันหมดอายุ

                Debug.WriteLine(personal.Address);
                Debug.WriteLine(personal.addrHouseNo); // บ้านเลขที่
                Debug.WriteLine(personal.addrVillageNo); // หมู่ที่
                Debug.WriteLine(personal.addrLane); // ซอย
                Debug.WriteLine(personal.addrRoad); // ถนน
                Debug.WriteLine(personal.addrTambol);
                Debug.WriteLine(personal.addrAmphur);
                Debug.WriteLine(personal.addrProvince);

                Debug.WriteLine(personal.Religion); // ศาสนา
                Debug.WriteLine(personal.UnderCardNumber); // เลขใต้บัตร

                // แสดงข้อมูลใน TextBox
                TextBox_Data.Text = $"Cid: {personal.Citizenid}\n" +
                                     $"Birthday: {personal.Birthday?.ToString("dd/MM/yyyy")}\n" +
                                     $"Sex: {personal.Sex}\n" +
                                     $"Thai Name: {personal.Th_Prefix} {personal.Th_Firstname} {personal.Th_Lastname}\n" +
                                     $"English Name: {personal.En_Prefix} {personal.En_Firstname} {personal.En_Lastname}\n" +
                                     $"Issue Date: {personal.Issue.ToString("dd/MM/yyyy")}\n" +
                                     $"Expire Date: {personal.Expire.ToString("dd/MM/yyyy")}\n" +
                                     $"Address: {personal.Address}\n" +
                                     $"House No: {personal.addrHouseNo}\n" +
                                     $"Village No: {personal.addrVillageNo}\n" +
                                     $"Lane: {personal.addrLane}\n" +
                                     $"Road: {personal.addrRoad}\n" +
                                     $"Tambol: {personal.addrTambol}\n" +
                                     $"Amphur: {personal.addrAmphur}\n" +
                                     $"Province: {personal.addrProvince}\n" +
                                     $"Religion: {personal.Religion}\n" +    // แสดงศาสนา
                                     $"Under Card Number: {personal.UnderCardNumber}"; // แสดงเลขใต้บัตร

                await LoadNHSODataAsync();
            });

        }

        private void RemoverCard()
        {
            Debug.WriteLine("Card Removed");

            Dispatcher.Invoke(() =>
            {
                Image_PersonalPhoto.Source = null; 
                TextBox_Data.Text = string.Empty;   // เคลียร์ข้อมูลใน TextBox
                TextBox_NHSO.Text = string.Empty;
            });
        }

        public void readCard()
        {
            var personal = idcard.readAll(true); 
            if (personal != null)
            {
                if (personal.PhotoRaw != null)
                {
                    var image = personal.ConvertByteArrayToBitmapImage(personal.PhotoRaw);
                    Image_PersonalPhoto.Source = image;
                }
                Debug.WriteLine(personal.Citizenid);
                Debug.WriteLine(personal.Birthday?.ToString("dd/MM/yyyy"));
                Debug.WriteLine(personal.Sex);
                Debug.WriteLine(personal.Th_Prefix);
                Debug.WriteLine(personal.Th_Firstname);
                Debug.WriteLine(personal.Th_Lastname);
                Debug.WriteLine(personal.En_Prefix);
                Debug.WriteLine(personal.En_Firstname);
                Debug.WriteLine(personal.En_Lastname);
                Debug.WriteLine(personal.Issue.ToString("dd/MM/yyyy")); // วันออกบัตร
                Debug.WriteLine(personal.Expire.ToString("dd/MM/yyyy")); // วันหมดอายุ

                Debug.WriteLine(personal.Address);
                Debug.WriteLine(personal.addrHouseNo); // บ้านเลขที่
                Debug.WriteLine(personal.addrVillageNo); // หมู่ที่
                Debug.WriteLine(personal.addrLane); // ซอย
                Debug.WriteLine(personal.addrRoad); // ถนน
                Debug.WriteLine(personal.addrTambol);
                Debug.WriteLine(personal.addrAmphur);
                Debug.WriteLine(personal.addrProvince);

                Debug.WriteLine(personal.Religion); // ศาสนา
                Debug.WriteLine(personal.UnderCardNumber); // เลขใต้บัตร

                // แสดงข้อมูลใน TextBox
                TextBox_Data.Text = $"Cid: {personal.Citizenid}\n" +
                                     $"Birthday: {personal.Birthday?.ToString("dd/MM/yyyy")}\n" +
                                     $"Sex: {personal.Sex}\n" +
                                     $"Thai Name: {personal.Th_Prefix} {personal.Th_Firstname} {personal.Th_Lastname}\n" +
                                     $"English Name: {personal.En_Prefix} {personal.En_Firstname} {personal.En_Lastname}\n" +
                                     $"Issue Date: {personal.Issue.ToString("dd/MM/yyyy")}\n" +
                                     $"Expire Date: {personal.Expire.ToString("dd/MM/yyyy")}\n" +
                                     $"Address: {personal.Address}\n" +
                                     $"House No: {personal.addrHouseNo}\n" +
                                     $"Village No: {personal.addrVillageNo}\n" +
                                     $"Lane: {personal.addrLane}\n" +
                                     $"Road: {personal.addrRoad}\n" +
                                     $"Tambol: {personal.addrTambol}\n" +
                                     $"Amphur: {personal.addrAmphur}\n" +
                                     $"Province: {personal.addrProvince}\n" +
                                     $"Religion: {personal.Religion}\n" +    // แสดงศาสนา
                                     $"Under Card Number: {personal.UnderCardNumber}"; // แสดงเลขใต้บัตร
            }
            else if (idcard.ErrorCode() > 0)
            {
                Debug.WriteLine(idcard.Error());
                Debug.WriteLine("Error Code : " + idcard.ErrorCode());
                if(idcard.ErrorCode() == 256)
                {
                    Debug.WriteLine("กรุณาใส่บัตรประชาชน");
                }
            }
        }

        private async Task LoadNHSODataAsync()
        {
            var (data, error) = await _apiService.GetNHSODataSmartCardAsync();
            if (data != null && error == "SUCCESS")
            {
                // แสดงผลใน TextBox หรือ Label
                TextBox_NHSO.Text =
                    $"สิทธิหลัก: {data.MainInscl} ({data.MainInsclCode})\n" +
                    $"สิทธิรอง: {data.SubInscl} ({data.SubInsclCode})\n" +
                    $"โรงพยาบาลหลัก: {data.HospMain?.Hname}\n" +
                    $"โรงพยาบาลรอง: {data.HospSub?.Hname}\n" +
                    $"เพศ: {data.Sex}  อายุ: {data.Age}\n" +
                    $"วันเกิด: {data.BirthDate}\n" +
                    $"ตรวจสอบล่าสุด: {data.CheckDate?.ToString("dd/MM/yyyy HH:mm")}";
            }
            else
            {
                Debug.WriteLine($"❌ NHSO Error: {error}");
                TextBox_NHSO.Text = $"ไม่สามารถดึงข้อมูล NHSO ได้: {error}";
            }
        }

    }
}

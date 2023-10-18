using DocumentFormat.OpenXml.Packaging;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuanLyMoiTruong.Application.Requests;
using QuanLyMoiTruong.Application.ViewModels;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

namespace QuanLyMoiTruong.WebApp.Service
{
    public class BaoCaoApiClient : IBaoCaoApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public BaoCaoApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<List<BaoCaoCapGiayPhepMoiTruongViewModel>>> SearchBaoCaoCapGiayPhepMoiTruong(GiayPhepMoiTruongRequest request)
        {
            var sessions = _httpContextAccessor
            .HttpContext
            .Session
            .GetString("AccessToken");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/BaoCao/BaoCaoCapGiayPhepMoiTruong", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<BaoCaoCapGiayPhepMoiTruongViewModel>>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<BaoCaoCapGiayPhepMoiTruongViewModel>>>(await response.Content.ReadAsStringAsync());
        }
        public async Task<ApiResult<byte[]>> ExportBaoCaoCapGiayPhepMoiTruong(GiayPhepMoiTruongRequest request)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var pathFileTemplate = "Assets/Template/MauBaoCaoCapGiayPhepMoiTruong.xlsx";
            var data = new List<BaoCaoCapGiayPhepMoiTruongViewModel>();
            var response = await SearchBaoCaoCapGiayPhepMoiTruong(request);
            if (response.Success) data = response.Data;
            var result = new ApiSuccessResult<byte[]>();
            var file = new FileInfo(pathFileTemplate);
            var p = new ExcelPackage(file);
            var wb = p.Workbook;
            var ws = wb.Worksheets.FirstOrDefault();
            p.Compression = CompressionLevel.Default;
            if (ws != null)
            {
                ExcelWorksheetView wv = ws.View;
                wv.ZoomScale = 100;
                wv.RightToLeft = false;
                ws.PrinterSettings.Orientation = eOrientation.Landscape;
                ws.Cells.AutoFitColumns();
                var i = 0;
                foreach (var obj in data)
                {
                    i++;
                    ws.Cells[7 + i, 1].Value = i.ToString();
                    ws.Cells[7 + i, 2].Value = obj.TenDuAn;
                    ws.Cells[7 + i, 3].Value = obj.TenDoanhNghiep;
                    ws.Cells[7 + i, 4].Value = obj.DiaChi;
                    ws.Cells[7 + i, 5].Value = obj.QuyMo;
                    ws.Cells[7 + i, 6].Value = obj.LoaiHinhSanXuat;
                    ws.Cells[7 + i, 7].Value = obj.TenNguoiDaiDien;
                    ws.Cells[7 + i, 8].Value = obj.SoGiayPhep;
                    ws.Cells[7 + i, 9].Value = obj.NgayCap;
                    ws.Cells[7 + i, 10].Value = obj.CoQuanCap;
                }
                ws.Cells[7, 1, i + 7, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 7, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 7, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 7, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells.AutoFitColumns();
                result.Data = p.GetAsByteArray();
            }
            return result;
        }
    }
}

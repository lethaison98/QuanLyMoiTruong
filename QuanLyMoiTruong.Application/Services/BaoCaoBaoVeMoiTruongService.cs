using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Data.Entities;
using QuanLyMoiTruong.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.UnitOfWork.Collections;
using System;
using System.Globalization;
using System.Reflection.Metadata;
using QuanLyMoiTruong.Application.Requests;
using System.Linq.Expressions;
using QuanLyMoiTruong.Common.Expressions;
using QuanLyMoiTruong.Common.Enums;
using QuanLyMoiTruong.Application.Request;
using OfficeOpenXml;

namespace QuanLyMoiTruong.Application.Services
{

    public class BaoCaoBaoVeMoiTruongService : IBaoCaoBaoVeMoiTruongService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;
        private readonly IKetQuaBaoVeMoiTruongDoanhNghiepService _ketQuaBaoVeMoiTruongDoanhNghiepService;
        private readonly IKetQuaBaoVeMoiTruongKCNService _ketQuaBaoVeMoiTruongKCNService;


        public BaoCaoBaoVeMoiTruongService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService, IKetQuaBaoVeMoiTruongKCNService ketQuaBaoVeMoiTruongKCNService, IKetQuaBaoVeMoiTruongDoanhNghiepService ketQuaBaoVeMoiTruongDoanhNghiepService)
        {
            _unitOfWork= unitOfWork;
            _fileTaiLieuService= fileTaiLieuService;
            _ketQuaBaoVeMoiTruongDoanhNghiepService = ketQuaBaoVeMoiTruongDoanhNghiepService;
            _ketQuaBaoVeMoiTruongKCNService = ketQuaBaoVeMoiTruongKCNService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted= true;
                await _unitOfWork.SaveChangesAsync();
                return new ApiSuccessResult<bool>() {};
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa") ;
            }
        } 

        public async Task<ApiResult<IList<BaoCaoBaoVeMoiTruongViewModel>>> GetAll()
        {
            var result = new List<BaoCaoBaoVeMoiTruongViewModel>();
            var entities =  await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<BaoCaoBaoVeMoiTruongViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruongViewModel>> GetById(int id)
        {
            var result = new BaoCaoBaoVeMoiTruongViewModel();
            var data = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().GetFirstOrDefaultAsync(predicate: x => x.IdBaoCaoBaoVeMoiTruong == id, include: x => x.Include(y => y.DuAn).Include(y=> y.KhuCongNghiep));
            result = MapEntityToViewModel(data);
            var dsFile = await _fileTaiLieuService.GetByTaiLieu(data.IdBaoCaoBaoVeMoiTruong, NhomTaiLieuEnum.BaoCaoBaoVeMoiTruong.ToString());
            if (dsFile.Success)
            {
                result.FileTaiLieu = dsFile.Data.ToList();
            }
            return new ApiSuccessResult<BaoCaoBaoVeMoiTruongViewModel>() {Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruong>> Insert(BaoCaoBaoVeMoiTruongViewModel obj)
        {
            var entity = new BaoCaoBaoVeMoiTruong();
            entity = MapViewModelToEntity(obj, entity);
            await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoBaoVeMoiTruong;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoBaoVeMoiTruong.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            if(obj.LoaiBaoCao == "KhuCongNghiep")
            {
                var listFile = await _fileTaiLieuService.GetByTaiLieu(idTaiLieu: entity.IdBaoCaoBaoVeMoiTruong, nhomTaiLieu: NhomTaiLieuEnum.BaoCaoBaoVeMoiTruong.ToString());
                var fileChung = listFile.Data.FirstOrDefault(x => x.LoaiFileTaiLieu == LoaiFileTaiLieuEnum.TongHopSoLieuBVMTChungKCN.ToString());
                if (fileChung != null)
                {
                    await ImportKetQuaBaoVeMoiTruongKCN(fileChung.File.LinkFile, entity);
                }
                var fileChiTiet = listFile.Data.FirstOrDefault(x => x.LoaiFileTaiLieu == LoaiFileTaiLieuEnum.TongHopSoLieuBVMTChitietKCN.ToString());
                if (fileChiTiet != null)
                {
                    await ImportKetQuaBaoVeMoiTruongDoanhNghiep(fileChiTiet.File.LinkFile, entity);
                }
            }
            return new ApiSuccessResult<BaoCaoBaoVeMoiTruong>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<BaoCaoBaoVeMoiTruongViewModel>>> GetAllPaging(BaoCaoBaoVeMoiTruongRequest request)
        {
            Expression<Func<BaoCaoBaoVeMoiTruong, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenBaoCao.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<BaoCaoBaoVeMoiTruongViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<BaoCaoBaoVeMoiTruongViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruong>> Update(BaoCaoBaoVeMoiTruongViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().GetFirstOrDefaultAsync(predicate: x => x.IdBaoCaoBaoVeMoiTruong == obj.IdBaoCaoBaoVeMoiTruong);
            entity = MapViewModelToEntity(obj, entity);
            _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoBaoVeMoiTruong;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoBaoVeMoiTruong.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            return new ApiSuccessResult<BaoCaoBaoVeMoiTruong>() { Data = entity };
        }
        public async Task<ApiResult<IList<BaoCaoBaoVeMoiTruongViewModel>>> GetListBaoCaoBaoVeMoiTruongByDuAn(int idDuAn)
        {
            var result = new List<BaoCaoBaoVeMoiTruongViewModel>();
            var entities = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdDuAn == idDuAn, include: x => x.Include(x => x.DuAn));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdBaoCaoBaoVeMoiTruong, NhomTaiLieuEnum.BaoCaoBaoVeMoiTruong.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<BaoCaoBaoVeMoiTruongViewModel>>() { Data = result };
        }
        
        public async Task<ApiResult<IList<BaoCaoBaoVeMoiTruongViewModel>>> GetListBaoCaoBaoVeMoiTruongByKhuCongNghiep(int idKhuCongNghiep)
        {
            var result = new List<BaoCaoBaoVeMoiTruongViewModel>();
            var entities = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdKhuCongNghiep == idKhuCongNghiep, include: x => x.Include(x => x.KhuCongNghiep));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdBaoCaoBaoVeMoiTruong, NhomTaiLieuEnum.BaoCaoBaoVeMoiTruong.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<BaoCaoBaoVeMoiTruongViewModel>>() { Data = result };
        }
        public async Task<ApiResult<IList<BaoCaoBaoVeMoiTruongViewModel>>> GetListBaoCaoBaoVeMoiTruongByKhuKinhTe()
        {
            var result = new List<BaoCaoBaoVeMoiTruongViewModel>();
            var entities = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().GetAllAsync(predicate: x => !x.IsDeleted && x.KhuKinhTe);
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdBaoCaoBaoVeMoiTruong, NhomTaiLieuEnum.BaoCaoBaoVeMoiTruong.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<BaoCaoBaoVeMoiTruongViewModel>>() { Data = result };
        }

        public async Task<ApiResult<List<string>>> ImportKetQuaBaoVeMoiTruongDoanhNghiep(string path, BaoCaoBaoVeMoiTruong bc)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var result = new List<string>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(path)))
            {
                var currentSheet = package.Workbook.Worksheets;

                var listKetQua = new List<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>();
                var ws = currentSheet[0];
                for (var i = 6; i <= ws.Dimension.End.Row; i++)
                { 

                    var imp = new KetQuaBaoVeMoiTruongDoanhNghiepViewModel();
                    imp.TenDoanhNghiep = ws.Cells[i, 2].Value == null ? "" : ws.Cells[i, 2].Value.ToString();
                    imp.SoGiayTo = ws.Cells[i, 3].Value == null ? "" : ws.Cells[i, 3].Value.ToString();
                    imp.LoaiHinhSanXuat = ws.Cells[i, 4].Value == null ? "" : ws.Cells[i, 4].Value.ToString();
                    imp.TongLuongNuocThai = ws.Cells[i, 5].Value == null ? "" : ws.Cells[i, 5].Value.ToString();
                    imp.DauNoiVaoHTXLNT = ws.Cells[i, 6].Value == null ? "" : ws.Cells[i, 6].Value.ToString();
                    imp.TachDauNoi = ws.Cells[i, 7].Value == null ? "" : ws.Cells[i, 7].Value.ToString();
                    imp.LuongKhiThai = ws.Cells[i, 8].Value == null ? "" : ws.Cells[i, 8].Value.ToString();
                    imp.QuanTracKhiThai = ws.Cells[i, 9].Value == null ? "" : ws.Cells[i, 9].Value.ToString();
                    imp.ChatThaiRanSinhHoat = ws.Cells[i, 10].Value == null ? "" : ws.Cells[i, 10].Value.ToString();
                    imp.ChatThaiRanCongNghiep = ws.Cells[i, 11].Value == null ? "" : ws.Cells[i, 11].Value.ToString();
                    imp.ChatThaiRanNguyHai = ws.Cells[i, 12].Value == null ? "" : ws.Cells[i, 12].Value.ToString();
                    imp.TyLeCayXanh = ws.Cells[i, 13].Value == null ? "" : ws.Cells[i, 13].Value.ToString();
                    imp.IdBaoCaoBaoVeMoiTruong = bc.IdBaoCaoBaoVeMoiTruong;
                    imp.TenKhuCongNghiep = bc.KhuCongNghiep != null? bc.KhuCongNghiep.TenKhuCongNghiep: "";
                    imp.IdKhuCongNghiep = bc.IdKhuCongNghiep != null? bc.IdKhuCongNghiep.Value : 0;
                    listKetQua.Add(imp);
                }
                await _ketQuaBaoVeMoiTruongDoanhNghiepService.InsertFromExcel(listKetQua);
                return new ApiSuccessResult<List<string>>() { Data = result };
            }
        }
        public async Task<ApiResult<List<string>>> ImportKetQuaBaoVeMoiTruongKCN(string path, BaoCaoBaoVeMoiTruong bc)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var result = new List<string>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(path)))
            {
                var currentSheet = package.Workbook.Worksheets;

                var listKetQua = new List<KetQuaBaoVeMoiTruongKCNViewModel>();
                var ws = currentSheet[0];
                for (var i = 6; i <= ws.Dimension.End.Row; i++)
                {
                    var imp = new KetQuaBaoVeMoiTruongKCNViewModel();
                    imp.TenKhuCongNghiep = ws.Cells[i, 2].Value == null ? "" : ws.Cells[i, 2].Value.ToString();
                    imp.DiaChi = ws.Cells[i, 3].Value == null ? "" : ws.Cells[i, 3].Value.ToString();
                    imp.DienTich = ws.Cells[i, 4].Value == null ? "" : ws.Cells[i, 4].Value.ToString();
                    imp.TenChuDautu = ws.Cells[i, 5].Value == null ? "" : ws.Cells[i, 5].Value.ToString();
                    imp.SoLuongCoSo = ws.Cells[i, 6].Value == null ? "" : ws.Cells[i, 6].Value.ToString();
                    imp.TyLeLapDay = ws.Cells[i, 7].Value == null ? "" : ws.Cells[i, 7].Value.ToString();
                    imp.HeThongThuGomNuocMua = ws.Cells[i, 8].Value == null ? "" : ws.Cells[i, 8].Value.ToString();
                    imp.TongLuongNuocThai = ws.Cells[i, 9].Value == null ? "" : ws.Cells[i, 9].Value.ToString();
                    imp.CongSuatThietKeHTXLNT = ws.Cells[i, 10].Value == null ? "" : ws.Cells[i, 10].Value.ToString();
                    imp.HeThongQuanTracNuocThai = ws.Cells[i, 11].Value == null ? "" : ws.Cells[i, 11].Value.ToString();
                    imp.ChatThaiRanSinhHoat = ws.Cells[i, 12].Value == null ? "" : ws.Cells[i, 12].Value.ToString();
                    imp.ChatThaiRanCongNghiep = ws.Cells[i, 13].Value == null ? "" : ws.Cells[i, 13].Value.ToString();
                    imp.ChatThaiRanNguyHai = ws.Cells[i, 14].Value == null ? "" : ws.Cells[i, 14].Value.ToString();
                    imp.CongTrinhPhongNgua = ws.Cells[i, 15].Value == null ? "" : ws.Cells[i, 15].Value.ToString();
                    imp.TyLeCayXanh = ws.Cells[i, 16].Value == null ? "" : ws.Cells[i, 16].Value.ToString();
                    imp.IdBaoCaoBaoVeMoiTruong = bc.IdBaoCaoBaoVeMoiTruong;
                    imp.IdKhuCongNghiep = bc.IdKhuCongNghiep != null? bc.IdKhuCongNghiep.Value : 0;
                    listKetQua.Add(imp);
                }
                await _ketQuaBaoVeMoiTruongKCNService.InsertFromExcel(listKetQua);
                return new ApiSuccessResult<List<string>>() { Data = result };
            }
        }

        public BaoCaoBaoVeMoiTruongViewModel MapEntityToViewModel(BaoCaoBaoVeMoiTruong entity) {
            var result = new BaoCaoBaoVeMoiTruongViewModel();
            result.IdBaoCaoBaoVeMoiTruong = entity.IdBaoCaoBaoVeMoiTruong;
            result.TenBaoCao = entity.TenBaoCao;
            result.NgayBaoCao = entity.NgayBaoCao != null ? entity.NgayBaoCao.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.IdDuAn = entity.IdDuAn;
            result.TenDuAn = entity.IdDuAn != null? entity.DuAn.TenDuAn: "";
            result.TenDoanhNghiep = entity.IdDuAn != null ? entity.DuAn.TenDoanhNghiep : "";
            result.IdKhuCongNghiep = entity.IdKhuCongNghiep;
            result.TenKhuCongNghiep = entity.IdKhuCongNghiep != null ? entity.KhuCongNghiep.TenKhuCongNghiep : "";
            result.TenChuDauTu = entity.IdKhuCongNghiep != null ? entity.KhuCongNghiep.TenChuDauTu : "";
            result.KhuKinhTe = entity.KhuKinhTe;
            result.LoaiBaoCao = entity.LoaiBaoCao;
            return result;
        }
        public BaoCaoBaoVeMoiTruong MapViewModelToEntity(BaoCaoBaoVeMoiTruongViewModel viewModel, BaoCaoBaoVeMoiTruong entity)
        {
            entity.IdBaoCaoBaoVeMoiTruong = viewModel.IdBaoCaoBaoVeMoiTruong;
            entity.TenBaoCao = viewModel.TenBaoCao;
            entity.NgayBaoCao = string.IsNullOrEmpty(viewModel.NgayBaoCao) ? null : DateTime.Parse(viewModel.NgayBaoCao, new CultureInfo("vi-VN"));
            entity.IdDuAn = viewModel.IdDuAn == 0 ? null : viewModel.IdDuAn;
            entity.IdKhuCongNghiep = viewModel.IdKhuCongNghiep == 0 ? null : viewModel.IdKhuCongNghiep;
            entity.KhuKinhTe = viewModel.KhuKinhTe;
            entity.LoaiBaoCao = viewModel.LoaiBaoCao;
            return entity;
        }
    }

}

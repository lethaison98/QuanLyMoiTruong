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

    public class BaoCaoThongKeNguonThaiService : IBaoCaoThongKeNguonThaiService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;
        private readonly IKetQuaThongKeNguonThaiService _ketQuaThongKeNguonThaiService;

        public BaoCaoThongKeNguonThaiService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService, IKetQuaThongKeNguonThaiService ketQuaThongKeNguonThaiService)
        {
            _unitOfWork = unitOfWork;
            _fileTaiLieuService = fileTaiLieuService;
            _ketQuaThongKeNguonThaiService = ketQuaThongKeNguonThaiService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await _unitOfWork.SaveChangesAsync();
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IList<BaoCaoThongKeNguonThaiViewModel>>> GetAll()
        {
            var result = new List<BaoCaoThongKeNguonThaiViewModel>();
            var entities = await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<BaoCaoThongKeNguonThaiViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoThongKeNguonThaiViewModel>> GetById(int id)
        {
            var result = new BaoCaoThongKeNguonThaiViewModel();
            var data = await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().GetFirstOrDefaultAsync(predicate: x => x.IdBaoCaoThongKeNguonThai == id, include: x => x.Include(y => y.DuAn).Include(y => y.KhuCongNghiep));
            result = MapEntityToViewModel(data);
            var dsFile = await _fileTaiLieuService.GetByTaiLieu(data.IdBaoCaoThongKeNguonThai, NhomTaiLieuEnum.BaoCaoThongKeNguonThai.ToString());
            if (dsFile.Success)
            {
                result.FileTaiLieu = dsFile.Data.ToList();
            }
            return new ApiSuccessResult<BaoCaoThongKeNguonThaiViewModel>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoThongKeNguonThai>> Insert(BaoCaoThongKeNguonThaiViewModel obj)
        {
            var entity = new BaoCaoThongKeNguonThai();
            entity = MapViewModelToEntity(obj, entity);
            await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoThongKeNguonThai;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoThongKeNguonThai.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            var listFile = await _fileTaiLieuService.GetByTaiLieu(idTaiLieu: entity.IdBaoCaoThongKeNguonThai, nhomTaiLieu: NhomTaiLieuEnum.BaoCaoThongKeNguonThai.ToString());
            var fileExcel = listFile.Data.FirstOrDefault(x => x.LoaiFileTaiLieu == LoaiFileTaiLieuEnum.TongHopSoLieuThongKeNguonThai.ToString());
            if (fileExcel != null)
            {
                await ImportKetQuaThongKeNguonThai(fileExcel.File.LinkFile, entity);
            }
            return new ApiSuccessResult<BaoCaoThongKeNguonThai>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<BaoCaoThongKeNguonThaiViewModel>>> GetAllPaging(PagingRequest request)
        {
            Expression<Func<BaoCaoThongKeNguonThai, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenBaoCao.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<BaoCaoThongKeNguonThaiViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom = data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<BaoCaoThongKeNguonThaiViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoThongKeNguonThai>> Update(BaoCaoThongKeNguonThaiViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().GetFirstOrDefaultAsync(predicate: x => x.IdBaoCaoThongKeNguonThai == obj.IdBaoCaoThongKeNguonThai);
            entity = MapViewModelToEntity(obj, entity);
            _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoThongKeNguonThai;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoThongKeNguonThai.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            return new ApiSuccessResult<BaoCaoThongKeNguonThai>() { Data = entity };
        }
        public async Task<ApiResult<IList<BaoCaoThongKeNguonThaiViewModel>>> GetListBaoCaoThongKeNguonThaiByDuAn(int idDuAn)
        {
            var result = new List<BaoCaoThongKeNguonThaiViewModel>();
            var entities = await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdDuAn == idDuAn, include: x => x.Include(x => x.DuAn));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdBaoCaoThongKeNguonThai, NhomTaiLieuEnum.BaoCaoThongKeNguonThai.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<BaoCaoThongKeNguonThaiViewModel>>() { Data = result };
        }

        public async Task<ApiResult<IList<BaoCaoThongKeNguonThaiViewModel>>> GetListBaoCaoThongKeNguonThaiByKhuCongNghiep(int idKhuCongNghiep)
        {
            var result = new List<BaoCaoThongKeNguonThaiViewModel>();
            var entities = await _unitOfWork.GetRepository<BaoCaoThongKeNguonThai>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdKhuCongNghiep == idKhuCongNghiep, include: x => x.Include(x => x.KhuCongNghiep));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdBaoCaoThongKeNguonThai, NhomTaiLieuEnum.BaoCaoThongKeNguonThai.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<BaoCaoThongKeNguonThaiViewModel>>() { Data = result };
        }
        public async Task<ApiResult<List<string>>> ImportKetQuaThongKeNguonThai(string path, BaoCaoThongKeNguonThai bc)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var result = new List<string>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(path)))
            {
                var currentSheet = package.Workbook.Worksheets;

                var listKetQua = new List<KetQuaThongKeNguonThaiViewModel>();
                var ws = currentSheet[0];
                for (var i = 4; i <= ws.Dimension.End.Row; i++)
                { //9 điểm quan trắc không khí

                    var imp = new KetQuaThongKeNguonThaiViewModel();
                    imp.TenKhuCongNghiep = ws.Cells[i, 2].Value == null ? "" : ws.Cells[i, 2].Value.ToString();
                    imp.NuocThaiSinhHoat = ws.Cells[i, 3].Value == null ? "" : ws.Cells[i, 3].Value.ToString();
                    imp.NuocThaiSanXuat = ws.Cells[i, 4].Value == null ? "" : ws.Cells[i, 4].Value.ToString();
                    imp.NuocThaiTaiSuDung = ws.Cells[i, 5].Value == null ? "" : ws.Cells[i, 5].Value.ToString();
                    imp.LuuLuongDauNoi = ws.Cells[i, 6].Value == null ? "" : ws.Cells[i, 6].Value.ToString();
                    imp.KhiThai = ws.Cells[i, 7].Value == null ? "" : ws.Cells[i, 7].Value.ToString();
                    imp.ChatThaiRanSinhHoat = ws.Cells[i, 8].Value == null ? "" : ws.Cells[i, 8].Value.ToString();
                    imp.ChatThaiRanSanXuat = ws.Cells[i, 9].Value == null ? "" : ws.Cells[i, 9].Value.ToString();
                    imp.ChatThaiRanTaiSuDung = ws.Cells[i, 10].Value == null ? "" : ws.Cells[i, 10].Value.ToString();
                    imp.TongChatThaiRan = ws.Cells[i, 11].Value == null ? "" : ws.Cells[i, 11].Value.ToString();
                    imp.ChatThaiNguyHai = ws.Cells[i, 12].Value == null ? "" : ws.Cells[i, 12].Value.ToString();
                    imp.IdBaoCaoThongKeNguonThai = bc.IdBaoCaoThongKeNguonThai;
                    listKetQua.Add(imp);
                }
                await _ketQuaThongKeNguonThaiService.InsertFromExcel(listKetQua);
                return new ApiSuccessResult<List<string>>() { Data = result };
            }
        }
        public async Task<ApiResult<List<KetQuaThongKeNguonThaiViewModel>>> GetKetQuaThongKeNguonThaiByIdBaoCao(int id)
        {
            var data = await _ketQuaThongKeNguonThaiService.GetAllByIdBaoCaoThongKeNguonThai(id);
            var result = data.Data;
            return new ApiSuccessResult<List<KetQuaThongKeNguonThaiViewModel>>() { Data = result };
        }
        public BaoCaoThongKeNguonThaiViewModel MapEntityToViewModel(BaoCaoThongKeNguonThai entity)
        {
            var result = new BaoCaoThongKeNguonThaiViewModel();
            result.IdBaoCaoThongKeNguonThai = entity.IdBaoCaoThongKeNguonThai;
            result.TenBaoCao = entity.TenBaoCao;
            result.NgayBaoCao = entity.NgayBaoCao != null ? entity.NgayBaoCao.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.IdDuAn = entity.IdDuAn;
            result.TenDuAn = entity.IdDuAn != null ? entity.DuAn.TenDuAn : "";
            result.TenDoanhNghiep = entity.IdDuAn != null ? entity.DuAn.TenDoanhNghiep : "";
            result.IdKhuCongNghiep = entity.IdKhuCongNghiep;
            result.TenKhuCongNghiep = entity.IdKhuCongNghiep != null ? entity.KhuCongNghiep.TenKhuCongNghiep : "";
            result.TenChuDauTu = entity.IdKhuCongNghiep != null ? entity.KhuCongNghiep.TenChuDauTu : "";
            result.Nam = entity.Nam;
            result.Lan = entity.Lan;
            result.GhiChu = entity.GhiChu;
            return result;
        }
        public BaoCaoThongKeNguonThai MapViewModelToEntity(BaoCaoThongKeNguonThaiViewModel viewModel, BaoCaoThongKeNguonThai entity)
        {
            entity.IdBaoCaoThongKeNguonThai = viewModel.IdBaoCaoThongKeNguonThai;
            entity.TenBaoCao = viewModel.TenBaoCao;
            entity.NgayBaoCao = string.IsNullOrEmpty(viewModel.NgayBaoCao) ? null : DateTime.Parse(viewModel.NgayBaoCao, new CultureInfo("vi-VN"));
            entity.IdDuAn = viewModel.IdDuAn == 0 ? null : viewModel.IdDuAn;
            entity.IdKhuCongNghiep = viewModel.IdKhuCongNghiep == 0 ? null : viewModel.IdKhuCongNghiep;
            entity.GhiChu = viewModel.GhiChu;
            entity.Nam = viewModel.Nam;
            entity.Lan = viewModel.Lan;
            return entity;
        }
    }

}

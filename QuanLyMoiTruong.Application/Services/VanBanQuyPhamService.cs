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

    public class VanBanQuyPhamService : IVanBanQuyPhamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;
        private readonly IKetQuaQuanTracService _ketQuaQuanTracService;

        public VanBanQuyPhamService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService, IKetQuaQuanTracService ketQuaQuanTracService)
        {
            _unitOfWork = unitOfWork;
            _fileTaiLieuService = fileTaiLieuService;
            _ketQuaQuanTracService = ketQuaQuanTracService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity = await _unitOfWork.GetRepository<VanBanQuyPham>().FindAsync(id);
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

        public async Task<ApiResult<IList<VanBanQuyPhamViewModel>>> GetAll()
        {
            var result = new List<VanBanQuyPhamViewModel>();
            var entities = await _unitOfWork.GetRepository<VanBanQuyPham>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).OrderByDescending(x=> x.Nam).ThenByDescending(x=> x.NgayBanHanh).ToList();
            return new ApiSuccessResult<IList<VanBanQuyPhamViewModel>>() { Data = result };
        }

        public async Task<ApiResult<VanBanQuyPhamViewModel>> GetById(int id)
        {
            var result = new VanBanQuyPhamViewModel();
            var data = await _unitOfWork.GetRepository<VanBanQuyPham>().GetFirstOrDefaultAsync(predicate: x => x.IdVanBanQuyPham == id);
            result = MapEntityToViewModel(data);
            var dsFile = await _fileTaiLieuService.GetByTaiLieu(data.IdVanBanQuyPham, NhomTaiLieuEnum.VanBanQuyPham.ToString());
            if (dsFile.Success)
            {
                result.FileTaiLieu = dsFile.Data.ToList();
            }
            return new ApiSuccessResult<VanBanQuyPhamViewModel>() { Data = result };
        }

        public async Task<ApiResult<VanBanQuyPham>> Insert(VanBanQuyPhamViewModel obj)
        {
            var entity = new VanBanQuyPham();
            entity = MapViewModelToEntity(obj, entity);
            await _unitOfWork.GetRepository<VanBanQuyPham>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdVanBanQuyPham;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.VanBanQuyPham.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            var listFile = await _fileTaiLieuService.GetByTaiLieu(idTaiLieu: entity.IdVanBanQuyPham, nhomTaiLieu: NhomTaiLieuEnum.VanBanQuyPham.ToString());
            var fileExcel = listFile.Data.FirstOrDefault(x => x.LoaiFileTaiLieu == LoaiFileTaiLieuEnum.TongHopKetQuaQuanTrac.ToString());
            return new ApiSuccessResult<VanBanQuyPham>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<VanBanQuyPham>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<VanBanQuyPham>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<VanBanQuyPhamViewModel>>> GetAllPaging(PagingRequest request)
        {
            Expression<Func<VanBanQuyPham, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TrichYeu.ToLower().Contains(fullTextSearch) || p.SoKyHieu.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<VanBanQuyPham>().GetPagedListAsync(predicate: filter, orderBy: o => o.OrderByDescending(s => s.NgayBanHanh), pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<VanBanQuyPhamViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom = data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            foreach (var item in result.Items)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdVanBanQuyPham, NhomTaiLieuEnum.VanBanQuyPham.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IPagedList<VanBanQuyPhamViewModel>>() { Data = result };
        }

        public async Task<ApiResult<VanBanQuyPham>> Update(VanBanQuyPhamViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<VanBanQuyPham>().GetFirstOrDefaultAsync(predicate: x => x.IdVanBanQuyPham == obj.IdVanBanQuyPham);
            entity = MapViewModelToEntity(obj, entity);
            _unitOfWork.GetRepository<VanBanQuyPham>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdVanBanQuyPham;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.VanBanQuyPham.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            return new ApiSuccessResult<VanBanQuyPham>() { Data = entity };
        }
        public VanBanQuyPhamViewModel MapEntityToViewModel(VanBanQuyPham entity)
        {
            var result = new VanBanQuyPhamViewModel();
            result.IdVanBanQuyPham = entity.IdVanBanQuyPham;
            result.TrichYeu = entity.TrichYeu;
            result.SoKyHieu = entity.SoKyHieu;
            result.CoQuanBanHanh = entity.CoQuanBanHanh;
            result.Nam = entity.Nam;
            result.NgayBanHanh = entity.NgayBanHanh != null ? entity.NgayBanHanh.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.GhiChu = entity.GhiChu;
            return result;
        }
        public VanBanQuyPham MapViewModelToEntity(VanBanQuyPhamViewModel viewModel, VanBanQuyPham entity)
        {
            entity.IdVanBanQuyPham = viewModel.IdVanBanQuyPham;
            entity.TrichYeu = viewModel.TrichYeu;
            entity.SoKyHieu = viewModel.SoKyHieu;
            entity.CoQuanBanHanh = viewModel.CoQuanBanHanh;
            entity.NgayBanHanh = string.IsNullOrEmpty(viewModel.NgayBanHanh) ? null : DateTime.Parse(viewModel.NgayBanHanh, new CultureInfo("vi-VN"));
            entity.Nam = viewModel.Nam;
            entity.GhiChu = viewModel.GhiChu;

            return entity;
        }
    }

}

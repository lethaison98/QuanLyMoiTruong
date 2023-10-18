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

namespace QuanLyMoiTruong.Application.Services
{

    public class KetQuaQuanTracService : IKetQuaQuanTracService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;
        private readonly IDiemQuanTracService _diemQuanTracService;

        public KetQuaQuanTracService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService, IDiemQuanTracService diemQuanTracService)
        {
            _unitOfWork = unitOfWork;
            _fileTaiLieuService = fileTaiLieuService;
            _diemQuanTracService = diemQuanTracService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity = await _unitOfWork.GetRepository<KetQuaQuanTrac>().FindAsync(id);
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

        public async Task<ApiResult<IList<KetQuaQuanTracViewModel>>> GetAll()
        {
            var result = new List<KetQuaQuanTracViewModel>();
            var entities = await _unitOfWork.GetRepository<KetQuaQuanTrac>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<KetQuaQuanTracViewModel>>() { Data = result };
        }

        public async Task<ApiResult<KetQuaQuanTracViewModel>> GetById(int id)
        {
            var result = new KetQuaQuanTracViewModel();
            var data = await _unitOfWork.GetRepository<KetQuaQuanTrac>().GetFirstOrDefaultAsync(predicate: x => x.IdKetQuaQuanTrac == id);
            result = MapEntityToViewModel(data);
            return new ApiSuccessResult<KetQuaQuanTracViewModel>() { Data = result };
        }

        public async Task<ApiResult<KetQuaQuanTrac>> Insert(KetQuaQuanTracViewModel obj)
        {
            var entity = new KetQuaQuanTrac();
            entity = MapViewModelToEntity(obj, entity);
            await _unitOfWork.GetRepository<KetQuaQuanTrac>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<KetQuaQuanTrac>() { Data = entity };
        }
        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<KetQuaQuanTrac>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<KetQuaQuanTrac>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<KetQuaQuanTracViewModel>>> GetAllPaging(PagingRequest request)
        {
            Expression<Func<KetQuaQuanTrac, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.ChiTieu.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<KetQuaQuanTrac>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<KetQuaQuanTracViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom = data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<KetQuaQuanTracViewModel>>() { Data = result };
        }

        public async Task<ApiResult<KetQuaQuanTrac>> Update(KetQuaQuanTracViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<KetQuaQuanTrac>().GetFirstOrDefaultAsync(predicate: x => x.IdKetQuaQuanTrac == obj.IdKetQuaQuanTrac);
            entity = MapViewModelToEntity(obj, entity);
            _unitOfWork.GetRepository<KetQuaQuanTrac>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<KetQuaQuanTrac>() { Data = entity };
        }
        public async Task<ApiResult<bool>> InsertFromExcel(List<KetQuaQuanTracViewModel> list)
        {
            try
            {
                foreach (var obj in list)
                {
                    var entity = new KetQuaQuanTrac();
                    obj.IdDiemQuanTrac = _unitOfWork.GetRepository<DiemQuanTrac>().GetFirstOrDefault(predicate: x => x.TenDiemQuanTrac == obj.TenDiemQuanTrac).IdDiemQuanTrac;
                    entity = MapViewModelToEntity(obj, entity);
                    await _unitOfWork.GetRepository<KetQuaQuanTrac>().InsertAsync(entity);
                }
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return new ApiSuccessResult<bool>() { Data = false };
            }
            return new ApiSuccessResult<bool>() { Data = true };
        }
        public async Task<ApiResult<IList<KetQuaQuanTracViewModel>>> GetAllByIdThanhPhanMoiTruong(int idThanhPhanMoiTruong)
        {
            var result = new List<KetQuaQuanTracViewModel>();
            var entities = await _unitOfWork.GetRepository<KetQuaQuanTrac>().GetAllAsync(predicate: x => x.IdThanhPhanMoiTruong == idThanhPhanMoiTruong && !x.IsDeleted, include: x => x.Include(y => y.DiemQuanTrac));
            result = entities.Select(MapEntityToViewModel).OrderBy(x => x.TenDiemQuanTrac).ToList();
            return new ApiSuccessResult<IList<KetQuaQuanTracViewModel>>() { Data = result };
        }
        public async Task<ApiResult<List<DuLieuQuanTracMoiTruongViewModel>>> DuLieuQuanTracMoiTruong(int idThanhPhanMoiTruong, string loai)
        {
            var result = new List<DuLieuQuanTracMoiTruongViewModel>();
            var listDQT = await _diemQuanTracService.GetAll();
            var listKQ = await GetAllByIdThanhPhanMoiTruong(idThanhPhanMoiTruong);
            if (listDQT.Success && listKQ.Success)
            {
                var dsDiemQuanTrac = listDQT.Data;
                var dsKetQua = listKQ.Data.OrderBy(x => x.ChiTieu).ThenBy(x => x.TenDiemQuanTrac).ToList();
                var dsChiTieuKhongKhi = listKQ.Data;
                if (!String.IsNullOrEmpty(loai))
                {
                    dsChiTieuKhongKhi = dsChiTieuKhongKhi.Where(x => x.Loai == loai).ToList();
                }
                var dsTenChiTieuKhongKhi = dsChiTieuKhongKhi.Select(x => x.ChiTieu).Distinct().ToList();
                //foreach (var item in dsDiemQuanTrac)
                //{
                //    var duLieu = new DuLieuQuanTracMoiTruongViewModel();
                //    duLieu.TenDiemQuanTrac = item.TenDiemQuanTrac;
                //    duLieu.Loai = item.Loai;
                //    duLieu.DsKetQua = dsKetQua.Where(x => x.IdDiemQuanTrac == item.IdDiemQuanTrac).OrderBy(x=> x.ChiTieu).ToList();
                //    result.Add(duLieu);
                //}
                foreach (var tenChiTieu in dsTenChiTieuKhongKhi)
                {
                    var duLieu = new DuLieuQuanTracMoiTruongViewModel();
                    duLieu.ChiTieu = tenChiTieu;
                    foreach (var item in dsChiTieuKhongKhi)
                    {
                        duLieu.DsKetQua = dsKetQua.Where(x => x.ChiTieu == tenChiTieu).OrderBy(x => x.ChiTieu).ToList();
                    }
                    duLieu.DonVi = duLieu.DsKetQua.FirstOrDefault().DonViTinh;
                    duLieu.Loai = duLieu.DsKetQua.FirstOrDefault().Loai;
                    result.Add(duLieu);
                }
            }
            return new ApiSuccessResult<List<DuLieuQuanTracMoiTruongViewModel>>() { Data = result };
        }

        public KetQuaQuanTracViewModel MapEntityToViewModel(KetQuaQuanTrac entity)
        {
            var result = new KetQuaQuanTracViewModel();
            result.IdKetQuaQuanTrac = entity.IdKetQuaQuanTrac;
            result.IdThanhPhanMoiTruong = entity.IdThanhPhanMoiTruong;
            result.IdDiemQuanTrac = entity.IdDiemQuanTrac;
            result.ChiTieu = entity.ChiTieu;
            result.GiaTri = entity.GiaTri;
            result.DonViTinh = entity.DonViTinh;
            result.TieuChuan = entity.TieuChuan;
            result.NguongToiThieu = entity.NguongToiThieu;
            result.NguongToiDa = entity.NguongToiDa;
            result.Nam = entity.Nam;
            result.Lan = entity.Lan;
            result.TenDiemQuanTrac = entity.DiemQuanTrac == null ? "" : entity.DiemQuanTrac.TenDiemQuanTrac;
            result.Loai = entity.DiemQuanTrac == null ? "" : entity.DiemQuanTrac.Loai;
            result.TenThanhPhanMoiTruong = entity.ThanhPhanMoiTruong == null ? "" : entity.ThanhPhanMoiTruong.TenThanhPhanMoiTruong;
            return result;
        }
        public KetQuaQuanTrac MapViewModelToEntity(KetQuaQuanTracViewModel viewModel, KetQuaQuanTrac entity)
        {
            entity.IdKetQuaQuanTrac = viewModel.IdKetQuaQuanTrac;
            entity.IdThanhPhanMoiTruong = viewModel.IdThanhPhanMoiTruong;
            entity.IdDiemQuanTrac = viewModel.IdDiemQuanTrac;
            entity.ChiTieu = viewModel.ChiTieu;
            entity.GiaTri = viewModel.GiaTri;
            entity.DonViTinh = viewModel.DonViTinh;
            entity.TieuChuan = viewModel.TieuChuan;
            entity.NguongToiThieu = viewModel.NguongToiThieu;
            entity.NguongToiDa = viewModel.NguongToiDa;
            entity.Nam = viewModel.Nam;
            entity.Lan = viewModel.Lan;
            return entity;
        }
    }

}

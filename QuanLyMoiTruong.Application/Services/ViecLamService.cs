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

namespace QuanLyMoiTruong.Application.Services
{

    public class ViecLamService : IViecLamService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ViecLamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<ViecLam>().FindAsync(id);
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

        public async Task<ApiResult<IList<ViecLamViewModel>>> GetAll()
        {
            var result = new List<ViecLamViewModel>();
            var entities =  await _unitOfWork.GetRepository<ViecLam>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<ViecLamViewModel>>() { Data = result };
        }

        public async Task<ApiResult<ViecLamViewModel>> GetById(int id)
        {
            var result = new ViecLamViewModel();
            var data = await _unitOfWork.GetRepository<ViecLam>().FindAsync(id);
            result = MapEntityToViewModel(data);
            return new ApiSuccessResult<ViecLamViewModel>() {Data = result };
        }

        public async Task<ApiResult<ViecLam>> Insert(ViecLamViewModel obj)
        {
            var entity = new ViecLam();
            entity = MapViewModelToEntity(obj);
            await _unitOfWork.GetRepository<ViecLam>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<ViecLam>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<ViecLam>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<ViecLam>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<ViecLamViewModel>>> GetAllPaging(ViecLamRequest request)
        {
            Expression<Func<ViecLam, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TieuDe.ToLower().Contains(fullTextSearch)
                                      || p.MoTa.ToLower().Contains(fullTextSearch)
                                      || p.ThongTinNhaTuyenDung.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<ViecLam>().GetPagedListAsync(predicate: filter, orderBy: o => o.OrderBy(s => s.MucUuTien).ThenBy(s => s.XepHang).ThenByDescending(s=> s.NgayPheDuyet), pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<ViecLamViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<ViecLamViewModel>>() { Data = result };
        }

        public Task<ApiResult<ViecLam>> Update(ViecLamViewModel obj)
        {
            throw new NotImplementedException();
        }
        public ViecLamViewModel MapEntityToViewModel(ViecLam entity) {
            var result = new ViecLamViewModel();
            result.IdViecLam = entity.IdViecLam;
            result.TieuDe = entity.TieuDe;
            result.MoTa = entity.MoTa;
            result.YeuCau = entity.YeuCau;
            result.QuyenLoi = entity.QuyenLoi;
            result.DiaDiem = entity.DiaDiem;
            result.MucLuongToiThieu = entity.MucLuongToiThieu;
            result.MucLuongToiDa = entity.MucLuongToiDa;
            result.DonViTienTe = entity.DonViTienTe;
            result.DonViThoiGian = entity.DonViThoiGian;
            result.TuyenDungTuNgay = entity.TuyenDungTuNgay != null ? entity.TuyenDungTuNgay.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.TuyenDungDenNgay = entity.TuyenDungDenNgay != null ? entity.TuyenDungDenNgay.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.ThongTinNhaTuyenDung = entity.ThongTinNhaTuyenDung;
            result.ThongTinKhac = entity.ThongTinKhac;
            result.DSIdDiaPhuong = entity.DSIdDiaPhuong;
            result.DSIdNganhNghe = entity.DSIdNganhNghe;
            result.DSIdPhucLoi = entity.DSIdPhucLoi;
            result.Url = entity.Url;
            result.TrangThai = entity.TrangThai;
            result.MucUuTien = entity.MucUuTien;
            result.XepHang = entity.XepHang;
            result.NgayPheDuyet = entity.NgayPheDuyet != null ? entity.NgayPheDuyet.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.NgayHetHan = entity.NgayPheDuyet != null ? entity.NgayPheDuyet.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.UserPheDuyet= entity.UserPheDuyet;
            return result;
        }
        public ViecLam MapViewModelToEntity(ViecLamViewModel viecLamViewModel)
        {
            var entity = new ViecLam();
            entity.IdViecLam = viecLamViewModel.IdViecLam;
            entity.TieuDe = viecLamViewModel.TieuDe;
            entity.MoTa = viecLamViewModel.MoTa;
            entity.YeuCau = viecLamViewModel.YeuCau;
            entity.QuyenLoi = viecLamViewModel.QuyenLoi;
            entity.DiaDiem = viecLamViewModel.DiaDiem;
            entity.MucLuongToiThieu = viecLamViewModel.MucLuongToiThieu;
            entity.MucLuongToiDa = viecLamViewModel.MucLuongToiDa;
            entity.DonViTienTe = viecLamViewModel.DonViTienTe;
            entity.DonViThoiGian = viecLamViewModel.DonViThoiGian;
            entity.TuyenDungTuNgay = string.IsNullOrEmpty(viecLamViewModel.TuyenDungTuNgay) ? null : DateTime.Parse(viecLamViewModel.TuyenDungTuNgay, new CultureInfo("vi-VN"));
            entity.TuyenDungDenNgay = string.IsNullOrEmpty(viecLamViewModel.TuyenDungDenNgay) ? null : DateTime.Parse(viecLamViewModel.TuyenDungDenNgay, new CultureInfo("vi-VN"));
            entity.ThongTinNhaTuyenDung = viecLamViewModel.ThongTinNhaTuyenDung;
            entity.ThongTinKhac = viecLamViewModel.ThongTinKhac;
            entity.DSIdDiaPhuong = viecLamViewModel.DSIdDiaPhuong;
            entity.DSIdNganhNghe = viecLamViewModel.DSIdNganhNghe;
            entity.DSIdPhucLoi = viecLamViewModel.DSIdPhucLoi;
            entity.Url = viecLamViewModel.Url;
            entity.TrangThai = viecLamViewModel.TrangThai;
            entity.MucUuTien = viecLamViewModel.MucUuTien;
            entity.XepHang = viecLamViewModel.XepHang;
            entity.NgayPheDuyet =  string.IsNullOrEmpty(viecLamViewModel.NgayPheDuyet) ? null : DateTime.Parse(viecLamViewModel.NgayPheDuyet, new CultureInfo("vi-VN"));
            entity.NgayHetHan = string.IsNullOrEmpty(viecLamViewModel.NgayHetHan) ? null : DateTime.Parse(viecLamViewModel.NgayHetHan, new CultureInfo("vi-VN"));
            entity.UserPheDuyet= viecLamViewModel.UserPheDuyet;
            return entity;
        }
    }

}

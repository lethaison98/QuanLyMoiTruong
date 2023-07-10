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

    public class DuAnService : IDuAnService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DuAnService(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<DuAn>().FindAsync(id);
            if (entity == null)
            {
                entity.IsDeleted= true;
                return new ApiSuccessResult<bool>() {};
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa") ;
            }
        } 

        public async Task<ApiResult<IList<DuAnViewModel>>> GetAll()
        {
            var result = new List<DuAnViewModel>();
            var entities =  await _unitOfWork.GetRepository<DuAn>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<DuAnViewModel>>() { Data = result };
        }

        public async Task<ApiResult<DuAnViewModel>> GetById(int id)
        {
            var result = new DuAnViewModel();
            var data = await _unitOfWork.GetRepository<DuAn>().FindAsync(id);
            result = MapEntityToViewModel(data);
            return new ApiSuccessResult<DuAnViewModel>() {Data = result };
        }

        public async Task<ApiResult<DuAn>> Insert(DuAnViewModel obj)
        {
            var entity = new DuAn();
            entity = MapViewModelToEntity(obj);
            await _unitOfWork.GetRepository<DuAn>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<DuAn>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<DuAn>().FindAsync(id);
            if (entity == null)
            {
                _unitOfWork.GetRepository<DuAn>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<DuAnViewModel>>> GetAllPaging(DuAnRequest request)
        {
            Expression<Func<DuAn, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenDoanhNghiep.ToLower().Contains(fullTextSearch)
                                      || p.TenDuAn.ToLower().Contains(fullTextSearch)
                                      || p.GiayPhepDKKD.ToLower().Contains(fullTextSearch));
            }

            var data = await _unitOfWork.GetRepository<DuAn>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<DuAnViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<DuAnViewModel>>() { Data = result };
        }

        public Task<ApiResult<DuAn>> Update(DuAnViewModel obj)
        {
            throw new NotImplementedException();
        }
        public DuAnViewModel MapEntityToViewModel(DuAn entity) {
            var result = new DuAnViewModel();
            result.IdDuAn = entity.IdDuAn;
            result.TenDuAn = entity.TenDuAn;
            result.TenDoanhNghiep = entity.TenDoanhNghiep;
            result.DiaChi = entity.DiaChi;
            result.TenNguoiDaiDien = entity.TenNguoiDaiDien;
            result.TenNguoiPhuTrachTNMT = entity.TenNguoiPhuTrachTNMT;
            result.GiayPhepDKKD = entity.GiayPhepDKKD;
            result.GhiChu = entity.GhiChu;
            return result;
        }
        public DuAn MapViewModelToEntity(DuAnViewModel viewModel)
        {
            var entity = new DuAn();
            entity.IdDuAn = viewModel.IdDuAn;
            entity.TenDuAn = viewModel.TenDuAn;
            entity.TenDoanhNghiep = viewModel.TenDoanhNghiep;
            entity.DiaChi = viewModel.DiaChi;
            entity.TenNguoiDaiDien = viewModel.TenNguoiDaiDien;
            entity.TenNguoiPhuTrachTNMT = viewModel.TenNguoiPhuTrachTNMT;
            entity.GiayPhepDKKD = viewModel.GiayPhepDKKD;
            entity.GhiChu = viewModel.GhiChu;
            return entity;
        }
    }

}

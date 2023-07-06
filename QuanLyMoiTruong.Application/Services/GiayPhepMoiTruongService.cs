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

    public class GiayPhepMoiTruongService : IGiayPhepMoiTruongService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GiayPhepMoiTruongService(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<GiayPhepMoiTruong>().FindAsync(id);
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

        public async Task<ApiResult<IList<GiayPhepMoiTruongViewModel>>> GetAll()
        {
            var result = new List<GiayPhepMoiTruongViewModel>();
            var entities =  await _unitOfWork.GetRepository<GiayPhepMoiTruong>().GetAllAsync(predicate: x => x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<GiayPhepMoiTruongViewModel>>() { Data = result };
        }

        public async Task<ApiResult<GiayPhepMoiTruongViewModel>> GetById(int id)
        {
            var result = new GiayPhepMoiTruongViewModel();
            var data = await _unitOfWork.GetRepository<GiayPhepMoiTruong>().FindAsync(id);
            result = MapEntityToViewModel(data);
            return new ApiSuccessResult<GiayPhepMoiTruongViewModel>() {Data = result };
        }

        public async Task<ApiResult<GiayPhepMoiTruong>> Insert(GiayPhepMoiTruongViewModel obj)
        {
            var entity = new GiayPhepMoiTruong();
            entity = MapViewModelToEntity(obj);
            await _unitOfWork.GetRepository<GiayPhepMoiTruong>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<GiayPhepMoiTruong>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<GiayPhepMoiTruong>().FindAsync(id);
            if (entity == null)
            {
                _unitOfWork.GetRepository<GiayPhepMoiTruong>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<GiayPhepMoiTruongViewModel>>> GetAllPaging(GiayPhepMoiTruongRequest request)
        {
            Expression<Func<GiayPhepMoiTruong, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.SoGiayPhep.ToLower().Contains(fullTextSearch)|| p.TenGiayPhep.ToLower().Contains(fullTextSearch));
            }

            var data = await _unitOfWork.GetRepository<GiayPhepMoiTruong>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<GiayPhepMoiTruongViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<GiayPhepMoiTruongViewModel>>() { Data = result };
        }

        public Task<ApiResult<GiayPhepMoiTruong>> Update(GiayPhepMoiTruongViewModel obj)
        {
            throw new NotImplementedException();
        }
        public GiayPhepMoiTruongViewModel MapEntityToViewModel(GiayPhepMoiTruong entity) {
            var result = new GiayPhepMoiTruongViewModel();
            result.IdGiayPhepMoiTruong = entity.IdGiayPhepMoiTruong;
            result.TenGiayPhep = entity.TenGiayPhep;
            result.SoGiayPhep = entity.SoGiayPhep;
            result.NgayCap = entity.NgayCap != null ? entity.NgayCap.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.IdDuAn = entity.IdDuAn;
            result.TenDuAn = entity.DuAn.TenDuAn;
            result.TenDoanhNghiep = entity.DuAn.TenDoanhNghiep;
            return result;
        }
        public GiayPhepMoiTruong MapViewModelToEntity(GiayPhepMoiTruongViewModel viewModel)
        {
            var entity = new GiayPhepMoiTruong();
            entity.IdGiayPhepMoiTruong = viewModel.IdGiayPhepMoiTruong;
            entity.TenGiayPhep = entity.TenGiayPhep;
            entity.SoGiayPhep = entity.SoGiayPhep;
            entity.NgayCap = string.IsNullOrEmpty(viewModel.NgayCap) ? null : DateTime.Parse(viewModel.NgayCap, new CultureInfo("vi-VN"));
            entity.IdDuAn = entity.IdDuAn;
            return entity;
        }
    }

}

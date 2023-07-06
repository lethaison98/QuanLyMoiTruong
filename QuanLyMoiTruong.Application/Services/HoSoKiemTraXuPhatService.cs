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

    public class HoSoKiemTraXuPhatService : IHoSoKiemTraXuPhatService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HoSoKiemTraXuPhatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().FindAsync(id);
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

        public async Task<ApiResult<IList<HoSoKiemTraXuPhatViewModel>>> GetAll()
        {
            var result = new List<HoSoKiemTraXuPhatViewModel>();
            var entities =  await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().GetAllAsync(predicate: x => x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<HoSoKiemTraXuPhatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<HoSoKiemTraXuPhatViewModel>> GetById(int id)
        {
            var result = new HoSoKiemTraXuPhatViewModel();
            var data = await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().FindAsync(id);
            result = MapEntityToViewModel(data);
            return new ApiSuccessResult<HoSoKiemTraXuPhatViewModel>() {Data = result };
        }

        public async Task<ApiResult<HoSoKiemTraXuPhat>> Insert(HoSoKiemTraXuPhatViewModel obj)
        {
            var entity = new HoSoKiemTraXuPhat();
            entity = MapViewModelToEntity(obj);
            await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<HoSoKiemTraXuPhat>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().FindAsync(id);
            if (entity == null)
            {
                _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<HoSoKiemTraXuPhatViewModel>>> GetAllPaging(HoSoKiemTraXuPhatRequest request)
        {
            Expression<Func<HoSoKiemTraXuPhat, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenHoSo.ToLower().Contains(fullTextSearch));
            }

            var data = await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<HoSoKiemTraXuPhatViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<HoSoKiemTraXuPhatViewModel>>() { Data = result };
        }

        public Task<ApiResult<HoSoKiemTraXuPhat>> Update(HoSoKiemTraXuPhatViewModel obj)
        {
            throw new NotImplementedException();
        }
        public HoSoKiemTraXuPhatViewModel MapEntityToViewModel(HoSoKiemTraXuPhat entity) {
            var result = new HoSoKiemTraXuPhatViewModel();
            result.IdHoSoKiemTraXuPhat = entity.IdHoSoKiemTraXuPhat;
            result.TenHoSo = entity.TenHoSo;
            result.IdDuAn = entity.IdDuAn;
            result.TenDuAn = entity.DuAn.TenDuAn;
            result.TenDoanhNghiep = entity.DuAn.TenDoanhNghiep;
            return result;
        }
        public HoSoKiemTraXuPhat MapViewModelToEntity(HoSoKiemTraXuPhatViewModel viewModel)
        {
            var entity = new HoSoKiemTraXuPhat();
            entity.IdHoSoKiemTraXuPhat = viewModel.IdHoSoKiemTraXuPhat;
            entity.TenHoSo = entity.TenHoSo;
            entity.IdDuAn = entity.IdDuAn;

            return entity;
        }
    }

}
